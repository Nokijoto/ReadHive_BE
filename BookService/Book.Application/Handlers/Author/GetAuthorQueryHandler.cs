using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Models.Dto;
using Application.Models.Results;
using Application.Queries.AuthorQueries;
using Book.Infrastructure.Interfaces;
using MediatR;

namespace Application.Handlers.Author;

public class GetAuthorQueryHandler : IRequestHandler<GetAuthorQuery, ResultBase<AuthorDto?>>
{
    private readonly IAuthorService _authorService;
    private readonly ILoggingService _logger;

    public GetAuthorQueryHandler(IAuthorService authorService, ILoggingService logger)
    {
        _authorService = authorService;
        _logger = logger;
    }
    public async Task<ResultBase<AuthorDto?>> Handle(GetAuthorQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var author = await _authorService.GetAuthorAsync(request.Id);
            if (author == null)
            {
                return new ResultBase<AuthorDto?>(false, null, new List<string> { "Author not found" });
            }
            return new ResultBase<AuthorDto?>(true, author);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message, e);
            throw;
        }
        
    }
}