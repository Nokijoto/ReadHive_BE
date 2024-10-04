using Application.Models.Results;
using MediatR;

namespace Application.Commands.Category;

public class DeleteCategoryCommand : IRequest<ResultBase<bool>>
{
    public Guid Id { get; set; }
    public DeleteCategoryCommand(Guid id)
    {
        Id = id;
    }
    
}