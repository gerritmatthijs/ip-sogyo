namespace Persistence;
using Tichu;

public interface ITichuRepository
{
    public void SaveGame(string key, ITichuFacade tichu);

    ITichuFacade GetGame(string key);

    void DeleteGame(string key);
}
