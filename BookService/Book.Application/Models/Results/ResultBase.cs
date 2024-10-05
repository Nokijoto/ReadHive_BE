using System.Collections.Generic;

namespace Application.Models.Results;

public class ResultBase<T>
{
    public bool Succeeded { get; set; }
    public List<string>? Errors { get; set; }
    public T? Data { get; set; }
    public ResultBase(bool succeeded,T data, List<string>? errors = null)
    {
        Succeeded = succeeded;
        Errors = errors;
        Data = data;
    }
}