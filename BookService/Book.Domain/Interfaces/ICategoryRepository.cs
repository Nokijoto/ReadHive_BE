using Book.Domain.Entities;


namespace Book.Domain.Interfaces;

public interface ICategoryRepository
{
    Task<bool> DeleteAsync(Guid id);
    Task<bool> AddAsync(Category category);
    Task<bool> UpdateAsync(Category category);
    Task<IEnumerable<Category>> GetAllAsync(bool includeDeleted=false);

    Task<Category?> GetByIdAsync(Guid id,bool includeDeleted=false);
    Task<Category?> GetByNameAsync(string name,bool includeDeleted=false);
    Task<Category?> GetByParentCategoryIdAsync(string parentCategoryId,bool includeDeleted=false);
    
    Task<string?> GetNameAsync(Guid id);
    Task<string?> GetParentCategoryIdAsync(Guid id);
    
    Task<bool> SetNameAsync(Guid id, string name);
    Task<bool> SetParentCategoryIdAsync(Guid id, string parentCategoryId);  
    
    
}