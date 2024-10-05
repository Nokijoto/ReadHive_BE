using Application.Models.Dto;
using Application.Models.Results;
using MediatR;

namespace Application.Commands.Genere;

public class AddGenreCommand : IRequest<ResultBase<GenreDto?>>
{
    public GenreDto? GenreDto { get; set; }
    
    public AddGenreCommand(GenreDto genreDto)
    {
        GenreDto = genreDto;
    }
}