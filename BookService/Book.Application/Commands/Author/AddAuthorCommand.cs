using Application.Models.Dto;
using MediatR;

namespace Application.Commands.Author;

public class AddAuthorCommand : IRequest<AuthorDto?>
{
    public AuthorDto AuthorDto { get; set; }
    public AddAuthorCommand(AuthorDto authorDto)
    {
        AuthorDto = authorDto;
    }
    
}