using Tichu;

namespace api.Models
{
    public class TichuDTO
    {
        public PlayerDTO[] Players { get; set; } = new PlayerDTO[4];
        public string LastPlayed { get; set; }

        public TichuDTO(ITichu tichu){
            for (int i = 0; i < 4; i++){
                Players[i] = new PlayerDTO(tichu, tichu.GetPlayerName(i));
            }
            LastPlayed = tichu.GetLastPlayed();
        }
    }
}