namespace WebApplication2.Dtos;

public class ResponseBookDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public int Likes { get; set; }
}