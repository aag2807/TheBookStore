using Boundaries.Persistance.Context;
using Boundaries.Persistance.Repositories.User;
using Core.Boundaries.Persistance;
using Core.User.Service;

namespace BookApi.Extensions;

internal static class ConfigureInjectionDependencies
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="builder"></param>
    internal static void ConfigureDi(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IBookDbContext, BookDbContext>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IUserService, UserService>();
    }
}