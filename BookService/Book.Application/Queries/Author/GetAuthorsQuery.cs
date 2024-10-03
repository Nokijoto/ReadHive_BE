using Application.Models.Dto;
using Application.Models.Results;
using MediatR;

namespace Application.Queries.AuthorQueries;

public class GetAuthorsQuery : IRequest<GetAuthorsQueryResult>
{
    public GetAuthorsQuery()
    {
        
    }
}