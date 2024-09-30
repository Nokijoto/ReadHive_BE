using Domain.Entities;

namespace Domain.Interfaces;

public interface ICategoryRepository
{
    Task<bool> DeleteAsync(Guid id);
    Task<bool> AddAsync(Category category);
    Task<bool> UpdateAsync(Category category);
    
    Task<Category?> GetByIdAsync(Guid id);
    Task<Category?> GetByNameAsync(string name);
    Task<Category?> GetByParentCategoryIdAsync(string parentCategoryId);
    
    Task<bool> SetNameAsync(Guid id, string name);
    Task<bool> SetParentCategoryIdAsync(Guid id, string parentCategoryId);  
    
    
}