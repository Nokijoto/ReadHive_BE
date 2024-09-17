using System.Security.Claims;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Infrastructure.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Services;

public class JwtTokenGenerator : IJwtGenerator
{
    private readonly IConfiguration _configuration;
    private readonly ILoggingService _Log;

    public JwtTokenGenerator(IConfiguration configuration, ILoggingService log)
    {
        _configuration = configuration;
        _Log = log;
    }

    public string GenerateToken(AppUser user)
    {
        try
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim("id", user.Id.ToString()),
                new Claim("username", user.UserName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddHours(2);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: expiration,
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        catch (Exception e)
        {
            _Log.LogError("Error generating token", e);
            throw;
        }
       
    }

    public Task<object> GenerateTokenAsync(AppUser user)
    {
        throw new NotImplementedException();
    }
}