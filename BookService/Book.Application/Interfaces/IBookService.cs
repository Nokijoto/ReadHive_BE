using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Book.Application.Models.Results;
using Book.Application.Models.Dto;

namespace Book.Application.Interfaces;

public interface IBookService 
{
    Task<bool> DeleteBookAsync(Guid id);
    Task<ResultBase<BookDto?>> GetBookAsync(Guid id);
    Task<ResultBase<IEnumerable<BookDto?>>> GetBooksAsync();
    Task<ResultBase<BookDto?>> UpdateBookAsync(BookDto? bookDto);
    
    Task<bool> AddBookAsync(BookDto? bookDto);

}