using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Book.Application.Commands.Book;
using Book.Application.Interfaces;
using Book.Application.Models.Dto;
using Book.Application.Models.Results;
using MediatR;

namespace Book.Application.Handlers.Book;

public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, ResultBase<BookDto?>>
{
    private readonly IBookService _service;
    public UpdateBookCommandHandler(IBookService service)
    {
        _service = service;
    }
    public async Task<ResultBase<BookDto?>> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _service.UpdateBookAsync(request.BookDto);
            return new ResultBase<BookDto?>(true, result.Data);
        }
        catch (Exception e)
        {
            return new ResultBase<BookDto?>(false, null, new List<string>() { e.Message });
        }
    }       
}