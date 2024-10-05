using Application.Models.Results;
using MediatR;

namespace Application.Commands.Book;

public class DeleteBookCommand : IRequest<ResultBase<bool>>
{
    public Guid Id { get; set; }
    public DeleteBookCommand(Guid id)
    {
        Id = id;
    }
}