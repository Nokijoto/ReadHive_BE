using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly BookDbContext _context;
    private readonly ILogger _logger;
    
    CategoryRepository(BookDbContext context, ILogger logger)
    {
        _context = context;
        _logger = logger;
    }
    public async Task<bool> DeleteAsync(Guid id)
    {
        try
        {
            var item =await GetByIdAsync(id);
            if (item == null) return false;
            _context.Entry(item).Property(x => x.DeletedAt).CurrentValue = DateTime.Now;
            _context.Entry(item).State = EntityState.Deleted;
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error occured while deleting category");
            throw;
        }
    }

    public async Task<bool> AddAsync(Category category)
    {
        try
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error occured while adding category");
            throw;
        }
    }

    public async Task<bool> UpdateAsync(Category category)
    {
        try
        {
            var item = await _context.Categories.FirstOrDefaultAsync(x => x.Id == category.Id);
            if (item == null) return false;
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error occured while updating category");
            throw;
        }
    }

    public async Task<Category?> GetByIdAsync(Guid id)
    {
        try
        {
            return await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error occured while getting category by id");
            throw;
        }
    }

    public async Task<Category?> GetByNameAsync(string name)
    {
        try
        {
            return await _context.Categories.FirstOrDefaultAsync(x => x.Name == name);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error occured while getting category by name");
            throw;
        }
    }

    public async Task<Category?> GetByParentCategoryIdAsync(string parentCategoryId)
    {
        try
        {
            return await _context.Categories.FirstOrDefaultAsync(x => x.ParentCategoryId == parentCategoryId);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error occured while getting category by parent category id");
            throw;
        }
    }

    public async Task<string?> GetNameAsync(Guid id)
    {
        try
        {
            var item = await GetByIdAsync(id);
            return item?.Name ?? String.Empty;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error occured while getting category name by id");
            throw;
        }   
    }

    public async Task<string?> GetParentCategoryIdAsync(Guid id)
    {
        try
        {
            var item = await GetByIdAsync(id);
            return item?.ParentCategoryId ?? String.Empty;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error occured while getting category parent category id by id");
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
            _logger.LogError(e, "Error occured while setting category name");
            throw;
        }
    }

    public async Task<bool> SetParentCategoryIdAsync(Guid id, string parentCategoryId)
    {
        try
        {
            var item = await GetByIdAsync(id);
            if (item == null) return false;
            _context.Entry(item).Property(x => x.ParentCategoryId).IsModified = true;
            _context.Entry(item).Property(x => x.ParentCategoryId).CurrentValue = parentCategoryId;
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error occured while setting category parent category id");
            throw;
        }
    }
}