using Application.Models.Dto;
using Application.Models.Results;
using MediatR;

namespace Application.Commands.Author;

public class AddAuthorCommand : IRequest<ResultBase<AuthorDto?>>
{
    public AuthorDto? AuthorDto { get; set; }
    
    public AddAuthorCommand(AuthorDto authorDto)
    {
        AuthorDto = authorDto;
    }
    
}