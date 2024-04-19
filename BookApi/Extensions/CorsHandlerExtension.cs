namespace BookApi.Extensions;

internal static class CorsHandlerExtension
{
    internal static void ConfigureCORS(this WebApplicationBuilder builder)
    {
        builder.Services.AddCors(options =>
        {
            const string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
            options.AddPolicy(name: MyAllowSpecificOrigins, policy =>
            {
                policy
                    .AllowAnyHeader()
                    .AllowAnyOrigin()
                    .AllowAnyMethod();
            });
        });
    }
}