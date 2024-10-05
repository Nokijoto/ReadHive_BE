using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Models.Dto;
using Domain.Entities;
using Domain.Interfaces;
using Book.Infrastructure.Interfaces;
using Serilog;

namespace Application.Services;

public class GenreService : IGenreService
{
    private readonly ILoggingService _log;
    private readonly IGenreRepository _genreRepository;
    public GenreService(ILoggingService log, IGenreRepository genreRepository)
    {
        _log = log;
        _genreRepository = genreRepository;
    }
    public async Task<bool> DeleteGenreAsync(Guid id)
    {
        try
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Genre id cannot be empty");
            }
            var genre = await _genreRepository.GetByIdAsync(id);
            if (genre != null)
            {
                await _genreRepository.DeleteAsync(genre.Id);
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

    public async Task<GenreDto?> GetGenreAsync(Guid id)
    {
        try
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Genre id cannot be empty");
            }
            var genre = await _genreRepository.GetByIdAsync(id);
            if (genre != null)
            {
                var genreDto = new GenreDto()
                {
                    Id = genre.Id,
                    Name = genre.Name,
                    DeletedAt = genre.DeletedAt,
                    CreatedAt = genre.CreatedAt,
                    UpdatedAt = genre.UpdatedAt,
                };
                return genreDto;
            }                
            return null;
        }
        catch (Exception e)
        {
            _log.LogError(e.Message, e);
            throw;
        }
    }

    public async Task<IEnumerable<GenreDto?>> GetGenreAsync()
    {
        try
        {
            var genres = await _genreRepository.GetAllAsync();    
            return new List<GenreDto?>(genres.Select(genre => genre != null ? new GenreDto() : new GenreDto()));
        }
        catch (Exception e)
        {
            _log.LogError(e.Message, e);
            throw;
        }
    }

    public async Task<GenreDto?> UpdateGenreAsync(GenreDto genreDto)
    {
        try
        {
            if (genreDto.Id == Guid.Empty)
            {
                throw new ArgumentException("Genre id cannot be empty");
            }
            var genre = await _genreRepository.GetByIdAsync(genreDto.Id);
            if (genre != null)
            {
                genre.Name = genreDto.Name;
                await _genreRepository.UpdateAsync(genre);
                return genreDto;
            }       
            return null;
        }
        catch (Exception e)
        {
            _log.LogError(e.Message, e);
            throw;
        }
    }

    public async Task<bool> AddGenreAsync(GenreDto genreDto)
    {
        try
        {
            var genre = new Genre()
            {
                Name = genreDto.Name,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };      
            await _genreRepository.AddAsync(genre);
            return true;
        }
        catch (Exception e)
        {
            _log.LogError(e.Message, e);
            throw;
        }
    }
}