using System;

namespace Book.Application.Models.Requests;

public class DeleteRequest
{
    public Guid Id { get; set; }
    
    public DeleteRequest(Guid id)
    {
        Id = id;
    }
}