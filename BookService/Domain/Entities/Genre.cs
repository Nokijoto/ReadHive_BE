using ProjectBase.Models;

namespace Domain.Entities;

public class Genre : BaseModel
{
    public string? Name { get; init; }
    public string? Description { get; init; }
    public string? ParentGenreId { get; init; }
}