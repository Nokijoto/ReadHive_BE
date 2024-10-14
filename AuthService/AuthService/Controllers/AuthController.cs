using Application.Commands;
using Application.Commands.ConfirmEmail;
using Application.Commands.Login;
using Application.Commands.Register;
using Application.Interfaces;
using Application.Models.Dto;
using Application.Models.Requests;
using Application.Models.Responses;
using Infrastructure.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public sealed class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IMediator _mediator;
    private readonly ILoggingService _log;

    public AuthController(IAuthService authService, IMediator mediator, ILoggingService log)
    {
        _authService = authService;
        _mediator = mediator;
        _log = log;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        try
        {
            var result = await _mediator.Send(new LoginCommand(request.Email, request.Password));

            if (!result.Succeeded)
            {
                return BadRequest(new ErrorResponse(result.Errors));
            }

            return Ok(new AuthResponse(result.Token, result.Expiration));
        }
        catch (Exception ex)
        {
            _log.LogError("An error occurred during login", ex);
            return StatusCode(500, new ErrorResponse(new List<string> { "An unexpected error occurred." }));
        }
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _mediator.Send(
                new RegisterCommand(
                    request.UserName,
                    request.Email,
                    request.Password,
                    request.FirstName,
                    request.LastName)
            );

            if ((bool)(!result.Succeeded)!)
            {
                var errorMessages = result.Errors ?.Select(e => e) ?? new List<string>();

                return BadRequest(new ErrorResponse(errorMessages));
            }

            return NoContent();
        }
        catch (Exception e)
        {
            _log.LogError("Error in AuthController", e);
            return StatusCode(500, new ErrorResponse(new List<string> { "Error while registering user" }));
        }
    }
    
    [HttpGet("verifyToken")]
    [Authorize]
    public async Task<IActionResult> VerifyToken()
    {
        return await Task.FromResult<IActionResult>(NoContent());
    }
    
    
    
    [HttpGet("send-reset-password-email")]
    public async Task<IActionResult> SendResetPasswordEmail([FromQuery] string email)
    {
        try
        {
            var result = await _mediator.Send(new SendResetPasswordEmailCommand()
            {
                Email = email
            });

            if (!result)
            {
                return BadRequest(new ErrorResponse(new List<string> { "Nie udało się wysłać emaila z linkiem do resetowania hasła." }));
            }

            return Ok();
        }
        catch (Exception e)
        {
            _log.LogError("Error in AuthController", e);
            return StatusCode(500, new ErrorResponse(new List<string> { "Error while sending reset password email" }));
        }
    }

    [HttpGet("reset-password")]
    public async Task<IActionResult> ResetPassword([FromQuery] string email, [FromQuery] string token, [FromQuery] string newPassword)
    {
        try
        {
            var result = await _mediator.Send(new ResetPasswordCommand()
            {
                Email = email,
                Token = token,
                NewPassword = newPassword
            });

            if (!result)
            {
                return BadRequest(new ErrorResponse(new List<string> { "Nie udało się zresetować hasła." }));
            }

            return Ok();
        }
        catch (Exception e)
        {
            _log.LogError("Error in AuthController", e);
            return StatusCode(500, new ErrorResponse(new List<string> { "Error while resetting password" }));
        }
        
    }


    [HttpGet("verify-email")]
    public async Task<IActionResult> VerifyEmail([FromQuery] string userEmail, [FromQuery] string token)
    {
        try
        {
            var result = await _mediator.Send(new ConfirmEmailCommand()
            {
                 userEmail = userEmail,
                 Token = token
                
            });
            
            if (!result)
            {
                return BadRequest(new ErrorResponse(new List<string> { "Nie udało się potwierdzić Emailu." }));
            }

            return Ok();
        }
        catch (Exception e)
        {
            _log.LogError("Error in AuthController", e);
            return StatusCode(500, new ErrorResponse(new List<string> { "Error while confirming email" }));
        }
    }
  

}