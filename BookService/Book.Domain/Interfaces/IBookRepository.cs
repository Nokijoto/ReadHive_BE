using Book.Application.Models.Dto;
using Book.Domain.Entities;

namespace Book.Domain.Interfaces;

public interface IBookRepository
{
    Task<bool> DeleteAsync(Guid id);
    Task<bool> AddAsync(BookDto BookDto);
    Task<bool> UpdateAsync(BookDto BookDto);
    Task<IEnumerable<BookDto>> GetAllAsync(bool includeDeleted=false);

    Task<BookDto?> GetByIdAsync(Guid id,bool includeDeleted=false);
    Task<BookDto?> GetByTitleAsync(string title,bool includeDeleted=false);
    Task<BookDto?> GetByAuthorIdAsync(Guid authorId,bool includeDeleted=false);
    Task<BookDto?> GetByPublisherIdAsync(Guid publisherId,bool includeDeleted=false);
    Task<BookDto?> GetByGenreIdAsync(Guid genreId,bool includeDeleted=false);
    Task<BookDto?> GetByCategoryIdAsync(Guid categoryId,bool includeDeleted=false);
    
    Task<string?> GetTitleAsync(Guid id);
    Task<Guid?> GetAuthorIdAsync(Guid id);
    Task<Guid?> GetPublisherIdAsync(Guid id);
    Task<Guid?> GetGenreIdAsync(Guid id);
    Task<Guid?> GetCategoryIdAsync(Guid id);
    
    Task<bool> SetTitleAsync(Guid id, string title);
    Task<bool> SetAuthorIdAsync(Guid id, Guid authorId);
    Task<bool> SetPublisherIdAsync(Guid id, Guid publisherId);
    Task<bool> SetGenreIdAsync(Guid id, Guid genreId);
    Task<bool> SetCategoryIdAsync(Guid id, Guid categoryId);    
    
}