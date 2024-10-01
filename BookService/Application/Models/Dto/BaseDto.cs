namespace Application.Models.Dto;

public class BaseDto
{
    public readonly Guid? Id;
    public readonly DateTime? CreatedAt;
    public readonly DateTime? UpdatedAt;
    public readonly DateTime? DeletedAt;
    public readonly bool isActive;
}