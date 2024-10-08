using Book.Application.Models.Dto;

namespace Book.Domain.Interfaces;

public interface IShelveRepository
{
    Task<bool> DeleteAsync(Guid id);
    Task<bool> AddAsync(ShelveDto shelve);
    Task<bool> UpdateAsync(ShelveDto shelve);
    Task<IEnumerable<ShelveDto>> GetAllAsync(bool includeDeleted=false);

    Task<ShelveDto?> GetByIdAsync(Guid id,bool includeDeleted=false);
    Task<ShelveDto?> GetByTitleAsync(string title,bool includeDeleted=false);
    
    Task<string?> GetTitleAsync(Guid id);
    Task<string?> GetDescriptionAsync(Guid id);
    
    
    
    Task<bool> SetTitleAsync(Guid id, string title);
    Task<bool> SetDescriptionAsync(Guid id, string description);
}