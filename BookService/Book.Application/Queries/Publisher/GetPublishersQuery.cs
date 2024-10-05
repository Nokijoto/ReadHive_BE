using Application.Models.Dto;
using Application.Models.Results;
using MediatR;

namespace Application.Queries.Publisher;

public class GetPublishersQuery : IRequest<ResultBase<IEnumerable<PublisherDto>>>
{
    public GetPublishersQuery()
    {
        
    }
}