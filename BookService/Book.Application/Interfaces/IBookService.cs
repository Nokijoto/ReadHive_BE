using Application.Models.Dto;

namespace Application.Interfaces;

public interface IBookService 
{
    Task<bool> DeleteBookAsync(Guid id);
    Task<BookDto?> GetBookAsync(Guid id);
    Task<IEnumerable<BookDto?>> GetBooksAsync();
    Task<BookDto?> UpdateBookAsync(BookDto bookDto);
    
    Task<bool> AddBookAsync(BookDto bookDto);

}