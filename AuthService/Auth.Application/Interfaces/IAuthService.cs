using Application.Models.Dto;

namespace Application.Interfaces;

public interface IAuthService
{
    Task<AuthenticationResultDto> RegisterAsync(RegisterDto registerDto);
    Task<AuthenticationResultDto> LoginAsync(LoginDto loginDto);
    
    Task<AuthenticationResultDto> SendPasswordResetAsync(string email);
    Task<AuthenticationResultDto> ResetPasswordAsync(ResetPasswordDto resetPasswordDto);
}