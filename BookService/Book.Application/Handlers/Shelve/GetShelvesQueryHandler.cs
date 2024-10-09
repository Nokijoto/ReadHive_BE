using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Book.Application.Interfaces;
using Book.Application.Models.Dto;
using Book.Application.Models.Results;
using Book.Application.Queries.Shelve;
using MediatR;

namespace Book.Application.Handlers.Shelve;

public class GetShelvesQueryHandler : IRequestHandler<GetShelvesQuery, ResultBase<IEnumerable<ShelveDto>>>  
{
    private readonly IShelveService _service;
    public GetShelvesQueryHandler(IShelveService service)
    {
        _service = service;
    }
    public async Task<ResultBase<IEnumerable<ShelveDto>>> Handle(GetShelvesQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _service.GetShelvesAsync();
            return new ResultBase<IEnumerable<ShelveDto>>(true, result.Data);
        }
        catch (Exception e)
        {
            return new ResultBase<IEnumerable<ShelveDto>>(false, null, new List<string>() { e.Message });
        }
    }   
    
}