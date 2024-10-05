using Application.Models.Dto;
using Application.Models.Results;
using MediatR;

namespace Application.Queries.Book;

public class GetBooksQuery : IRequest<ResultBase<IEnumerable<BookDto?>>>
{
    public GetBooksQuery()
    {
        
    }
}