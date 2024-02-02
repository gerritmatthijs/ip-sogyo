using Tichu;

namespace api.Models
{
    public class TichuDTO(ITichu tichu)
    {
        private string hand = tichu.GetPlayerHand("Gerrit");
        private string lastPlayed = tichu.GetLastPlayed();

        public string GetHand()
        {
            return hand;
        }

        public void SetHand(string hand)
        {
            this.hand = hand;
        }

        public string GetLastPleyd()
        {
            return lastPlayed;
        }

        public void SetLastPlayed(string lastPlayed)
        {
            this.lastPlayed = lastPlayed;
        }
    }
}