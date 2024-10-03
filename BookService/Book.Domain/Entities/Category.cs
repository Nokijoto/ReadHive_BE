using ProjectBase.Models;

namespace Domain.Entities;

public class Category : BaseModel
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? ParentCategoryId { get; set; }
}