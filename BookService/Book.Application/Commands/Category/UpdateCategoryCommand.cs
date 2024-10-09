using System;
using Book.Application.Models.Dto;
using Book.Application.Models.Results;
using MediatR;

namespace Book.Application.Commands.Category;

public class UpdateCategoryCommand : IRequest<ResultBase<CategoryDto?>>
{
    public Guid Id { get; set; }
    public CategoryDto Category { get; set; }
    
    public UpdateCategoryCommand(Guid id, CategoryDto category)
    {
        Id = id;
        Category = category;
    }
}