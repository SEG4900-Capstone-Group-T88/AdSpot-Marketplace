using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AdSpot.Extensions;

public static class ServiceCollectionExtensions
{
    public static T AddAndValidateOptions<T>(this IServiceCollection services, IConfiguration config)
        where T : class
    {
        var configSectionPath = typeof(T).Name.Replace("Options", "");
        services.AddOptions<T>()
            .BindConfiguration(configSectionPath)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        var options = config.GetSection(configSectionPath).Get<T>()
            ?? throw new Exception($"Could not bind configuration for {typeof(T).Name}");

        return options;
    }
}
