using Application.Commands.Author;
using Application.Models.Dto;
using Application.Models.Requests;
using Application.Models.Responses;
using Application.Queries.AuthorQueries;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BookApi.Controllers;


public class AuthorController :BaseApiController
{
    private readonly ISender _sender;
    private readonly ILogger<AuthorController> _logger;
    public AuthorController(ILogger<AuthorController> logger, ISender sender)
    {
        _logger = logger;
        _sender = sender;
    }
    
    // [HttpPost("add")]
    // public async Task<IActionResult> AddAuthor([FromBody] AddAuthorRequest request)
    // {
    //     try
    //     {
    //         var result = await _sender.Send(new AddAuthorCommand(request.AuthorDto));
    //        if ((bool)(!result.Succeeded)!)
    //         {
    //             return BadRequest(new ErrorResponse(result.Errors));
    //         }
    //         return Ok(new BaseResponse<AuthorDto>() { Data = result, Status = "Ok" });
    //     }
    //     catch (Exception e)
    //     {
    //         _logger.LogError(e.Message, e);
    //         return StatusCode(500, new ErrorResponse(new List<string> { "Error while adding author" }));
    //     }
    // }
    //
    // [HttpPost("update")]
    // public async Task<IActionResult> UpdateAuthor([FromBody] UpdateAuthorRequest request)
    // {
    //     try
    //     {
    //         var result = await _sender.Send(new UpdateAuthorCommand(request.AuthorDto));
    //         return result != null
    //             ? new BaseResponse<AuthorDto>() { Data = result, Status = "Ok" }
    //             : new BaseResponse<AuthorDto>() { Status = "Error" };
    //     }
    //     catch (Exception e)
    //     {
    //         _logger.LogError(e.Message, e);
    //         return new BaseResponse<AuthorDto>() { Status = "Error" };
    //     }
    // }
    //       
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
    //
    // [HttpDelete("delete")]
    // public async Task<IActionResult> DeleteAuthor([FromQuery] DeleteAuthorRequest request)
    // {
    //     try
    //     {
    //         var result = await _sender.Send(new DeleteAuthorCommand(request.Id));
    //         return result != null
    //             ? new BaseResponse<bool>() { Data = result, Status = "Ok" }
    //             : new BaseResponse<bool>() { Status = "Error" };
    //     }
    //     catch (Exception e)
    //     {
    //         _logger.LogError(e.Message, e);
    //         return new BaseResponse<bool>() { Status = "Error" };
    //     }
    // }
}