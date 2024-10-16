using Application.Commands;
using Application.Exceptions;
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
    private readonly IMediator _mediator;
    private readonly ILoggingService _log;

    public AuthController(IMediator mediator, ILoggingService log)
    {
        _mediator = mediator;
        _log = log;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        try
        {
            var result = await _mediator.Send(
                new LoginCommand(
                    request.Email, 
                    request.Password
                    ));

            if (!result.Succeeded)
            {
                _log.LogError($"An error occurred during login: {result.Errors}");
                return BadRequest(new ErrorResponse(result.Errors));
            }

            return Ok(new AuthResponse(result));
        }
        catch (Exception ex)
        {
            _log.LogError("An error occurred during login", ex);
            return StatusCode(500, new ErrorResponse(new List<string> { new InternalServerErrorException().Message}));
        }
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        try
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            
            var result = await _mediator.Send(
                new RegisterCommand(
                    request.UserName,
                    request.Email,
                    request.Password,
                    request.FirstName,
                    request.LastName)
            );

            if (!result.Succeeded)
            {
                var errorMessages = result.Errors?.Select(e => e.ToString()) ?? new List<string>();
                _log.LogError($"Registration failed for user {request.Email}");
                return BadRequest(new ErrorResponse(errorMessages));
            }

            return NoContent();

        }
        catch (Exception e)
        {
            _log.LogError("Error in AuthController", e);
            return StatusCode(500, new ErrorResponse(new List<string> { new InternalServerErrorException().Message}));
        }
    }
    
    //TODO: DELETE THIS IN FINAL VERSION
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
                return BadRequest(new ErrorResponse(new List<string> { new ErrorSendingEmailException().Message }));
            }

            return Ok();
        }
        catch (Exception e)
        {
            _log.LogError("Error in AuthController", e);
            return StatusCode(500, new ErrorResponse(new List<string> { new InternalServerErrorException().Message }));
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
            return StatusCode(500, new ErrorResponse(new List<string> { new InternalServerErrorException().Message }));
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
                return BadRequest(new ErrorResponse(new List<string> { "Nie udało się potwierdzić E-mailu." }));
            }

            return Ok();
        }
        catch (Exception e)
        {
            _log.LogError("Error in AuthController", e);
            return StatusCode(500, new ErrorResponse(new List<string> { new InternalServerErrorException().Message }));
        }
    }
  

}