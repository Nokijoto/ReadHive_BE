using Application.Interfaces;
using Application.Models.Dto;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Interfaces;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class AuthorService : IAuthorService
{
    private readonly ILogger _log;
    private readonly IAuthorRepository _authorRepository;

    public AuthorService(IAuthorRepository authorRepository, ILogger log)
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

    public async Task<IEnumerable<AuthorDto?>> GetAuthorAsync()
    {
        try
        {
            var authors = await _authorRepository.GetAllAsync();
            return new List<AuthorDto?>(authors.Select(author => author != null ? new AuthorDto() : new AuthorDto()));
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

    public async Task<bool> AddAuthorAsync(AuthorDto authorDto)
    {
        try
        {
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
            };
            await _authorRepository.AddAsync(author);
            return true;
                
        }
        catch (Exception e)
        {
            _log.LogError(e.Message, e);
            throw;
        }
    }
}