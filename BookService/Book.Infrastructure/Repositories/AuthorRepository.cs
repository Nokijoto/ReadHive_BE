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
        try
        {
            if(id==Guid.Empty)
            {
                throw new ArgumentException("Id is empty");
            }
            var author = await _context.Authors.FirstOrDefaultAsync(x => x.Id == id);
            if (author == null) return false;
            _context.Entry(author).Property(x => x.DeletedAt).CurrentValue = DateTime.Now;
            _context.Entry(author).State = EntityState.Deleted;
            _context.Entry(author).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError("Error occured while deleting author",e);
            throw;
        }
    }

    public async Task<bool> AddAsync(Author author)
    {
        try
        {
            await _context.Authors.AddAsync(author);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError("Error occured while adding author", e);
            throw;
        }
    }

    public async Task<bool> UpdateAsync(Author author)
    {
        try
        {
            var item = await _context.Authors.FirstOrDefaultAsync(x => x.Id == author.Id);
            if (item == null) return false;
            _context.Authors.Update(author);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError("Error occured while updating author",e);
            throw;
        }
    }

    public async Task<IEnumerable<Author>> GetAllAsync(bool includeDeleted=false)
    {
        try
        {
            var query = _context.Authors.AsQueryable();

            if (!includeDeleted)
            {
                query = query.Where(u => u.DeletedAt == null);
            }

            return await query.ToListAsync();
        }
        catch (Exception e)
        {
            _logger.LogError( "Error occured while getting all authors",e);
            throw;
        }
    }

    public async Task<Author?> GetByIdAsync(Guid id,bool includeDeleted=false)
    {
        try
        {
            var query = _context.Authors.AsQueryable();

            if (!includeDeleted)
            {
                query = query.Where(u => u.DeletedAt == null);
            }

            return await query.FirstOrDefaultAsync(u => u.Id == id);
        }
        catch (Exception e)
        {
            _logger.LogError( "Error occured while getting author by id",e);
            throw;
        }
    }

    public async Task<Author?> GetByFirstNameAsync(string firstName,bool includeDeleted=false)
    {
        try
        {
            return await _context.Authors.FirstOrDefaultAsync(x => x.FirstName == firstName);
        }
        catch (Exception e)
        {
            _logger.LogError( "Error occured while getting author by first name",e);
            throw;
        }
    }

    public async Task<Author?> GetByLastNameAsync(string lastName,bool includeDeleted=false)
    {
        try
        {
            return await _context.Authors.FirstOrDefaultAsync(x => x.LastName == lastName);
        }
        catch (Exception e)
        {
            _logger.LogError("Error occured while getting author by last name",e);
            throw;
        }
    }

    public async Task<string?> GetFirstNameAsync(Guid id)
    {
        try
        {
            var item = await _context.Authors.FirstOrDefaultAsync(x => x.Id == id);
            return item?.FirstName ?? string.Empty;
        }
        catch (Exception e)
        {
            _logger.LogError("Error occured while getting author first name by id",e);
            throw;
        }
    }

    public async Task<string?> GetLastNameAsync(Guid id)
    {
        try
        {
            var item = await _context.Authors.FirstOrDefaultAsync(x => x.Id == id);
            return item?.LastName ?? string.Empty;
        }
        catch (Exception e)
        {
            _logger.LogError("Error occured while getting author last name by id",e);
            throw;
        }
    }

    public async Task<string?> GetBioAsync(Guid id)
    {
        try
        {
            var item = await _context.Authors.FirstOrDefaultAsync(x => x.Id == id);
            return item?.Bio ?? string.Empty;
        }
        catch (Exception e)
        {
            _logger.LogError("Error occured while getting author bio by id",e);
            throw;
        }
    }

    public async Task<string?> GetPictureUrlAsync(Guid id)
    {
        try
        {
            var item = await _context.Authors.FirstOrDefaultAsync(x => x.Id == id);
            return item?.PictureUrl ?? string.Empty;
        }
        catch (Exception e)
        {
            _logger.LogError("Error occured while getting author picture url by id",e);
            throw;
        }
    }

    public async Task<DateTime?> GetBirthDateAsync(Guid id)
    {
        try
        {
            var item = await _context.Authors.FirstOrDefaultAsync(x => x.Id == id);
            return item?.BirthDate ?? DateTime.MinValue;
        }
        catch (Exception e)
        {
            _logger.LogError("Error occured while getting author birth date by id",e);
            throw;
        }
    }

    public async Task<DateTime?> GetDeathDateAsync(Guid id)
    {
        try
        {
            var item = await _context.Authors.FirstOrDefaultAsync(x => x.Id == id);
            return item?.DeathDate ?? DateTime.MinValue;
        }
        catch (Exception e)
        {
            _logger.LogError("Error occured while getting author death date by id",e);
            throw;
        }
    }

    public async Task<string?> GetNationalityAsync(Guid id)
    {
        try
        {
            var item = await _context.Authors.FirstOrDefaultAsync(x => x.Id == id);
            return item?.Nationality ?? string.Empty;
        }
        catch (Exception e)
        {
            _logger.LogError("Error occured while getting author nationality by id",e);
            throw;
        }
    }

    public async Task<bool> SetFirstNameAsync(Guid id, string firstName)
    {
        try
        {
            var item = await GetByIdAsync(id);
            if (item == null) return false;
            _context.Entry(item).Property(x => x.FirstName).IsModified = true;
            _context.Entry(item).Property(x => x.FirstName).CurrentValue = firstName;
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError("Error occured while setting author first name",e);
            throw;
        }
    }

    public async Task<bool> SetLastNameAsync(Guid id, string lastName)
    {
        try
        {
            var item = await GetByIdAsync(id);
            if (item == null) return false;
            _context.Entry(item).Property(x => x.LastName).IsModified = true;
            _context.Entry(item).Property(x => x.LastName).CurrentValue = lastName;
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError("Error occured while setting author last name",e);
            throw;
        }
    }

    public async Task<bool> SetBioAsync(Guid id, string bio)
    {
        try
        {
            var item = await GetByIdAsync(id);
            if (item == null) return false;
            _context.Entry(item).Property(x => x.Bio).IsModified = true;
            _context.Entry(item).Property(x => x.Bio).CurrentValue = bio;
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError("Error occured while setting author bio",e);
            throw;
        }
    }

    public async Task<bool> SetPictureUrlAsync(Guid id, string pictureUrl)
    {
        try
        {
            var item = await GetByIdAsync(id);
            if (item == null) return false;
            _context.Entry(item).Property(x => x.PictureUrl).IsModified = true;
            _context.Entry(item).Property(x => x.PictureUrl).CurrentValue = pictureUrl;
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError("Error occured while setting author picture url",e);
            throw;
        }
    }

    public async Task<bool> SetBirthDateAsync(Guid id, DateTime birthDate)
    {
        try
        {
            var item = await GetByIdAsync(id);
            if (item == null) return false;
            _context.Entry(item).Property(x => x.BirthDate).IsModified = true;
            _context.Entry(item).Property(x => x.BirthDate).CurrentValue = birthDate;
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError("Error occured while setting author birth date",e);
            throw;
        }
    }

    public async Task<bool> SetDeathDateAsync(Guid id, DateTime deathDate)
    {
        try
        {
            var item = await GetByIdAsync(id);
            if (item == null) return false;
            _context.Entry(item).Property(x => x.DeathDate).IsModified = true;
            _context.Entry(item).Property(x => x.DeathDate).CurrentValue = deathDate;
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError("Error occured while setting author death date",e);
            throw;
        }
    }

    public async Task<bool> SetNationalityAsync(Guid id, string nationality)
    {
        try
        {
            var item = await GetByIdAsync(id);
            if (item == null) return false;
            _context.Entry(item).Property(x => x.Nationality).IsModified = true;
            _context.Entry(item).Property(x => x.Nationality).CurrentValue = nationality;
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError( "Error occured while setting author nationality",e);
            throw;
        }
    }
}