using Microsoft.AspNetCore.Mvc;

namespace BookApi.Controllers;

[ApiController]
[Route("api/[controller]/")]
public abstract class BaseApiController : ControllerBase
{

}