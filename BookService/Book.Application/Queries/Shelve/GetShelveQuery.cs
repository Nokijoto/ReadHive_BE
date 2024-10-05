using Application.Models.Dto;
using Application.Models.Results;
using MediatR;

namespace Application.Queries.Shelve;

public class GetShelveQuery : IRequest<ResultBase<ShelveDto?>>
{
    public Guid Id { get; set; }
        
    public GetShelveQuery(Guid id)
    {
        Id = id;
    }
}