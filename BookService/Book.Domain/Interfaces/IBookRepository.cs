
namespace Book.Domain.Interfaces;

public interface IBookRepository
{
    Task<bool> DeleteAsync(Guid id);
    Task<bool> AddAsync(Entities.Book BookDto);
    Task<bool> UpdateAsync(Entities.Book BookDto);
    Task<IEnumerable<Entities.Book>> GetAllAsync(bool includeDeleted=false);

    Task<Entities.Book?> GetByIdAsync(Guid? id, bool includeDeleted = false);
    Task<Entities.Book?> GetByTitleAsync(string title,bool includeDeleted=false);
    Task<Entities.Book?> GetByAuthorIdAsync(Guid authorId,bool includeDeleted=false);
    Task<Entities.Book?> GetByPublisherIdAsync(Guid publisherId,bool includeDeleted=false);
    Task<Entities.Book?> GetByGenreIdAsync(Guid genreId,bool includeDeleted=false);
    Task<Entities.Book?> GetByCategoryIdAsync(Guid categoryId,bool includeDeleted=false);
    
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