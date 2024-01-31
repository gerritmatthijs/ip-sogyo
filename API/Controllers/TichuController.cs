using Microsoft.AspNetCore.Mvc;
// If the Tichu import fails to resolve in Visual Studio Code, build the project and restart VS Code.
using Tichu;
using Persistence;

namespace api.Controllers;

[ApiController]
[Route("[controller]")]
public class TichuController(ITichuRepository repository) : ControllerBase
{
    private readonly ITichuRepository _repository = repository;

    [HttpPost("play")]
    [Consumes("application/json")]
    public IActionResult GetGreeting(Dictionary<String, String> body)
    {
        return Ok("You played card " + body["card"] + "!");
    }
}
