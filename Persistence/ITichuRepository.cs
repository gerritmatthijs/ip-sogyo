namespace Persistence;
using Tichu;

public interface ITichuRepository
{
    public void SaveGame(String key, ITichu tichu);

    ITichu GetGame(String key);

    void DeleteGame(String key);

    Boolean ContainsGame(String key);
}
