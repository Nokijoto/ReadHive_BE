namespace Application.Models.Dto;

public class AuthenticationResultDto
{
    public bool Succeeded { get; set; }
    public string? Token { get; set; } 
    public IEnumerable<Exception>? Errors { get; set; }
    public DateTime? Expiration { get; set; }
}