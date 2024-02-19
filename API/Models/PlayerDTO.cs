using Tichu;

namespace api.Models
{
    public class PlayerDTO(ITichuFacade tichu, int i)
    {
        public string Name { get; set; } = tichu.GetPlayerName(i);
        public string Hand { get; set; } = tichu.GetPlayerHand(i);
    }
}