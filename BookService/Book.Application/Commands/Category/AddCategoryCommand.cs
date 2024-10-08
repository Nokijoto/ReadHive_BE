using Book.Application.Models.Dto;
using Book.Application.Models.Results;
using MediatR;

namespace Book.Application.Commands.Category;

public class AddCategoryCommand : IRequest<ResultBase<CategoryDto?>>
{
    public CategoryDto? CategoryDto { get; set; }
    
    public AddCategoryCommand(CategoryDto categoryDto)
    {
        CategoryDto = categoryDto;
    }
}