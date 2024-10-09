using Book.Application.Models.Dto;
using Book.Domain.Entities;
using Book.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using ProjectBase;
using ProjectBase.Interfaces;

namespace Book.Infrastructure.Repositories;

public class ShelveRepository : IShelveRepository
{
    private readonly BookDbContext _context;
    private readonly ILoggingService _logger;
        
    public ShelveRepository(BookDbContext context, ILoggingService logger)
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
            _logger.LogError( "Error while deleting shelve",e);
            throw;
        }
    }

    public async Task<bool> AddAsync(Shelve shelve)
    {
        try
        {
            await _context.Shelves.AddAsync(new Shelve()
            {
                Description = shelve.Description,
                Title = shelve.Title,
                OwnerId = shelve.OwnerId,
                DeletedAt = null,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                IsActive = shelve.IsActive,
            });
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError( "Error while adding shelve",e);
            throw;
        }
    }

    public async Task<bool> UpdateAsync(Shelve shelve)
    {
        try
        {
            var item = await _context.Shelves.FirstOrDefaultAsync(x => x.Id == shelve.Id);
            if (item == null) return false;
            item.Description = shelve.Description;
            item.Title = shelve.Title;
            item.OwnerId = shelve.OwnerId;
            item.DeletedAt = shelve.DeletedAt;
            item.UpdatedAt = DateTime.Now;
            item.IsActive = shelve.IsActive;
            _context.Shelves.Update(item);  
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError("Error while updating shelve",e);
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
            var result = await query.ToListAsync();
            return new List<Shelve?>(result.Select(shelve => shelve != null
                ? new Shelve()
                {
                    Id = shelve.Id,
                    Description = shelve.Description,
                    Title = shelve.Title,
                    OwnerId = shelve.OwnerId,
                    DeletedAt = shelve.DeletedAt,
                    CreatedAt = shelve.CreatedAt,
                    UpdatedAt = shelve.UpdatedAt,
                    IsActive = shelve.IsActive
                }
                : null));
        }
        catch (Exception e)
        {
            _logger.LogError("Error while getting all shelves",e);
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

            var result = await query.FirstOrDefaultAsync(u => u.Id == id);
            return result != null
                ? new Shelve()
                {
                    Id = result.Id,
                    Description = result.Description,
                    Title = result.Title,
                    OwnerId = result.OwnerId,
                    DeletedAt = result.DeletedAt,
                    CreatedAt = result.CreatedAt,
                    UpdatedAt = result.UpdatedAt,
                    IsActive = result.IsActive
                }
                : null; 
        }
        catch (Exception e)
        {
            _logger.LogError( "Error while getting shelve by id",e);
            throw;
        }
    }

    public async Task<Shelve?> GetByTitleAsync(string title,bool includeDeleted=false)
    {
        try
        {
            if(string.IsNullOrEmpty(title))
            {
                return null;
            }
            
            var shelve = await _context.Shelves.FirstOrDefaultAsync(x => x.Title == title);
            return shelve != null
                ? new Shelve()
                {
                    Id = shelve.Id,
                    Description = shelve.Description,
                    Title = shelve.Title,
                    OwnerId = shelve.OwnerId,
                    DeletedAt = shelve.DeletedAt,
                    CreatedAt = shelve.CreatedAt,
                    UpdatedAt = shelve.UpdatedAt,
                    IsActive = shelve.IsActive
                }                
                : null;
        }
        catch (Exception e)
        {
            _logger.LogError("Error while getting shelve by title",e);
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
            _logger.LogError( "Error while getting shelve title by id",e);
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
            _logger.LogError( "Error while getting shelve description by id",e);
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
            _logger.LogError( "Error while setting shelve title",e);
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
            _logger.LogError("Error while setting shelve description",e);
            throw;
        }
    }
}