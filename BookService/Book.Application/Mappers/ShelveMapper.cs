using Book.Application.Models.Dto;
using Book.Domain.Entities;

namespace Book.Application.Mappers;

public static class ShelveMapper
{
    public static ShelveDto ToDto(this Shelve shelve)
    {
        return new ShelveDto
        {
            Id = shelve.Id,
            Description = shelve.Description,
            Title = shelve.Title,
            OwnerId = shelve.OwnerId,
            DeletedAt = shelve.DeletedAt,
            CreatedAt = shelve.CreatedAt,
            UpdatedAt = shelve.UpdatedAt,
            IsActive = shelve.IsActive
        };
    }

    public static Shelve ToEntity(this ShelveDto shelveDto)
    {
        return new Shelve
        {
            Id = shelveDto.Id,
            Description = shelveDto.Description,
            Title = shelveDto.Title,
            OwnerId = shelveDto.OwnerId,
            DeletedAt = shelveDto.DeletedAt,
            CreatedAt = shelveDto.CreatedAt,            
            UpdatedAt = shelveDto.UpdatedAt,
            IsActive = shelveDto.IsActive
        };
    }   
    
}