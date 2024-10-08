using System;
using Book.Application.Models.Results;
using MediatR;

namespace Book.Application.Commands.Category;

public class DeleteCategoryCommand : IRequest<ResultBase<bool>>
{
    public Guid Id { get; set; }
    public DeleteCategoryCommand(Guid id)
    {
        Id = id;
    }
    
}