using System;

namespace Book.Application.Models.Requests;

public class DeleteAuthorRequest
{
    public Guid Id { get; set; }
}