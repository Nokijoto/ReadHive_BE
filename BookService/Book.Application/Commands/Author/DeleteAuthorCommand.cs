using System;
using Book.Application.Models.Results;
using MediatR;

namespace Book.Application.Commands.Author;

public class DeleteAuthorCommand : IRequest<ResultBase<bool>>
{
    public Guid Id { get; set; }
    public DeleteAuthorCommand(Guid id)
    {
        Id = id;
    }
}