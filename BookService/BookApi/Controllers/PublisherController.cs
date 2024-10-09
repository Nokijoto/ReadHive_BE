using Book.Application.Commands.Genere;
using Book.Application.Commands.Publisher;
using Book.Application.Models.Dto;
using Book.Application.Models.Requests;
using Book.Application.Models.Responses;
using Book.Application.Queries.Publisher;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProjectBase.Interfaces;

namespace BookApi.Controllers;

public class PublisherController : BaseApiController
{
    private readonly ISender _sender;
    private readonly ILoggingService _logger;   
    public PublisherController(ILoggingService logger, ISender sender)
    {
        _logger = logger;
        _sender = sender;
    }  
    
     [HttpPost("add")]
    public async Task<IActionResult> AddPublisher([FromBody] AddRequest<PublisherDto> data)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList();
            return BadRequest(new ErrorResponse(errors));
        }
        if (data.Data == null)
        {
            throw new ArgumentNullException(nameof(data.Data), "PublisherDto cannot be null");
        }

        try
        {
            var result = await _sender.Send(new AddPublisherCommand(data.Data));
            if (result.Succeeded)
            {
                return StatusCode(201, new BaseResponse<PublisherDto> { Data = result.Data });
                // return StatusCode(201, new BaseResponse<PublisherDto> { Data = result.Data, Status = "Ok" });
            }
            return BadRequest(new ErrorResponse(result.Errors ?? new List<string>(), result.Succeeded));
        }   
        catch (Exception e)
        {   
            _logger.LogError("Error while adding Publisher",e);
            return StatusCode(500, new ErrorResponse(new List<string> { "Error while adding Publisher" }));
        }
    }   
    
    [HttpPost("update")]
    public async Task<IActionResult> UpdatePublisher([FromBody] UpdateRequest<PublisherDto> request)
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
                throw new ArgumentNullException(nameof(request.Data), "PublisherDto cannot be null");
            }

            if (request.Id == Guid.Empty)
            {
                throw new ArgumentException("Id cannot be empty");
            }
            
            var result = await _sender.Send(new UpdatePublisherCommand(request.Id, request.Data));
            if (result.Succeeded)
            {
                // return Ok(new BaseResponse<PublisherDto>() { Data = result.Data, Status = "Ok" });                
                return Ok(new BaseResponse<PublisherDto>() { Data = result.Data });
            }       
            return BadRequest(new ErrorResponse(result.Errors ?? new List<string>(), result.Succeeded));
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message, e);
            return StatusCode(500, new ErrorResponse(new List<string> { "Error while updating Publisher" }));
        }
    }
    [HttpGet("get")]
    public async Task<IActionResult> GetPublisher([FromQuery] GetRequest request)
    {
        try
        {
            var result = await _sender.Send(new GetPublisherQuery(request.Id));
            if (result.Succeeded)
            {
                // return Ok(new BaseResponse<PublisherDto>() { Data = result.Data, Status = "Ok" });
                return Ok(new BaseResponse<PublisherDto>() { Data = result.Data });
            }       
            return NotFound(new ErrorResponse(result.Errors ?? new List<string>()));
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message, e); 
            return BadRequest(new ErrorResponse(new List<string> { "Error while getting Publisher" }));
        }
    }
    
    [HttpGet("getAll")]
    public async Task<IActionResult> GetPublishers()
    {
        try
        {            
            var result = await _sender.Send(new GetPublishersQuery());
            if (result.Succeeded)
            {                
                // return Ok(new BaseResponse<IEnumerable<PublisherDto>>() { Data = result.Data, Status = "Ok" });
                return Ok(new BaseResponse<IEnumerable<PublisherDto?>>() { Data = result.Data });
            }       
            return NotFound(new ErrorResponse(result.Errors ?? new List<string>()));
        }
        catch (Exception e)
        {   
            _logger.LogError(e.Message, e);
            return BadRequest(new ErrorResponse(new List<string> { "Error while getting Publisher" }));
        }
    }
    
    [HttpDelete("delete")]
    public async Task<IActionResult> DeletePublisher([FromQuery] DeleteRequest request)
    {
        try
        {
            var result = await _sender.Send(new DeletePublisherCommand(request.Id));
            if (result.Succeeded)
            {
                return NoContent();
            }
            return BadRequest(new ErrorResponse(result.Errors ?? new List<string>(), result.Succeeded));
        }   
        catch (Exception e)
        {
            _logger.LogError(e.Message, e);
            return StatusCode(500, new ErrorResponse(new List<string> { "Error while deleting Publisher" }));
        }
    }   
}