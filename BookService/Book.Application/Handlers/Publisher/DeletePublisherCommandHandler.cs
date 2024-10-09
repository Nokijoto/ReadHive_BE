using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Book.Application.Commands.Publisher;
using Book.Application.Interfaces;
using Book.Application.Models.Results;
using MediatR;

namespace Book.Application.Handlers.Publisher;

public class DeletePublisherCommandHandler : IRequestHandler<DeletePublisherCommand, ResultBase<bool>>
{
    private readonly IPublisherService _service;
    public DeletePublisherCommandHandler(IPublisherService service)
    {
        _service = service;
    }
    public async Task<ResultBase<bool>> Handle(DeletePublisherCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _service.DeletePublisherAsync(request.Id);
            if(result)
            {
                return new ResultBase<bool>( result, result);
            }
            return new ResultBase<bool>(false, false, new List<string>() { "Publisher not found" });
        }
        catch (Exception e)
        {
            return new ResultBase<bool>(false, false, new List<string>() { e.Message });
        }
    }   
}