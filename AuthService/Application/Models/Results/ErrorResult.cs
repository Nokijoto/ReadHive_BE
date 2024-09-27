namespace Application.Models.Results;

public class ErrorResult 
{
    public ErrorResult(string error)
    {
        Errors = new List<string> { error };
    }
    public List<string> Errors { get; set; }
}