using System.Security.Claims;
using Application.Interfaces;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Controllers;

public class UserController : ControllerBase
{
    private readonly ILoggingService _log;
    private readonly IUserService _userService;

    public UserController(IUserService userService, ILoggingService log)
    {
        _userService = userService;
        _log = log;
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

            var user = await _userService.GetByIdAsync(userId);
            if (user != null)
                return Ok(user);
            else
                return NotFound();
        }
        catch (Exception e)
        {
            _log.LogError(e.Message, e);
            return StatusCode(500, e.Message);
        }
    }
}