using Domain.Entities;

namespace Domain.Interfaces;

public interface IJwtGenerator
{
    string GenerateToken(AppUser user);
    Task<object> GenerateTokenAsync(AppUser user);
}