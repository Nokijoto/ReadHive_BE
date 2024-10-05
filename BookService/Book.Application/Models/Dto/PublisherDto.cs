namespace Application.Models.Dto;

public class PublisherDto:BaseDto
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? PictureUrl { get; set; }
    public string? WebsiteUrl { get; set; }
    public string? Country { get; set; }
    public string? FoundedAt { get; set; }
    public string? FoundedBy { get; set; }
}