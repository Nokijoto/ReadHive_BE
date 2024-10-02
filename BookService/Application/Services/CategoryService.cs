using Application.Interfaces;
using Application.Models.Dto;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Interfaces;
using Serilog;

namespace Application.Services;

public class CategoryService : ICategoryService
{
    private readonly ILogger _log;
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository, ILogger log)
    {
        _categoryRepository = categoryRepository;
        _log = log;
    }
    public async Task<bool> DeleteCategoryAsync(Guid id)
    {
        try
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Category id cannot be empty");
            }
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category != null)
            {
                await _categoryRepository.DeleteAsync(category.Id);
                return true;
            }       
            return false;
        }
        catch (Exception e)
        {
            _log.Error(e.Message, e);
            throw;
        }
    }

    public async Task<CategoryDto?> GetCategoryAsync(Guid id)
    {
        try
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Category id cannot be empty");
            }
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category != null)
            {
                var categoryDto = new CategoryDto()
                {
                    Id = category.Id,
                    Name = category.Name,
                    DeletedAt = category.DeletedAt,
                    CreatedAt = category.CreatedAt,
                    UpdatedAt = category.UpdatedAt,
                };
                return categoryDto;
            }                
            return null;
        }
        catch (Exception e)
        {
            _log.Error(e.Message, e);    
            throw;
        }
    }

    public async Task<IEnumerable<CategoryDto?>> GetCategoriesAsync()
    {
        try
        {
            var categories = await _categoryRepository.GetAllAsync();
            return new List<CategoryDto?>(categories.Select(category => category != null ? new CategoryDto() : new CategoryDto()));
        }
        catch (Exception e)
        {
            _log.Error(e.Message, e);
            throw;
        }
    }

    public async Task<CategoryDto?> UpdateCategoryAsync(CategoryDto categoryDto)
    {
        try
        {
            if (categoryDto.Id == Guid.Empty)
            {
                throw new ArgumentException("Category id cannot be empty");
            }
            var category = await _categoryRepository.GetByIdAsync(categoryDto.Id);
            if (category != null)
            {
                category.Name = categoryDto.Name;
                await _categoryRepository.UpdateAsync(category);
                return categoryDto;
            }       
            return null;
        }
        catch (Exception e)
        {
            _log.Error(e.Message, e);
            throw;
        }
    }

    public async Task<bool> AddCategoryAsync(CategoryDto categoryDto)
    {
        try
        {
            var category = new Category()
            {
                Name = categoryDto.Name,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };      
            await _categoryRepository.AddAsync(category);
            return true;
        }
        catch (Exception e)
        {
            _log.Error(e.Message, e);
            throw;
        }
    }
}