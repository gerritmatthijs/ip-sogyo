namespace Tichu

type TichuGame(hand: Card list) = 
    
    new(handstring: string) = new TichuGame(Hand.StringToCardList(handstring))
       
    interface ITichu with
        member this.GetPlayerName(playerNumber: int): string = "Gerrit"

        member this.GetPlayerHand(name: string): string = 
            Hand.CardListToString(hand)

        member this.GetLastPlayed(): string = 
            failwith "Not Implemented"

        member this.HasTurn(name: string): bool = 
            failwith "Not Implemented"

        member this.DoTurn(name: string, setstring: string): Result<ITichu, string> = 
            let set = Hand.StringToCardList(setstring)
            let newHand = Hand.RemoveCards(hand, set)
            Ok(new TichuGame(newHand))


        member this.IsEndOfGame(): bool = 
            failwith "Not Implemented"
