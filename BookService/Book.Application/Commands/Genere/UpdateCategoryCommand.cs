using Application.Models.Dto;
using Application.Models.Results;
using MediatR;

namespace Application.Commands.Genere;

public class UpdateCategoryCommand : IRequest<ResultBase<GenreDto?>>
{
    public Guid Id { get; set; }
    public GenreDto Genre { get; set; }
    public UpdateCategoryCommand(Guid id, GenreDto genre)
    {
        Id = id;
        Genre = genre;
    }
}