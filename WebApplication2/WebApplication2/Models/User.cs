namespace WebApplication2.Models;

public class User
{
    public int Id { get; set; }

    public string Username { get; set; } = string.Empty;

    // NOT the password. A scrambled, one-way version of it.
    // We fill this in properly in the Register step.
    public string PasswordHash { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation property: "all books this user donated".
    // Not a column. EF fills it in only when we ask for it.
    public List<Book> Books { get; set; } = new();
}