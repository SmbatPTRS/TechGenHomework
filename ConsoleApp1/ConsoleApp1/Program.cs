using System.Data;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace ConsoleApp1;

class Program
{
    
    static void Main(string[] args)
    {
        using var context = new LibraryContext();
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        var books = new List<Book>
        {
            new Book
            {
                Title = "Clean Code",
                Author = "Robert Martin",
                Year = 2008,
                Pages = 464,
                Price = 42.50m,   // 'm' suffix marks this literal as decimal, not double
                IsRead = true,
                AddedAt = DateTime.UtcNow
            },
            new Book
            {
                Title = "The Pragmatic Programmer",
                Author = "Andrew Hunt",
                Year = 1999,
                Pages = 352,
                Price = 38.00m,
                IsRead = true,
                AddedAt = DateTime.UtcNow
            },
            new Book
            {
                Title = "Designing Data-Intensive Applications",
                Author = "Martin Kleppmann",
                Year = 2017,
                Pages = 616,
                Price = 55.90m,
                IsRead = false,
                AddedAt = DateTime.Now
            },
            new Book
            {
                Title = "Refactoring",
                Author = "Martin Fowler",
                Year = 2018,
                Pages = 448,
                Price = 47.25m,
                IsRead = false,
                AddedAt = DateTime.UtcNow
            },
            new Book
            {
                Title = "Code Complete",
                Author = "Steve McConnell",
                Year = 2004,
                Pages = 960,
                Price = 51.00m,
                IsRead = true,
                AddedAt = DateTime.UtcNow
            },
            new Book
            {
                Title = "SQL Antipatterns",
                Author = "Bill Karwin",
                Year = 2010,
                Pages = 328,
                Price = 34.75m,
                IsRead = false,
                AddedAt = DateTime.UtcNow
            }
        };

        context.Books.AddRange(books);
        Console.WriteLine($"Before SaveChanges: {books[0].BookId}");

        context.SaveChanges();
        Console.WriteLine($"After SaveChanges: {books[0].BookId}");

    }
}
