using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Book.Application.Interfaces;
using Book.Application.Models.Dto;
using Book.Application.Models.Results;
using Book.Domain.Interfaces;
using ProjectBase.Interfaces;

namespace Book.Application.Services;

public class BookService : IBookService
{
    private readonly ILoggingService _log;
    private readonly IBookRepository _bookRepository;

    public BookService(ILoggingService log, IBookRepository bookRepository)
    {
        _log = log;
        _bookRepository = bookRepository;
    }

    public async Task<bool> AddBookAsync(BookDto bookDto)
    {
        try
        {
            
            await _bookRepository.AddAsync(bookDto);
            return true;
        }
        catch (Exception e)
        {
            _log.LogError(e.Message, e);
            throw;
        }
    }

    public async Task<bool> DeleteBookAsync(Guid id)
    {
        try
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Book id cannot be empty");
            }
            var book = await _bookRepository.GetByIdAsync(id);
            if (book != null)
            {
                await _bookRepository.DeleteAsync(book.Id);
                return true;
            }
            return false;   
        }
        catch (Exception e)
        {
            _log.LogError(e.Message, e);
            throw;
        }
    }

    public async Task<ResultBase<BookDto?>> GetBookAsync(Guid id)
    {
        try
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Book id cannot be empty");
            }
            var book = await _bookRepository.GetByIdAsync(id);
            if (book!= null)
            {
                return new ResultBase<BookDto?>(true, book);
            }
            return null;
        }
        catch (Exception e)
        {
            _log.LogError(e.Message, e);
            throw;
        }
    }

    public async Task<ResultBase<IEnumerable<BookDto?>>> GetBooksAsync()
    {
        try
        {
            var books = await _bookRepository.GetAllAsync();
            return new ResultBase<IEnumerable<BookDto?>>(true, books);
        }
        catch (Exception e)
        {
            _log.LogError(e.Message, e);
            throw;
        }
    }

    public async Task<ResultBase<BookDto?>> UpdateBookAsync(BookDto bookDto)
    {
        try
        {
            if (bookDto.Id == Guid.Empty)
            {
                throw new ArgumentException("Book id cannot be empty");
            }
            var book = await _bookRepository.GetByIdAsync(bookDto.Id);
            if (book != null)
            {
                await _bookRepository.UpdateAsync(book);
                return new ResultBase<BookDto?>(true, book);
            }
            return null;
        }
        catch (Exception e)
        {
            _log.LogError(e.Message, e);
            throw;
        }
    }

}