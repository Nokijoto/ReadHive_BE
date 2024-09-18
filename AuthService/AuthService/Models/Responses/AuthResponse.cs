namespace AuthService.Models.Responses;

public class AuthResponse
{
    public AuthResponse(string token, DateTime? expiration)
    {
        Token = token;
        Expiration = expiration;
    }
    public string Token { get; set; }
    public DateTime? Expiration { get; set; }
}