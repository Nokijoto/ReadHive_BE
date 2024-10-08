using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Book.Application.Commands.Shelve;
using Book.Application.Interfaces;
using Book.Application.Models.Dto;
using Book.Application.Models.Results;
using MediatR;

namespace Book.Application.Handlers.Shelve;

public class UpdateShelveCommandHandler : IRequestHandler<UpdateShelveCommand, ResultBase<ShelveDto?>>
{
    private readonly IShelveService _service;
    public UpdateShelveCommandHandler(IShelveService service)
    {
        _service = service;
    }
    public async Task<ResultBase<ShelveDto?>> Handle(UpdateShelveCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _service.UpdateShelveAsync(request.Shelve);
            return new ResultBase<ShelveDto?>(true, result.Data);
        }
        catch (Exception e)
        {
            return new ResultBase<ShelveDto?>(false, null, new List<string>() { e.Message });
        }
    }   
    
}