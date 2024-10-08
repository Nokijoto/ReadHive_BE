using System;
using Book.Application.Models.Dto;
using Book.Application.Models.Results;
using MediatR;

namespace Book.Application.Queries.Genre;

public class GetGenreQuery : IRequest<ResultBase<GenreDto?>>
{
    public Guid Id { get; set; }
    public GetGenreQuery(Guid id)
    {
        Id = id;
    }   
    
}