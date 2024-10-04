using Application.Models.Dto;
using Application.Models.Results;
using MediatR;

namespace Application.Commands.Author;

public class UpdateAuthorCommand : IRequest<ResultBase<AuthorDto?>>
{
    public Guid Id { get; set; }
    public AuthorDto AuthorDto { get; set; }
    public UpdateAuthorCommand(Guid id, AuthorDto authorDto)
    {
        Id = id;
        AuthorDto = authorDto;
    }
}