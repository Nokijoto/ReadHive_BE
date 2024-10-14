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
using Application.Interfaces;

namespace Application.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<AppUser> _userManager; 
    private readonly IConfiguration _configuration;
    private readonly ILoggingService _log;
    private readonly IMailService _mailService;
    private readonly bool _isDevelopment;


    public AuthService(UserManager<AppUser> userManager, IConfiguration configuration, ILoggingService log, IMailService mailService)
    {
        _userManager = userManager;
        _configuration = configuration;
        _log = log;
        _mailService = mailService;
        _isDevelopment = _configuration["APP_ENVIRONMENT"] == "Development";
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
            
            if (_isDevelopment)
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

            
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var confirmationLink = $"{_configuration["CLIENT_URL"]}/api/v1/Auth/verify-email?userEmail={user.Email}&token={Uri.EscapeDataString(token)}";
            var subject = "Potwierdzenie rejestracji";
            var body = $"Kliknij w link, aby potwierdzić swoje konto: <a href='{confirmationLink}'>Potwierdź konto</a>";

            await _mailService.SendEmailAsync(user.Email, subject, body);
            
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
        if (_isDevelopment && loginDto is { Email: "user@example.com", Password: "string" })
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

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT_SECRET_KEY"]));
        var credits = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expiration = DateTime.UtcNow.AddHours(1);

        if(_isDevelopment)
        {
            claims.Add(new Claim("IsDevelopment", "true"));
            expiration = DateTime.UtcNow.AddHours(4);
        }

        var token = new JwtSecurityToken(
            issuer: _configuration["JWT_ISSUER"],
            audience: _configuration["JWT_AUDIENCE"],
            claims: claims,
            expires: expiration,
            signingCredentials: credits);

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        return (tokenString, expiration);
    }
    
    public async Task<AuthenticationResultDto> SendPasswordResetAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
        {
            return new AuthenticationResultDto
            {
                Succeeded = false,
                Errors = new[] { new UserNotFoundException("Użytkownik o podanym e-mailu nie istnieje.") }
            };
        }

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        var resetLink = $"{_configuration["CLIENT_URL"]}/reset-password?userId={user.Id}&token={Uri.EscapeDataString(token)}";
        var subject = "Resetowanie hasła";
        var body = $"Kliknij w link, aby zresetować hasło: <a href='{resetLink}'>Resetuj hasło</a>";

        await _mailService.SendEmailAsync(user.Email, subject, body);

        return new AuthenticationResultDto
        {
            Succeeded = true,
        };
    }

    
    
    public async Task<AuthenticationResultDto> ResetPasswordAsync(ResetPasswordDto resetPasswordDto)
    {
        var user = await _userManager.FindByIdAsync(resetPasswordDto.UserId);
        if (user == null)
        {
            return new AuthenticationResultDto
            {
                Succeeded = false,
                Errors = new[] { new UserNotFoundException() }
            };
        }

        var result = await _userManager.ResetPasswordAsync(user, resetPasswordDto.Token, resetPasswordDto.NewPassword);

        if (!result.Succeeded)
        {
            return new AuthenticationResultDto
            {
                Succeeded = false,
                Errors = result.Errors.Select(e => new DomainException(e.Description))
            };
        }

        return new AuthenticationResultDto
        {
            Succeeded = true,
        };
    }

    
    
}