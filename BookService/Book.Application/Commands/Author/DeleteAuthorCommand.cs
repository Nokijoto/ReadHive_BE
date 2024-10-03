using MediatR;

namespace Application.Commands.Author;

public class DeleteAuthorCommand : IRequest<bool>
{
    public Guid Id { get; set; }
    public DeleteAuthorCommand(Guid id)
    {
        Id = id;
    }
}