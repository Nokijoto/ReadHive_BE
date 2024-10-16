﻿using Book.Domain.Entities;

namespace Book.Domain.Interfaces;

public interface IPublisherRepository
{
    Task<bool> DeleteAsync(Guid id);
    Task<bool> AddAsync(Publisher publisher);
    Task<bool> UpdateAsync(Publisher publisher);
    Task<IEnumerable<Publisher>> GetAllAsync(bool includeDeleted=false);

    Task<Publisher?> GetByIdAsync(Guid id,bool includeDeleted=false);
    Task<Publisher?> GetByNameAsync(string name,bool includeDeleted=false);
    
    Task<string?> GetNameAsync(Guid id);
    Task<string?> GetDescriptionAsync(Guid id);
    Task<string?> GetPictureUrlAsync(Guid id);
    Task<string?> GetWebsiteUrlAsync(Guid id);
    Task<string?> GetCountryAsync(Guid id);
    Task<string?> GetFoundedAtAsync(Guid id);
    Task<string?> GetFoundedByAsync(Guid id);
    
    Task<bool> SetNameAsync(Guid id, string name);
    Task<bool> SetDescriptionAsync(Guid id, string description);
    Task<bool> SetPictureUrlAsync(Guid id, string pictureUrl);
    Task<bool> SetWebsiteUrlAsync(Guid id, string websiteUrl);
    Task<bool> SetCountryAsync(Guid id, string country);
    Task<bool> SetFoundedAtAsync(Guid id, string foundedAt);
    Task<bool> SetFoundedByAsync(Guid id, string foundedBy);
    
    
}