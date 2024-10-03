using Application.Interfaces;
using Application.Models.Dto;
using Application.Models.Results;
using Book.Infrastructure.Interfaces;
using MediatR;

namespace Application.Queries.AuthorQueries;

public class GetAuthorQueryHandler : IRequestHandler<GetAuthorQuery, GetAuthorQueryResult>
{
    private readonly IAuthorService _authorService;
    private readonly ILoggingService _logger;

    public GetAuthorQueryHandler(IAuthorService authorService, ILoggingService logger)
    {
        _authorService = authorService;
        _logger = logger;
    }
    public async Task<GetAuthorQueryResult> Handle(GetAuthorQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var author = await _authorService.GetAuthorAsync(request.Id);
            if (author == null)
            {
                return new GetAuthorQueryResult(false, null, new List<string> { "Author not found" });
            }
            return new GetAuthorQueryResult(true, author);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message, e);
            throw;
        }
        
    }
}