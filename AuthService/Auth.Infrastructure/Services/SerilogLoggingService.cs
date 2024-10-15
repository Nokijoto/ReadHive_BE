using Infrastructure.Interfaces;
using Serilog;

namespace Infrastructure.Services;

public class SerilogLoggingService : ILoggingService
{
    
    private readonly ILogger _logger;

    public SerilogLoggingService()
    {
        _logger = Log.ForContext<SerilogLoggingService>();
    }
    public void LogInformation(string message)
    {
        _logger.Information(message);
    }

    public void LogError(string message, Exception ex)
    {
        _logger.Error(ex, message);
    }
    public void LogError(string message)
    {
        _logger.Error(message);
    }
}