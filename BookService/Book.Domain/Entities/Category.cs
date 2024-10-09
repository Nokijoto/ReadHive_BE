using ProjectBase.Models;

namespace Book.Domain.Entities;

public class Category : BaseModel
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? ParentCategoryId { get; set; }
}