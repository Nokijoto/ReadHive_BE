using Book.Application.Models.Dto;
using Book.Domain.Entities;

namespace Book.Application.Mappers;

public static class AuthorMapper
{
    public static AuthorDto ToDto(this Author author)
    {
        return new AuthorDto
        {
            Id = author.Id,
            FirstName = author.FirstName,
            LastName = author.LastName,
            Bio = author.Bio,
            PictureUrl = author.PictureUrl,
            BirthDate = author.BirthDate,
            DeathDate = author.DeathDate,
            Nationality = author.Nationality,
            IsActive = author.IsActive,
            CreatedAt = author.CreatedAt,
            UpdatedAt = author.UpdatedAt,
            DeletedAt = author.DeletedAt
        };
    }

    public static Author ToEntity(this AuthorDto authorDto)
    {
        return new Author
        {
            Id = authorDto.Id,
            FirstName = authorDto.FirstName,
            LastName = authorDto.LastName,
            Bio = authorDto.Bio,
            PictureUrl = authorDto.PictureUrl,
            BirthDate = authorDto.BirthDate,
            DeathDate = authorDto.DeathDate,
            Nationality = authorDto.Nationality,
            IsActive = authorDto.IsActive,
            CreatedAt = authorDto.CreatedAt,
            UpdatedAt = authorDto.UpdatedAt,
            DeletedAt = authorDto.DeletedAt
        };
    }
}