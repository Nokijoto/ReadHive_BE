using Domain.Entities;

namespace Domain.Interfaces;

public interface IPublisherRepository
{
    Task<bool> DeleteAsync(Guid id);
    Task<bool> AddAsync(Publisher publisher);
    Task<bool> UpdateAsync(Publisher publisher);
    
    Task<Publisher?> GetByIdAsync(Guid id);
    Task<Publisher?> GetByNameAsync(string name);
    
    Task<bool> SetNameAsync(Guid id, string name);
    Task<bool> SetDescriptionAsync(Guid id, string description);
    Task<bool> SetPictureUrlAsync(Guid id, string pictureUrl);
    Task<bool> SetWebsiteUrlAsync(Guid id, string websiteUrl);
    Task<bool> SetCountryAsync(Guid id, string country);
    Task<bool> SetFoundedAtAsync(Guid id, string foundedAt);
    Task<bool> SetFoundedByAsync(Guid id, string foundedBy);
    
    
}