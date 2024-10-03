using Application.Models.Dto;
using Domain.Entities;

namespace Application.Interfaces;

public interface IAuthorService
{
    Task<bool> DeleteAuthorAsync(Guid id);
    Task<AuthorDto?> GetAuthorAsync(Guid id);
    Task<IEnumerable<AuthorDto?>> GetAuthorsAsync();
    Task<AuthorDto?> UpdateAuthorAsync(AuthorDto authorDto);
    Task<bool> AddAuthorAsync(AuthorDto authorDto);
}