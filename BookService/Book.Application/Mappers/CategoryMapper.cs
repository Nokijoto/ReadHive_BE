using Book.Application.Models.Dto;
using Book.Domain.Entities;

namespace Book.Application.Mappers;

public static class CategoryMapper
{
    public static CategoryDto ToDto(this Category category)
    {
        return new CategoryDto
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description,
            ParentCategoryId = category.ParentCategoryId,
            DeletedAt = category.DeletedAt,
            CreatedAt = category.CreatedAt,
            UpdatedAt = category.UpdatedAt,
            IsActive = category.IsActive
        };
    }                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                 
    public static Category ToEntity(this CategoryDto categoryDto)
    {
        return new Category
        {
            Id = categoryDto.Id,
            Name = categoryDto.Name,
            Description = categoryDto.Description,
            ParentCategoryId = categoryDto.ParentCategoryId,
            DeletedAt = categoryDto.DeletedAt,
            CreatedAt = categoryDto.CreatedAt,
            UpdatedAt = categoryDto.UpdatedAt,
            IsActive = categoryDto.IsActive
        };
    }   
    
}