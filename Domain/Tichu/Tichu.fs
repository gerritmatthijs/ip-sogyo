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

        member this.CheckAllowed(name: string, setstring: string): string = 
            match lastPlayed with
            | None -> "OK"
            | Some(card) -> 
                let cardPlayed = {value = setstring[0]};
                if cardPlayed.IntValue() > card.IntValue() then "OK" else "Your card has to be higher than the last played card."


        member this.DoTurn(name: string, setstring: string): ITichu = 
            if not ((this :> ITichu).CheckAllowed(name, setstring) = "OK") 
                then failwith "Move not allowed: call CheckAllowed function first."
            let set = Hand.StringToCardList(setstring)
            let newHand = Hand.RemoveCards(hand, set)
            new TichuGame(newHand, Some(set[0]))

        member this.IsEndOfGame(): bool = 
            failwith "Not Implemented"
