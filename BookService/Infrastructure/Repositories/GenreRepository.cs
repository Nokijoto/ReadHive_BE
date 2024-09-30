using Domain.Entities;
using Domain.Interfaces;

namespace Infrastructure.Repositories;

public class GenreRepository : IGenreRepository
{
    public async Task<bool> DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> AddAsync(Genre genre)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> UpdateAsync(Genre genre)
    {
        throw new NotImplementedException();
    }

    public async Task<Genre?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<Genre?> GetByNameAsync(string name)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> SetNameAsync(Guid id, string name)
    {
        throw new NotImplementedException();
    }
}