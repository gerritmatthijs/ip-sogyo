using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("[controller]")]
public class TichuController : ControllerBase
{
    private readonly ILogger<TichuController> _logger;

    public TichuController(ILogger<TichuController> logger)
    {
        _logger = logger;
    }

    [HttpPost("play")]
    [Consumes("application/json")]
    public IActionResult GetGreeting(Dictionary<String, String> body)
    {
        return Ok("Hello " + body["name"] + "!");
    }
}
