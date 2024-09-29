using System.Numerics;
using ProjectBase.Models;

namespace Domain.Entities;

public class Book : BaseModel
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

    public BigInteger? Price { get; set; }
    
    public DateTime? PublishedAt { get; set; } 

}