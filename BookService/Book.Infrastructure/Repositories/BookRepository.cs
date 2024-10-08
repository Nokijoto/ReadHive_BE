using Book.Application.Models.Dto;
using Book.Domain.Interfaces;
using Book.Domain.Entities;
using Book.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using ProjectBase;
using ProjectBase.Interfaces;

namespace Book.Infrastructure.Repositories;

public class BookRepository : IBookRepository
{
    private readonly BookDbContext _context;
    private readonly ILoggingService _logger;

    public BookRepository(BookDbContext context, ILoggingService logger)
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
            _logger.LogError("Error occured while deleting book",e);
            throw;
        }
    }

    public async Task<bool> AddAsync(BookDto book)
    {
        try
        {   
            await _context.Books.AddAsync(new Domain.Entities.Book()
            {
                Title = book.Title,
                Subtitle = book.Subtitle,
                Isbn = book.Isbn,
                Description = book.Description,
                Author = book.Author,
                Publisher = book.Publisher,
                Language = book.Language,
                Series = book.Series,
                NumberOfPages = book.NumberOfPages,
                Dimensions = book.Dimensions,
                Format = book.Format,
                Edition = book.Edition,
                TableOfContents = book.TableOfContents,
                Price = book.Price,
                PublishedAt = book.PublishedAt,
                AuthorId = book.AuthorId,
                PublisherId = book.PublisherId,
                GenreId = book.GenreId,
                CategoryId = book.CategoryId,
                DeletedAt = book.DeletedAt,
                CreatedAt = book.CreatedAt,
                UpdatedAt = book.UpdatedAt,
                IsActive = book.IsActive,
            });
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError("Error occured while adding book",e);
            throw;
        }
    }

    public async Task<bool> UpdateAsync(BookDto book)
    {
        try
        {
            var item = await _context.Books.FirstOrDefaultAsync(x => x.Id == book.Id);
            if (item == null) return false;
            item.Title = book.Title;
            item.Subtitle = book.Subtitle;
            item.Isbn = book.Isbn;
            item.Description = book.Description;
            item.Author = book.Author;
            item.Publisher = book.Publisher;
            item.Language = book.Language;
            item.Series = book.Series;
            item.NumberOfPages = book.NumberOfPages;
            item.Dimensions = book.Dimensions;
            item.Format = book.Format;
            item.Edition = book.Edition;
            item.TableOfContents = book.TableOfContents;
            item.Price = book.Price;
            item.PublishedAt = book.PublishedAt;
            item.AuthorId = book.AuthorId;
            item.PublisherId = book.PublisherId;
            item.GenreId = book.GenreId;
            item.CategoryId = book.CategoryId;
            item.DeletedAt = book.DeletedAt;
            item.CreatedAt = book.CreatedAt;
            item.UpdatedAt = book.UpdatedAt;
            item.IsActive = book.IsActive;
            _context.Books.Update(item);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError( "Error occured while updating book",e);
            throw;
        }
    }
    public async Task<IEnumerable<BookDto>> GetAllAsync(bool includeDeleted=false)
    {
        try
        {
            var query = _context.Books.AsQueryable();

            if (!includeDeleted)
            {
                query = query.Where(u => u.DeletedAt == null);
            }
            var result = await query.ToListAsync();
            return new List<BookDto?>(result.Select(book => book != null
                ? new BookDto()
                {
                    Id = book.Id,
                    Title = book.Title,
                    Subtitle = book.Subtitle,
                    Isbn = book.Isbn,
                    Description = book.Description,
                    Author = book.Author,
                    Publisher = book.Publisher,
                    Language = book.Language,
                    Series = book.Series,
                    NumberOfPages = book.NumberOfPages,
                    Dimensions = book.Dimensions,
                    Format = book.Format,
                    Edition = book.Edition,
                    TableOfContents = book.TableOfContents,
                    Price = book.Price,
                    PublishedAt = book.PublishedAt,
                    AuthorId = book.AuthorId,
                    PublisherId = book.PublisherId,   
                    GenreId = book.GenreId,
                    CategoryId = book.CategoryId,
                    DeletedAt = book.DeletedAt,
                    CreatedAt = book.CreatedAt,
                    UpdatedAt = book.UpdatedAt,
                    IsActive = book.IsActive
                }   
                : null));
        }
        catch (Exception e)
        {
            _logger.LogError( "Error occured while getting all books",e);
            throw;
        }
    }

    public async Task<BookDto?> GetByIdAsync(Guid id,bool includeDeleted=false)
    {
        try
        {
            var query = _context.Books.AsQueryable();

            if (!includeDeleted)
            {
                query = query.Where(u => u.DeletedAt == null);
            }
            var result = await query.FirstOrDefaultAsync(u => u.Id == id);
            return result != null
                ? new BookDto()
                {
                    Id = result.Id,
                    Title = result.Title,
                    Subtitle = result.Subtitle,
                    Isbn = result.Isbn,
                    Description = result.Description,
                    Author = result.Author,
                    Publisher = result.Publisher,
                    Language = result.Language,
                    Series = result.Series,
                    NumberOfPages = result.NumberOfPages,
                    Dimensions = result.Dimensions,
                    Format = result.Format,                                        
                    Edition = result.Edition,
                    TableOfContents = result.TableOfContents,
                    Price = result.Price,
                    PublishedAt = result.PublishedAt,
                    AuthorId = result.AuthorId,   
                    PublisherId = result.PublisherId,
                    GenreId = result.GenreId,
                    CategoryId = result.CategoryId,
                    DeletedAt = result.DeletedAt,
                    CreatedAt = result.CreatedAt,
                    UpdatedAt = result.UpdatedAt,   
                    IsActive = result.IsActive
                }
                : null;
        }
        catch (Exception e)
        {
            _logger.LogError( "Error occured while getting book by id",e);
            throw;
        }
    }

    public async Task<BookDto?> GetByTitleAsync(string? title,bool includeDeleted=false)
    {
        try
        {
           if(string.IsNullOrEmpty(title))
           {
               return null;
           }
           
           var book = await _context.Books.FirstOrDefaultAsync(x => x.Title == title);
           
           return book != null
               ? new BookDto()
               {
                   Id = book.Id,
                   Title = book.Title,
                   Subtitle = book.Subtitle,
                   Isbn = book.Isbn,
                   Description = book.Description,
                   Author = book.Author,
                   Publisher = book.Publisher,
                   Language = book.Language,
                   Series = book.Series,
                   NumberOfPages = book.NumberOfPages,
                   Dimensions = book.Dimensions,
                   Format = book.Format,
                   Edition = book.Edition,
                   TableOfContents = book.TableOfContents,
                   Price = book.Price,
                   PublishedAt = book.PublishedAt,
                   AuthorId = book.AuthorId,   
                   PublisherId = book.PublisherId,
                   GenreId = book.GenreId,
                   CategoryId = book.CategoryId,
                   DeletedAt = book.DeletedAt,
                   CreatedAt = book.CreatedAt,
                   UpdatedAt = book.UpdatedAt,
                   IsActive = book.IsActive
               }
               : null;
        }
        catch (Exception e)
        {
            _logger.LogError( "Error occured while getting book by title",e);
            throw;
        }
    }

    public async Task<BookDto?> GetByAuthorIdAsync(Guid authorId,bool includeDeleted=false)
    {
        try
        {
            if(authorId==Guid.Empty)
            {
                return null;
            }
            
            var book = await _context.Books.FirstOrDefaultAsync(x => x.AuthorId == authorId);

            return book != null
                ? new BookDto()
                {
                    Id = book.Id,
                    Title = book.Title,
                    Subtitle = book.Subtitle,
                    Isbn = book.Isbn,
                    Description = book.Description,
                    Author = book.Author,
                    Publisher = book.Publisher,
                    Language = book.Language,
                    Series = book.Series,
                    NumberOfPages = book.NumberOfPages,
                    Dimensions = book.Dimensions,
                    Format = book.Format,
                    Edition = book.Edition,
                    TableOfContents = book.TableOfContents,
                    Price = book.Price,
                    PublishedAt = book.PublishedAt,
                    AuthorId = book.AuthorId,
                    PublisherId = book.PublisherId,
                    GenreId = book.GenreId,
                    CategoryId = book.CategoryId,
                    DeletedAt = book.DeletedAt,
                    CreatedAt = book.CreatedAt,
                    UpdatedAt = book.UpdatedAt,
                    IsActive = book.IsActive
                }
                : null;
        }
        catch (Exception e)
        {
            _logger.LogError( "Error occured while getting book by author id",e);
            throw;
        }
    }

    public async Task<BookDto?> GetByPublisherIdAsync(Guid publisherId,bool includeDeleted=false)
    { 
        try
        {
            if(publisherId==Guid.Empty)
            {
                return null;
            }
            
            var book = await _context.Books.FirstOrDefaultAsync(x => x.PublisherId == publisherId);
            
            return book != null
                ? new BookDto()
                {
                    Id = book.Id,
                    Title = book.Title,
                    Subtitle = book.Subtitle,
                    Isbn = book.Isbn,
                    Description = book.Description,
                    Author = book.Author,
                    Publisher = book.Publisher,
                    Language = book.Language,
                    Series = book.Series,
                    NumberOfPages = book.NumberOfPages,
                    Dimensions = book.Dimensions,
                    Format = book.Format,
                    Edition = book.Edition,
                    TableOfContents = book.TableOfContents,
                    Price = book.Price,
                    PublishedAt = book.PublishedAt,
                    AuthorId = book.AuthorId,   
                    PublisherId = book.PublisherId,
                    GenreId = book.GenreId,
                    CategoryId = book.CategoryId,
                    DeletedAt = book.DeletedAt,
                    CreatedAt = book.CreatedAt,
                    UpdatedAt = book.UpdatedAt,
                    IsActive = book.IsActive
                }   
                : null; 
        }
        catch (Exception e)
        {
            _logger.LogError("Error occured while getting book by publisher id",e);
            throw;
        }
    }

    public async Task<BookDto?> GetByGenreIdAsync(Guid genreId,bool includeDeleted=false)
    {
        try
        {
            var book = await _context.Books.FirstOrDefaultAsync(x => x.GenreId == genreId);
            return book != null
                ? new BookDto()
                {
                    Id = book.Id,
                    Title = book.Title,
                    Subtitle = book.Subtitle,
                    Isbn = book.Isbn,
                    Description = book.Description,
                    Author = book.Author,
                    Publisher = book.Publisher,
                    Language = book.Language,
                    Series = book.Series,
                    NumberOfPages = book.NumberOfPages,
                    Dimensions = book.Dimensions,
                    Format = book.Format,
                    Edition = book.Edition,
                    TableOfContents = book.TableOfContents,
                    Price = book.Price,
                    PublishedAt = book.PublishedAt,
                    AuthorId = book.AuthorId,   
                    PublisherId = book.PublisherId,
                    GenreId = book.GenreId,
                    CategoryId = book.CategoryId,
                    DeletedAt = book.DeletedAt,
                    CreatedAt = book.CreatedAt,
                    UpdatedAt = book.UpdatedAt,
                    IsActive = book.IsActive
                }
                : null;
        }
        catch (Exception e)
        {
            _logger.LogError( "Error occured while getting book by genre id",e);
            throw;
        }
    }

    public async Task<BookDto?> GetByCategoryIdAsync(Guid categoryId,bool includeDeleted=false)
    {
        try
        {
            if(categoryId==Guid.Empty)
            {
                return null;
            }
            
            var book = await _context.Books.FirstOrDefaultAsync(x => x.CategoryId == categoryId);
            
            return book != null
                ? new BookDto()
                {
                    Id = book.Id,
                    Title = book.Title,
                    Subtitle = book.Subtitle,
                    Isbn = book.Isbn,
                    Description = book.Description,
                    Author = book.Author,
                    Publisher = book.Publisher,
                    Language = book.Language,
                    Series = book.Series,
                    NumberOfPages = book.NumberOfPages,
                    Dimensions = book.Dimensions,
                    Format = book.Format,
                    Edition = book.Edition,
                    TableOfContents = book.TableOfContents,
                    Price = book.Price,
                    PublishedAt = book.PublishedAt,
                    AuthorId = book.AuthorId,   
                    PublisherId = book.PublisherId,
                    GenreId = book.GenreId,
                    CategoryId = book.CategoryId,
                    DeletedAt = book.DeletedAt,
                    CreatedAt = book.CreatedAt,
                    UpdatedAt = book.UpdatedAt,
                    IsActive = book.IsActive
                }
                : null;
        }
        catch (Exception e)
        {
            _logger.LogError( "Error occured while getting book by category id",e);
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
            _logger.LogError("Error occured while getting book title by id",e);
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
            _logger.LogError( "Error occured while getting book author id by id",e);
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
            _logger.LogError("Error occured while getting book publisher id by id",e);
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
            _logger.LogError( "Error occured while getting book genre id by id",e);
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
            _logger.LogError( "Error occured while getting book category id by id",e);
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
            _logger.LogError("Error occured while setting book title",e);
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
            _logger.LogError( "Error occured while setting book author id",e);
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
            _logger.LogError( "Error occured while setting book publisher id",e);
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
            _logger.LogError("Error occured while setting book genre id",e);
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
            _logger.LogError("Error occured while setting book category id",e);
            throw;
        }
    }
}