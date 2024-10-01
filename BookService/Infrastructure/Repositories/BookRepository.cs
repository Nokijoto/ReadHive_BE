using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repositories;

public class BookRepository : IBookRepository
{
    private readonly BookDbContext _context;
    private readonly ILogger _logger;

    public BookRepository(BookDbContext context, ILogger logger)
    {
        _context = context;
        _logger = logger;
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
            if (book == null) return false;
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<bool> AddAsync(Book book)
    {
        try
        {
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error occured while adding book");
            throw;
        }
    }

    public async Task<bool> UpdateAsync(Book book)
    {
        try
        {
            var item = await _context.Books.FirstOrDefaultAsync(x => x.Id == book.Id);
            if (item == null) return false;
            _context.Books.Update(book);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error occured while updating book");
            throw;
        }
    }
    public async Task<IEnumerable<Book>> GetAllAsync(bool includeDeleted=false)
    {
        try
        {
            var query = _context.Books.AsQueryable();

            if (!includeDeleted)
            {
                query = query.Where(u => u.DeletedAt == null);
            }

            return await query.ToListAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error occured while getting all books");
            throw;
        }
    }

    public async Task<Book?> GetByIdAsync(Guid id,bool includeDeleted=false)
    {
        try
        {
            var query = _context.Books.AsQueryable();

            if (!includeDeleted)
            {
                query = query.Where(u => u.DeletedAt == null);
            }

            return await query.FirstOrDefaultAsync(u => u.Id == id);
            
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error occured while getting book by id");
            throw;
        }
    }

    public Task<Book?> GetByTitleAsync(string? title,bool includeDeleted=false)
    {
        try
        {
            return string.IsNullOrEmpty(title) ? null! : _context.Books.FirstOrDefaultAsync(x => x.Title == title);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error occured while getting book by title");
            throw;
        }
    }

    public Task<Book?> GetByAuthorIdAsync(Guid authorId,bool includeDeleted=false)
    {
        try
        {
            return authorId==Guid.Empty ? null! : _context.Books.FirstOrDefaultAsync(x => x.AuthorId == authorId);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error occured while getting book by author id");
            throw;
        }
    }

    public Task<Book?> GetByPublisherIdAsync(Guid publisherId,bool includeDeleted=false)
    {
        try
        {
            return publisherId==Guid.Empty ? null! : _context.Books.FirstOrDefaultAsync(x => x.PublisherId == publisherId);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error occured while getting book by publisher id");
            throw;
        }
    }

    public Task<Book?> GetByGenreIdAsync(Guid genreId,bool includeDeleted=false)
    {
        try
        {
            return genreId==Guid.Empty ? null! : _context.Books.FirstOrDefaultAsync(x => x.GenreId == genreId);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error occured while getting book by genre id");
            throw;
        }
    }

    public Task<Book?> GetByCategoryIdAsync(Guid categoryId,bool includeDeleted=false)
    {
        try
        {
            return categoryId==Guid.Empty ? null! : _context.Books.FirstOrDefaultAsync(x => x.CategoryId == categoryId);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error occured while getting book by category id");
            throw;
        }
    }

    public async Task<string?> GetTitleAsync(Guid id)
    {
        try
        {
            var item = await _context.Books.FirstOrDefaultAsync(x => x.Id == id);
            return item?.Title ?? string.Empty;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error occured while getting book title by id");
            throw;
        }
    }

    public async Task<Guid?> GetAuthorIdAsync(Guid id)
    {
        try
        {
            var item = await _context.Books.FirstOrDefaultAsync(x => x.Id == id);
            return item?.AuthorId ?? Guid.Empty;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error occured while getting book author id by id");
            throw;
        }
    }

    public async Task<Guid?> GetPublisherIdAsync(Guid id)
    {
        try
        {
            var item = await _context.Books.FirstOrDefaultAsync(x => x.Id == id);
            return item?.PublisherId ?? Guid.Empty;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error occured while getting book publisher id by id");
            throw;
        }
    }

    public async Task<Guid?> GetGenreIdAsync(Guid id)
    {
        try
        {
            var item = await _context.Books.FirstOrDefaultAsync(x => x.Id == id);
            return item?.GenreId ?? Guid.Empty;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error occured while getting book genre id by id");
            throw;
        }
    }

    public async Task<Guid?> GetCategoryIdAsync(Guid id)
    {
        try
        {
            var item = await _context.Books.FirstOrDefaultAsync(x => x.Id == id);
            return item?.CategoryId ?? Guid.Empty;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error occured while getting book category id by id");
            throw;
        }
    }

    public async Task<bool> SetTitleAsync(Guid id, string title)
    {
        try
        {
            var item = await _context.Books.FirstOrDefaultAsync(x => x.Id == id);
            if (item == null) return false;
            item.Title = title;
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error occured while setting book title");
            throw;
        }
    }

    public async Task<bool> SetAuthorIdAsync(Guid id, Guid authorId)
    {
        try
        {
            var item = await _context.Books.FirstOrDefaultAsync(x => x.Id == id);
            if (item == null) return false;
            item.AuthorId = authorId;
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error occured while setting book author id");
            throw;
        }
    }

    public async Task<bool> SetPublisherIdAsync(Guid id, Guid publisherId)
    {
        try
        {
            var item = await _context.Books.FirstOrDefaultAsync(x => x.Id == id);
            if (item == null) return false;
            item.PublisherId = publisherId;
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error occured while setting book publisher id");
            throw;
        }
    }

    public async Task<bool> SetGenreIdAsync(Guid id, Guid genreId)
    {
        try
        {
            var item = await _context.Books.FirstOrDefaultAsync(x => x.Id == id);
            if (item == null) return false;
            item.GenreId = genreId;
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error occured while setting book genre id");
            throw;
        }
    }

    public async Task<bool> SetCategoryIdAsync(Guid id, Guid categoryId)
    {
        try
        {
            var item = await _context.Books.FirstOrDefaultAsync(x => x.Id == id);
            if (item == null) return false;
            item.CategoryId = categoryId;
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error occured while setting book category id");
            throw;
        }
    }
}