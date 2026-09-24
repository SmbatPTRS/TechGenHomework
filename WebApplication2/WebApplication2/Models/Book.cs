namespace WebApplication2.Models;

public class Book
{
    public int Id {get; set;}
    
    public string Title {get; set;}= string.Empty;
    
    public string Author {get; set;}= string.Empty;

    public int Likes { get; set; } = 0;
    
    public int OwnerId { get; set; }

    // NEW: navigation property, so code can write book.Owner.Username.
    // "= null!" tells the compiler: "I know this looks null right now.
    // EF Core will fill it in when the owner is loaded.
    public User Owner { get; set; } = null!;
}