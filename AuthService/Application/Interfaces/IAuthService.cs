using Application.Models;

namespace Application.Interfaces;

public interface IAuthService
{
    Task<AuthenticationResult> RegisterAsync(RegisterDto registerDto);
    Task<AuthenticationResult> LoginAsync(LoginDto loginDto);
}