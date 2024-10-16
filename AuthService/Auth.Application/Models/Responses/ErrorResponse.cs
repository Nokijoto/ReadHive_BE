﻿
namespace Application.Models.Responses;

public class ErrorResponse
{
    public ErrorResponse(IEnumerable<string> errors)
    {
        Errors = errors?.ToList() ?? new List<string>();
    }
    public List<string> Errors { get; } 
}