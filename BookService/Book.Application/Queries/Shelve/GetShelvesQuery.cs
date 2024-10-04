using Application.Models.Dto;
using Application.Models.Results;
using MediatR;

namespace Application.Queries.Shelve;

public class GetShelvesQuery : IRequest<ResultBase<IEnumerable<ShelveDto>>>
{
    public GetShelvesQuery()
    {
        
    }
    
}