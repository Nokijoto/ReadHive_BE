using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Book.Application.Models.Dto;
using Book.Application.Models.Results;

namespace Book.Application.Interfaces;

public interface ICategoryService
{
    Task<bool> DeleteCategoryAsync(Guid id);
    Task<ResultBase<CategoryDto?>> GetCategoryAsync(Guid id);
    Task<ResultBase<IEnumerable<CategoryDto?>>> GetCategoriesAsync();
    Task<ResultBase<CategoryDto?>> UpdateCategoryAsync(CategoryDto? categoryDto);
    Task<bool> AddCategoryAsync(CategoryDto? categoryDto);
}