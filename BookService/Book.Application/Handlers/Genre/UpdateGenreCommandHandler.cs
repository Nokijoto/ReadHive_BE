using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Book.Application.Commands.Genere;
using Book.Application.Interfaces;
using Book.Application.Models.Dto;
using Book.Application.Models.Results;
using MediatR;

namespace Book.Application.Handlers.Genre;

public class UpdateGenreCommandHandler : IRequestHandler<UpdateGenreCommand, ResultBase<GenreDto?>>
{
    private readonly IGenreService _service;
    public UpdateGenreCommandHandler(IGenreService service)
    {
        _service = service;
    }
    
    public async Task<ResultBase<GenreDto?>> Handle(UpdateGenreCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _service.UpdateGenreAsync(request.Genre);
            return new ResultBase<GenreDto?>(true, result.Data);
        }
        catch (Exception e)
        {
            return new ResultBase<GenreDto?>(false, null, new List<string>() { e.Message });
        }
    }   
}