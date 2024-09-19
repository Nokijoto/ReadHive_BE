using System.Threading.Tasks;
using Application.Interfaces;
using Application.Models;
using AuthService.Models.Requests;
using AuthService.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var result = await _authService.LoginAsync(new LoginDto(request.Email, request.Password));

        if (!result.Succeeded)
        {
            return BadRequest(new ErrorResponse(result.Errors));
        }

        return Ok(new AuthResponse(result.Token, result.Expiration));
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        var result = await _authService.RegisterAsync(new RegisterDto(request.UserName, request.Email, request.Password));

        if (!result.Succeeded)
        {
            return BadRequest(new ErrorResponse(result.Errors));
        }

        return Ok(new AuthResponse(result.Token, result.Expiration));
    }
    
    [HttpGet("verifyToken")]
    [Authorize]
    public async Task<IActionResult> GetMe()
    {
        return NoContent();
    }
}