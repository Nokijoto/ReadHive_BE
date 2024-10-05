using System;
using Application.Models.Dto;
using Application.Models.Results;
using MediatR;

namespace Application.Commands.Publisher;

public class UpdatePublisherCommand : IRequest<ResultBase<PublisherDto?>>
{
    public Guid Id { get; set; }
    public PublisherDto Publisher { get; set; }
    public UpdatePublisherCommand(Guid id, PublisherDto publisher)
    {
        Id = id;
        Publisher = publisher;
    }
    
}