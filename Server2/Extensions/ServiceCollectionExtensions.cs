using Application.Interfaces.Services;
using Infrastructure;
using Infrastructure.Contexts;
using Infrastructure.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

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
}


