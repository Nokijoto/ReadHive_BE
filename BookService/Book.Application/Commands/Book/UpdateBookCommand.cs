using System;
using Application.Models.Dto;
using Application.Models.Results;
using MediatR;

namespace Application.Commands.Book;

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