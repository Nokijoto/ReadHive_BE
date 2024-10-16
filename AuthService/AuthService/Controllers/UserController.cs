using System.Security.Claims;
using Application.Interfaces;
using Application.Queries.GetMe;
using Infrastructure.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Controllers;

public class UserController : ControllerBase
{
    private readonly ILoggingService _log;
    private readonly IUserService _userService;
    private readonly IMediator _mediator;

    public UserController(IUserService userService, ILoggingService log, IMediator mediator)
    {
        _userService = userService;
        _log = log;
        _mediator = mediator;
    }
    
    [HttpGet("me")]
    public async Task<IActionResult> GetMe()
    {
        try
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdString))
            {
                return Unauthorized();
            }
    
            if (!Guid.TryParse(userIdString, out var userId))
            {
                return BadRequest("Nieprawidłowy identyfikator użytkownika.");
            }

            // Wysłanie zapytania do MediatR
            var userDto = await _mediator.Send(new GetUserQuery(userId));
        
            if (userDto != null)
                return Ok(userDto);
            else
                return NotFound();
        }
        catch (Exception e)
        {
            _log.LogError( "Błąd podczas pobierania danych użytkownika.",e);
            return StatusCode(500, "Wystąpił błąd wewnętrzny.");
        }
    }
}