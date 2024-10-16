namespace Application.Exceptions;

public class ErrorSendingEmailException : Exception
{
    private new const string Message = "Nie udało się wysłać emaila z linkiem do resetowania hasła.";

    public ErrorSendingEmailException() : base(Message)
    {
    }
}