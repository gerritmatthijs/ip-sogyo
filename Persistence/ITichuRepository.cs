namespace Persistence;
using Tichu;

public interface ITichuRepository
{
    public void SaveGame(string key, ITichu tichu);

    ITichu GetGame(string key);

    void DeleteGame(string key);

    Boolean ContainsGame(string key);
}
