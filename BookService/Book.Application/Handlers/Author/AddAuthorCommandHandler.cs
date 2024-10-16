﻿using System;
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

public class AddAuthorCommandHandler : IRequestHandler<AddAuthorCommand, ResultBase<AuthorDto?>>
{
    
    private readonly IAuthorService _authorService;
    private readonly ILoggingService _loggingService;
    public AddAuthorCommandHandler(IAuthorService authorService, ILoggingService loggingService)
    {
        _authorService = authorService;
        _loggingService = loggingService;
    }
    
    public async Task<ResultBase<AuthorDto?>> Handle(AddAuthorCommand request, CancellationToken cancellationToken)
    {
        if(request.AuthorDto == null)
        {
            return new ResultBase<AuthorDto?>(false, null, new List<string> { "AuthorDto is null" });
        }
        try
        {
            var result = await _authorService.AddAuthorAsync(request.AuthorDto);
            
            if (result.Succeeded)
            {
                return new ResultBase<AuthorDto?>(result.Succeeded, result.Data);
            }
            
            return new ResultBase<AuthorDto?>(result.Succeeded, null, new List<string> { "Author already exists" });
        }
        catch (Exception e)
        {
            _loggingService.LogError($"Error adding author: {request.AuthorDto?.FirstName +" "+ request.AuthorDto?.LastName}. Exception: {e.Message}", e);

            return new ResultBase<AuthorDto?>(false, null, new List<string> { "An error occurred while adding the author." });
        }
    }
}