using Serilog;

namespace CarDiagnostics.API.src.Modules.User.Core.Exceptions.ExceptionHandler;

public class GlobalExceptionHandler
{
    private readonly RequestDelegate _next;

    public GlobalExceptionHandler(RequestDelegate next)
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
            Log.Error(ex, ex.Message);

            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var response = context.Response;
        response.ContentType = "application/json";

        switch (exception)
        {
            case EmailAlreadyUsedException:
                response.StatusCode = StatusCodes.Status409Conflict;
                break;
            case UserNotFoundException:
                response.StatusCode = StatusCodes.Status404NotFound;
                break;
            default:
                response.StatusCode = StatusCodes.Status500InternalServerError;
                break;
        }

        var errorMessage = exception.Message;
        return context.Response.WriteAsJsonAsync(errorMessage);
    }
}