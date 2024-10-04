using Application.Models.Dto;
using Application.Models.Results;
using MediatR;

namespace Application.Queries.Genre;

public class GetGenresQuery : IRequest<ResultBase<IEnumerable<GenreDto>>>
{
    public GetGenresQuery()
    {
        
    }
    
}