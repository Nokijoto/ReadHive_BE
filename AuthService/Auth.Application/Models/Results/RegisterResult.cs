namespace Application.Models.Results;

public class RegisterResult : Result
{
    public bool Succeeded { get; set; }
    public List<string>? Errors { get; set; }
    
    public RegisterResult(bool succeeded, List<string>? errors = null)
    {
        Succeeded = succeeded;
        Errors = new List<string>();
    }
    
}