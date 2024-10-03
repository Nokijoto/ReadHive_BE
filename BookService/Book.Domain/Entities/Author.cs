using ProjectBase.Models;

namespace Domain.Entities;

public class Author : BaseModel
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    
    public string? Bio { get; set; }
    
    public string? PictureUrl { get; set; } 
    public DateTime? BirthDate { get; set; }
    public DateTime? DeathDate { get; set; }
    public string? Nationality { get; set; }
    
}