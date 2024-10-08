using Book.Application.Models.Dto;
using Book.Application.Models.Results;
using MediatR;

namespace Book.Application.Commands.Author;

public class AddAuthorCommand : IRequest<ResultBase<AuthorDto?>>
{
    public AuthorDto? AuthorDto { get; set; }
    
    public AddAuthorCommand(AuthorDto authorDto)
    {
        AuthorDto = authorDto;
    }
    
}