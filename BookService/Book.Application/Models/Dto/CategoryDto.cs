namespace Book.Application.Models.Dto;

public class CategoryDto : BaseDto
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? ParentCategoryId { get; set; }
}