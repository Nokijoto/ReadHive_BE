namespace Application.Exceptions;

public class ObjectAlreadyExistException : Exception
{
    public ObjectAlreadyExistException()
        : base("Object already exists.") 
    {
    }

    public ObjectAlreadyExistException(string message)
        : base(message) 
    {
    }

    public ObjectAlreadyExistException(string message, Exception innerException)
        : base(message, innerException) 
    {
    }
}