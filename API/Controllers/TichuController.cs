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
    public IActionResult PlayCards(Dictionary<String, String> body)
    {
        String gameID = HttpContext.Session.GetString(SessionClientID) ?? throw new Exception("Game ID not found in session.");
        ITichu tichu = _repository.GetGame(gameID);
        ITichu newTichu  = tichu.DoTurn("Gerrit", body["card"]);
        Console.WriteLine("Old hand: " + tichu.GetPlayerHand("Gerrit"));
        Console.WriteLine("New hand: " + newTichu.GetPlayerHand("Gerrit"));
        _repository.SaveGame(gameID, newTichu);
        return Ok(new TichuDTO(newTichu));
    }

    [HttpPost("newgame")]
    [Consumes("application/json")]
    public IActionResult CreateGame(Dictionary<String, String> body)
    {
        ITichu tichu = _factory.createNewGame([body["name"]]);
        String gameID = Guid.NewGuid().ToString();
        HttpContext.Session.SetString(SessionClientID, gameID);
        _repository.SaveGame(gameID, tichu);
        return Ok(new TichuDTO(tichu));
    }
}
