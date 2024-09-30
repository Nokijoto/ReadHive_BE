using Domain.Entities;

namespace Domain.Interfaces;

public interface IGenreRepository
{
    Task<bool> DeleteAsync(Guid id);
    Task<bool> AddAsync(Genre genre);
    Task<bool> UpdateAsync(Genre genre);
    
    Task<Genre?> GetByIdAsync(Guid id);
    Task<Genre?> GetByNameAsync(string name);
    
    Task<bool> SetNameAsync(Guid id, string name);  
    
}