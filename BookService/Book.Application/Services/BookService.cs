using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Models.Dto;
using Domain.Entities;
using Domain.Interfaces;
using Book.Infrastructure.Interfaces;

namespace Application.Services;

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
            var book = new Domain.Entities.Book()
            {
                Title = bookDto.Title,
                Subtitle = bookDto.Subtitle,
                Isbn = bookDto.Isbn,
                Description = bookDto.Description,
                AuthorId = bookDto.AuthorId,
                PublisherId = bookDto.PublisherId,
                GenreId = bookDto.GenreId,
                CategoryId = bookDto.CategoryId,
                Author = bookDto.Author,
                Publisher = bookDto.Publisher,
                Language = bookDto.Language,
                Series = bookDto.Series,
                NumberOfPages = bookDto.NumberOfPages,
                Dimensions = bookDto.Dimensions,
                Format = bookDto.Format,
                Edition = bookDto.Edition,
                TableOfContents = bookDto.TableOfContents,
                Price = bookDto.Price,
                PublishedAt = bookDto.PublishedAt,
            };      
            
            await _bookRepository.AddAsync(book);
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

    public async Task<BookDto?> GetBookAsync(Guid id)
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
                var bookDto = new BookDto()
                {
                    Id = book.Id,
                    Title = book.Title,
                    Subtitle = book.Subtitle,
                    Isbn = book.Isbn,
                    Description = book.Description,
                    AuthorId = book.AuthorId,
                    PublisherId = book.PublisherId,
                    GenreId = book.GenreId,
                    CategoryId = book.CategoryId,
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
                };
                return bookDto;
            }
            return null;
        }
        catch (Exception e)
        {
            _log.LogError(e.Message, e);
            throw;
        }
    }

    public async Task<IEnumerable<BookDto?>> GetBooksAsync()
    {
        try
        {
            var books = await _bookRepository.GetAllAsync();
            return new List<BookDto?>(books.Select(book => book != null ? new BookDto() : new BookDto()));
        }
        catch (Exception e)
        {
            _log.LogError(e.Message, e);
            throw;
        }
    }

    public async Task<BookDto?> UpdateBookAsync(BookDto bookDto)
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
                book.Title = bookDto.Title;
                book.Subtitle = bookDto.Subtitle;
                book.Isbn = bookDto.Isbn;
                book.Description = bookDto.Description;
                book.AuthorId = bookDto.AuthorId;
                book.PublisherId = bookDto.PublisherId;
                book.GenreId = bookDto.GenreId;
                book.CategoryId = bookDto.CategoryId;
                book.Author = bookDto.Author;
                book.Publisher = bookDto.Publisher;
                book.Language = bookDto.Language;
                book.Series = bookDto.Series;
                book.NumberOfPages = bookDto.NumberOfPages;
                book.Dimensions = bookDto.Dimensions;
                book.Format = bookDto.Format;
                book.Edition = bookDto.Edition;
                book.TableOfContents = bookDto.TableOfContents;
                book.Price = bookDto.Price;
                book.PublishedAt = bookDto.PublishedAt;
                await _bookRepository.UpdateAsync(book);
                return bookDto;
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