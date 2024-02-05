using Tichu;

namespace api.Models
{
    public class PlayerDTO(ITichu tichu)
    {
        public string Name { get; set; } = tichu.GetPlayerName(0);
        public string Hand { get; set; } = tichu.GetPlayerHand(Name);
    }
}