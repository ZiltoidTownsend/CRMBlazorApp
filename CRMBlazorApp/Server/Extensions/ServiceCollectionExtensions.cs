using Application.Interfaces.Services;
using Infrastructure;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Server.Extensions;

internal static class ServiceCollectionExtensions
{
    internal static IServiceCollection AddDatabase(
        this IServiceCollection services,
        IConfiguration configuration)
        => services
            .AddDbContext<MainContext>(options => options
                .UseNpgsql(configuration.GetConnectionString("DefaultConnection")))
        .AddTransient<IDatabaseSeeder, DatabaseSeeder>();
}
