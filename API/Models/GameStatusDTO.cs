using Tichu;

namespace api.Models
{
    public class GameStatusDTO(ITichu tichu)
    {
        public string Message { get; set; } = tichu.GetMessage();
        public string Alert { get; set; } = tichu.GetAlert();
        public bool EndOfGame { get; set; } = tichu.IsEndOfGame();
    }
}