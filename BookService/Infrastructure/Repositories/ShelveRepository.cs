using Domain.Entities;
using Domain.Interfaces;

namespace Infrastructure.Repositories;

public class ShelveRepository : IShelveRepository
{
    private readonly BookDbContext _context;
        
    ShelveRepository(BookDbContext context)
    {
        _context = context;
    }
    public Task<bool> DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> AddAsync(Shelve shelve)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(Shelve shelve)
    {
        throw new NotImplementedException();
    }

    public Task<Shelve?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Shelve?> GetByTitleAsync(string title)
    {
        throw new NotImplementedException();
    }

    public Task<bool> SetTitleAsync(Guid id, string title)
    {
        throw new NotImplementedException();
    }

    public Task<bool> SetDescriptionAsync(Guid id, string description)
    {
        throw new NotImplementedException();
    }
}