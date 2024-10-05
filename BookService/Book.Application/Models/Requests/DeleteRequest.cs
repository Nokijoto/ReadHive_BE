using System;

namespace Application.Models.Requests;

public class DeleteRequest
{
    public Guid Id { get; set; }
    
    public DeleteRequest(Guid id)
    {
        Id = id;
    }
}