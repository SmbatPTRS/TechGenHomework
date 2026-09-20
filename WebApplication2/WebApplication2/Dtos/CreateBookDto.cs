using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Dtos;

public class CreateBookDto
{
    [Required]
    [StringLength(200)]
    public string Title { get; set; } = string.Empty;

    [Required]
    [StringLength(200)]
    public string Author { get; set; } = string.Empty;

}