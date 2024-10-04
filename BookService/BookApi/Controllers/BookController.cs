using Book.Infrastructure.Interfaces;
using MediatR;

namespace BookApi.Controllers;

public class BookController : BaseApiController
{
    private readonly ISender _sender;
    private readonly ILoggingService _logger;
    public BookController(ILoggingService logger, ISender sender)
    {
        _logger = logger;
        _sender = sender;
    }

    
}