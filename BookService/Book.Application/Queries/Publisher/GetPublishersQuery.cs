using System.Collections.Generic;
using Book.Application.Models.Dto;
using Book.Application.Models.Results;
using MediatR;

namespace Book.Application.Queries.Publisher;

public class GetPublishersQuery : IRequest<ResultBase<IEnumerable<PublisherDto>>>
{
    public GetPublishersQuery()
    {
        
    }
}