using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Exceptions;
using Application.Interfaces;
using Application.Models.Dto;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Domain.Entities;
using Infrastructure.Interfaces;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Application.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<AppUser> _userManager; 
    private readonly IConfiguration _configuration;
    private readonly ILoggingService _log;

    public AuthService(UserManager<AppUser> userManager, IConfiguration configuration, ILoggingService log)
    {
        _userManager = userManager;
        _configuration = configuration;
        _log = log;
    }

    public async Task<AuthenticationResultDto> RegisterAsync(RegisterDto registerDto)
    {
        try
        {
            var user = new AppUser
            {
                UserName = registerDto.UserName,
                Email = registerDto.Email,
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsDeleted = false,
                DeletedAt = null,
                LockoutEnabled = false,
                LockoutEnd = null,
                NormalizedUserName = registerDto.UserName.ToUpper(),
                NormalizedEmail = registerDto.Email.ToUpper(),
                EmailConfirmed = false,
                LastLoginDate = null,
                PhoneNumber = null,
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString(),
                PasswordHash = null,
                AgreedToPrivacyPolicy = false,
                AgreedToTermsAndConditions = false,
                IsActive = true,
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                IsSuspended = false,
                SuspensionReason = null,
                AccessFailedCount = 0,
                ProfilePictureUrl = null,
            };
            
            if (_configuration["AppEnvironment"] == "Development")
            {
                user.EmailConfirmed= true;
                user.IsActive = true;
            }
            
            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
            {
                return new AuthenticationResultDto
                {
                    Succeeded = false,
                    Errors = result.Errors.Select(e => new DomainException(e.Description))
                };
            }

            var tokenResult = await GenerateJwtToken(user);

            return new AuthenticationResultDto
            {
                Succeeded = true,
                Token = tokenResult.Token,
                Expiration = tokenResult.Expiration,
            };
        }
        catch (Exception e)
        {
            _log.LogError("Error in AuthService", e);
            return new AuthenticationResultDto{ Succeeded = false, Errors = new List<Exception> { e } };
        }
    }

    public async Task<AuthenticationResultDto> LoginAsync(LoginDto loginDto)
    {
        if ( _configuration["AppEnvironment"] == "Development" && (loginDto.Email == "user@example.com" && loginDto.Password == "string"))
        {
            var tokenResult2 = await GenerateJwtToken(new AppUser
            {
                UserName = "string",
                Email = "string"
            });

            return new AuthenticationResultDto
            {
                Succeeded = true,
                Token = tokenResult2.Token,
                Expiration = tokenResult2.Expiration
            };
        }
        
        var user = await _userManager.FindByEmailAsync(loginDto.Email);
        if (user == null || !await _userManager.CheckPasswordAsync(user, loginDto.Password))
        {
            return new AuthenticationResultDto
            {
                Succeeded = false,
                Errors = new[] { new InvalidCredentialsException("Email or Password is Incorrect") },
            };
        }

        var tokenResult = await GenerateJwtToken(user);

        return new AuthenticationResultDto
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
        var credits = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var expiration = DateTime.UtcNow.AddHours(4);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: expiration,
            signingCredentials: credits);

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        return (tokenString, expiration);
    }
}