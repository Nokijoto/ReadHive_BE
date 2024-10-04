using Application.Models.Dto;
using Application.Models.Results;
using MediatR;

namespace Application.Commands.Category;

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