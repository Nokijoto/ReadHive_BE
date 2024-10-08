using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Book.Application.Models.Dto;
using Book.Application.Models.Results;

namespace Book.Application.Interfaces;

public interface IGenreService
{
        
    Task<bool> DeleteGenreAsync(Guid id);
    Task<ResultBase<GenreDto?>> GetGenreAsync(Guid id);
    Task<ResultBase<IEnumerable<GenreDto?>>> GetGenresAsync();
    Task<ResultBase<GenreDto?>> UpdateGenreAsync(GenreDto genreDto);
    Task<bool> AddGenreAsync(GenreDto genreDto);
}