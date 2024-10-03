using Book.Infrastructure.Interfaces;
using Serilog;

namespace Book.Infrastructure.Services;

public class SerilogLoggingService : ILoggingService
{
    
    private readonly ILogger _logger;

    public SerilogLoggingService(ILogger logger)
    {
        _logger = logger ?? Log.ForContext<SerilogLoggingService>();
    }
    public void LogInformation(string message)
    {
        _logger.Information(message);
    }

    public void LogError(string message, Exception? ex)
    {
        if (ex == null)
        {
            _logger.Error(message);
        }
        else
        {
            _logger.Error(ex, message);
        }
    }
}