using Domain.Entities;

namespace Domain.Interfaces;

public interface IShelveRepository
{
    Task<bool> DeleteAsync(Guid id);
    Task<bool> AddAsync(Shelve shelve);
    Task<bool> UpdateAsync(Shelve shelve);
    Task<IEnumerable<Shelve>> GetAllAsync(bool includeDeleted=false);

    Task<Shelve?> GetByIdAsync(Guid id,bool includeDeleted=false);
    Task<Shelve?> GetByTitleAsync(string title,bool includeDeleted=false);
    
    Task<string?> GetTitleAsync(Guid id);
    Task<string?> GetDescriptionAsync(Guid id);
    
    
    
    Task<bool> SetTitleAsync(Guid id, string title);
    Task<bool> SetDescriptionAsync(Guid id, string description);
}