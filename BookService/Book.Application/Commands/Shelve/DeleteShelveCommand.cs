using System;
using Book.Application.Models.Results;
using MediatR;

namespace Book.Application.Commands.Shelve;

public class DeleteShelveCommand : IRequest<ResultBase<bool>>
{
    public Guid Id { get; set; }
    public DeleteShelveCommand(Guid id)
    {
        Id = id;
    }
    
}