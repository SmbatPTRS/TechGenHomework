using Microsoft.AspNetCore.Mvc;
using WebApplication2.Dtos;
using WebApplication2.Services;

namespace WebApplication2.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class BooksController : ControllerBase
{
    private readonly IBookService _bookService;
    public BooksController(IBookService bookService)
    {
        _bookService = bookService;
    }


    [HttpPost]
    [ProducesResponseType(typeof(ResponseBookDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    public async Task<ActionResult<ResponseBookDto>> Create([FromBody] CreateBookDto dto, CancellationToken ct)
    {
        var created = await _bookService.CreateAsync(dto, ct);

        if (created is null)
        {
            // Null means "duplicate", HTTP answer: 409.
            return Conflict(new ProblemDetails
            {
                Status = StatusCodes.Status409Conflict,
                Title = "Book already exists",
                Detail = $"A book titled '{dto.Title}' already exists.",
                Instance = HttpContext.Request.Path
            });
        }
        
        return CreatedAtAction(
            nameof(GetById),                 
            new { id = created.Id },       
            created);
    }
    
    // the : int part is a constraint, id must be an int
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ResponseBookDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ResponseBookDto>> GetById(int id, CancellationToken ct)
    {
        // "id" is filled automatically from the {id} part of the URL.
        var book = await _bookService.GetByIdAsync(id, ct);

        if (book is null)
        {
            return NotFound(new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = "Book not found",
                Detail = $"No book with id {id} exists.",
                Instance = HttpContext.Request.Path
            });
        }
        // Ok(...) → 200 with the DTO as JSON.
        
        return Ok(book);
    }
}