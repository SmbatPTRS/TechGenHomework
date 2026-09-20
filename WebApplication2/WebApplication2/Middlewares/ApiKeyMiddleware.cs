using System.Security.Cryptography;
using System.Text;

namespace WebApplication2.Middleware;

public class ApiKeyMiddleware
{
    private const string HeaderName = "X-Api-Key";

    private readonly RequestDelegate _next;
    private readonly string _expectedKey;

    // IConfiguration is an interface which sees all application configuration properties
    // basically has access to the appsettings.json
    public ApiKeyMiddleware(RequestDelegate next, IConfiguration configuration)
    {
        _next = next;

        // Read the real key from appsettings.json ONCE, at startup.
        // If it is missing, crash immediately with a clear message.
        _expectedKey = configuration["ApiKey"]
            ?? throw new InvalidOperationException("Configuration value 'ApiKey' not found.");
    }
    
    //Every middleware and controller needs to read the request and write the response
    //Without HttpContext, each one would need separate parameters for the method, the path, each header, the status code, and so on.
    //HttpContext puts all of it in one object, and each middleware just passes it to the next one
    public async Task InvokeAsync(HttpContext context)
    {
        // Only protect the API.
        // any other url doesnt touch my data 
        if (!context.Request.Path.StartsWithSegments("/api"))
        {
            await _next(context);
            return;
        }

        // Look for the header. TryGetValue = "give me the value if it exists".
        if (!context.Request.Headers.TryGetValue(HeaderName, out var providedKey))
        {
            await RejectAsync(context, "API key is missing.");
            return; // We do NOT call _next: the request stops here.
        }

        // Compare the keys. FixedTimeEquals takes the same time whether the
        
        //The HTTP rules allow a client to send the same header name more than once
        //X-Api-Key: aaa
        //X-Api-Key: bbb
        // so the providedkey has a special type named StringValues , thats why we cast it
        var isValid = CryptographicOperations.FixedTimeEquals(
            Encoding.UTF8.GetBytes(providedKey.ToString()),
            Encoding.UTF8.GetBytes(_expectedKey));

        if (!isValid)
        {
            await RejectAsync(context, "API key is invalid.");
            return;
        }   

        // Key is correct: let the request continue to the controller.
        await _next(context);
    }

    //fill in the Response part of the HttpContext
    
    private static async Task RejectAsync(HttpContext context, string message)
    {
        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        
        //Turns the object into JSON text: {"status":401,"error":"API key is missing."}
        //Sets the response header Content-Type: application/json
        //Writes that text into context.Response.Body
        await context.Response.WriteAsJsonAsync(new { status = 401, error = message });
    }
}