using System;
using MyFinance.Api.Authentication;

namespace MyFinance.Api.Middleware;

public class LoginsMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        // Lógica antes do endpoint (ex.: log da requisição)
        Console.WriteLine($"Login attempt at: {DateTime.UtcNow}");

        LoginManager.Logins =
        [
            new Login { Id = 1, Username = "Admin", Password = "Abc123", AccessType = AccessType.Privileged },
            new Login { Id = 2, Username = "Fiap", Password = "Abc123", AccessType = AccessType.Public },
        ];

        // Call the next delegate/middleware in the pipeline.
        await _next(context);

        // Lógica após o endpoint (ex.: log da resposta)
        Console.WriteLine($"Login response status: {context.Response.StatusCode}");
    }
}

public static class LoginsMiddlewareExtensions
{
    public static IApplicationBuilder UseLoginsMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<LoginsMiddleware>();
    }
}