using Microsoft.Extensions.DependencyInjection;

namespace Simbad.State;

public static class ServiceCollectionExtensions
{

    public static IServiceCollection AddStateManagement(this IServiceCollection services)
    {
        services.AddSingleton<Store>();
        return services;
    }

}