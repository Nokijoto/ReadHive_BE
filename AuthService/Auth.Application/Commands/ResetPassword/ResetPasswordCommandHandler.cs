using Domain.Entities;
using Infrastructure.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Commands;

public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, bool>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly ILoggingService _log;

    public ResetPasswordCommandHandler(UserManager<AppUser> userManager, ILoggingService log)
    {
        _userManager = userManager;
        _log = log;
    }

    public async Task<bool> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        try
        {
            // Znajdź użytkownika na podstawie adresu email.
            var user = await _userManager.FindByEmailAsync(request.Email);
            _log.LogInformation($"Resetting password for user {request.Email}");

            if (user == null)
            {
                _log.LogInformation($"User not found with email: {request.Email}");
                return false; // Użytkownik nie istnieje.
            }
        
            // Resetowanie hasła.
            _log.LogInformation($"User : {user},    TOKEN : {request.Token} , PASSWD : {request.NewPassword}  ");
            var result = await _userManager.ResetPasswordAsync(user, Uri.UnescapeDataString(request.Token), request.NewPassword);

            // Sprawdzenie, czy resetowanie hasła się powiodło.
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                _log.LogError($"Failed to reset password for user {request.Email}. Errors: {errors}",null);
                return false;
            }

            _log.LogInformation($"Password reset successful for user {request.Email}.");
            return true; // Resetowanie hasła zakończone sukcesem.
        }
        catch (Exception e)
        {
            _log.LogError("Error in ResetPasswordCommandHandler", e);
            return false;
        }
    }
}