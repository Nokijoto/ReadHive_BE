using Book.Application.Models.Dto;
using Book.Domain.Entities;

namespace Book.Application.Mappers;

public static class GenreMapper
{
    public static GenreDto ToDto(this Genre genre)
    {
        return new GenreDto
        {
            Id = genre.Id,
            Name = genre.Name,
            Description = genre.Description,
            ParentGenreId = genre.ParentGenreId,
            DeletedAt = genre.DeletedAt,
            CreatedAt = genre.CreatedAt,
            UpdatedAt = genre.UpdatedAt,
            IsActive = genre.IsActive
        };
    }                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                 
    public static Genre ToEntity(this GenreDto genreDto)
    {
        return new Genre
        {
            Id = genreDto.Id,
            Name = genreDto.Name,
            Description = genreDto.Description,
            ParentGenreId = genreDto.ParentGenreId,
            DeletedAt = genreDto.DeletedAt,
            CreatedAt = genreDto.CreatedAt,
            UpdatedAt = genreDto.UpdatedAt,
            IsActive = genreDto.IsActive
        };
    }       
}