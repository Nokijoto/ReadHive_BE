using Application.Models.Dto;
using Application.Models.Results;
using MediatR;

namespace Application.Commands.Category;

public class AddCategoryCommand : IRequest<ResultBase<CategoryDto?>>
{
    public CategoryDto? CategoryDto { get; set; }
    
    public AddCategoryCommand(CategoryDto categoryDto)
    {
        CategoryDto = categoryDto;
    }
}