namespace Application.Models;

public class AuthenticationResult
{
    public bool Succeeded { get; set; }
    public string Token { get; set; } // Token JWT
    public IEnumerable<string> Errors { get; set; }
    public DateTime? Expiration { get; set; }
}