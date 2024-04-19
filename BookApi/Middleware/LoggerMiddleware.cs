using Boundaries.Logger;

namespace BookApi.Extensions;

public class LoggerMiddleware
{
    IBookLogger bookLogger = new BookLogger();

    private readonly RequestDelegate _next;

    public LoggerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        string ipAddress = context.Connection.RemoteIpAddress.ToString();
        string route = context.Request.Path;
        string method = context.Request.Method;

        bookLogger.WriteToLogFile(LogType.Info, $"[ {ipAddress} ] [ {method} {route} ] \n\n");

        await _next(context);
    }
}