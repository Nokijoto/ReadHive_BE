using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repositories;

public class ShelveRepository : IShelveRepository
{
    private readonly BookDbContext _context;
    private readonly ILogger _logger;
        
    ShelveRepository(BookDbContext context, ILogger logger)
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
            _logger.LogError(e, "Error while deleting shelve");
            throw;
        }
    }

    public async Task<bool> AddAsync(Shelve shelve)
    {
        try
        {
            await _context.Shelves.AddAsync(shelve);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while adding shelve");
            throw;
        }
    }

    public async Task<bool> UpdateAsync(Shelve shelve)
    {
        try
        {
            var item = await _context.Shelves.FirstOrDefaultAsync(x => x.Id == shelve.Id);
            if (item == null) return false;
            _context.Shelves.Update(shelve);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while updating shelve");
            throw;
        }
    }
    public async Task<IEnumerable<Shelve>> GetAllAsync(bool includeDeleted=false)
    {
        try
        {
            var query = _context.Shelves.AsQueryable();

            if (!includeDeleted)
            {
                query = query.Where(u => u.DeletedAt == null);
            }

            return await query.ToListAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while getting all shelves");
            throw;
        }
    }
    public async Task<Shelve?> GetByIdAsync(Guid id,bool includeDeleted=false)
    {
        try
        {
            var query = _context.Shelves.AsQueryable();

            if (!includeDeleted)
            {
                query = query.Where(u => u.DeletedAt == null);
            }

            return await query.FirstOrDefaultAsync(u => u.Id == id);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while getting shelve by id");
            throw;
        }
    }

    public async Task<Shelve?> GetByTitleAsync(string title,bool includeDeleted=false)
    {
        try
        {
            return await _context.Shelves.FirstOrDefaultAsync(x => x.Title == title);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while getting shelve by title");
            throw;
        }
    }

    public async Task<string?> GetTitleAsync(Guid id)
    {
        try
        {
            var item = await _context.Shelves.FirstOrDefaultAsync(x => x.Id == id);
            return item?.Title??string.Empty;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while getting shelve title by id");
            throw;
        }
    }

    public async Task<string?> GetDescriptionAsync(Guid id)
    {
        try
        {
            var item = await _context.Shelves.FirstOrDefaultAsync(x => x.Id == id);
            return item?.Description??string.Empty;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while getting shelve description by id");
            throw;
        }
    }

    public async Task<bool> SetTitleAsync(Guid id, string title)
    {
        try
        {
            var item = await GetByIdAsync(id);
            if (item == null) return false;
            _context.Entry(item).Property(x => x.Title).IsModified = true;
            _context.Entry(item).Property(x => x.Title).CurrentValue = title;
            await _context.SaveChangesAsync();  
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while setting shelve title");
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
            _logger.LogError(e, "Error while setting shelve description");
            throw;
        }
    }
}