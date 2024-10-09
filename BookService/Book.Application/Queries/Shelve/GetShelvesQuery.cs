using System.Collections.Generic;
using Book.Application.Models.Dto;
using Book.Application.Models.Results;
using MediatR;

namespace Book.Application.Queries.Shelve;

public class GetShelvesQuery : IRequest<ResultBase<IEnumerable<ShelveDto>>>
{
    public GetShelvesQuery()
    {
        
    }
    
}