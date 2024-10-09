using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Book.Application.Commands.Author;
using Book.Application.Commands.Book;
using Book.Application.Interfaces;
using Book.Application.Models.Results;
using MediatR;

namespace  Book.Application.Handlers.Book;

public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, ResultBase<bool>>
{
    
    private readonly IBookService _service;
    public DeleteBookCommandHandler(IBookService service)
    {
        _service = service;
    }   
    public async Task<ResultBase<bool>> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _service.DeleteBookAsync(request.Id);
            if(result)
            {
                return new ResultBase<bool>( result, result);
            }
            return new ResultBase<bool>(false, false, new List<string>() { "Book not found" });
        }
        catch (Exception e)
        {
            return new ResultBase<bool>(false, false, new List<string>() { e.Message });
        }
    }   
}