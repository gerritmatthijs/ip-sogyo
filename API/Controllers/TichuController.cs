using Microsoft.AspNetCore.Mvc;
// If the Tichu import fails to resolve in Visual Studio Code, build the project and restart VS Code.
using Tichu;
using Persistence;
using api.Models;

namespace api.Controllers;

[ApiController]
[Route("[controller]")]
public class TichuController(ITichuRepository repository, ITichuFactory factory) : ControllerBase
{
    private readonly ITichuRepository _repository = repository;
    private readonly ITichuFactory _factory = factory;
    private const string SessionClientID = "_ClientId"; 

    [HttpPost("play")]
    [Consumes("application/json")]
    public IActionResult PlayCards(Dictionary<string, string> body)
    {
        string gameID = HttpContext.Session.GetString(SessionClientID) ?? throw new Exception("Game ID not found in session.");
        ITichuFacade tichu = _repository.GetGame(gameID);

        ITichuFacade newTichu  = tichu.DoTurn(body["action"]);
        _repository.SaveGame(gameID, newTichu);
        return Ok(new TichuDTO(newTichu));
    }

    [HttpPost("check")]
    [Consumes("application/json")]
    public IActionResult CheckAllowed(Dictionary<string, string> body)
    {
        string gameID = HttpContext.Session.GetString(SessionClientID) ?? throw new Exception("Game ID not found in session.");
        ITichuFacade tichu = _repository.GetGame(gameID);
        bool result = tichu.CheckAllowed(body["action"]);

        return Ok(tichu.CheckAllowed(body["action"]));
    }

    [HttpPost("newgame")]
    [Consumes("application/json")]
    public IActionResult CreateGame(Dictionary<string, string> body)
    {
        ITichuFacade tichu = _factory.createNewGame(body["names"].Split(","));
        string gameID = HttpContext.Session.GetString(SessionClientID) ?? Guid.NewGuid().ToString();
        HttpContext.Session.SetString(SessionClientID, gameID);
        _repository.SaveGame(gameID, tichu);
        return Ok(new TichuDTO(tichu));
    }
}
