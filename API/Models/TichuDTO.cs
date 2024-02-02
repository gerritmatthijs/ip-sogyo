using Tichu;

namespace api.Models
{
    public class TichuDTO(ITichu tichu)
    {
        public string Hand { get; set; } = tichu.GetPlayerHand("Gerrit");
        public string LastPlayed { get; set; } = tichu.GetLastPlayed();
    }
}