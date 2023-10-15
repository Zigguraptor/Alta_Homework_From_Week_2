using Microsoft.AspNetCore.Http.Extensions;

namespace Alta_Homework_Week_2.WebApi.Middleware;

public class IpLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<IpLoggingMiddleware> _logger;

    public IpLoggingMiddleware(RequestDelegate next, ILogger<IpLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        _logger.LogInformation("Запрос с {host}. Url: {url}",
            context.Connection.RemoteIpAddress, context.Request.GetDisplayUrl());
        
        await _next(context);
    }
}
