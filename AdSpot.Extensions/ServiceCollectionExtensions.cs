using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AdSpot.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAndValidateOptions<T>(
        this IServiceCollection services,
        IConfiguration config,
        out T options
    )
        where T : class
    {
        var configSectionPath = typeof(T).Name.Replace("Options", "");
        services.AddOptions<T>().BindConfiguration(configSectionPath).ValidateDataAnnotations().ValidateOnStart();

        options =
            config.GetSection(configSectionPath).Get<T>()
            ?? throw new Exception($"Could not bind configuration for {typeof(T).Name}");

        return services;
    }

    public static IServiceCollection AddAndValidateOptions<T>(this IServiceCollection services)
        where T : class
    {
        var configSectionPath = typeof(T).Name.Replace("Options", "");
        services.AddOptions<T>().BindConfiguration(configSectionPath).ValidateDataAnnotations().ValidateOnStart();

        return services;
    }
}
