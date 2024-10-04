using Application.Commands.Author;
using Application.Models.Dto;
using Application.Models.Requests;
using Application.Models.Responses;
using Application.Queries.AuthorQueries;
using Book.Infrastructure.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookApi.Controllers;

public class AuthorController :BaseApiController
{
    private readonly ISender _sender;
    private readonly ILoggingService _logger;
    public AuthorController(ILoggingService logger, ISender sender)
    {
        _logger = logger;
        _sender = sender;
    }
    
    
    [HttpPost("add")]
    public async Task<IActionResult> AddAuthor([FromBody] AddRequest<AuthorDto> data)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList();
            return BadRequest(new ErrorResponse(errors));
        }

        try
        {
            var result = await _sender.Send(new AddAuthorCommand(data.Data));
            if (result.Succeeded)
            {
                return StatusCode(201, new BaseResponse<AuthorDto> { Data = result.Data, Status = "Ok" });
            }
            return BadRequest(new ErrorResponse(result.Errors ?? new List<string>(), result.Succeeded));
        }
        catch (Exception e)
        {
            _logger.LogError("Error while adding author",e); 
            return StatusCode(500, new ErrorResponse(new List<string> { "Error while adding author" }));
        }
    }
    
    [HttpPost("update")]
    public async Task<IActionResult> UpdateAuthor([FromBody] UpdateRequest<AuthorDto> request)
    {
        try
        {
            var result = await _sender.Send(new UpdateAuthorCommand(request.Id, request.Data));
            if (result.Succeeded)
            {
                return Ok(new BaseResponse<AuthorDto>() { Data = result.Data, Status = "Ok" });
            }
            return BadRequest(new ErrorResponse(result.Errors ?? new List<string>(), result.Succeeded));
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message, e);
            return StatusCode(500, new ErrorResponse(new List<string> { "Error while updating author" }));
        }
    }
          
    [HttpGet("get")]
    public async Task<IActionResult> GetAuthor([FromQuery] GetAuthorRequest request)
    {
      try
      {
          var result = await _sender.Send(new GetAuthorQuery(request.Id));
          if (result.Succeeded)
          {
              return Ok(new BaseResponse<AuthorDto>() { Data = result.Data, Status = "Ok" });
          }
          return NotFound(new ErrorResponse(result.Errors??new List<string>()));
      }
      catch (Exception e)
      {
          _logger.LogError(e.Message, e);
          return BadRequest(new ErrorResponse(new List<string> { "Error while getting author" }));
      }
    }
    
    [HttpGet("getAll")]
    public async Task<IActionResult> GetAuthors()
    {
        try
        {
            var result = await _sender.Send(new GetAuthorsQuery());
            if (result.Succeeded)
            {
                return Ok(new BaseResponse<IEnumerable<AuthorDto>>() { Data = result.Data, Status = "Ok" });
            }
            return NotFound(new ErrorResponse(result.Errors??new List<string>()));
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message, e);
            return BadRequest(new ErrorResponse(new List<string> { "Error while getting author" }));
        }
    }
    
    [HttpDelete("delete")]
    public async Task<IActionResult> DeleteAuthor([FromQuery] DeleteAuthorRequest request)
    {
        try
        {
            var result = await _sender.Send(new DeleteAuthorCommand(request.Id));
            if (result.Succeeded)
            {
                return NoContent();
            }
            return BadRequest(new ErrorResponse(result.Errors ?? new List<string>(), result.Succeeded));
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message, e);
            return StatusCode(500, new ErrorResponse(new List<string> { "Error while deleting author" }));
        }
    }
}