using WebApplication2.Middlewares;
using WebApplication2.Middlewares;
using WebApplication2.Services;

namespace WebApplication2;
using WebApplication2.Data;
using Microsoft.EntityFrameworkCore;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Getting the connection string from JSON file
        var connectionString = builder.Configuration.GetConnectionString("BookDb")
                               ?? throw new InvalidOperationException("Connection string 'BookDb' not found.");

        builder.Services.AddOpenApi();
        
        
        //Register BookContext in the DI container.
        builder.Services.AddDbContext<BookContext>(options =>
            options.UseNpgsql(connectionString));
        
        // Registering our service as scoped
        builder.Services.AddScoped<IBookService, BookService>();
        
        
        
        //registers all the internal services the controller system needs
        //(model binding, JSON serialization, validation)
        builder.Services.AddControllers();
        
        builder.Services.AddEndpointsApiExplorer();
        
        // NEW AUTHENTIFICATION SERVICE, ONE PER REQUEST
        builder.Services.AddScoped<IAuthService, AuthService>();

        
        var app = builder.Build();
        app.UseMiddleware<ExceptionHandlingMiddleware>();

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();  

            app.UseSwaggerUI(options =>
            {
                // Tell the page WHERE to read the description from.
                // This is the JSON from MapOpenApi() above.
                options.SwaggerEndpoint("/openapi/v1.json", "BooksHomeworkSmbat");
            });
        }
        app.UseHttpsRedirection();

        
        //app.UseMiddleware<ApiKeyMiddleware>();

        //It scans your assembly for classes with [Route]/[ApiController] and
        //adds them into the routing pipeline, 
        app.MapControllers(); 



        

        app.Run();
    }
}