using Application.Models.Dto;
using Application.Models.Results;
using MediatR;

namespace Application.Queries.Genre;

public class GetGenreQuery : IRequest<ResultBase<GenreDto?>>
{
    public Guid Id { get; set; }
    public GetGenreQuery(Guid id)
    {
        Id = id;
    }   
    
}