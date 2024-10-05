namespace Application.Models.Results;

public class AuthResult : Result
{
    public bool Succeeded { get; set; }
    public string? Token { get; set; }
    public DateTime? Expiration { get; set; }
    public List<string> Errors { get; set; } = new List<string>();

    public AuthResult(bool succeeded, string? token, DateTime? expiration)
    {
        Succeeded = succeeded;
        Token = token;
        Expiration = expiration;
    }

    public AuthResult(List<string> errors)
    {
        Succeeded = false;
        Errors = errors;
    }
}