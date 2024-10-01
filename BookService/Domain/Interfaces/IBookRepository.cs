using Domain.Entities;

namespace Domain.Interfaces;

public interface IBookRepository
{
    Task<bool> DeleteAsync(Guid id);
    Task<bool> AddAsync(Book book);
    Task<bool> UpdateAsync(Book book);
    
    Task<Book?> GetByIdAsync(Guid id);
    Task<Book?> GetByTitleAsync(string title);
    Task<Book?> GetByAuthorIdAsync(Guid authorId);
    Task<Book?> GetByPublisherIdAsync(Guid publisherId);
    Task<Book?> GetByGenreIdAsync(Guid genreId);
    Task<Book?> GetByCategoryIdAsync(Guid categoryId);
    
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