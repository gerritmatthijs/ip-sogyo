namespace Tichu

type TichuGame(players: Player list, lastPlayed: Option<Card>) = 
    
    let getPlayer(name: string): Player = 
        players |> List.find(fun player -> player.name.Equals name)
       
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
            let updatedPlayer = getPlayer name |> Player.PlayCards(set)
            let updatePlayerList = players |> List.map(fun p -> if p.name.Equals name then updatedPlayer else p)
            new TichuGame(updatePlayerList, Some(set[0]))

        member this.IsEndOfGame(): bool = 
            failwith "Not Implemented"
