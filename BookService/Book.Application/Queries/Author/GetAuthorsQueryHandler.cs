using Application.Interfaces;
using Application.Models.Dto;
using Application.Models.Results;
using Book.Infrastructure.Interfaces;
using MediatR;

namespace Application.Queries.AuthorQueries;

public class GetAuthorsQueryHandler : IRequestHandler<GetAuthorsQuery, GetAuthorsQueryResult>
{
    private readonly IAuthorService _authorService;
    private readonly ILoggingService _logger;

    public GetAuthorsQueryHandler(IAuthorService authorService, ILoggingService logger)
    {
        _authorService = authorService;
        _logger = logger;
    }
    
    public async Task<GetAuthorsQueryResult> Handle(GetAuthorsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var authors = await _authorService.GetAuthorsAsync();
            var result = new GetAuthorsQueryResult(true, authors, new List<string>());
            if (authors == null)
            {
                result.Errors = new List<string> { "Author not found" };
            }
            return result;
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message, e);
            throw;
        }
    }   
}