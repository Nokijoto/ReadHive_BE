using Application.Models.Dto;
using Application.Models.Results;
using MediatR;

namespace Application.Commands.Book;

public class AddBookCommand : IRequest<ResultBase<BookDto?>>
{
    public BookDto? BookDto { get; set; }
    
    public AddBookCommand(BookDto bookDto)
    {
        BookDto = bookDto;
    }
}