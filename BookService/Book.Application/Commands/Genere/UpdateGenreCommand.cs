using System;
using Book.Application.Models.Dto;
using Book.Application.Models.Results;
using MediatR;

namespace Book.Application.Commands.Genere;

public class UpdateGenreCommand : IRequest<ResultBase<GenreDto?>>
{
    public Guid Id { get; set; }
    public GenreDto Genre { get; set; }
    public UpdateGenreCommand(Guid id, GenreDto genre)
    {
        Id = id;
        Genre = genre;
    }
}