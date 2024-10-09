using System;
using Book.Application.Models.Dto;
using Book.Application.Models.Results;
using MediatR;

namespace Book.Application.Commands.Book;

public class UpdateBookCommand : IRequest<ResultBase<BookDto?>>
{
    public Guid Id { get; set; }
    public BookDto BookDto { get; set; }
    public UpdateBookCommand(Guid id, BookDto bookDto)
    {
        Id = id;
        BookDto = bookDto;
    }
}