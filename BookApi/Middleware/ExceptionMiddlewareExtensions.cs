using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace Wepsys.SMRI.WebAPI.Extensions
{
    /// <summary>
    /// Catch all exception to the application and return a custom message
    /// </summary>
    internal static class ExceptionMiddlewareExtensions
    {
        private const string DefaultErrorMessage = "Ocurrió un error procesando la información, Favor contactar con su administrador.";
        private const string ContentType = "application/json";

        internal static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.ContentType = ContentType;
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

                    if (contextFeature == null)
                    {
                        await BuildResponse(context, (int)HttpStatusCode.InternalServerError,
                            DefaultErrorMessage).ConfigureAwait(false);
                        return;
                    }

                    await BuildResponse(context, 400, DefaultErrorMessage).ConfigureAwait(false);
                });
            });
        }

        private static async Task BuildResponse(HttpContext context, int statusCode, string message)
        {
            context.Response.StatusCode = statusCode;
            await context.Response.WriteAsJsonAsync(new { Message = message }).ConfigureAwait(false);
        }
    }
}