using Book.Application.Models.Dto;
using Book.Domain.Entities;

namespace Book.Application.Mappers;

public static class PublisherMapper
{
    public static PublisherDto ToDto(this Publisher publisher)
    {
        return new PublisherDto
        {            
            Id = publisher.Id,
            Name = publisher.Name,
            Description = publisher.Description,
            PictureUrl = publisher.PictureUrl,
            WebsiteUrl = publisher.WebsiteUrl,
            Country = publisher.Country,
            FoundedAt = publisher.FoundedAt,
            FoundedBy = publisher.FoundedBy,
            DeletedAt = publisher.DeletedAt,
            CreatedAt = publisher.CreatedAt,            
            UpdatedAt = publisher.UpdatedAt,
            IsActive = publisher.IsActive
        };
    }

    public static Publisher ToEntity(this PublisherDto publisherDto)
    {
        return new Publisher
        {
            Id = publisherDto.Id,
            Name = publisherDto.Name,
            Description = publisherDto.Description,
            PictureUrl = publisherDto.PictureUrl,
            WebsiteUrl = publisherDto.WebsiteUrl,
            Country = publisherDto.Country,
            FoundedAt = publisherDto.FoundedAt,
            FoundedBy = publisherDto.FoundedBy,            
            DeletedAt = publisherDto.DeletedAt,
            CreatedAt = publisherDto.CreatedAt,            
            UpdatedAt = publisherDto.UpdatedAt,
            IsActive = publisherDto.IsActive
        };
    }   
}