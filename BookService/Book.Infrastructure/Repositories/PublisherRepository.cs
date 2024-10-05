﻿using Domain.Entities;
using Domain.Interfaces;
using Book.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

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
            await _context.Publishers.AddAsync(publisher);
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
            _context.Publishers.Update(publisher);
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

            return await query.ToListAsync();
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

            return await query.FirstOrDefaultAsync(u => u.Id == id);
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
            return await _context.Publishers.FirstOrDefaultAsync(x => x.Name == name);
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