using System;
using Book.Application.Models.Dto;
using Book.Application.Models.Results;
using MediatR;

namespace Book.Application.Commands.Shelve;

public class UpdateShelveCommand : IRequest<ResultBase<ShelveDto?>>
{
    public Guid Id { get; set; }
    public ShelveDto Shelve { get; set; }
    public UpdateShelveCommand(Guid id, ShelveDto shelve)
    {
        Id = id;
        Shelve = shelve;
    }
}