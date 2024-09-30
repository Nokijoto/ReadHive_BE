using ProjectBase.Models;

namespace Domain.Entities;

public class Publisher : BaseModel
{
    public string? Name { get; init; }
    public string? Description { get; init; }
    
    public string? PictureUrl { get; init; }
    public string? WebsiteUrl { get; init; }
    
    public string? Country { get; init; }
    
    public string? FoundedAt { get; init; }
    public string? FoundedBy { get; init; }
}