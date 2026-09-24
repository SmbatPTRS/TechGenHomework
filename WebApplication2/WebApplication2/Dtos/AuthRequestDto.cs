namespace WebApplication2.Dtos;
using System.ComponentModel.DataAnnotations;

public class AuthRequestDto
{
    // [Required]: missing/empty -> automatic 400.

    // [StringLength]: min and max length. A max also stops someone sending a
    // 10 MB "password" to make the slow hasher work for a long time.
    [Required]
    [StringLength(50, MinimumLength = 3)]
    public string Username { get; set; } = string.Empty;

    [Required]
    [StringLength(100, MinimumLength = 8)]
    public string Password { get; set; } = string.Empty;
}