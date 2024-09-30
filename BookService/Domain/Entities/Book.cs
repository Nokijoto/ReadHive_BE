using System.Numerics;
using ProjectBase.Models;

namespace Domain.Entities;

public class Book : BaseModel
{
    public string? Title { get; init; }
    public string? Subtitle { get; init; }
    
    public string? Isbn { get; init; }
    public string? Description { get; init; }

    public string? Author { get; init; }
    public string? Publisher { get; init; }
    public string? Language { get; init; }
    public string? Series { get; init; }
    public string? NumberOfPages { get; init; }
    public string? Dimensions { get; init; }
    public string? Format { get; init; }
    public string? Edition { get; init; }
    public string? TableOfContents { get; init; }

    public BigInteger? Price { get; init; }
    
    public DateTime? PublishedAt { get; init; } 

}