using Application.Models.Results;
using MediatR;

namespace Application.Commands;

public  class LoginCommand : IRequest<AuthResult>
{
    public string Email { get; set; }
    public string Password { get; set; }
    public LoginCommand(string email, string password) 
    {
        Email = email;
        Password = password;
    }
}