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

public class GetShelveQueryHandler : IRequestHandler<GetShelveQuery, ResultBase<ShelveDto?>>
{
    private readonly IShelveService _service;
    public GetShelveQueryHandler(IShelveService service)
    {
        _service = service;
    }
    public async Task<ResultBase<ShelveDto?>> Handle(GetShelveQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _service.GetShelveAsync(request.Id);
            return new ResultBase<ShelveDto?>(true, result.Data);
        }
        catch (Exception e)
        {
            return new ResultBase<ShelveDto?>(false, null, new List<string>() { e.Message });
        }
    }   
}