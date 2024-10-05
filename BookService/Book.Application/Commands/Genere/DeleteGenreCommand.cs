using System;
using Application.Models.Results;
using MediatR;

namespace Application.Commands.Genere;

public class DeleteGenreCommand : IRequest<ResultBase<bool>>
{
    public Guid Id { get; set; }
    public DeleteGenreCommand(Guid id)
    {
        Id = id;
    }
    
}