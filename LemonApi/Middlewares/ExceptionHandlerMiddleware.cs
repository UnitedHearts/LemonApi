using Contracts.Http;
using System.Net;

namespace LemonApi;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    public ExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex, Guid.NewGuid());
        }
    }

    ///<summary>
    ///Обработчик общих ошибок
    ///</summary>
    ///<paramname="context"></param>
    ///<paramname="exception"></param>
    ///<paramname="id"></param>
    ///<returns></returns>
    private async Task HandleExceptionAsync(HttpContext context, Exception exception, Guid id)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        await context.Response.WriteAsJsonAsync(new Response<Error>(new Error(id, exception.Message)));
    }
}