using System.Collections.Generic;
using Book.Application.Models.Dto;

namespace Book.Application.Models.Results;

public class GetAuthorsQueryResult
{
    public bool Succeeded { get; set; }
    public IEnumerable<AuthorDto>? Data { get; set; }
    public List<string>? Errors { get; set; }
    
    public GetAuthorsQueryResult(bool succeeded, IEnumerable<AuthorDto>? data, List<string>? errors = null)
    {
        Succeeded = succeeded;
        Data = data;
        Errors = errors;
    }
}