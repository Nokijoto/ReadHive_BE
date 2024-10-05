using System;
using Application.Models.Dto;
using Application.Models.Results;
using MediatR;

namespace Application.Queries.Book;

public class GetBookQuery : IRequest<ResultBase<BookDto?>>
{
    public Guid Id { get; set; }
    public GetBookQuery(Guid id)
    {
        Id = id;
    }
}