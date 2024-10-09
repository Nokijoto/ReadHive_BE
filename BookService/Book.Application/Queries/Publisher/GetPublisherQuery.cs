using System;
using Book.Application.Models.Dto;
using Book.Application.Models.Results;
using MediatR;

namespace Book.Application.Queries.Publisher;

public class GetPublisherQuery: IRequest<ResultBase<PublisherDto?>>
{
    public Guid Id { get; set; }
    public GetPublisherQuery(Guid id)
    {
        Id = id;
    }   
    
}