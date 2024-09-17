using Application.Models;

namespace Domain.Interfaces;

public interface IAuthService
{
    Task<AuthenticationResult> RegisterAsync(RegisterDto registerDto);
    Task<AuthenticationResult> LoginAsync(LoginDto loginDto);
}