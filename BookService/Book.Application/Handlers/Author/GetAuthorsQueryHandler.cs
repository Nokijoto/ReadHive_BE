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
            var result = new ResultBase<IEnumerable<AuthorDto?>>(true, authors.Data);
            return result;
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message, e);
            throw;
        }
    }   
}