namespace Application.Exceptions;

public class RegisterFailedException : Exception
{
    private new static readonly string Message = "Error while registering user";
    public RegisterFailedException() : base(Message)
    {
    }
    
    public RegisterFailedException(string customMessage) : base(customMessage)
    {
    }
    
}