namespace Application.Exceptions;

public class ObjectNotExistException : Exception
{
    public ObjectNotExistException()
        : base("Object does not exist.") 
    {
    }

    public ObjectNotExistException(string message)
        : base(message) 
    {
    }

    public ObjectNotExistException(string message, Exception innerException)
        : base(message, innerException) 
    {
    }
    
}