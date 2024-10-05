using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Models.Dto;
using Application.Models.Results;
using Domain.Entities;

namespace Application.Interfaces;

public interface IAuthorService
{
    Task<bool> DeleteAuthorAsync(Guid id);
    Task<AuthorDto?> GetAuthorAsync(Guid id);
    Task<IEnumerable<AuthorDto?>> GetAuthorsAsync();
    Task<AuthorDto?> UpdateAuthorAsync(AuthorDto authorDto);
    Task<ResultBase<AuthorDto?>> AddAuthorAsync(AuthorDto authorDto);
    Task<bool> AuthorExistsAsync(AuthorDto authorName);
}