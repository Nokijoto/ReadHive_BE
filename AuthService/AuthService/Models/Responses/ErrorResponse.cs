namespace AuthService.Models.Responses;

public class ErrorResponse
{
    public IEnumerable<string> Errors { get; set; }
}