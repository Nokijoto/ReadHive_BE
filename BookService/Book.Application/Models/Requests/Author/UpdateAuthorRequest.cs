using System;
using Book.Application.Models.Dto;

namespace Book.Application.Models.Requests;

public class UpdateAuthorRequest
{
    public Guid? Id { get; set; }
    public AuthorDto AuthorDto { get; set; }
    public UpdateAuthorRequest(Guid id, AuthorDto authorDto)
    {
        Id = id;
        AuthorDto = authorDto;
    }
    
}