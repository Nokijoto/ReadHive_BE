using System.Collections.Generic;
using Book.Application.Models.Dto;
using Book.Application.Models.Results;
using MediatR;

namespace Book.Application.Queries.Book;

public class GetBooksQuery : IRequest<ResultBase<IEnumerable<BookDto?>>>
{
    public GetBooksQuery()
    {
        
    }
}