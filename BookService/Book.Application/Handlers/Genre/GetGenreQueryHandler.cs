using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Book.Application.Interfaces;
using Book.Application.Models.Dto;
using Book.Application.Models.Results;
using Book.Application.Queries.Genre;
using MediatR;

namespace Book.Application.Handlers.Genre;

public class GetGenreQueryHandler : IRequestHandler<GetGenreQuery, ResultBase<GenreDto?>>
{
    private readonly IGenreService _service;
    public GetGenreQueryHandler(IGenreService service)
    {
        _service = service;
    }

    public async Task<ResultBase<GenreDto?>> Handle(GetGenreQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _service.GetGenreAsync(request.Id);
            return new ResultBase<GenreDto?>(true, result.Data);
        }
        catch (Exception e)
        {
            return new ResultBase<GenreDto?>(false, null, new List<string>() { e.Message });
        }
    }
}