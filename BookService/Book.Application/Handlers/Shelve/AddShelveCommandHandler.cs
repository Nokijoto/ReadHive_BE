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

public class AddShelveCommandHandler : IRequestHandler<AddShelveCommand, ResultBase<ShelveDto?>>
{
    private readonly IShelveService _service;
    public AddShelveCommandHandler(IShelveService service)
    {
        _service = service;
    }
    public async Task<ResultBase<ShelveDto?>> Handle(AddShelveCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _service.AddShelveAsync(request.ShelveDto);
            return new ResultBase<ShelveDto?>(true, request.ShelveDto);
        }
        catch (Exception e)
        {
            return new ResultBase<ShelveDto?>(false, null, new List<string>() { e.Message });
        }
    }   
    
}