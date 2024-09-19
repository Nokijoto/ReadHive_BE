using System;
using System.Collections.Generic;
using System.Linq;

namespace AuthService.Models.Responses;

public class ErrorResponse
{
    public ErrorResponse(IEnumerable<Exception> errors)
    {
        Errors = errors.Select(e => e.Message);
    }
    public IEnumerable<string> Errors { get; set; }
}