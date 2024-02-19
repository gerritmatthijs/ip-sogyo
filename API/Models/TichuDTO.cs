using Tichu;

namespace api.Models
{
    public class TichuDTO
    {
        public PlayerDTO[] Players { get; set; } = new PlayerDTO[4];
        public GameStatusDTO GameStatus { get; set; }
        public string LastPlayed { get; set; }
        public string CurrentLeader { get; set; }
        public int Turn { get; set; }
        public string GameID{ get; set; }

        public TichuDTO(ITichuFacade tichu, string gameID){
            for (int i = 0; i < 4; i++){
                Players[i] = new PlayerDTO(tichu, i);
            }
            GameStatus = new GameStatusDTO(tichu);
            LastPlayed = tichu.GetLastPlayed();
            CurrentLeader = tichu.GetCurrentLeader();
            Turn = tichu.GetTurn();
            GameID = gameID;
        }
    }
}