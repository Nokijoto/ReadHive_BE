using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Book.Application.Exceptions;
using Book.Application.Interfaces;
using Book.Application.Models.Dto;
using Book.Application.Models.Results;
using Book.Domain.Interfaces;
using ProjectBase.Interfaces;


namespace Book.Application.Services;

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

    public async Task<ResultBase<AuthorDto?>> GetAuthorAsync(Guid id)
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
                return new ResultBase<AuthorDto?>(true, author);
            }
            return null;    
        }
        catch (Exception e)
        {
            _log.LogError(e.Message, e);
            throw;
        }
    }

    public async Task<ResultBase<IEnumerable<AuthorDto?>>> GetAuthorsAsync()
    {
        try
        {
            var authors = await _authorRepository.GetAllAsync();
            return new ResultBase<IEnumerable<AuthorDto?>>(true, authors);
        }
        catch (Exception e)
        {
            _log.LogError(e.Message, e);
            throw;
        }
    }

    public async Task<ResultBase<AuthorDto?>> UpdateAuthorAsync(AuthorDto authorDto)
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
                return new ResultBase<AuthorDto?>(true, author);
            }       
            return null;
        }
        catch (Exception e)
        {
            _log.LogError(e.Message, e);
            throw;
        }
    }

    public async Task<ResultBase<AuthorDto?>> AddAuthorAsync(AuthorDto? request)
    {
        try
        {
            if (await AuthorExistsAsync(request))
            {
                throw new ObjectAlreadyExistException("Author already exists");
            }
            var author = new AuthorDto()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Bio = request.Bio,
                Nationality = request.Nationality,
                PictureUrl = request.PictureUrl,
                BirthDate = request.BirthDate,
                DeathDate = request.DeathDate,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                DeletedAt = null,
                IsActive = true,
            };
            await _authorRepository.AddAsync(author);
            return new ResultBase<AuthorDto?>(true, request);
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