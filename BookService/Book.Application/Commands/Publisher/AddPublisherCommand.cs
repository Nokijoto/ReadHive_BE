using Application.Models.Dto;
using Application.Models.Results;
using MediatR;

namespace Application.Commands.Publisher;

public class AddPublisherCommand : IRequest<ResultBase<PublisherDto?>>
{
    public PublisherDto? PublisherDto { get; set; }
    
    public AddPublisherCommand(PublisherDto publisherDto)
    {
        PublisherDto = publisherDto;
    }
    
}