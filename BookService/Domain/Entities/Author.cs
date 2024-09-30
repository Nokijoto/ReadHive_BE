using ProjectBase.Models;

namespace Domain.Entities;

public class Author : BaseModel
{
    public string? FirstName { get; init; }
    public string? LastName { get; init; }
    
    public string? Bio { get; init; }
    
    public string? PictureUrl { get; init; } 
    public DateTime? BirthDate { get; init; }
    public DateTime? DeathDate { get; init; }
    public string? Nationality { get; init; }
    
}