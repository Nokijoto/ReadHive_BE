using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Book.Application.Commands.Book;
using Book.Application.Interfaces;
using Book.Application.Models.Dto;
using Book.Application.Models.Results;
using MediatR;
using ProjectBase.Interfaces;

namespace Application.Handlers.Book;

public class AddBookCommandHandler:IRequestHandler<AddBookCommand, ResultBase<BookDto?>>
{
    private readonly IBookService _bookService;
    private readonly ILoggingService _logger;

    public AddBookCommandHandler(IBookService bookService, ILoggingService logger)
    {
        _bookService = bookService;
        _logger = logger;
    }

    public async Task<ResultBase<BookDto?>> Handle(AddBookCommand request, CancellationToken cancellationToken)
    {
        if (request.BookDto == null)
        {
            return new ResultBase<BookDto?>(false, null, new List<string> { "BookDto is null" });
        }

        try
        {
            var result = await _bookService.AddBookAsync(request.BookDto);

            if (result)
            {
                return new ResultBase<BookDto?>(true, request.BookDto);
            }
            return new ResultBase<BookDto?>(result, null, new List<string> { "Book already exists" });
        }
        catch (Exception e)
        {
            _logger.LogError($"Error adding book: {request.BookDto?.Isbn }. Exception: {e.Message}", e);

            return new ResultBase<BookDto?>(false, null, new List<string> { "An error occurred while adding the book." });
        }
    }
}