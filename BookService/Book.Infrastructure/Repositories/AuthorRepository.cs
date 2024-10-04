using Domain.Entities;
using Domain.Interfaces;
using Book.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Book.Infrastructure.Repositories;

public class AuthorRepository : IAuthorRepository
{
    private readonly BookDbContext _context;
    private readonly ILoggingService _logger;

    public AuthorRepository(BookDbContext context, ILoggingService logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentException("Id is empty");
        }

        try
        {
            var author = await _context.Authors.FirstOrDefaultAsync(x => x!.Id == id);
            if (author == null) return false;

            author.DeletedAt = DateTime.Today;
            _context.Authors.Update(author);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError("Error occurred while deleting author", e);
            throw; 
        }
    }

    public async Task<bool> AddAsync(Author? author)
    {
        if (author == null)
        {
            _logger.LogWarn("Attempted to add a null author.", null);
            return false; 
        }

        try
        {
            await _context.Authors.AddAsync(author);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateException dbEx)
        {
            _logger.LogError($"Database error occurred while adding author '{author.FirstName} {author.LastName}': {dbEx.Message}", dbEx);
            return false; 
        }
        catch (Exception e)
        {
            _logger.LogError($"Error occurred while adding author '{author.FirstName} {author.LastName}': {e.Message}", e);
            return false; 
        }
    }

    public async Task<bool> UpdateAsync(Author? author)
    {
        if (author == null)
        {
            _logger.LogWarn("Attempted to update a null author.", null);
            return false; 
        }

        try
        {
            _context.Authors.Update(author);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError("Error occurred while updating author", e);
            throw; 
        }
    }

    public async Task<IEnumerable<Author?>> GetAllAsync(bool includeDeleted = false)
    {
        try
        {
            var query = _context.Authors.AsQueryable();

            if (!includeDeleted)
            {
                query = query.Where(u => u!.DeletedAt == null);
            }

            return await query.ToListAsync();
        }
        catch (Exception e)
        {
            _logger.LogError("Error occurred while getting all authors", e);
            throw;
        }
    }

    public async Task<Author?> GetByIdAsync(Guid id, bool includeDeleted = false)
    {
        try
        {
            var query = _context.Authors.AsQueryable();

            if (!includeDeleted)
            {
                query = query.Where(u => u!.DeletedAt == null);
            }

            return await query.FirstOrDefaultAsync(u => u!.Id == id);
        }
        catch (Exception e)
        {
            _logger.LogError("Error occurred while getting author by id", e);
            throw;
        }
    }

    public async Task<Author?> GetByFirstNameAsync(string? firstName, bool includeDeleted = false)
    {
        try
        {
            var query = _context.Authors.AsQueryable();

            if (!includeDeleted)
            {
                query = query.Where(u => u != null && u.DeletedAt == null);
            }

            return await query.FirstOrDefaultAsync(x => x != null && x.FirstName == firstName);
        }
        catch (Exception e)
        {
            _logger.LogError("Error occurred while getting author by first name", e);
            throw;
        }
    }

    public async Task<Author?> GetByLastNameAsync(string? lastName, bool includeDeleted = false)
    {
        try
        {
            var query = _context.Authors.AsQueryable();

            if (!includeDeleted)
            {
                query = query.Where(u => u != null && u.DeletedAt == null);
            }

            return await query.FirstOrDefaultAsync(x => x != null && x.LastName == lastName);
        }
        catch (Exception e)
        {
            _logger.LogError("Error occurred while getting author by last name", e);
            throw;
        }
    }

    private async Task<T?> GetPropertyAsync<T>(Guid id, Func<Author, T?> propertySelector)
    {
        try
        {
            var item = await _context.Authors.FirstOrDefaultAsync(x => x != null && x.Id == id);
            return propertySelector(item ?? throw new InvalidOperationException());
        }
        catch (Exception e)
        {
            _logger.LogError("Error occurred while getting author property", e);
            throw;
        }
    }

    public Task<string?> GetFirstNameAsync(Guid id) => GetPropertyAsync(id, item => item.FirstName);
    public Task<string?> GetLastNameAsync(Guid id) => GetPropertyAsync(id, item => item.LastName);
    public Task<string?> GetBioAsync(Guid id) => GetPropertyAsync(id, item => item.Bio);
    public Task<string?> GetPictureUrlAsync(Guid id) => GetPropertyAsync(id, item => item.PictureUrl);
    public Task<DateOnly?> GetBirthDateAsync(Guid id) => GetPropertyAsync(id, item => item.BirthDate);
    public Task<DateOnly?> GetDeathDateAsync(Guid id) => GetPropertyAsync(id, item => item.DeathDate);
    public Task<string?> GetNationalityAsync(Guid id) => GetPropertyAsync(id, item => item.Nationality);

    private async Task<bool> SetPropertyAsync<T>(Guid id, Action<Author, T> setProperty, T value)
    {
        try
        {
            var item = await GetByIdAsync(id);
            if (item == null) return false;
            setProperty(item, value);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError("Error occurred while setting author property", e);
            throw;
        }
    }

    public Task<bool> SetFirstNameAsync(Guid id, string firstName) => SetPropertyAsync(id, (author, fn) => author.FirstName = fn, firstName);
    public Task<bool> SetLastNameAsync(Guid id, string lastName) => SetPropertyAsync(id, (author, ln) => author.LastName = ln, lastName);
    public Task<bool> SetBioAsync(Guid id, string bio) => SetPropertyAsync(id, (author, b) => author.Bio = b, bio);
    public Task<bool> SetPictureUrlAsync(Guid id, string pictureUrl) => SetPropertyAsync(id, (author, url) => author.PictureUrl = url, pictureUrl);
    public Task<bool> SetBirthDateAsync(Guid id, DateOnly birthDate) => SetPropertyAsync(id, (author, bd) => author.BirthDate = bd, birthDate);
    public Task<bool> SetDeathDateAsync(Guid id, DateOnly deathDate) => SetPropertyAsync(id, (author, dd) => author.DeathDate = dd, deathDate);
    public Task<bool> SetNationalityAsync(Guid id, string nationality) => SetPropertyAsync(id, (author, nat) => author.Nationality = nat, nationality);
}