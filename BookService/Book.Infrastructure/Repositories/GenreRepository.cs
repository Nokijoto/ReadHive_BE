using Book.Application.Models.Dto;
using Book.Domain.Entities;
using Book.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using ProjectBase;
using ProjectBase.Interfaces;

namespace Book.Infrastructure.Repositories;

public class GenreRepository : IGenreRepository
{
    private readonly BookDbContext _context;
    private readonly ILoggingService _logger;

    public GenreRepository(BookDbContext context, ILoggingService logger)
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
            _logger.LogError("Error occured while deleting genre",e);
            throw;
        }
    }

    public async Task<bool> AddAsync(GenreDto genre)
    {
        try
        {
            await _context.Genres.AddAsync(new Genre()
            {
                IsActive = genre.IsActive,
                Name = genre.Name,
                Description = genre.Description,
                ParentGenreId = genre.ParentGenreId,
                DeletedAt = null,
                CreatedAt = DateTime.Now,                
                UpdatedAt = DateTime.Now,
            }); 
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError("Error occured while adding genre",e);
            throw;
        }
    }

    public async Task<bool> UpdateAsync(GenreDto genre)
    {
        try
        {
            var item = await _context.Genres.FirstOrDefaultAsync(x => x.Id == genre.Id);
            if (item == null) return false;
            item.Name = genre.Name;
            item.Description = genre.Description;
            item.ParentGenreId = genre.ParentGenreId;            
            item.DeletedAt = genre.DeletedAt;
            item.UpdatedAt = DateTime.Now;
            item.IsActive = genre.IsActive;
            _context.Genres.Update(item);               
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError("Error occured while updating genre",e);
            throw;
        }
    }
    public async Task<IEnumerable<GenreDto>> GetAllAsync(bool includeDeleted=false)
    {
        try
        {
            var query = _context.Genres.AsQueryable();

            if (!includeDeleted)
            {
                query = query.Where(u => u.DeletedAt == null);
            }
            var result = await query.ToListAsync();
            return new List<GenreDto?>(result.Select(genre => genre != null
                ? new GenreDto()
                {
                    Id = genre.Id,
                    Name = genre.Name,                    
                    Description = genre.Description,
                    ParentGenreId = genre.ParentGenreId,
                    DeletedAt = genre.DeletedAt,
                    CreatedAt = genre.CreatedAt,
                    UpdatedAt = genre.UpdatedAt,
                    IsActive = genre.IsActive
                }: null));
        }
        catch (Exception e)
        {
            _logger.LogError("Error occured while getting all genres",e);
            throw;
        }
    }
    
    public async Task<GenreDto?> GetByIdAsync(Guid id,bool includeDeleted=false)
    {
        try
        {
            var query = _context.Genres.AsQueryable();

            if (!includeDeleted)
            {
                query = query.Where(u => u.DeletedAt == null);
            }
            var result = await query.FirstOrDefaultAsync(u => u.Id == id);
            return result != null
                ? new GenreDto()
                {
                    Id = result.Id,
                    Name = result.Name,
                    Description = result.Description,
                    ParentGenreId = result.ParentGenreId,
                    DeletedAt = result.DeletedAt,                    
                    CreatedAt = result.CreatedAt,
                    UpdatedAt = result.UpdatedAt,
                    IsActive = result.IsActive
                }   
                : null;
        }
        catch (Exception e)
        {
            _logger.LogError( "Error occured while getting genre by id",e);
            throw;
        }
    }

    public async Task<GenreDto?> GetByNameAsync(string name,bool includeDeleted=false)
    {
        try
        {
            var genre = await _context.Genres.FirstOrDefaultAsync(x => x.Name == name);
            return genre != null
                ? new GenreDto()
                {
                    Id = genre.Id,
                    Name = genre.Name,
                    Description = genre.Description,
                    ParentGenreId = genre.ParentGenreId,
                    DeletedAt = genre.DeletedAt,
                    CreatedAt = genre.CreatedAt,
                    UpdatedAt = genre.UpdatedAt,
                    IsActive = genre.IsActive
                }
                : null;
        }
        catch (Exception e)
        {
            _logger.LogError( "Error occured while getting genre by name",e);
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
            _logger.LogError( "Error occured while getting genre name by id",e);
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
            _logger.LogError( "Error occured while getting genre description by id",e);
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
            _logger.LogError( "Error occured while getting genre parent genre id by id",e);
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
            _logger.LogError("Error occured while setting genre name",e);
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
            _logger.LogError( "Error occured while setting genre description",e);
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
            _logger.LogError( "Error occured while setting genre parent genre id",e);
            throw;
        }
    }
}