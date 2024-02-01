namespace Tichu

type TichuGame(hand: Card list) = 
    
    new(handstring: string) = new TichuGame(Hand.StringToCardList(handstring))
       
    interface ITichu with
        member this.getPlayerName(playerNumber: int): string = "Gerrit"

        member this.getPlayerHand(name: string): string = 
            Hand.CardListToString(hand)

        member this.getLastPlayed(): string = 
            failwith "Not Implemented"

        member this.hasTurn(name: string): bool = 
            failwith "Not Implemented"

        member this.doTurn(name: string, setstring: string): Result<ITichu, string> = 
            let set = Hand.StringToCardList(setstring)
            let newHand = Hand.RemoveCards(hand, set)
            Ok(new TichuGame(newHand))


        member this.isEndOfGame(): bool = 
            failwith "Not Implemented"
