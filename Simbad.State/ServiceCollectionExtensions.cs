using Microsoft.Extensions.DependencyInjection;
using Simbad.State.State;

namespace Simbad.State;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddStateManagement(this IServiceCollection services)
    {
        services.AddSingleton<IStore, Store>();
        return services;
    }
}