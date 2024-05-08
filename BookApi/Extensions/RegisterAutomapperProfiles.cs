using Shared.MappingProfiles.User;

namespace BookApi.Extensions;

internal static class RegisterAutomapperProfiles
{
    internal static void RegisterAutomapperProfilesFromShared(this WebApplicationBuilder builder)
    {
        builder.Services.AddAutoMapper(typeof(UserProfile));
    }
}