using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Exceptions;
using Application.Interfaces;
using Application.Models.Dto;
using Application.Models.Results;
using Book.Infrastructure.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class AuthorService : IAuthorService
{
    private readonly ILoggingService _log;
    private readonly IAuthorRepository _authorRepository;

    public AuthorService(IAuthorRepository authorRepository, ILoggingService log)
    {
        _authorRepository = authorRepository;
        _log = log;
    }

    public async Task<bool> DeleteAuthorAsync(Guid id)
    {
        try
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Author id cannot be empty");
            }
            var author = await _authorRepository.GetByIdAsync(id);
            if (author != null)
            {
                await _authorRepository.DeleteAsync(author.Id);
                return true;
            }   
            return false;
        }
        catch (Exception e)
        {
            _log.LogError(e.Message, e);
            throw;
        }
    }

    public async Task<AuthorDto?> GetAuthorAsync(Guid id)
    {
        try
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Author id cannot be empty");
            }
            var author = await _authorRepository.GetByIdAsync(id);
            if (author != null)
            {
                var authorDto = new AuthorDto()
                {
                    Id = author.Id,
                    FirstName = author.FirstName,
                    LastName = author.LastName,
                    Nationality = author.Nationality,
                    DeletedAt = author.DeletedAt,
                    CreatedAt = author.CreatedAt,
                    UpdatedAt = author.UpdatedAt,
                };
                return authorDto;
            }
            return null;    
        }
        catch (Exception e)
        {
            _log.LogError(e.Message, e);
            throw;
        }
    }

    public async Task<IEnumerable<AuthorDto?>> GetAuthorsAsync()
    {
        try
        {
            var authors = await _authorRepository.GetAllAsync();
            return new List<AuthorDto?>(authors.Select(author => author != null ?
                new AuthorDto() 
                {
                    Id = author.Id,
                    FirstName = author.FirstName,
                    LastName = author.LastName,
                    Nationality = author.Nationality,
                    DeletedAt = author.DeletedAt,
                    CreatedAt = author.CreatedAt,
                    UpdatedAt = author.UpdatedAt,
                    PictureUrl = author.PictureUrl,
                    BirthDate = author.BirthDate,
                    DeathDate = author.DeathDate,
                    Bio = author.Bio,
                    IsActive = author.IsActive,
                }
                : new AuthorDto()));
        }
        catch (Exception e)
        {
            _log.LogError(e.Message, e);
            throw;
        }
    }

    public async Task<AuthorDto?> UpdateAuthorAsync(AuthorDto authorDto)
    {
        try
        {
            if (authorDto.Id == Guid.Empty)
            {
                throw new ArgumentException("Author id cannot be empty");
            }
            var author = await _authorRepository.GetByIdAsync(authorDto.Id);
            if (author != null)
            {
                author.FirstName = authorDto.FirstName;
                author.LastName = authorDto.LastName;
                author.Bio = authorDto.Bio; 
                author.PictureUrl = authorDto.PictureUrl;
                author.BirthDate = authorDto.BirthDate;
                author.DeathDate = authorDto.DeathDate;
                author.Nationality = authorDto.Nationality;
                await _authorRepository.UpdateAsync(author);
                return authorDto;
            }       
            return null;
        }
        catch (Exception e)
        {
            _log.LogError(e.Message, e);
            throw;
        }
    }

    public async Task<ResultBase<AuthorDto?>> AddAuthorAsync(AuthorDto authorDto)
    {
        try
        {
            if (await AuthorExistsAsync(authorDto))
            {
                throw new ObjectAlreadyExistException("Author already exists");
            }
            var author = new Author()
            {
                FirstName = authorDto.FirstName,
                LastName = authorDto.LastName,
                Bio = authorDto.Bio,
                Nationality = authorDto.Nationality,
                PictureUrl = authorDto.PictureUrl,
                BirthDate = authorDto.BirthDate,
                DeathDate = authorDto.DeathDate,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                DeletedAt = null,
                IsActive = true,
            };
            await _authorRepository.AddAsync(author);
            return new ResultBase<AuthorDto?>(true, authorDto);
        }
        catch (ObjectAlreadyExistException e)
        {
            return new ResultBase<AuthorDto?>(false, null, new List<string>() { e.Message });
        }
        catch (Exception e)
        {
            _log.LogError(e.Message, e);
            return new ResultBase<AuthorDto?>(false, null, new List<string>() { e.Message });
        }
    }

    public async Task<bool> AuthorExistsAsync(AuthorDto authorName)
    {
        try
        {
            var author = await _authorRepository.GetByFirstNameAsync(authorName.FirstName);
            var secondAuthor = await _authorRepository.GetByLastNameAsync(authorName.LastName);
            return author != null || secondAuthor != null;
        }
        catch (Exception e)
        {
            _log.LogError(e.Message, e);
            return false;
        }
    }
}