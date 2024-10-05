using System.Collections.Generic;
using System.Linq;

namespace Application.Models.Responses;

public class ErrorResponse
{
    public ErrorResponse(IEnumerable<string> errors, bool succeeded = false)
    {
        Errors = errors?.ToList() ?? new List<string>();
        Succeeded = succeeded;
    }

    public bool Succeeded { get; set; }
    public List<string> Errors { get; } 
}