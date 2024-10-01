using ProjectBase.Models;

namespace Domain.Entities;

public class Shelve : BaseModel
{
    public Guid? OwnerId { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }

}