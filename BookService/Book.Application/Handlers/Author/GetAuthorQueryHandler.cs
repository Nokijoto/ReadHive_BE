using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Book.Application.Interfaces;
using Book.Application.Models.Dto;
using Book.Application.Models.Results;
using Book.Application.Queries.AuthorQueries;

using MediatR;
using ProjectBase.Interfaces;

namespace Book.Application.Handlers.Author;

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
            return new ResultBase<AuthorDto?>(true, author.Data);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message, e);
            throw;
        }
        
    }
}