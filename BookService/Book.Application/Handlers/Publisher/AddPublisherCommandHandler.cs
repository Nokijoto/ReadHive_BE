using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Book.Application.Commands.Publisher;
using Book.Application.Interfaces;
using Book.Application.Models.Dto;
using Book.Application.Models.Results;
using MediatR;

namespace Book.Application.Handlers.Publisher;

public class AddPublisherCommandHandler : IRequestHandler<AddPublisherCommand, ResultBase<PublisherDto?>>
{
    
    private readonly IPublisherService _service;
    public AddPublisherCommandHandler(IPublisherService service)
    {
        _service = service;
    }
    
    public async Task<ResultBase<PublisherDto?>> Handle(AddPublisherCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _service.AddPublisherAsync(request.PublisherDto);
            return new ResultBase<PublisherDto?>(true, request.PublisherDto);
        }
        catch (Exception e)
        {
            return new ResultBase<PublisherDto?>(false, null, new List<string>() { e.Message });
        }
    }   
    
}