namespace Application.Exceptions;

public class InternalServerErrorException : Exception
{
    private new const string Message = "Internal server error";

    public InternalServerErrorException() : base(Message)
    {
    }
}
