using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Book.Application.Interfaces;
using Book.Application.Models.Dto;
using Book.Application.Models.Results;
using Book.Application.Queries.Publisher;
using MediatR;

namespace Book.Application.Handlers.Publisher;

public class GetPublisherQueryHandler : IRequestHandler<GetPublisherQuery, ResultBase<PublisherDto?>>
{
    private readonly IPublisherService _service;
    public GetPublisherQueryHandler(IPublisherService service)
    {
        _service = service;
    }
    public async Task<ResultBase<PublisherDto?>> Handle(GetPublisherQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _service.GetPublisherAsync(request.Id);
            return new ResultBase<PublisherDto?>(true, result.Data);
        }
        catch (Exception e)
        {
            return new ResultBase<PublisherDto?>(false, null, new List<string>() { e.Message });
        }
    }       
    
}