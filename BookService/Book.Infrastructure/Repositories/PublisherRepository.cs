using Book.Application.Models.Dto;
using Book.Domain.Entities;
using Book.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using ProjectBase.Interfaces;

namespace Book.Infrastructure.Repositories;

public class PublisherRepository : IPublisherRepository
{
    private readonly BookDbContext _context;
    private readonly ILoggingService _logger;
        
    public PublisherRepository(BookDbContext context, ILoggingService logger)
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
            _logger.LogError("Error occured while deleting publisher",e);
            throw;
        }
    }

    public async Task<bool> AddAsync(Publisher publisher)
    {
        try
        {
            await _context.Publishers.AddAsync(new Publisher()
            {
                Country = publisher.Country,
                Description = publisher.Description,
                FoundedAt = publisher.FoundedAt,
                FoundedBy = publisher.FoundedBy,
                IsActive = publisher.IsActive,
                Name = publisher.Name,
                PictureUrl = publisher.PictureUrl,
                WebsiteUrl = publisher.WebsiteUrl,
                DeletedAt = null,                
                CreatedAt = DateTime.Now,
                UpdatedAt = null         
            });
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError( "Error occured while adding publisher",e);
            throw;
        }
    }

    public async Task<bool> UpdateAsync(Publisher publisher)
    {
        try
        {
            var item = await _context.Publishers.FirstOrDefaultAsync(x => x.Id == publisher.Id);
            if (item == null) return false;
            item.Name = publisher.Name;
            item.Description = publisher.Description;
            item.PictureUrl = publisher.PictureUrl;
            item.WebsiteUrl = publisher.WebsiteUrl;
            item.Country = publisher.Country;
            item.FoundedAt = publisher.FoundedAt;
            item.FoundedBy = publisher.FoundedBy;
            item.DeletedAt = publisher.DeletedAt;
            item.UpdatedAt = DateTime.Now;
            item.IsActive = publisher.IsActive;
            _context.Publishers.Update(item);       
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError("Error occured while updating publisher",e);
            throw;
        }
    }
    
    public async Task<IEnumerable<Publisher>> GetAllAsync(bool includeDeleted=false)
    {
        try
        {
            var query = _context.Publishers.AsQueryable();

            if (!includeDeleted)
            {
                query = query.Where(u => u.DeletedAt == null);
            }
            var result = await query.ToListAsync();
            return new List<Publisher?>(result.Select(publisher => publisher != null
                ? new Publisher()
                {
                    Id = publisher.Id,
                    Name = publisher.Name,
                    Description = publisher.Description,
                    PictureUrl = publisher.PictureUrl,
                    WebsiteUrl = publisher.WebsiteUrl,
                    Country = publisher.Country,
                    FoundedAt = publisher.FoundedAt,
                    FoundedBy = publisher.FoundedBy,
                    DeletedAt = publisher.DeletedAt,
                    CreatedAt = publisher.CreatedAt,
                    UpdatedAt = publisher.UpdatedAt,
                    IsActive = publisher.IsActive
                }                
                : null));
        }
        catch (Exception e)
        {
            _logger.LogError("Error occured while getting all publishers",e);
            throw;
        }
    }

    public async Task<Publisher?> GetByIdAsync(Guid id,bool includeDeleted=false)
    {
        try
        {
            var query = _context.Publishers.AsQueryable();

            if (!includeDeleted)
            {
                query = query.Where(u => u.DeletedAt == null);
            }
            var result = await query.FirstOrDefaultAsync(u => u.Id == id);
            return result != null
                ? new Publisher()
                {
                    Id = result.Id,
                    Name = result.Name,
                    Description = result.Description,
                    PictureUrl = result.PictureUrl,
                    WebsiteUrl = result.WebsiteUrl,
                    Country = result.Country,
                    FoundedAt = result.FoundedAt,
                    FoundedBy = result.FoundedBy,
                    DeletedAt = result.DeletedAt,
                    CreatedAt = result.CreatedAt,
                    UpdatedAt = result.UpdatedAt,
                    IsActive = result.IsActive
                }
                : null;
            
        }
        catch (Exception e)
        {
            _logger.LogError( "Error occured while getting publisher by id",e);
            throw;
        }
    }

    public async Task<Publisher?> GetByNameAsync(string name,bool includeDeleted=false)
    {
        try
        {
            var publisher = await _context.Publishers.FirstOrDefaultAsync(x => x.Name == name);
            return publisher != null
                ? new Publisher()
                {
                    Id = publisher.Id,
                    Name = publisher.Name,
                    Description = publisher.Description,
                    PictureUrl = publisher.PictureUrl,
                    WebsiteUrl = publisher.WebsiteUrl,
                    Country = publisher.Country,
                    FoundedAt = publisher.FoundedAt,
                    FoundedBy = publisher.FoundedBy,
                    DeletedAt = publisher.DeletedAt,
                    CreatedAt = publisher.CreatedAt,
                    UpdatedAt = publisher.UpdatedAt,
                    IsActive = publisher.IsActive
                }                
                : null;
        }
        catch (Exception e)
        {
            _logger.LogError( "Error occured while getting publisher by name",e);
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
            _logger.LogError( "Error occured while getting publisher name by id",e);
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
            _logger.LogError( "Error occured while getting publisher description by id",e);
            throw;
        }
    }

    public async Task<string?> GetPictureUrlAsync(Guid id)
    {
        try
        {
            var item = await GetByIdAsync(id);
            return item?.PictureUrl ?? string.Empty;    
        }
        catch (Exception e)
        {
            _logger.LogError( "Error occured while getting publisher picture url by id",e);
            throw;
        }
    }

    public async Task<string?> GetWebsiteUrlAsync(Guid id)
    {
        try
        {
            var item = await GetByIdAsync(id);
            return item?.WebsiteUrl ?? string.Empty;
        }
        catch (Exception e)
        {
            _logger.LogError( "Error occured while getting publisher website url by id",e);
            throw;
        }
    }

    public async Task<string?> GetCountryAsync(Guid id)
    {
        try
        {
            var item = await GetByIdAsync(id);
            return item?.Country ?? string.Empty;
        }   
        catch (Exception e)
        {
            _logger.LogError( "Error occured while getting publisher country by id",e);
            throw;
        }
    }

    public async Task<string?> GetFoundedAtAsync(Guid id)
    {
        try
        {
            var item = await GetByIdAsync(id);
            return item?.FoundedAt ?? string.Empty;
        }
        catch (Exception e)
        {
            _logger.LogError( "Error occured while getting publisher founded at by id",e);
            throw;
        }
    }

    public async Task<string?> GetFoundedByAsync(Guid id)
    {
        try
        {
            var item = await GetByIdAsync(id);
            return item?.FoundedBy ?? string.Empty; 
        }
        catch (Exception e)
        {
            _logger.LogError( "Error occured while getting publisher founded by by id",e);
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
            _logger.LogError( "Error occured while setting publisher name",e);
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
            _logger.LogError( "Error occured while setting publisher description",e);
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
            _logger.LogError( "Error occured while setting publisher picture url",e);
            throw;
        }
    }

    public async Task<bool> SetWebsiteUrlAsync(Guid id, string websiteUrl)
    {
        try
        {
            var item = await GetByIdAsync(id);
            if (item == null) return false;
            _context.Entry(item).Property(x => x.WebsiteUrl).IsModified = true;
            _context.Entry(item).Property(x => x.WebsiteUrl).CurrentValue = websiteUrl;
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError( "Error occured while setting publisher website url",e);
            throw;
        }
    }

    public async Task<bool> SetCountryAsync(Guid id, string country)
    {
        try
        {
            var item = await GetByIdAsync(id);
            if (item == null) return false;
            _context.Entry(item).Property(x => x.Country).IsModified = true;
            _context.Entry(item).Property(x => x.Country).CurrentValue = country;
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError( "Error occured while setting publisher country",e);
            throw;
        }
    }

    public async Task<bool> SetFoundedAtAsync(Guid id, string foundedAt)
    {
        try
        {
            var item = await GetByIdAsync(id);
            if (item == null) return false;
            _context.Entry(item).Property(x => x.FoundedAt).IsModified = true;
            _context.Entry(item).Property(x => x.FoundedAt).CurrentValue = foundedAt;
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError( "Error occured while setting publisher founded at",e);
            throw;
        }
    }

    public async Task<bool> SetFoundedByAsync(Guid id, string foundedBy)
    {
        try
        {
            var item = await GetByIdAsync(id);
            if (item == null) return false;
            _context.Entry(item).Property(x => x.FoundedBy).IsModified = true;
            _context.Entry(item).Property(x => x.FoundedBy).CurrentValue = foundedBy;
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError( "Error occured while setting publisher founded by",e);
            throw;
        }
    }
}