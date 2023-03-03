namespace LemonApi.Extensions;

public static class WebApplicationExtensions
{
public static void UseExceptionHandler(this WebApplication app)
{
app.UseMiddleware<ExceptionMiddleware>();
}
}