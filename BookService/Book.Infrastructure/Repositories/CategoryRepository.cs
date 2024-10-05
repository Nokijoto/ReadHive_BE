using Domain.Entities;
using Domain.Interfaces;
using Book.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Book.Infrastructure.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly BookDbContext _context;
    private readonly ILoggingService _logger;
    
    public CategoryRepository(BookDbContext context, ILoggingService logger)
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
            _logger.LogError( "Error occured while deleting category",e);
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
            _logger.LogError("Error occured while adding category",e);
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
            _logger.LogError("Error occured while updating category",e);
            throw;
        }
    }

    public async Task<IEnumerable<Category>> GetAllAsync(bool includeDeleted=false) 
    {
        try
        {
            var query = _context.Categories.AsQueryable();

            if (!includeDeleted)
            {
                query = query.Where(u => u.DeletedAt == null);
            }

            return await query.ToListAsync();
        }
        catch (Exception e)
        {
            _logger.LogError("Error occured while getting all categories",e);
            throw;
        }
    }
    
    public async Task<Category?> GetByIdAsync(Guid id,bool includeDeleted=false)
    {
        try
        {
            var query = _context.Categories.AsQueryable();

            if (!includeDeleted)
            {
                query = query.Where(u => u.DeletedAt == null);
            }

            return await query.FirstOrDefaultAsync(u => u.Id == id);
        }
        catch (Exception e)
        {
            _logger.LogError( "Error occured while getting category by id",e);
            throw;
        }
    }

    public async Task<Category?> GetByNameAsync(string name,bool includeDeleted=false)
    {
        try
        {
            return await _context.Categories.FirstOrDefaultAsync(x => x.Name == name);
        }
        catch (Exception e)
        {
            _logger.LogError("Error occured while getting category by name",e);
            throw;
        }
    }

    public async Task<Category?> GetByParentCategoryIdAsync(string parentCategoryId,bool includeDeleted=false)
    {
        try
        {
            return await _context.Categories.FirstOrDefaultAsync(x => x.ParentCategoryId == parentCategoryId);
        }
        catch (Exception e)
        {
            _logger.LogError("Error occured while getting category by parent category id",e);
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
            _logger.LogError( "Error occured while getting category name by id",e);
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
            _logger.LogError("Error occured while getting category parent category id by id",e);
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
            _logger.LogError( "Error occured while setting category name",e);
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
            _logger.LogError("Error occured while setting category parent category id",e);
            throw;
        }
    }
}