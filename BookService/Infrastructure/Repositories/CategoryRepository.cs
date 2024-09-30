using Domain.Entities;
using Domain.Interfaces;

namespace Infrastructure.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly BookDbContext _context;
        
    CategoryRepository(BookDbContext context)
    {
        _context = context;
    }
    public async Task<bool> DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> AddAsync(Category category)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> UpdateAsync(Category category)
    {
        throw new NotImplementedException();
    }

    public async Task<Category?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<Category?> GetByNameAsync(string name)
    {
        throw new NotImplementedException();
    }

    public async Task<Category?> GetByParentCategoryIdAsync(string parentCategoryId)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> SetNameAsync(Guid id, string name)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> SetParentCategoryIdAsync(Guid id, string parentCategoryId)
    {
        throw new NotImplementedException();
    }
}