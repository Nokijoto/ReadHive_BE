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

public class GetGenresQueryHandler : IRequestHandler<GetGenresQuery, ResultBase<IEnumerable<GenreDto>>>
{
    private readonly IGenreService _service;
    public GetGenresQueryHandler(IGenreService service)
    {
        _service = service;
    }
    public async Task<ResultBase<IEnumerable<GenreDto>>> Handle(GetGenresQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _service.GetGenresAsync();
            return new ResultBase<IEnumerable<GenreDto>>(true, result.Data);
        }
        catch (Exception e)
        {
            return new ResultBase<IEnumerable<GenreDto>>(false, null, new List<string>() { e.Message });
        }
    }
}