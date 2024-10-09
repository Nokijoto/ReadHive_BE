using Book.Application.Models.Dto;
using Book.Domain.Entities;
using Book.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using ProjectBase.Interfaces;

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

    public async Task<bool> AddAsync(Author author)
    {
        var item = new Author()
        {
            FirstName = author.FirstName,
            LastName = author.LastName,
            Bio = author.Bio,
            Nationality = author.Nationality,
            PictureUrl = author.PictureUrl,
            BirthDate = author.BirthDate,
            DeathDate = author.DeathDate,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            DeletedAt = null,
            
        };
        if (item == null)
        {
            _logger.LogWarn("Attempted to add a null author.", null);
            return false; 
        }

        try
        {
            await _context.Authors.AddAsync(item);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateException dbEx)
        {
            _logger.LogError($"Database error occurred while adding author '{item.FirstName} {item.LastName}': {dbEx.Message}", dbEx);
            return false; 
        }
        catch (Exception e)
        {
            _logger.LogError($"Error occurred while adding author '{item.FirstName} {item.LastName}': {e.Message}", e);
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
        var item = new Author()
        {
            FirstName = author.FirstName,
            LastName = author.LastName,
            Bio = author.Bio,
            Nationality = author.Nationality,
            PictureUrl = author.PictureUrl,
            BirthDate = author.BirthDate,   
            DeathDate = author.DeathDate,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            DeletedAt = null,
            IsActive = true,
        };
        try
        {
            _context.Authors.Update(item);
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

            var author = await query.ToListAsync();
            return new List<Author?>(query.Select(author => author != null ? new Author()
            {
                Id = author.Id,
                FirstName = author.FirstName,
                LastName = author.LastName,
                Nationality = author.Nationality,
                DeletedAt = author.DeletedAt,
                CreatedAt = author.CreatedAt,
                UpdatedAt = author.UpdatedAt,
                PictureUrl = author.PictureUrl,
                BirthDate = author.BirthDate,
                DeathDate = author.DeathDate,
                Bio = author.Bio,
                IsActive = author.IsActive,
            }: new Author()
            {
                Id = Guid.Empty,
                FirstName = string.Empty,
                LastName = string.Empty,
                Nationality = string.Empty,
                DeletedAt = null,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                PictureUrl = string.Empty,
                BirthDate = null,
                DeathDate = null,
                Bio = string.Empty,
                IsActive = null
            }));
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
            var author =await query.FirstOrDefaultAsync(u => u!.Id == id);
            return new Author()
            {
                Id = author.Id,
                FirstName = author.FirstName,
                LastName = author.LastName,
                Nationality = author.Nationality,
                DeletedAt = author.DeletedAt,
                CreatedAt = author.CreatedAt,
                UpdatedAt = author.UpdatedAt,
                PictureUrl = author.PictureUrl,
                BirthDate = author.BirthDate,
                DeathDate = author.DeathDate,
                Bio = author.Bio,
                IsActive = author.IsActive,
            };
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

            var author =await query.FirstOrDefaultAsync(u => u!.FirstName == firstName);
            return new Author()
            {
                Id = author.Id,
                FirstName = author.FirstName,
                LastName = author.LastName,
                Nationality = author.Nationality,
                DeletedAt = author.DeletedAt,
                CreatedAt = author.CreatedAt,
                UpdatedAt = author.UpdatedAt,
                PictureUrl = author.PictureUrl,
                BirthDate = author.BirthDate,
                DeathDate = author.DeathDate,
                Bio = author.Bio,
                IsActive = author.IsActive,
            };
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

            var author =await query.FirstOrDefaultAsync(u => u!.LastName == lastName);
            return new Author()
            {
                Id = author.Id,
                FirstName = author.FirstName,
                LastName = author.LastName,
                Nationality = author.Nationality,
                DeletedAt = author.DeletedAt,
                CreatedAt = author.CreatedAt,
                UpdatedAt = author.UpdatedAt,
                PictureUrl = author.PictureUrl,
                BirthDate = author.BirthDate,
                DeathDate = author.DeathDate,
                Bio = author.Bio,
                IsActive = author.IsActive,
            };
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

    public async Task<string?> GetFirstNameAsync(Guid id) => await GetPropertyAsync(id, item => item.FirstName);
    public async Task<string?> GetLastNameAsync(Guid id) => await GetPropertyAsync(id, item => item.LastName);
    public async Task<string?> GetBioAsync(Guid id) => await GetPropertyAsync(id, item => item.Bio);
    public async Task<string?> GetPictureUrlAsync(Guid id) => await GetPropertyAsync(id, item => item.PictureUrl);
    public async Task<DateOnly?> GetBirthDateAsync(Guid id) => await GetPropertyAsync(id, item => item.BirthDate);
    public async Task<DateOnly?> GetDeathDateAsync(Guid id) => await GetPropertyAsync(id, item => item.DeathDate);
    public async Task<string?> GetNationalityAsync(Guid id) => await GetPropertyAsync(id, item => item.Nationality);

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

    public async Task<bool> SetFirstNameAsync(Guid id, string firstName) => await SetPropertyAsync(id, (author, fn) => author.FirstName = fn, firstName);
    public async Task<bool> SetLastNameAsync(Guid id, string lastName) => await SetPropertyAsync(id, (author, ln) => author.LastName = ln, lastName);
    public async Task<bool> SetBioAsync(Guid id, string bio) => await SetPropertyAsync(id, (author, b) => author.Bio = b, bio);
    public async Task<bool> SetPictureUrlAsync(Guid id, string pictureUrl) => await SetPropertyAsync(id, (author, url) => author.PictureUrl = url, pictureUrl);
    public async Task<bool> SetBirthDateAsync(Guid id, DateOnly birthDate) => await SetPropertyAsync(id, (author, bd) => author.BirthDate = bd, birthDate);
    public async Task<bool> SetDeathDateAsync(Guid id, DateOnly deathDate) => await SetPropertyAsync(id, (author, dd) => author.DeathDate = dd, deathDate);
    public async Task<bool> SetNationalityAsync(Guid id, string nationality) => await SetPropertyAsync(id, (author, nat) => author.Nationality = nat, nationality);
}