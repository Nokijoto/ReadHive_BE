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

public class AddGenreCommandHandler : IRequestHandler<AddGenreCommand, ResultBase<GenreDto?>>
{
    private readonly IGenreService _service;
    public AddGenreCommandHandler(IGenreService service)
    {
        _service = service;
    }
    public async Task<ResultBase<GenreDto?>> Handle(AddGenreCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _service.AddGenreAsync(request.GenreDto);
            return new ResultBase<GenreDto?>(true, request.GenreDto);
        }
        catch (Exception e)
        {
            return new ResultBase<GenreDto?>(false, null, new List<string>() { e.Message });
        }
    }   
    
}