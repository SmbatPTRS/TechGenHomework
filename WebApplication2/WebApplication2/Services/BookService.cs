using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.Dtos;
using WebApplication2.Models;

namespace WebApplication2.Services;

public class BookService : IBookService
{
    //the door to the database
    private readonly BookContext _db;

    public BookService(BookContext db)
    {
        _db = db;
    }

    public async Task<ResponseBookDto?> CreateAsync(CreateBookDto dto, CancellationToken ct)
    {
        bool alreadyExists = await _db.Books.AnyAsync(b => b.Title == dto.Title, ct);

        if (alreadyExists)
        {
            // Tell the caller "I did not create anything".
            return null;
        }
        
        var book = new Book
        {
            Title = dto.Title,
            Author = dto.Author
        };
        
        _db.Books.Add(book);
    
        
        await _db.SaveChangesAsync(ct);

        return ToResponse(book);

    }

    public async Task<ResponseBookDto?> GetByIdAsync(int id, CancellationToken ct)
    {
        // AsNoTracking(): we only READ this book, so we tell EF Core not to watch it for changes
        
        var book = await _db.Books
            .AsNoTracking()
            .FirstOrDefaultAsync(b => b.Id == id, ct);

        return book is null ? null : ToResponse(book);
    }
    
    private static ResponseBookDto ToResponse(Book book) => new()
    {
        Id = book.Id,
        Title = book.Title,
        Author = book.Author,
        Likes = book.Likes
    };



}