using Tichu;

namespace api.Models
{
    public class TichuDTO(ITichu tichu)
    {
        public PlayerDTO Player { get; set; } = new PlayerDTO(tichu);
        public string LastPlayed { get; set; } = tichu.GetLastPlayed();
    }
}