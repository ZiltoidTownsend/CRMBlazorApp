using Application.Interfaces.Services;
using Infrastructure;
using Infrastructure.Contexts;
using Infrastructure.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Application.Configuration;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Newtonsoft.Json;
using System.Net;
using CRMBlazorApp.Shared.Wrapper;
using CRMBlazorApp.Shared.Constants;
using System.Reflection;
using BlazorHero.Shared.Constants.Permission;
using Microsoft.Extensions.Localization;
using System.Globalization;
using Application.Interfaces.Services.Identity;
using Infrastructure.Services.Identity;

namespace Server.Extensions;

internal static class ServiceCollectionExtensions
{
    internal static IServiceCollection AddDatabase(
        this IServiceCollection services,
        IConfiguration configuration)
        => services
            .AddDbContext<MainContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")))
        .AddTransient<IDatabaseSeeder, DatabaseSeeder>();

    internal static IServiceCollection AddIdentity(this IServiceCollection services)
    {
        services
            .AddIdentity<CRMUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<MainContext>()
            .AddDefaultTokenProviders();

        return services;
    }
    internal static AppConfiguration GetApplicationSettings(
           this IServiceCollection services,
           IConfiguration configuration)
    {
        var applicationSettingsConfiguration = configuration.GetSection(nameof(AppConfiguration));
        services.Configure<AppConfiguration>(applicationSettingsConfiguration);
        return applicationSettingsConfiguration.Get<AppConfiguration>();
    }
    internal static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        //services.AddTransient<IRoleClaimService, RoleClaimService>();
        services.AddTransient<ITokenService, IdentityService>();
        //services.AddTransient<IRoleService, RoleService>();
        //services.AddTransient<IAccountService, AccountService>();
        //services.AddTransient<IUserService, UserService>();
        //services.AddTransient<IChatService, ChatService>();
        //services.AddTransient<IUploadService, UploadService>();
        //services.AddTransient<IAuditService, AuditService>();
        //services.AddScoped<IExcelService, ExcelService>();
        return services;
    }
    internal static IServiceCollection AddJwtAuthentication(
            this IServiceCollection services, AppConfiguration config)
    {
        var key = Encoding.ASCII.GetBytes(config.Secret);
        services
            .AddAuthentication(authentication =>
            {
                authentication.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authentication.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(async bearer =>
            {
                bearer.RequireHttpsMetadata = false;
                bearer.SaveToken = true;
                bearer.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    RoleClaimType = ClaimTypes.Role,
                    ClockSkew = TimeSpan.Zero
                };


                bearer.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = c =>
                    {
                        if (c.Exception is SecurityTokenExpiredException)
                        {
                            c.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                            c.Response.ContentType = "application/json";
                            var result = JsonConvert.SerializeObject(Result.Fail("The Token is expired.")); // to do localizer
                            return c.Response.WriteAsync(result);
                        }
                        else
                        {
                            c.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                            c.Response.ContentType = "application/json";
                            var result = JsonConvert.SerializeObject(Result.Fail("An unhandled error has occurred.")); // to do localizer
                            return c.Response.WriteAsync(result);
                        }
                    },
                    OnChallenge = context =>
                    {
                        context.HandleResponse();
                        if (!context.Response.HasStarted)
                        {
                            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                            context.Response.ContentType = "application/json";
                            var result = JsonConvert.SerializeObject(Result.Fail("You are not Authorized.")); // to do localizer
                            return context.Response.WriteAsync(result);
                        }

                        return Task.CompletedTask;
                    },
                    OnForbidden = context =>
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                        context.Response.ContentType = "application/json";
                        var result = JsonConvert.SerializeObject(Result.Fail("You are not authorized to access this resource.")); // to do localizer
                        return context.Response.WriteAsync(result);
                    },
                };
            });
        services.AddAuthorization(options =>
        {
            // Here I stored necessary permissions/roles in a constant
            foreach (var prop in typeof(Permissions).GetNestedTypes().SelectMany(c => c.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)))
            {
                var propertyValue = prop.GetValue(null);
                if (propertyValue is not null)
                {
                    options.AddPolicy(propertyValue.ToString(), policy => policy.RequireClaim(ApplicationClaimTypes.Permission, propertyValue.ToString()));
                }
            }
        });
        return services;
    }
}


