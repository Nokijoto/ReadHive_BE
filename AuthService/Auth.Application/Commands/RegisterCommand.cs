using Application.Models.Results;
using MediatR;

namespace Application.Commands;

public class RegisterCommand: IRequest<RegisterResult>
{
    public string UserName { get; }
    public string Email { get; }
    public string Password { get; }
    public string FirstName { get; }
    public string LastName { get; }

    public RegisterCommand(string userName, string email, string password, string firstName, string lastName)
    {
        UserName = userName;
        Email = email;
        Password = password;
        FirstName = firstName;
        LastName = lastName;
    }
}
