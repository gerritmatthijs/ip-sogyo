using Tichu;

namespace api.Models
{
    public class PlayerDTO(ITichuFacade tichu, string name)
    {
        public string Name { get; set; } = name;
        public string Hand { get; set; } = tichu.GetPlayerHand(name);
    }
}