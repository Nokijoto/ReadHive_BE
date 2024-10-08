using Book.Application.Models.Dto;
using Book.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using ProjectBase.Interfaces;

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

    public async Task<bool> AddAsync(CategoryDto category)
    {
        try
        {
            await _context.Categories.AddAsync(new Domain.Entities.Category()
            {
                Name = category.Name,
                Description = category.Description,
                ParentCategoryId = category.ParentCategoryId,
                DeletedAt = null,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                IsActive = category.IsActive,
                
            });
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError("Error occured while adding category",e);
            throw;
        }
    }

    public async Task<bool> UpdateAsync(CategoryDto category)
    {
        try
        {
            var item = await _context.Categories.FirstOrDefaultAsync(x => x.Id == category.Id);
            if (item == null) return false;
            item.Name = category.Name;
            item.Description = category.Description;
            item.ParentCategoryId = category.ParentCategoryId;
            item.DeletedAt = category.DeletedAt;
            item.UpdatedAt = DateTime.Now;
            item.IsActive = category.IsActive;
            _context.Categories.Update(item);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError("Error occured while updating category",e);
            throw;
        }
    }

    public async Task<IEnumerable<CategoryDto>> GetAllAsync(bool includeDeleted=false) 
    {
        try
        {
            var query = _context.Categories.AsQueryable();

            if (!includeDeleted)
            {
                query = query.Where(u => u.DeletedAt == null);
            }
            var result = await query.ToListAsync();
            return new List<CategoryDto?>(result.Select(category => category != null
                ? new CategoryDto()
                {
                    Id = category.Id,
                    Name = category.Name,
                    Description = category.Description,
                    ParentCategoryId = category.ParentCategoryId,
                    DeletedAt = category.DeletedAt,
                    CreatedAt = category.CreatedAt,
                    UpdatedAt = category.UpdatedAt,
                    IsActive = category.IsActive
                }   
                : null));
        }
        catch (Exception e)
        {
            _logger.LogError("Error occured while getting all categories",e);
            throw;
        }
    }
    
    public async Task<CategoryDto?> GetByIdAsync(Guid id,bool includeDeleted=false)
    {
        try
        {
            var query = _context.Categories.AsQueryable();

            if (!includeDeleted)
            {
                query = query.Where(u => u.DeletedAt == null);
            }
            var result = await query.FirstOrDefaultAsync(u => u.Id == id);
            return result != null
                ? new CategoryDto()
                {
                    Id = result.Id,
                    Name = result.Name,
                    Description = result.Description,
                    ParentCategoryId = result.ParentCategoryId,
                    DeletedAt = result.DeletedAt,
                    CreatedAt = result.CreatedAt,
                    UpdatedAt = result.UpdatedAt,
                    IsActive = result.IsActive
                }   
                : null;
        }
        catch (Exception e)
        {
            _logger.LogError( "Error occured while getting category by id",e);
            throw;
        }
    }

    public async Task<CategoryDto?> GetByNameAsync(string name,bool includeDeleted=false)
    {
        try
        {
            var category = await _context.Categories.FirstOrDefaultAsync(x => x.Name == name);
            return category != null
                ? new CategoryDto()
                {
                    Id = category.Id,
                    Name = category.Name,
                    Description = category.Description,
                    ParentCategoryId = category.ParentCategoryId,
                    DeletedAt = category.DeletedAt,
                    CreatedAt = category.CreatedAt,
                    UpdatedAt = category.UpdatedAt,
                    IsActive = category.IsActive
                }
                : null;
        }
        catch (Exception e)
        {
            _logger.LogError("Error occured while getting category by name",e);
            throw;
        }
    }

    public async Task<CategoryDto?> GetByParentCategoryIdAsync(string parentCategoryId,bool includeDeleted=false)
    {
        try
        {
            var category = await _context.Categories.FirstOrDefaultAsync(x => x.ParentCategoryId == parentCategoryId);
            return category != null
                ? new CategoryDto()
                {
                    Id = category.Id,
                    Name = category.Name,
                    Description = category.Description,
                    ParentCategoryId = category.ParentCategoryId,
                    DeletedAt = category.DeletedAt,
                    CreatedAt = category.CreatedAt,
                    UpdatedAt = category.UpdatedAt,
                    IsActive = category.IsActive
                }   
                : null;
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