using System;
using Book.Application.Models.Dto;
using Book.Application.Models.Results;
using MediatR;

namespace Book.Application.Queries.AuthorQueries;

public class GetAuthorQuery :  IRequest<ResultBase<AuthorDto?>>
{
    public Guid Id { get; set; }
    public GetAuthorQuery(Guid id)
    {
        Id = id;
    }
}