using Application.Models.Dto;

namespace Application.Interfaces;

public interface ICategoryService
{
    Task<bool> DeleteCategoryAsync(Guid id);
    Task<CategoryDto?> GetCategoryAsync(Guid id);
    Task<IEnumerable<CategoryDto?>> GetCategoriesAsync();
    Task<CategoryDto?> UpdateCategoryAsync(CategoryDto categoryDto);
    Task<bool> AddCategoryAsync(CategoryDto categoryDto);
}