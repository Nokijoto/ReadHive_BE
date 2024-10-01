using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repositories;

public class PublisherRepository : IPublisherRepository
{
    private readonly BookDbContext _context;
    private readonly ILogger _logger;
        
    PublisherRepository(BookDbContext context, ILogger logger)
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
            _logger.LogError(e, "Error occured while deleting publisher");
            throw;
        }
    }

    public async Task<bool> AddAsync(Publisher publisher)
    {
        try
        {
            await _context.Publishers.AddAsync(publisher);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error occured while adding publisher");
            throw;
        }
    }

    public async Task<bool> UpdateAsync(Publisher publisher)
    {
        try
        {
            var item = await _context.Publishers.FirstOrDefaultAsync(x => x.Id == publisher.Id);
            if (item == null) return false;
            _context.Publishers.Update(publisher);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error occured while updating publisher");
            throw;
        }
    }

    public async Task<Publisher?> GetByIdAsync(Guid id)
    {
        try
        {
            return await _context.Publishers.FirstOrDefaultAsync(x => x.Id == id);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error occured while getting publisher by id");
            throw;
        }
    }

    public async Task<Publisher?> GetByNameAsync(string name)
    {
        try
        {
            return await _context.Publishers.FirstOrDefaultAsync(x => x.Name == name);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error occured while getting publisher by name");
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
            _logger.LogError(e, "Error occured while getting publisher name by id");
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
            _logger.LogError(e, "Error occured while getting publisher description by id");
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
            _logger.LogError(e, "Error occured while getting publisher picture url by id");
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
            _logger.LogError(e, "Error occured while getting publisher website url by id");
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
            _logger.LogError(e, "Error occured while getting publisher country by id");
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
            _logger.LogError(e, "Error occured while getting publisher founded at by id");
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
            _logger.LogError(e, "Error occured while getting publisher founded by by id");
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
            _logger.LogError(e, "Error occured while setting publisher name");
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
            _logger.LogError(e, "Error occured while setting publisher description");
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
            _logger.LogError(e, "Error occured while setting publisher picture url");
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
            _logger.LogError(e, "Error occured while setting publisher website url");
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
            _logger.LogError(e, "Error occured while setting publisher country");
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
            _logger.LogError(e, "Error occured while setting publisher founded at");
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
            _logger.LogError(e, "Error occured while setting publisher founded by");
            throw;
        }
    }
}