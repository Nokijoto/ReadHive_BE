using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Book.Application.Commands.Genere;
using Book.Application.Interfaces;
using Book.Application.Models.Results;
using MediatR;

namespace Book.Application.Handlers.Genre;

public class DeleteGenreCommandHandler : IRequestHandler<DeleteGenreCommand, ResultBase<bool>>
{
    
    private readonly IGenreService _service;
    public DeleteGenreCommandHandler(IGenreService service)
    {
        _service = service;
    }   
    public async Task<ResultBase<bool>> Handle(DeleteGenreCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _service.DeleteGenreAsync(request.Id);
            if(result)
            {
                return new ResultBase<bool>( result, result);
            }
            return new ResultBase<bool>(false, false, new List<string>() { "Genre not found" });
        }
        catch (Exception e)
        {
            return new ResultBase<bool>(false, false, new List<string>() { e.Message });
        }
    }   
}