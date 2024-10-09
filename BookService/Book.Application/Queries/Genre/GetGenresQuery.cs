using System.Collections.Generic;
using Book.Application.Models.Dto;
using Book.Application.Models.Results;
using MediatR;

namespace Book.Application.Queries.Genre;

public class GetGenresQuery : IRequest<ResultBase<IEnumerable<GenreDto>>>
{
    public GetGenresQuery()
    {
        
    }
    
}