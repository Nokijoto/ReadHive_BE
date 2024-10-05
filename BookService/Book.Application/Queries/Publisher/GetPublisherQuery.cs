using System;
using Application.Models.Dto;
using Application.Models.Results;
using MediatR;

namespace Application.Queries.Publisher;

public class GetPublisherQuery: IRequest<ResultBase<PublisherDto?>>
{
    public Guid Id { get; set; }
    public GetPublisherQuery(Guid id)
    {
        Id = id;
    }   
    
}