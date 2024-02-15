namespace Persistence;
using Tichu;

public class TichuRepositoryInMemory : ITichuRepository
{
    private readonly Dictionary<string, ITichuFacade> storage = [];

    public void SaveGame(string key, ITichuFacade tichu)
    {
        if (!storage.TryAdd(key, tichu)){
            storage[key] = tichu;
        }
    }

    public ITichuFacade GetGame(string key)
    {
        return storage[key];
    }

    public void DeleteGame(string key)
    {
        storage.Remove(key);
    }

    public bool ContainsGame(string key)
    {
        return storage.ContainsKey(key);
    }
}