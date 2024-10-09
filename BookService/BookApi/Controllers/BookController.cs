using Book.Application.Commands.Book;
using Book.Application.Models.Dto;
using Book.Application.Models.Requests;
using Book.Application.Models.Requests.Book;
using Book.Application.Models.Responses;
using Book.Application.Queries.Book;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProjectBase;
using ProjectBase.Interfaces;

namespace BookApi.Controllers;

public class BookController : BaseApiController
{
    private readonly ISender _sender;
    private readonly ILoggingService _logger;

    public BookController(ILoggingService logger, ISender sender)
    {
        _logger = logger;
        _sender = sender;
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddBook([FromBody] AddRequest<BookDto> data)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList();
            return BadRequest(new ErrorResponse(errors));
        }

        if (data.Data == null)
        {
            throw new ArgumentNullException(nameof(data.Data), "BookDto cannot be null");
        }

        try
        {
            var result = await _sender.Send(new AddBookCommand(data.Data));
            if (result.Succeeded)
            {
                return StatusCode(201, new BaseResponse<BookDto> { Data = result.Data });
                // return StatusCode(201, new BaseResponse<BookDto> { Data = result.Data, Status = "Ok" });
            }

            return BadRequest(new ErrorResponse(result.Errors ?? new List<string>(), result.Succeeded));
        }
        catch (Exception e)
        {
            _logger.LogError("Error while adding book", e);
            return StatusCode(500, new ErrorResponse(new List<string> { "Error while adding book" }));
        }
    }

    [HttpPut("update")]
    public async Task<IActionResult> UpdateBook([FromBody] UpdateRequest<BookDto> request)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList();
                return BadRequest(new ErrorResponse(errors));
            }

            if (request.Data == null)
            {
                throw new ArgumentNullException(nameof(request.Data), "BookDto cannot be null");
            }

            if (request.Id == Guid.Empty)
            {
                throw new ArgumentException("Id cannot be empty");
            }

            var result = await _sender.Send(new UpdateBookCommand(request.Id, request.Data));
            if (result.Succeeded)
            {
                // return Ok(new BaseResponse<BookDto>() { Data = result.Data, Status = "Ok" });
                return Ok(new BaseResponse<BookDto>() { Data = result.Data });
            }

            return BadRequest(new ErrorResponse(result.Errors ?? new List<string>(), result.Succeeded));
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message, e);
            return StatusCode(500, new ErrorResponse(new List<string> { "Error while updating book" }));
        }
    }

    [HttpGet("get")]
    public async Task<IActionResult> GetBook([FromQuery] GetRequest request)
    {
        try
        {
            var result = await _sender.Send(new GetBookQuery(request.Id));
            if (result.Succeeded)
            {
                // return Ok(new BaseResponse<BookDto>() { Data = result.Data, Status = "Ok" });
                return Ok(new BaseResponse<BookDto>() { Data = result.Data });
            }

            return NotFound(new ErrorResponse(result.Errors ?? new List<string>()));
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message, e);
            return BadRequest(new ErrorResponse(new List<string> { "Error while getting book" }));
        }
    }

    [HttpGet("getAll")]
    public async Task<IActionResult> GetBooks()
    {
        try
        {
            var result = await _sender.Send(new GetBooksQuery());
            if (result.Succeeded)
            {

                // return Ok(new BaseResponse<IEnumerable<BookDto>>() { Data = result.Data, Status = "Ok" });                
                return Ok(new BaseResponse<IEnumerable<BookDto?>>() { Data = result.Data });
            }

            return NotFound(new ErrorResponse(result.Errors ?? new List<string>()));
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message, e);
            return BadRequest(new ErrorResponse(new List<string> { "Error while getting book" }));
        }
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> DeleteBook([FromQuery] DeleteBookRequest request)
    {
        try
        {
            var result = await _sender.Send(new DeleteBookCommand(request.Id));
            if (result.Succeeded)
            {
                return NoContent();
            }

            return BadRequest(new ErrorResponse(result.Errors ?? new List<string>(), result.Succeeded));
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message, e);
            return StatusCode(500, new ErrorResponse(new List<string> { "Error while deleting book" }));
        }
    }
}