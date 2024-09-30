using Domain.Entities;
using Domain.Interfaces;

namespace Infrastructure.Repositories;

public class PublisherRepository : IPublisherRepository
{
    private readonly BookDbContext _context;
        
    PublisherRepository(BookDbContext context)
    {
        _context = context;
    }
    public async Task<bool> DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> AddAsync(Publisher publisher)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> UpdateAsync(Publisher publisher)
    {
        throw new NotImplementedException();
    }

    public async Task<Publisher?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<Publisher?> GetByNameAsync(string name)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> SetNameAsync(Guid id, string name)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> SetDescriptionAsync(Guid id, string description)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> SetPictureUrlAsync(Guid id, string pictureUrl)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> SetWebsiteUrlAsync(Guid id, string websiteUrl)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> SetCountryAsync(Guid id, string country)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> SetFoundedAtAsync(Guid id, string foundedAt)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> SetFoundedByAsync(Guid id, string foundedBy)
    {
        throw new NotImplementedException();
    }
}