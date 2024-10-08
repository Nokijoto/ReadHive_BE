using System;
using Book.Application.Models.Results;
using MediatR;

namespace Book.Application.Commands.Publisher;

public class DeletePublisherCommand : IRequest<ResultBase<bool>>
{
    public Guid Id { get; set; }
    public DeletePublisherCommand(Guid id)
    {
        Id = id;
    }
    
}