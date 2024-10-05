namespace Application.Models.Dto;

public class AuthenticationResultDto
{
    public bool Succeeded { get; init; }
    public string? Token { get; init; } 
    public IEnumerable<Exception>? Errors { get; init; }
    public DateTime? Expiration { get; init; }
}