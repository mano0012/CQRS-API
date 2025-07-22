using Ambev.DeveloperEvaluation.WebApi.Middleware;

namespace Ambev.DeveloperEvaluation.WebApi.Extensions;

public static class ExceptionMiddlewaresExtension
{
    public static IApplicationBuilder UseExceptionMiddlewares(this IApplicationBuilder app)
    {
        return app
            .UseMiddleware<ValidationExceptionMiddleware>()
            .UseMiddleware<DomainExceptionMiddleware>()
            .UseMiddleware<UnauthorizedAccessExceptionMiddleware>();
    }
}
