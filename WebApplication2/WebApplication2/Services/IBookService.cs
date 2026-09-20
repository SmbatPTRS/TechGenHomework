using WebApplication2.Dtos;

namespace WebApplication2.Services;

public interface IBookService
{
    // Creates a book from an order slip.
    // Returns ResponseBookDto, or NULL
    Task<ResponseBookDto?> CreateAsync(CreateBookDto dto, CancellationToken ct);

    // Finds one book by id.
    // Returns NULL if no such book exists
    Task<ResponseBookDto?> GetByIdAsync(int id, CancellationToken ct);

}