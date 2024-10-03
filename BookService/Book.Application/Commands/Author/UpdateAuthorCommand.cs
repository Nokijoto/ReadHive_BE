using Application.Models.Dto;
using MediatR;

namespace Application.Commands.Author;

public class UpdateAuthorCommand : IRequest<AuthorDto?>
{
    public Guid Id { get; set; }
    public AuthorDto AuthorDto { get; set; }
    public UpdateAuthorCommand(AuthorDto authorDto)
    {
        AuthorDto = authorDto;
    }
}