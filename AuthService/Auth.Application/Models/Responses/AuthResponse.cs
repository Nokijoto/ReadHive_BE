using Application.Models.Results;

namespace Application.Models.Responses;

public class AuthResponse
{
    public AuthResponse(string? token, DateTime? expiration)
    {
        Token = token;
        Expiration = expiration;
    }

    public AuthResponse(AuthResult result)
    {
        Token = result.Token;
        Expiration = result.Expiration;
    }
    public string? Token { get; set; }
    public DateTime? Expiration { get; set; }
}