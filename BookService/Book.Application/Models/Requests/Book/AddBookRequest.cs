namespace Application.Models.Requests.Book;

public class AddBookRequest
{
    public string? Title { get; set; }
    public string? Subtitle { get; set; }
    public string? Isbn { get; set; }
    public string? Description { get; set; }
    public string? Author { get; set; }
    public string? Publisher { get; set; }
    public string? Language { get; set; }
    public string? Series { get; set; }
    public string? NumberOfPages { get; set; }
    public string? Dimensions { get; set; }
    public string? Format { get; set; }
    public string? Edition { get; set; }
    public string? TableOfContents { get; set; }
    public int? Price { get; set; }
    public DateTime? PublishedAt { get; set; }
    
    public Guid? AuthorId { get; set; }
    public Guid? PublisherId { get; set; }
    public Guid? GenreId { get; set; }
    
    public Guid? CategoryId { get; set; }
}