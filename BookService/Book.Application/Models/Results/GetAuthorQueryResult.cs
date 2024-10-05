using Application.Models.Dto;

namespace Application.Models.Results;

public class GetAuthorQueryResult
{
    public bool Succeeded { get; set; }
    public AuthorDto? Data { get; set; }
    public List<string>? Errors { get; set; }
    
    public GetAuthorQueryResult(bool succeeded, AuthorDto? data, List<string>? errors = null)
    {
        Succeeded = succeeded;
        Data = data;
        Errors = errors;
    }
}