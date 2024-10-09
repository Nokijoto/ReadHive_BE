using Book.Application.Commands.Genere;
using Book.Application.Commands.Shelve;
using Book.Application.Models.Dto;
using Book.Application.Models.Requests;
using Book.Application.Models.Responses;
using Book.Application.Queries.Shelve;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProjectBase.Interfaces;

namespace BookApi.Controllers;

public class ShelveController :BaseApiController
{
    private readonly ISender _sender;
    private readonly ILoggingService _logger;   
    public ShelveController(ILoggingService logger, ISender sender)
    {
        _logger = logger;
        _sender = sender;
    }   
    
     [HttpPost("add")]
    public async Task<IActionResult> AddShelve([FromBody] AddRequest<ShelveDto> data)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList();
            return BadRequest(new ErrorResponse(errors));
        }
        if (data.Data == null)
        {
            throw new ArgumentNullException(nameof(data.Data), "ShelveDto cannot be null");
        }

        try
        {
            var result = await _sender.Send(new AddShelveCommand(data.Data));
            if (result.Succeeded)
            {
                return StatusCode(201, new BaseResponse<ShelveDto> { Data = result.Data });
                // return StatusCode(201, new BaseResponse<ShelveDto> { Data = result.Data, Status = "Ok" });
            }
            return BadRequest(new ErrorResponse(result.Errors ?? new List<string>(), result.Succeeded));
        }   
        catch (Exception e)
        {   
            _logger.LogError("Error while adding Shelve",e);
            return StatusCode(500, new ErrorResponse(new List<string> { "Error while adding Shelve" }));
        }
    }   
    
    [HttpPost("update")]
    public async Task<IActionResult> UpdateShelve([FromBody] UpdateRequest<ShelveDto> request)
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
                throw new ArgumentNullException(nameof(request.Data), "ShelveDto cannot be null");
            }

            if (request.Id == Guid.Empty)
            {
                throw new ArgumentException("Id cannot be empty");
            }
            
            var result = await _sender.Send(new UpdateShelveCommand(request.Id, request.Data));
            if (result.Succeeded)
            {
                // return Ok(new BaseResponse<ShelveDto>() { Data = result.Data, Status = "Ok" });                
                return Ok(new BaseResponse<ShelveDto>() { Data = result.Data });
            }       
            return BadRequest(new ErrorResponse(result.Errors ?? new List<string>(), result.Succeeded));
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message, e);
            return StatusCode(500, new ErrorResponse(new List<string> { "Error while updating Shelve" }));
        }
    }
    [HttpGet("get")]
    public async Task<IActionResult> GetShelve([FromQuery] GetRequest request)
    {
        try
        {
            var result = await _sender.Send(new GetShelveQuery(request.Id));
            if (result.Succeeded)
            {
                // return Ok(new BaseResponse<ShelveDto>() { Data = result.Data, Status = "Ok" });
                return Ok(new BaseResponse<ShelveDto>() { Data = result.Data });
            }       
            return NotFound(new ErrorResponse(result.Errors ?? new List<string>()));
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message, e); 
            return BadRequest(new ErrorResponse(new List<string> { "Error while getting Shelve" }));
        }
    }
    
    [HttpGet("getAll")]
    public async Task<IActionResult> GetShelves()
    {
        try
        {            
            var result = await _sender.Send(new GetShelvesQuery());
            if (result.Succeeded)
            {                
                // return Ok(new BaseResponse<IEnumerable<ShelveDto>>() { Data = result.Data, Status = "Ok" });
                return Ok(new BaseResponse<IEnumerable<ShelveDto?>>() { Data = result.Data });
            }       
            return NotFound(new ErrorResponse(result.Errors ?? new List<string>()));
        }
        catch (Exception e)
        {   
            _logger.LogError(e.Message, e);
            return BadRequest(new ErrorResponse(new List<string> { "Error while getting Shelve" }));
        }
    }
    
    [HttpDelete("delete")]
    public async Task<IActionResult> DeleteShelve([FromQuery] DeleteRequest request)
    {
        try
        {
            var result = await _sender.Send(new DeleteShelveCommand(request.Id));
            if (result.Succeeded)
            {
                return NoContent();
            }
            return BadRequest(new ErrorResponse(result.Errors ?? new List<string>(), result.Succeeded));
        }   
        catch (Exception e)
        {
            _logger.LogError(e.Message, e);
            return StatusCode(500, new ErrorResponse(new List<string> { "Error while deleting Shelve" }));
        }
    }   
}