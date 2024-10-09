using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Book.Application.Commands.Shelve;
using Book.Application.Interfaces;
using Book.Application.Models.Results;
using MediatR;

namespace Book.Application.Handlers.Shelve;

public class DeleteShelveCommandHandler : IRequestHandler<DeleteShelveCommand, ResultBase<bool>>
{
    private readonly IShelveService _service;
    public DeleteShelveCommandHandler(IShelveService service)
    {
        _service = service;
    }
    public async Task<ResultBase<bool>> Handle(DeleteShelveCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _service.DeleteShelveAsync(request.Id);
            if(result)
            {
                return new ResultBase<bool>( result, result);
            }
            return new ResultBase<bool>(false, false, new List<string>() { "Shelve not found" });
        }
        catch (Exception e)
        {
            return new ResultBase<bool>(false, false, new List<string>() { e.Message });
        }
    }   
}