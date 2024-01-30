namespace Persistence;
using Tichu;

public class TichuRepositoryInMemory : ITichuRepository
{
    private readonly Dictionary<string, ITichu> storage = [];

    public void SaveGame(string key, ITichu tichu)
    {
        if (storage.ContainsKey(key)){
            storage.Add(key, tichu);
        }
        else {
            storage[key] = tichu;
        }
        Console.WriteLine(storage);
    }

    public ITichu GetGame(string key)
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