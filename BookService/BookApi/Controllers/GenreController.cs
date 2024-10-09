using Book.Application.Commands.Genere;
using Book.Application.Models.Dto;
using Book.Application.Models.Requests;
using Book.Application.Models.Responses;
using Book.Application.Queries.Genre;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProjectBase.Interfaces;

namespace BookApi.Controllers;

public class GenreController : BaseApiController
{
    private readonly ISender _sender;
    private readonly ILoggingService _logger;   
    public GenreController(ILoggingService logger, ISender sender)
    {
        _logger = logger;
        _sender = sender;
    }   
    
    [HttpPost("add")]
    public async Task<IActionResult> AddGenre([FromBody] AddRequest<GenreDto> data)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList();
            return BadRequest(new ErrorResponse(errors));
        }
        if (data.Data == null)
        {
            throw new ArgumentNullException(nameof(data.Data), "GenreDto cannot be null");
        }

        try
        {
            var result = await _sender.Send(new AddGenreCommand(data.Data));
            if (result.Succeeded)
            {
                return StatusCode(201, new BaseResponse<GenreDto> { Data = result.Data });
                // return StatusCode(201, new BaseResponse<GenreDto> { Data = result.Data, Status = "Ok" });
            }
            return BadRequest(new ErrorResponse(result.Errors ?? new List<string>(), result.Succeeded));
        }   
        catch (Exception e)
        {   
            _logger.LogError("Error while adding genre",e);
            return StatusCode(500, new ErrorResponse(new List<string> { "Error while adding genre" }));
        }
    }   
    
    [HttpPost("update")]
    public async Task<IActionResult> UpdateGenre([FromBody] UpdateRequest<GenreDto> request)
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
                throw new ArgumentNullException(nameof(request.Data), "GenreDto cannot be null");
            }

            if (request.Id == Guid.Empty)
            {
                throw new ArgumentException("Id cannot be empty");
            }
            
            var result = await _sender.Send(new UpdateGenreCommand(request.Id, request.Data));
            if (result.Succeeded)
            {
                // return Ok(new BaseResponse<GenreDto>() { Data = result.Data, Status = "Ok" });                
                return Ok(new BaseResponse<GenreDto>() { Data = result.Data });
            }       
            return BadRequest(new ErrorResponse(result.Errors ?? new List<string>(), result.Succeeded));
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message, e);
            return StatusCode(500, new ErrorResponse(new List<string> { "Error while updating genre" }));
        }
    }
    [HttpGet("get")]
    public async Task<IActionResult> GetGenre([FromQuery] GetRequest request)
    {
        try
        {
            var result = await _sender.Send(new GetGenreQuery(request.Id));
            if (result.Succeeded)
            {
                // return Ok(new BaseResponse<GenreDto>() { Data = result.Data, Status = "Ok" });
                return Ok(new BaseResponse<GenreDto>() { Data = result.Data });
            }       
            return NotFound(new ErrorResponse(result.Errors ?? new List<string>()));
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message, e); 
            return BadRequest(new ErrorResponse(new List<string> { "Error while getting genre" }));
        }
    }
    
    [HttpGet("getAll")]
    public async Task<IActionResult> GetGenres()
    {
        try
        {            
            var result = await _sender.Send(new GetGenresQuery());
            if (result.Succeeded)
            {                
                // return Ok(new BaseResponse<IEnumerable<GenreDto>>() { Data = result.Data, Status = "Ok" });
                return Ok(new BaseResponse<IEnumerable<GenreDto?>>() { Data = result.Data });
            }       
            return NotFound(new ErrorResponse(result.Errors ?? new List<string>()));
        }
        catch (Exception e)
        {   
            _logger.LogError(e.Message, e);
            return BadRequest(new ErrorResponse(new List<string> { "Error while getting genre" }));
        }
    }
    
    [HttpDelete("delete")]
    public async Task<IActionResult> DeleteGenre([FromQuery] DeleteRequest request)
    {
        try
        {
            var result = await _sender.Send(new DeleteGenreCommand(request.Id));
            if (result.Succeeded)
            {
                return NoContent();
            }
            return BadRequest(new ErrorResponse(result.Errors ?? new List<string>(), result.Succeeded));
        }   
        catch (Exception e)
        {
            _logger.LogError(e.Message, e);
            return StatusCode(500, new ErrorResponse(new List<string> { "Error while deleting genre" }));
        }
    }   
    
}