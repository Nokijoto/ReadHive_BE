using System;

namespace Book.Application.Models.Requests.Book;

public class DeleteBookRequest
{
    public Guid? Id { get; set; }
}