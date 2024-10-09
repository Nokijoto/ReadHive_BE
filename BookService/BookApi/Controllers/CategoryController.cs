using Book.Application.Commands.Category;
using Book.Application.Models.Dto;
using Book.Application.Models.Requests;
using Book.Application.Models.Responses;
using Book.Application.Queries.Category;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProjectBase.Interfaces;

namespace BookApi.Controllers;

public class CategoryController : BaseApiController
{
    private readonly ISender _sender;
    private readonly ILoggingService _logger;
    public CategoryController(ILoggingService logger, ISender sender)
    {
        _logger = logger;
        _sender = sender;
    }
    
    [HttpPost("add")]
    public async Task<IActionResult> AddCategory([FromBody] AddRequest<CategoryDto> data)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList();
            return BadRequest(new ErrorResponse(errors));
        }
        if (data.Data == null)
        {
            throw new ArgumentNullException(nameof(data.Data), "CategoryDto cannot be null");
        }

        try
        {            
            var result = await _sender.Send(new AddCategoryCommand(data.Data));            
            if (result.Succeeded)
            {
                return StatusCode(201, new BaseResponse<CategoryDto> { Data = result.Data });                
            }
            return BadRequest(new ErrorResponse(result.Errors ?? new List<string>(), result.Succeeded));
        }
        catch (Exception e)
        {
            _logger.LogError("Error while adding category",e);            
            return StatusCode(500, new ErrorResponse(new List<string> { "Error while adding category" }));
        }
    }
    
    [HttpPost("update")]
    public async Task<IActionResult> UpdateCategory([FromBody] UpdateRequest<CategoryDto> request)
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
                throw new ArgumentNullException(nameof(request.Data), "CategoryDto cannot be null");
            }
            if(request.Id == Guid.Empty)
            {
                throw new ArgumentException("Id cannot be empty");
            }            
            var result = await _sender.Send(new UpdateCategoryCommand(request.Id, request.Data));
            if (result.Succeeded)
            {
                return Ok(new BaseResponse<CategoryDto>() { Data = result.Data });
            }
            return BadRequest(new ErrorResponse(result.Errors ?? new List<string>(), result.Succeeded));
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message, e);
            return StatusCode(500, new ErrorResponse(new List<string> { "Error while updating category" ,e.Message }));
        }
    }
    
    [HttpGet("get")]
    public async Task<IActionResult> GetCategory([FromQuery] GetRequest request)
    {
        try
        {
            var result = await _sender.Send(new GetCategoryQuery(request.Id));
            if (result.Succeeded)
            {
                return Ok(new BaseResponse<CategoryDto>() { Data = result.Data });
            }
            return NotFound(new ErrorResponse(result.Errors ?? new List<string>()));
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message, e);
            return BadRequest(new ErrorResponse(new List<string> { "Error while getting category" }));
        }
    }
    
    [HttpGet("getAll")]
    public async Task<IActionResult> GetCategories()
    {
        try
        {
            var result = await _sender.Send(new GetCategoriesQuery());
            if (result.Succeeded)
            {
                return Ok(new BaseResponse<IEnumerable<CategoryDto>>() { Data = result.Data });
            }
            return NotFound(new ErrorResponse(result.Errors ?? new List<string>()));
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message, e);
            return BadRequest(new ErrorResponse(new List<string> { "Error while getting category" }));
        }
    }
    
    [HttpDelete("delete")]
    public async Task<IActionResult> DeleteCategory([FromQuery] DeleteRequest request)
    {
        try
        {
            var result = await _sender.Send(new DeleteCategoryCommand(request.Id));
            if (result.Succeeded)
            {
                return NoContent();
            }
            return BadRequest(new ErrorResponse(result.Errors ?? new List<string>(), result.Succeeded));
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message, e);
            return StatusCode(500, new ErrorResponse(new List<string> { "Error while deleting category" }));
        }
    }   
    
}