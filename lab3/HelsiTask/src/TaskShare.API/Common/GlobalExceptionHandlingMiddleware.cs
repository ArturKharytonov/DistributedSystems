using System.Net;
using System.Text.Json;
using TaskShare.Application.Exceptions;

namespace TaskShare.API.Common;

public class GlobalExceptionHandlingMiddleware(ILogger<GlobalExceptionHandlingMiddleware> logger) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An unexpected error occurred");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var response = context.Response;
        response.ContentType = "application/json";

        var errorResponse = new
        {
            Message = exception.Message,
            StatusCode = GetStatusCode(exception)
        };

        response.StatusCode = errorResponse.StatusCode;
        await response.WriteAsync(JsonSerializer.Serialize(errorResponse));
    }

    private static int GetStatusCode(Exception exception) => exception switch
    {
        TaskListNotFoundException => (int)HttpStatusCode.NotFound,
        UnauthorizedTaskListAccessException => (int)HttpStatusCode.BadRequest,
        _ => (int)HttpStatusCode.InternalServerError
    };
}