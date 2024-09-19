using System.Threading.Tasks;
using Application.Models;

namespace Application.Interfaces;

public interface IAuthService
{
    Task<AuthenticationResultDto> RegisterAsync(RegisterDto registerDto);
    Task<AuthenticationResultDto> LoginAsync(LoginDto loginDto);
}