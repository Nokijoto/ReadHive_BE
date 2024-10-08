using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Book.Application.Commands.Author;
using Book.Application.Interfaces;
using Book.Application.Models.Results;
using MediatR;

namespace Book.Application.Handlers.Author;

public class DeleteAuthorCommandHandler : IRequestHandler<DeleteAuthorCommand, ResultBase<bool>>
{
    
    private readonly IAuthorService _authorService;
    public DeleteAuthorCommandHandler(IAuthorService authorService)
    {
        _authorService = authorService;
    }   
    public async Task<ResultBase<bool>> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _authorService.DeleteAuthorAsync(request.Id);
            if(result)
            {
                return new ResultBase<bool>( result, result);
            }
            return new ResultBase<bool>(false, false, new List<string>() { "Author not found" });
        }
        catch (Exception e)
        {
            return new ResultBase<bool>(false, false, new List<string>() { e.Message });
        }
    }   
}