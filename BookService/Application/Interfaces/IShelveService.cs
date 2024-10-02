using Application.Models.Dto;

namespace Application.Interfaces;

public interface IShelveService
{
    Task<bool> DeleteShelveAsync(Guid id);
    Task<ShelveDto?> GetShelveAsync(Guid id);
    Task<IEnumerable<ShelveDto?>> GetShelvesAsync();
    Task<ShelveDto?> UpdateShelveAsync(ShelveDto shelveDto);
    Task<bool> AddShelveAsync(ShelveDto shelveDto);
}