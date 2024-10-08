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

public class UpdatePublisherCommandHandler : IRequestHandler<UpdatePublisherCommand, ResultBase<PublisherDto?>>
{
    private readonly IPublisherService _service;
    public UpdatePublisherCommandHandler(IPublisherService service)
    {
        _service = service;
    }
    public async Task<ResultBase<PublisherDto?>> Handle(UpdatePublisherCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _service.UpdatePublisherAsync(request.Publisher);
            return new ResultBase<PublisherDto?>(true, result.Data);
        }
        catch (Exception e)
        {
            return new ResultBase<PublisherDto?>(false, null, new List<string>() { e.Message });
        }
    }
}