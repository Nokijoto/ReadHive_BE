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

public class GetBookQueryHandler : IRequestHandler<GetBookQuery, ResultBase<BookDto?>>
{
    private readonly IBookService _bookService;
    private readonly ILoggingService _logger;

    public GetBookQueryHandler(IBookService bookService, ILoggingService logger)
    {
        _bookService = bookService;
        _logger = logger;
    }
    public async Task<ResultBase<BookDto?>> Handle(GetBookQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var book = await _bookService.GetBookAsync(request.Id);
            if (book != null)
            {
                return new ResultBase<BookDto?>(true, book.Data);
            }
            return new ResultBase<BookDto?>(false, null, new List<string> { "Book not found" });
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message, e);
            throw;
        }
    }
}