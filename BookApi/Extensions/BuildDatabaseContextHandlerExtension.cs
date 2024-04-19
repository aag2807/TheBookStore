using System.Diagnostics;
using Boundaries.Persistance.Context;
using Microsoft.EntityFrameworkCore;

namespace BookApi.Extensions;

internal static class BuildDatabaseContextHandlerExtension
{
    internal static void ConfigureDatabase(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<BookDbContext>(options =>
        {
            string sqlSeverConnectionString = builder.Configuration.GetConnectionString("SqlServerConnection") ?? throw new InvalidOperationException();
            string sqlProvider = builder.Configuration["SqlProvider"] ?? throw new InvalidOperationException();

            _ = sqlProvider switch
            {
                "SqlServer" => options.UseSqlServer(sqlSeverConnectionString).LogTo((msg) => Debug.WriteLine(msg)),
                _ => throw new NotImplementedException($"Unsupported sql provider: {sqlProvider}")
            };
        });
    }
}