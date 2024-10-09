using System;
using System.Collections.Generic;

namespace Book.Application.Models.Dto;

public class ResultDto<T>
{
    public bool Succeeded { get; set; }
    public T? Data { get; set; }
    public List<string>? Errors { get; set; }
    
    public ResultDto()
    {
        Succeeded = true; 
        Errors = new List<string>();
    }

    public ResultDto(T data)
    {
        Succeeded = true;
        Data = data;
        Errors = new List<string>();
    }

    public void AddError(string error)
    {
        if (Errors == null)
            Errors = new List<string>();
            
        Errors.Add(error);
        Succeeded = false;
    }

    public void SetData(T data)
    {
        Data = data;
        Succeeded = true;
    }
    public void AddError(Exception ex)
    {
        if (Errors == null)
            Errors = new List<string>();

        Errors.Add($"{ex.Message}\n{ex.StackTrace}");
        Succeeded = false;
    }
}