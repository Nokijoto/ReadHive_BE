using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Book.Application.Models.Dto;
using Book.Application.Models.Results;

namespace Book.Application.Interfaces;

public interface IAuthorService
{
    Task<bool> DeleteAuthorAsync(Guid id);
    Task<ResultBase<AuthorDto?>?> GetAuthorAsync(Guid id);
    Task<ResultBase<IEnumerable<AuthorDto?>>> GetAuthorsAsync();
    Task<ResultBase<AuthorDto?>> UpdateAuthorAsync(AuthorDto? authorDto);
    Task<ResultBase<AuthorDto?>> AddAuthorAsync(AuthorDto? authorDto);
    Task<bool> AuthorExistsAsync(AuthorDto? authorName);
}