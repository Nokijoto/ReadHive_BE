using System.ComponentModel.DataAnnotations;

namespace Application.Models.Dto;

public class AuthorDto : BaseDto
{
    // [Required]
    // [StringLength(50)]
    public string? FirstName { get; set; }
    
    // [Required]
    // [StringLength(50)]
    public string? LastName { get; set; }
    
    // [StringLength(1000)]
    public string? Bio { get; set; }
    
    // [StringLength(200)]
    public string? PictureUrl { get; set; }
    
    // [DataType(DataType.Date)]
    public DateOnly? BirthDate { get; set; }
    
    // [DataType(DataType.Date)]
    public DateOnly? DeathDate { get; set; }
    
    // [StringLength(50)]
    public string? Nationality { get; set; }
}