using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Book.Application.Commands.Author;
using Book.Application.Interfaces;
using Book.Application.Models.Dto;
using Book.Application.Models.Results;
using MediatR;
using ProjectBase.Interfaces;

namespace Book.Application.Handlers.Author;

public class UpdateAuthorCommandHandler : IRequestHandler<UpdateAuthorCommand, ResultBase<AuthorDto?>>
{
        
    private readonly IAuthorService _authorService;
    private readonly ILoggingService _logger;
    public UpdateAuthorCommandHandler(IAuthorService authorService, ILoggingService logger)
    {
        _authorService = authorService;
        _logger = logger;
    }
    public async Task<ResultBase<AuthorDto?>> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
    {
        try
        {   
            request.AuthorDto.Id = request.Id;
            _logger.LogInformation($"Updating author with id {request.Id}");
            var result = await _authorService.UpdateAuthorAsync(request.AuthorDto);
            return new ResultBase<AuthorDto?>(true, result.Data);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message, e);
            return new ResultBase<AuthorDto?>(false, null, new List<string>() { e.Message });
        }
    }
}