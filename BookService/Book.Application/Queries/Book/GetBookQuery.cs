using System;
using Book.Application.Models.Dto;
using Book.Application.Models.Results;
using MediatR;

namespace Book.Application.Queries.Book;

public class GetBookQuery : IRequest<ResultBase<BookDto?>>
{
    public Guid Id { get; set; }
    public GetBookQuery(Guid id)
    {
        Id = id;
    }
}