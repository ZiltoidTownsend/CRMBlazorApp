using Application.Configuration;
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

    internal static IApplicationBuilder UseForwarding(this IApplicationBuilder app, IConfiguration configuration)
    {
        AppConfiguration config = GetApplicationSettings(configuration);
        if (config.BehindSSLProxy)
        {
            app.UseCors();
            app.UseForwardedHeaders();
        }

        return app;
    }
    private static AppConfiguration GetApplicationSettings(IConfiguration configuration)
    {
        var applicationSettingsConfiguration = configuration.GetSection(nameof(AppConfiguration));
        return applicationSettingsConfiguration.Get<AppConfiguration>();
    }
}
