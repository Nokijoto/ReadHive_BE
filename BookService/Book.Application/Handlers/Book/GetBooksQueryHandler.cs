using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Book.Application.Interfaces;
using Book.Application.Models.Dto;
using Book.Application.Models.Results;
using Book.Application.Queries.Book;
using MediatR;
using ProjectBase.Interfaces;

namespace  Book.Application.Handlers.Book;

public class GetBooksQueryHandler:IRequestHandler<GetBooksQuery, ResultBase<IEnumerable<BookDto?>>>
{
    private readonly IBookService _bookService;
    private readonly ILoggingService _logger;

    public GetBooksQueryHandler(IBookService bookService, ILoggingService logger)
    {
        _bookService = bookService;
        _logger = logger;
    }
    
    public async Task<ResultBase<IEnumerable<BookDto?>>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var books = await _bookService.GetBooksAsync();
            var result = new ResultBase<IEnumerable<BookDto?>>(true, books.Data);
            return result;
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message, e);
            throw;
        }
    }   
}