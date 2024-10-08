using System.Collections.Generic;
using Book.Application.Models.Dto;
using Book.Application.Models.Results;
using MediatR;

namespace Book.Application.Queries.AuthorQueries;

public class GetAuthorsQuery : IRequest<ResultBase<IEnumerable<AuthorDto?>>>
{
    public GetAuthorsQuery()
    {
        
    }
}