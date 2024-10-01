using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repositories;

public class GenreRepository : IGenreRepository
{
    private readonly BookDbContext _context;
    private readonly ILogger _logger;

    public GenreRepository(BookDbContext context, ILogger logger)
    {
        _context = context;
        _logger = logger;
    }
    public async Task<bool> DeleteAsync(Guid id)
    {
        try
        {
            var item = await GetByIdAsync(id);
            if (item == null) return false;
            _context.Entry(item).Property(x => x.DeletedAt).CurrentValue = DateTime.Now;
            _context.Entry(item).State = EntityState.Deleted;
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error occured while deleting genre");
            throw;
        }
    }

    public async Task<bool> AddAsync(Genre genre)
    {
        try
        {
            await _context.Genres.AddAsync(genre);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error occured while adding genre");
            throw;
        }
    }

    public async Task<bool> UpdateAsync(Genre genre)
    {
        try
        {
            var item = await _context.Genres.FirstOrDefaultAsync(x => x.Id == genre.Id);
            if (item == null) return false;
            _context.Genres.Update(genre);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error occured while updating genre");
            throw;
        }
    }
    public async Task<IEnumerable<Genre>> GetAllAsync(bool includeDeleted=false)
    {
        try
        {
            var query = _context.Genres.AsQueryable();

            if (!includeDeleted)
            {
                query = query.Where(u => u.DeletedAt == null);
            }

            return await query.ToListAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error occured while getting all genres");
            throw;
        }
    }
    
    public async Task<Genre?> GetByIdAsync(Guid id,bool includeDeleted=false)
    {
        try
        {
            var query = _context.Genres.AsQueryable();

            if (!includeDeleted)
            {
                query = query.Where(u => u.DeletedAt == null);
            }

            return await query.FirstOrDefaultAsync(u => u.Id == id);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error occured while getting genre by id");
            throw;
        }
    }

    public async Task<Genre?> GetByNameAsync(string name,bool includeDeleted=false)
    {
        try
        {
            return await _context.Genres.FirstOrDefaultAsync(x => x.Name == name);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error occured while getting genre by name");
            throw;
        }
    }

    public async Task<string?> GetNameAsync(Guid id)
    {
        try
        {
            var item = await GetByIdAsync(id);
            return item?.Name ?? string.Empty;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error occured while getting genre name by id");
            throw;
        }
    }

    public async Task<string?> GetDescriptionAsync(Guid id)
    {
        try
        {
            var item = await GetByIdAsync(id);
            return item?.Description ?? string.Empty;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error occured while getting genre description by id");
            throw;
        }
    }

    public async Task<string?> GetParentGenreIdAsync(Guid id)
    {
        try
        {
            var item = await GetByIdAsync(id);  
            return item?.ParentGenreId ?? string.Empty;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error occured while getting genre parent genre id by id");
            throw;
        }
    }

    public async Task<bool> SetNameAsync(Guid id, string name)
    {
        try
        {
            var item = await GetByIdAsync(id);
            if (item == null) return false;
            _context.Entry(item).Property(x => x.Name).IsModified = true;
            _context.Entry(item).Property(x => x.Name).CurrentValue = name;
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error occured while setting genre name");
            throw;
        }
    }

    public async Task<bool> SetDescriptionAsync(Guid id, string description)
    {
        try
        {
            var item = await GetByIdAsync(id);
            if (item == null) return false;
            _context.Entry(item).Property(x => x.Description).IsModified = true;
            _context.Entry(item).Property(x => x.Description).CurrentValue = description;
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error occured while setting genre description");
            throw;
        }
    }

    public async Task<bool> SetParentGenreIdAsync(Guid id, string parentGenreId)
    {
        try
        {
            var item = await GetByIdAsync(id);
            if (item == null) return false;
            _context.Entry(item).Property(x => x.ParentGenreId).IsModified = true;
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error occured while setting genre parent genre id");
            throw;
        }
    }
}