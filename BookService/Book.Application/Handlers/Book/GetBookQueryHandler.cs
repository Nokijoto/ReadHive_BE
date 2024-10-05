using Application.Interfaces;
using Application.Models.Dto;
using Application.Models.Results;
using Application.Queries.Book;
using Book.Infrastructure.Interfaces;
using MediatR;

namespace Application.Handlers.Book;

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
            // var result = await _bookService.GetBookAsync(request.Id);
            // if (result.Succeeded)
            // {
            //     return new ResultBase<BookDto?>(true, result.Data);
            // }
            return new ResultBase<BookDto?>(false, null, new List<string> { "Book not found" });
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message, e);
            throw;
        }
    }
}