namespace Application.Exceptions;

public class UserNotFoundException : DomainException
{
    public UserNotFoundException(string message = "User not found") : base(message)
    {
    }
}