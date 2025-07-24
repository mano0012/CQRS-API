
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.WebApi.Common;
using System.Text.Json;

namespace Ambev.DeveloperEvaluation.WebApi.Middleware;

public class NotFoundExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public NotFoundExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (NotFoundException ex)
        {
            await HandleDomainExceptionAsync(context, ex);
        }
    }

    private static Task HandleDomainExceptionAsync(HttpContext context, NotFoundException exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = StatusCodes.Status404NotFound;

        var response = new ApiResponse
        {
            Success = false,
            Message = exception.Message
        };

        var jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        return context.Response.WriteAsync(JsonSerializer.Serialize(response, jsonOptions));
    }
}
