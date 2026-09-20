namespace WebApplication2.Middlewares;

public class ExceptionHandlingMiddleware 
{
    // the next checkpoint in the pipeline
    private readonly RequestDelegate _next;

    // Used to write the error into the server log (console).
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    // Used to know if we are in Development (to show details) or not.
    private readonly IHostEnvironment _environment;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger, IHostEnvironment environment)
    {
        _next = next;
        _logger = logger;
        _environment = environment;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            // Let the request go deeper (security desk, controller, service, database...).
            // If nothing goes wrong, we simply come back here and finish.
            await _next(context);
        }
        catch (Exception exception)
        {
            // Something below us threw an exception and nobody handled it.
            await HandleExceptionAsync(context, exception);
        }
    }
    
    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        // Step 1: write the error into the console, so YOU can see it.
        _logger.LogError(exception, "Something went wrong");

        // Step 2: decide the status code AND the message by the type of the exception.
        var (statusCode, message) = exception switch
        {
            KeyNotFoundException => (404, exception.Message),
            ArgumentException    => (400, exception.Message),
            _                    => (500, "An unexpected error occurred.")
        };

        // Step 3: send the answer to the client as JSON.
        context.Response.StatusCode = statusCode;
        await context.Response.WriteAsJsonAsync(new { status = statusCode, error = message });
    }
    
}