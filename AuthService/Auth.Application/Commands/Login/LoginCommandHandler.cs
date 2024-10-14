using Application.Interfaces;
using Application.Models.Dto;
using Application.Models.Results;
using Infrastructure.Interfaces;
using MediatR;

namespace Application.Commands.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthResult>
{
    private readonly IAuthService _authService;
    private readonly ILoggingService _log;
    public LoginCommandHandler(IAuthService authService, ILoggingService log)
    {
        _authService = authService;
        _log = log;
    }

    public async Task<AuthResult> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
        {
            return new AuthResult(false, string.Empty, null)
            {
                Errors = new List<string> { "Email or Password cannot be empty" }
            };
        }

        try
        {
            var response = await _authService.LoginAsync(new LoginDto(request.Email, request.Password));
            var result = new AuthResult(response.Succeeded, response.Token, response.Expiration);

            if (!response.Succeeded)
            {
                result.Errors = response.Errors?.Select(e => e.Message).ToList() ?? new List<string>();
            }
            return result;
        }
        catch (Exception e)
        {
            _log.LogError("Error in LoginCommandHandler", e);
            return new AuthResult(false, String.Empty, null);
        }
    }
}