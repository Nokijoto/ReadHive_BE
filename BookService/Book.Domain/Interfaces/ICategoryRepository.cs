using Book.Application.Models.Dto;


namespace Book.Domain.Interfaces;

public interface ICategoryRepository
{
    Task<bool> DeleteAsync(Guid id);
    Task<bool> AddAsync(CategoryDto category);
    Task<bool> UpdateAsync(CategoryDto category);
    Task<IEnumerable<CategoryDto>> GetAllAsync(bool includeDeleted=false);

    Task<CategoryDto?> GetByIdAsync(Guid id,bool includeDeleted=false);
    Task<CategoryDto?> GetByNameAsync(string name,bool includeDeleted=false);
    Task<CategoryDto?> GetByParentCategoryIdAsync(string parentCategoryId,bool includeDeleted=false);
    
    Task<string?> GetNameAsync(Guid id);
    Task<string?> GetParentCategoryIdAsync(Guid id);
    
    Task<bool> SetNameAsync(Guid id, string name);
    Task<bool> SetParentCategoryIdAsync(Guid id, string parentCategoryId);  
    
    
}