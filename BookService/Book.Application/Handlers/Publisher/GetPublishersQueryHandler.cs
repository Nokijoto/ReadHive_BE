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

public class GetPublishersQueryHandler : IRequestHandler<GetPublishersQuery, ResultBase<IEnumerable<PublisherDto>>>
{
    private readonly IPublisherService _service;
    public GetPublishersQueryHandler(IPublisherService service)
    {
        _service = service;
    }   
    
    public async Task<ResultBase<IEnumerable<PublisherDto>>> Handle(GetPublishersQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _service.GetPublishersAsync();
            return new ResultBase<IEnumerable<PublisherDto>>(true, result.Data);
        }
        catch (Exception e)
        {
            return new ResultBase<IEnumerable<PublisherDto>>(false, null, new List<string>() { e.Message });
        }
    }   
    
}