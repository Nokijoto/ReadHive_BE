using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Book.Application.Interfaces;
using Book.Application.Models.Dto;
using Book.Application.Models.Results;
using Book.Domain.Interfaces;
using ProjectBase.Interfaces;

namespace Book.Application.Services;

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

    public async Task<ResultBase<GenreDto?>> GetGenreAsync(Guid id)
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
                return new ResultBase<GenreDto?>(true, genre);
            }                
            return null;
        }
        catch (Exception e)
        {
            _log.LogError(e.Message, e);
            throw;
        }
    }

    public async Task<ResultBase<IEnumerable<GenreDto?>>> GetGenresAsync()
    {
        try
        {
            var genres = await _genreRepository.GetAllAsync();
            return new ResultBase<IEnumerable<GenreDto?>>(true, genres);
        }
        catch (Exception e)
        {
            _log.LogError(e.Message, e);
            throw;
        }
    }

    public async Task<ResultBase<GenreDto?>> UpdateGenreAsync(GenreDto genreDto)
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
                return new ResultBase<GenreDto?>(true, genreDto);
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
            await _genreRepository.AddAsync(genreDto);
            return true;
        }
        catch (Exception e)
        {
            _log.LogError(e.Message, e);
            throw;
        }
    }
}