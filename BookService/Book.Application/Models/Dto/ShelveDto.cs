using System;

namespace Application.Models.Dto;

public class ShelveDto : BaseDto
{
    public Guid? OwnerId { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
}