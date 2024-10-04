using Application.Commands.Author;
using Application.Interfaces;
using Application.Models.Results;
using MediatR;

namespace Application.Handlers.Author;

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
                return new ResultBase<bool>( result, result, null);
            }
            return new ResultBase<bool>(false, false, new List<string>() { "Author not found" });
        }
        catch (Exception e)
        {
            return new ResultBase<bool>(false, false, new List<string>() { e.Message });
        }
    }   
}