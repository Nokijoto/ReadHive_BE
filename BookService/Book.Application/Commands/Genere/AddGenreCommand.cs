using Book.Application.Models.Dto;
using Book.Application.Models.Results;
using MediatR;

namespace Book.Application.Commands.Genere;

public class AddGenreCommand : IRequest<ResultBase<GenreDto?>>
{
    public GenreDto? GenreDto { get; set; }
    
    public AddGenreCommand(GenreDto genreDto)
    {
        GenreDto = genreDto;
    }
}