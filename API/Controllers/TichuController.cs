using Microsoft.AspNetCore.Mvc;
// If the Tichu import fails to resolve in Visual Studio Code, build the project and restart VS Code.
using Tichu;
using Persistence;
using api.Models;

namespace api.Controllers;

[ApiController]
[Route("[controller]")]
public class TichuController(ITichuRepository DBrepository, ITichuRepository memoryRepository, ITichuFactory factory) : ControllerBase
{
    private readonly ITichuRepository _DBrepository = DBrepository;
    private readonly ITichuRepository _memoryRepository = memoryRepository;
    private readonly ITichuFactory _factory = factory;
    private const string SessionClientID = "_ClientId"; 

    [HttpPost("play")]
    [Consumes("application/json")]
    public IActionResult PlayCards(Dictionary<string, string> body)
    {
        string gameID = HttpContext.Session.GetString(SessionClientID) ?? throw new Exception("Game ID not found in session.");
        ITichuFacade tichu = GetGameFromMemoryOrDB(gameID);

        ITichuFacade newTichu  = tichu.DoTurn(body["action"]);
        if (newTichu.IsEndOfGame())
        {
            _DBrepository.DeleteGame(gameID);
            _memoryRepository.DeleteGame(gameID);
            HttpContext.Session.Remove(SessionClientID);
        }
        else 
        {
            _memoryRepository.SaveGame(gameID, newTichu);
            _DBrepository.SaveGame(gameID, newTichu);
        }
        return Ok(new TichuDTO(newTichu, gameID));
    }

    private ITichuFacade GetGameFromMemoryOrDB(string gameID)
    {
        if (_memoryRepository.ContainsGame(gameID))
        {
            return _memoryRepository.GetGame(gameID);
        }
        return _DBrepository.GetGame(gameID);
        
    }

    [HttpPost("check")]
    [Consumes("application/json")]
    public IActionResult ParseCardSelection(Dictionary<string, string> body)
    {
        string gameID = HttpContext.Session.GetString(SessionClientID) ?? throw new Exception("Game ID not found in session.");
        ITichuFacade tichu = GetGameFromMemoryOrDB(gameID);

        return Ok(new TichuDTO(tichu.DoTurn(body["action"]), gameID));
    }

    [HttpPost("getgame")]
    [Consumes("application/json")]
    public IActionResult GetGame(Dictionary<string, string> body)
    {
        string gameID = body["gameID"];
        ITichuFacade tichu = GetGameFromMemoryOrDB(gameID);
        HttpContext.Session.SetString(SessionClientID, gameID);

        return Ok(new TichuDTO(tichu, gameID));
    }

    [HttpPost("newgame")]
    [Consumes("application/json")]
    public IActionResult CreateGame(Dictionary<string, string> body)
    {
        ITichuFacade tichu = _factory.CreateNewGame(body["names"].Split(","));
        string gameID = Guid.NewGuid().ToString();
        HttpContext.Session.SetString(SessionClientID, gameID);
        _DBrepository.SaveGame(gameID, tichu);
        _memoryRepository.SaveGame(gameID, tichu);

        return Ok(new TichuDTO(tichu, gameID));
    }
}
