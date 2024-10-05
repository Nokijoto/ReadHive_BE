using Application.Models.Dto;
using Application.Models.Results;
using MediatR;

namespace Application.Queries.AuthorQueries;

public class GetAuthorQuery :  IRequest<ResultBase<AuthorDto?>>
{
    public Guid Id { get; set; }
    public GetAuthorQuery(Guid id)
    {
        Id = id;
    }
}