namespace Application.Exceptions;

public class invalidCredentialsException : DomainException
{
    public invalidCredentialsException(string message) : base($"Invalid credentials: {message}")
    {
    }
}