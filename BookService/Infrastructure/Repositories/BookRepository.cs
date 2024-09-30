using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class BookRepository : IBookRepository
{
    private readonly BookDbContext _context;
        
    BookRepository(BookDbContext context)
    {
        _context = context;
    }
    
    
    public async Task<bool> DeleteAsync(Guid id)
    {
        try
        {
            if(id==Guid.Empty)
            {
                throw new ArgumentException("Id is empty");
            }
            var book = await _context.Books.FirstOrDefaultAsync(x => x.Id == id);
            if(book!=null)
            {
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public Task<bool> AddAsync(Book book)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(Book book)
    {
        throw new NotImplementedException();
    }

    public async Task<Book?> GetByIdAsync(Guid id)
    {
        try
        {
            if(id!=Guid.Empty)
            {
                return await _context.Books.FirstOrDefaultAsync(x => x.Id == id);
            }
            return null;
            
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public Task<Book?> GetByTitleAsync(string title)
    {
        throw new NotImplementedException();
    }

    public Task<Book?> GetByAuthorIdAsync(Guid authorId)
    {
        throw new NotImplementedException();
    }

    public Task<Book?> GetByPublisherIdAsync(Guid publisherId)
    {
        throw new NotImplementedException();
    }

    public Task<Book?> GetByGenreIdAsync(Guid genreId)
    {
        throw new NotImplementedException();
    }

    public Task<Book?> GetByCategoryIdAsync(Guid categoryId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> SetTitleAsync(Guid id, string title)
    {
        throw new NotImplementedException();
    }

    public Task<bool> SetAuthorIdAsync(Guid id, Guid authorId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> SetPublisherIdAsync(Guid id, Guid publisherId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> SetGenreIdAsync(Guid id, Guid genreId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> SetCategoryIdAsync(Guid id, Guid categoryId)
    {
        throw new NotImplementedException();
    }
}