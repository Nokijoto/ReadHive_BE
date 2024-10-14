using Domain.Entities;
using Infrastructure.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Application.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Application.Commands;

public class SendResetPasswordEmailCommandHandler : IRequestHandler<SendResetPasswordEmailCommand, bool>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IMailService _mailService;
    private readonly IConfiguration _configuration;
    private readonly ILoggingService _log;

    public SendResetPasswordEmailCommandHandler(UserManager<AppUser> userManager, IMailService mailService, IConfiguration configuration, ILoggingService log)
    {
        _userManager = userManager;
        _mailService = mailService;
        _configuration = configuration;
        _log = log;
    }

    public async Task<bool> Handle(SendResetPasswordEmailCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null) return false;

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            await _mailService.SendEmailAsync(user.Email, "Resetowanie hasła", $"Kliknij w link, aby zresetować hasło: {_configuration["CLIENT_URL"]}/api/v1/Auth/reset-password?userId={user.Email}&token={Uri.EscapeDataString(token)}");
            _log.LogInformation($"Sending email to {user.Email}");
            return true;
        }
        catch (Exception e)
        {
            _log.LogError("Error in SendResetPasswordEmailCommandHandler", e);
            throw;
        }
    }
}