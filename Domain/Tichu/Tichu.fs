namespace Tichu

type TichuGame(players: Player list, lastPlayed: Option<Card>) = 
    
    let getPlayer(name: string): Player = 
        players |> List.find(fun player -> player.name.Equals name)

    // new(name: string, handstring: string, lastPlayed: Option<char>) = 
    //     let player = {name = name; hand = handstring |> Hand.StringToCardList}
    //     new TichuGame(player, lastPlayed |> Option.map(fun c -> {value = c}))
       
    interface ITichu with
        member this.GetPlayerName(playerNumber: int): string = 
            players[playerNumber].name

        member this.GetPlayerHand(name: string): string = 
            Hand.CardListToString(getPlayer(name).hand)

        member this.GetLastPlayed(): string = 
            match lastPlayed with 
            | None -> ""
            | Some(card) -> card.value.ToString()

        member this.HasTurn(name: string): bool = 
            failwith "Not Implemented"

        member this.CheckAllowed(setstring: string): string = 
            match lastPlayed with
            | None -> "OK"
            | Some(card) -> 
                let cardPlayed = {value = setstring[0]};
                if cardPlayed.IntValue() > card.IntValue() then "OK" else "Your card has to be higher than the last played card."


        member this.DoTurn(name: string, setstring: string): ITichu = 
            if not ((this :> ITichu).CheckAllowed(setstring) = "OK") 
                then failwith "Move not allowed: call CheckAllowed function first."
            let set = setstring |> Hand.StringToCardList 
            let currentPlayer = getPlayer name
            let newHand = currentPlayer.hand |> Hand.RemoveCards(set)
            let newCurrentPlayer = {currentPlayer with hand = newHand}
            new TichuGame(newCurrentPlayer, Some(set[0]))

        member this.IsEndOfGame(): bool = 
            failwith "Not Implemented"
