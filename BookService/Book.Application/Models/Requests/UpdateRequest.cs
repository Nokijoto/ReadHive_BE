using System;

namespace Application.Models.Requests;

public class UpdateRequest<T>
{
    public Guid Id { get; set; }
    public  T? Data { get; set; }
    
    public UpdateRequest(Guid id, T? data)
    {
        Id = id;
        Data = data;
    }
}