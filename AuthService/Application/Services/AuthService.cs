using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Exceptions;
using Application.Interfaces;
using Application.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Application.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<AppUser> _userManager; 
    private readonly IConfiguration _configuration;

    public AuthService(UserManager<AppUser> userManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
    }

    public async Task<AuthenticationResult> RegisterAsync(RegisterDto registerDto)
    {
        var user = new AppUser
        {
            UserName = registerDto.UserName,
            Email = registerDto.Email,
            // FirstName = registerDto.FirstName,
            // LastName = registerDto.LastName
        };

        var result = await _userManager.CreateAsync(user, registerDto.Password);

        if (!result.Succeeded)
        {
            return new AuthenticationResult
            {
                Succeeded = false,
                Errors = result.Errors.Select(e => new DomainException(e.Description))
            };
        }

        var tokenResult = await GenerateJwtToken(user);

        return new AuthenticationResult
        {
            Succeeded = true,
            Token = tokenResult.Token,
            Expiration = tokenResult.Expiration
        };
    }

    public async Task<AuthenticationResult> LoginAsync(LoginDto loginDto)
    {
        var user = await _userManager.FindByEmailAsync(loginDto.Email);
        if (user == null || !await _userManager.CheckPasswordAsync(user, loginDto.Password))
        {
            return new AuthenticationResult
            {
                Succeeded = false,
                Errors = new[] { new invalidCredentialsException("Invalid credentials") }
            };
        }

        var tokenResult = await GenerateJwtToken(user);

        return new AuthenticationResult
        {
            Succeeded = true,
            Token = tokenResult.Token,
            Expiration = tokenResult.Expiration
        };
    }
    
    
    private async Task<(string Token, DateTime Expiration)> GenerateJwtToken(AppUser user)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.UserName)
        };

        var userRoles = await _userManager.GetRolesAsync(user);
        claims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var expiration = DateTime.UtcNow.AddHours(2);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: expiration,
            signingCredentials: creds);

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        return (tokenString, expiration);
    }
}