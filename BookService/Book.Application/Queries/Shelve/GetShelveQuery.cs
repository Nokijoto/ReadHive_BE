using System;
using Book.Application.Models.Dto;
using Book.Application.Models.Results;
using MediatR;

namespace Book.Application.Queries.Shelve;

public class GetShelveQuery : IRequest<ResultBase<ShelveDto?>>
{
    public Guid Id { get; set; }
        
    public GetShelveQuery(Guid id)
    {
        Id = id;
    }
}