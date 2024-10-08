using Book.Application.Models.Dto;
using Book.Application.Models.Results;
using MediatR;

namespace Book.Application.Commands.Book;

public class AddBookCommand : IRequest<ResultBase<BookDto?>>
{
    public BookDto? BookDto { get; set; }
    
    public AddBookCommand(BookDto bookDto)
    {
        BookDto = bookDto;
    }
}