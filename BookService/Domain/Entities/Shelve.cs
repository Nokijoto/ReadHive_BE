using ProjectBase.Models;

namespace Domain.Entities;

public class Shelve : BaseModel
{
    public Guid? OwnerId { get; init; }
    public string? Title { get; init; }
    public string? Description { get; init; }

}