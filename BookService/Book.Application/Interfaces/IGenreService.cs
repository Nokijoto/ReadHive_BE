using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Models.Dto;

namespace Application.Interfaces;

public interface IGenreService
{
        
    Task<bool> DeleteGenreAsync(Guid id);
    Task<GenreDto?> GetGenreAsync(Guid id);
    Task<IEnumerable<GenreDto?>> GetGenreAsync();
    Task<GenreDto?> UpdateGenreAsync(GenreDto genreDto);
    Task<bool> AddGenreAsync(GenreDto genreDto);
}