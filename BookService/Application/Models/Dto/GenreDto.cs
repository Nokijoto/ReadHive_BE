namespace Application.Models.Dto;

public class GenreDto : BaseDto
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? ParentGenreId { get; set; }  
}