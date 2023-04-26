using Application.Interfaces.Services;

namespace Server.Extensions;

internal static class ApplicationBuilderExtensions
{
    internal static IApplicationBuilder Initialize(this IApplicationBuilder app,IConfiguration _configuration)
    {
        using var serviceScope = app.ApplicationServices.CreateScope();

        var initializers = serviceScope.ServiceProvider.GetServices<IDatabaseSeeder>();

        foreach (var initializer in initializers)
        {
            initializer.Initialize();
        }

        return app;
    }
}
