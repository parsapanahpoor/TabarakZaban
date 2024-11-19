using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SharedProject;
using System.Net;

namespace Framework.Persistence.Context.ExceptionsHandler;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
        => _next = next;

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        string message;

        if (exception is DbUpdateException dbUpdateEx)
        {
            if (dbUpdateEx.InnerException?.Message.Contains("FOREIGN KEY") == true)
            {
                message = "تغییرات بر روی کلیدخارجی نامعتبر";
                context.Response.StatusCode = (int)HttpStatusCode.Conflict;
            }
            else message = exception.Message;
        }
        else message = exception.Message;

        return context.Response
            .WriteAsync(new Error(context.Response.StatusCode.ToString() , message).ToJson());
    }
}
