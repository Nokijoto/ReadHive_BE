namespace Application.Models.Dto;

public class BaseDto
{
    public  Guid Id;
    public  DateTime? CreatedAt;
    public  DateTime? UpdatedAt;
    public  DateTime? DeletedAt;
    public  bool? IsActive;
}