using ProjectBase.Models;

namespace Domain.Entities;

public class Author : BaseModel
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    
    public string? Bio { get; set; }
    
    public string? PictureUrl { get; set; } 
    public DateOnly? BirthDate { get; set; }
    public DateOnly? DeathDate { get; set; }
    public string? Nationality { get; set; }
    
}