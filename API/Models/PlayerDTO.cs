using Tichu;

namespace api.Models
{
    public class PlayerDTO(ITichu tichu, string name)
    {
        public string Name { get; set; } = name;
        public string Hand { get; set; } = tichu.GetPlayerHand(name);
    }
}