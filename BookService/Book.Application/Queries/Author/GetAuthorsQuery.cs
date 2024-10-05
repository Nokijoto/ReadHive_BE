using Application.Models.Dto;
using Application.Models.Results;
using MediatR;

namespace Application.Queries.AuthorQueries;

public class GetAuthorsQuery : IRequest<ResultBase<IEnumerable<AuthorDto?>>>
{
    public GetAuthorsQuery()
    {
        
    }
}