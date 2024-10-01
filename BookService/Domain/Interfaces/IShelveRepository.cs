using Domain.Entities;

namespace Domain.Interfaces;

public interface IShelveRepository
{
    Task<bool> DeleteAsync(Guid id);
    Task<bool> AddAsync(Shelve shelve);
    Task<bool> UpdateAsync(Shelve shelve);
    
    Task<Shelve?> GetByIdAsync(Guid id);
    Task<Shelve?> GetByTitleAsync(string title);
    
    Task<string?> GetTitleAsync(Guid id);
    Task<string?> GetDescriptionAsync(Guid id);
    
    
    
    Task<bool> SetTitleAsync(Guid id, string title);
    Task<bool> SetDescriptionAsync(Guid id, string description);
}