using System;
using Book.Application.Models.Results;
using MediatR;

namespace Book.Application.Commands.Book;

public class DeleteBookCommand : IRequest<ResultBase<bool>>
{
    public Guid? Id { get; set; }
    public DeleteBookCommand(Guid? id)
    {
        Id = id;
    }
}