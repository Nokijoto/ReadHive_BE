using Book.Application.Models.Dto;

namespace Book.Application.Models.Requests;

public class AddAuthorRequest
{
    public AuthorDto AuthorDto { get; set; }
    public AddAuthorRequest(AuthorDto authorDto)
    {
        AuthorDto = authorDto;
    }
}