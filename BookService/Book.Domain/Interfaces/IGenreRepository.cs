using Book.Application.Models.Dto;

namespace Book.Domain.Interfaces;

public interface IGenreRepository
{
    Task<bool> DeleteAsync(Guid id);
    Task<bool> AddAsync(GenreDto genre);
    Task<bool> UpdateAsync(GenreDto genre);
    Task<IEnumerable<GenreDto>> GetAllAsync(bool includeDeleted=false);

    Task<GenreDto?> GetByIdAsync(Guid id,bool includeDeleted=false);
    Task<GenreDto?> GetByNameAsync(string name,bool includeDeleted=false);
    
    Task<string?> GetNameAsync(Guid id);
    Task<string?> GetDescriptionAsync(Guid id);
    Task<string?> GetParentGenreIdAsync(Guid id);
    
    Task<bool> SetNameAsync(Guid id, string name);  
    Task<bool> SetDescriptionAsync(Guid id, string description);
    Task<bool> SetParentGenreIdAsync(Guid id, string parentGenreId);
    
}