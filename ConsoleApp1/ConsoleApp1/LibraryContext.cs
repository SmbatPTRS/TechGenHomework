using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;

namespace ConsoleApp1;

public class LibraryContext : DbContext
{
    const string ConnectionString =
        "Host=127.0.0.1;Port=5434;Username=admin;Password=admin1234;Database=AppPgDb2;";



    public DbSet<Book> Books { get; set; }

    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(ConnectionString)
            .LogTo(
                Console.WriteLine,                 // where the log output goes
                new[] { DbLoggerCategory.Database.Command.Name }, // the category filter
                LogLevel.Information,              // minimum severity to show
                DbContextLoggerOptions.SingleLine  // formatting option
            )
            .EnableSensitiveDataLogging();  
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>(entity =>
        {
            entity.Property(b => b.Title)
                .IsRequired().HasMaxLength(200);
            
            entity.Property(b => b.Author)
                .IsRequired().HasMaxLength(120);

            entity.Property(b => b.Price).HasPrecision(10,2);
            
            entity.HasIndex(b => b.Author);


        });
    }
}