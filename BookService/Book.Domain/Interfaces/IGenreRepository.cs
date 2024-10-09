using Book.Domain.Entities;

namespace Book.Domain.Interfaces;

public interface IGenreRepository
{
    Task<bool> DeleteAsync(Guid id);
    Task<bool> AddAsync(Genre genre);
    Task<bool> UpdateAsync(Genre genre);
    Task<IEnumerable<Genre>> GetAllAsync(bool includeDeleted=false);

    Task<Genre?> GetByIdAsync(Guid id,bool includeDeleted=false);
    Task<Genre?> GetByNameAsync(string name,bool includeDeleted=false);
    
    Task<string?> GetNameAsync(Guid id);
    Task<string?> GetDescriptionAsync(Guid id);
    Task<string?> GetParentGenreIdAsync(Guid id);
    
    Task<bool> SetNameAsync(Guid id, string name);  
    Task<bool> SetDescriptionAsync(Guid id, string description);
    Task<bool> SetParentGenreIdAsync(Guid id, string parentGenreId);
    
}