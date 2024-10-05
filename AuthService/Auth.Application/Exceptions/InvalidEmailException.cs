namespace Application.Exceptions;

public class InvalidEmailException : DomainException
{
    public InvalidEmailException(string message) : base($"Invalid email: {message}")
    {
    }
}