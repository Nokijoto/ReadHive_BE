namespace Book.Infrastructure.Interfaces;

public interface ILoggingService
{
    void LogInformation(string message);
    void LogError(string message, Exception? ex);
    void LogWarn(string message, Exception? ex);
    void LogDebug(string message, Exception? ex);
  
    
}