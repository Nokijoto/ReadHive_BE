using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Book.Application.Interfaces;
using Book.Application.Mappers;
using Book.Application.Models.Dto;
using Book.Application.Models.Results;
using Book.Domain.Interfaces;
using ProjectBase.Interfaces;

namespace Book.Application.Services;

public class CategoryService : ICategoryService
{
    private readonly ILoggingService _log;
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository, ILoggingService log)
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
            _log.LogError(e.Message, e);
            throw;
        }
    }

    public async Task<ResultBase<CategoryDto?>> GetCategoryAsync(Guid id)
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
                return new ResultBase<CategoryDto?>(true, category.ToDto());
            }                
            return null;
        }
        catch (Exception e)
        {
            _log.LogError(e.Message, e);    
            throw;
        }
    }

    public async Task<ResultBase<IEnumerable<CategoryDto?>>> GetCategoriesAsync()
    {
        try
        {
            var categories = await _categoryRepository.GetAllAsync();
            return new ResultBase<IEnumerable<CategoryDto?>>(true, categories.Select(category => category.ToDto()));    
        }
        catch (Exception e)
        {
            _log.LogError(e.Message, e);
            throw;
        }
    }

    public async Task<ResultBase<CategoryDto?>> UpdateCategoryAsync(CategoryDto categoryDto)
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
                return new ResultBase<CategoryDto?>(true,category.ToDto());
            }       
            return null;
        }
        catch (Exception e)
        {
            _log.LogError(e.Message, e);
            throw;
        }
    }

    public async Task<bool> AddCategoryAsync(CategoryDto categoryDto)
    {
        try
        {
            
            await _categoryRepository.AddAsync(categoryDto.ToEntity());
            return true;
        }
        catch (Exception e)
        {
            _log.LogError(e.Message, e);
            throw;
        }
    }
}