using Book.Application.Models.Dto;
using Book.Application.Models.Results;
using MediatR;

namespace Book.Application.Commands.Publisher;

public class AddPublisherCommand : IRequest<ResultBase<PublisherDto?>>
{
    public PublisherDto? PublisherDto { get; set; }
    
    public AddPublisherCommand(PublisherDto publisherDto)
    {
        PublisherDto = publisherDto;
    }
    
}