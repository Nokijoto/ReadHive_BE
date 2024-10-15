using Application.Commands;
using Application.Interfaces;
using Application.Models.Dto;
using Application.Models.Results;
using Infrastructure.Interfaces;
using MediatR;

namespace Application.Handlers;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand,RegisterResult>
{
    private readonly IAuthService _authService;
    private readonly ILoggingService _log;
    
    public RegisterCommandHandler(IAuthService authService, ILoggingService log)
    {
        _authService = authService;
        _log = log;
    }

    public async Task<RegisterResult> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var response = await _authService.RegisterAsync( new RegisterDto(request.UserName, request.Email, request.Password));
            var result = new RegisterResult(response.Succeeded);
            if (!response.Succeeded)
            {
                result.Errors = response.Errors?.Select(e => e.Message).ToList() ?? new List<string>();
            }

            return result;
        }
        catch (Exception e)
        {
            _log.LogError("Error in RegisterCommandHandler", e);
            return new RegisterResult(false, new List<string> { e.Message });
        }
    }
}