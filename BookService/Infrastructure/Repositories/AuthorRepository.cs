using Domain.Entities;
using Domain.Interfaces;

namespace Infrastructure.Repositories;

public class AuthorRepository : IAuthorRepository
{
    private readonly BookDbContext _context;
        
    AuthorRepository(BookDbContext context)
    {
        _context = context;
    }
    public Task<bool> DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> AddAsync(Author author)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(Author author)
    {
        throw new NotImplementedException();
    }

    public Task<Author?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Author?> GetByFirstNameAsync(string firstName)
    {
        throw new NotImplementedException();
    }

    public Task<Author?> GetByLastNameAsync(string lastName)
    {
        throw new NotImplementedException();
    }

    public Task<bool> SetFirstNameAsync(Guid id, string firstName)
    {
        throw new NotImplementedException();
    }

    public Task<bool> SetLastNameAsync(Guid id, string lastName)
    {
        throw new NotImplementedException();
    }

    public Task<bool> SetBioAsync(Guid id, string bio)
    {
        throw new NotImplementedException();
    }

    public Task<bool> SetPictureUrlAsync(Guid id, string pictureUrl)
    {
        throw new NotImplementedException();
    }

    public Task<bool> SetBirthDateAsync(Guid id, DateTime birthDate)
    {
        throw new NotImplementedException();
    }

    public Task<bool> SetDeathDateAsync(Guid id, DateTime deathDate)
    {
        throw new NotImplementedException();
    }

    public Task<bool> SetNationalityAsync(Guid id, string nationality)
    {
        throw new NotImplementedException();
    }
}