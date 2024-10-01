using ProjectBase.Models;

namespace Domain.Entities;

public class Genre : BaseModel
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? ParentGenreId { get; set; }
}