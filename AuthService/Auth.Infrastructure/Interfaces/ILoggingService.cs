namespace Infrastructure.Interfaces;

public interface ILoggingService
{
    void LogInformation(string message);
    void LogError(string message, Exception ex);
    void LogError(string message);
}