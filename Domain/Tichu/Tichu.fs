namespace Tichu

type TichuGame(hand: Card list, lastPlayed: Option<Card>) = 
    
    new(handstring: string, lastPlayed: Option<char>) = new TichuGame(Hand.StringToCardList(handstring), lastPlayed |> Option.map(fun c -> {value = c}))
       
    interface ITichu with
        member this.GetPlayerName(playerNumber: int): string = "Gerrit"

        member this.GetPlayerHand(name: string): string = 
            Hand.CardListToString(hand)

        member this.GetLastPlayed(): string = 
            match lastPlayed with 
            | None -> ""
            | Some(card) -> card.value.ToString()

        member this.HasTurn(name: string): bool = 
            failwith "Not Implemented"

        member this.DoTurn(name: string, setstring: string): ITichu = 
            let set = Hand.StringToCardList(setstring)
            let newHand = Hand.RemoveCards(hand, set)
            new TichuGame(newHand, Some(set[0]))


        member this.IsEndOfGame(): bool = 
            failwith "Not Implemented"
