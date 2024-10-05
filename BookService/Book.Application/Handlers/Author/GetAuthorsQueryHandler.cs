using Application.Interfaces;
using Application.Models.Dto;
using Application.Models.Results;
using Application.Queries.AuthorQueries;
using Book.Infrastructure.Interfaces;
using MediatR;

namespace Application.Handlers.Author;

public class GetAuthorsQueryHandler : IRequestHandler<GetAuthorsQuery, ResultBase<IEnumerable<AuthorDto?>>>
{
    private readonly IAuthorService _authorService;
    private readonly ILoggingService _logger;

    public GetAuthorsQueryHandler(IAuthorService authorService, ILoggingService logger)
    {
        _authorService = authorService;
        _logger = logger;
    }
    
    public async Task<ResultBase<IEnumerable<AuthorDto?>>> Handle(GetAuthorsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var authors = await _authorService.GetAuthorsAsync();
            var result = new ResultBase<IEnumerable<AuthorDto?>>(true, authors);
            return result;
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message, e);
            throw;
        }
    }   
}