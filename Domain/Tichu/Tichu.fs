namespace Tichu

type TichuGame(players: Player list, lastPlayed: Option<Card>, turn: int) = 
    
    let getPlayer(name: string): Player = 
        players |> List.find(fun player -> player.name.Equals name)

    member this.checkErrors(name: string, setstring: string): unit = 
        if not (players[turn].name.Equals name)
            then failwith "It is not allowed to play out of turn."
        if not ((this :> ITichu).CheckAllowed(setstring) = "OK") 
            then failwith "Move not allowed: call CheckAllowed function first."
       
    interface ITichu with
        member this.GetPlayerName(playerNumber: int): string = 
            players[playerNumber].name

        member this.GetPlayerHand(name: string): string = 
            Hand.CardListToString(getPlayer(name).hand)

        member this.GetLastPlayed(): string = 
            match lastPlayed with 
            | None -> ""
            | Some(card) -> card.value.ToString()

        member this.GetTurn(): int = turn

        member this.CheckAllowed(setstring: string): string = 
            match lastPlayed with
            | None -> "OK"
            | Some(card) -> 
                let cardPlayed = {value = setstring[0]};
                if cardPlayed.IntValue() > card.IntValue() then "OK" else "Your card has to be higher than the last played card."

        member this.DoTurn(name: string, setstring: string): ITichu = 
            this.checkErrors(name, setstring)

            let set = setstring |> Hand.StringToCardList 
            let updatedPlayer = getPlayer name |> Player.PlayCards(set)
            let updatedPlayerList = players |> List.map(fun p -> if p.name.Equals name then updatedPlayer else p)
            new TichuGame(updatedPlayerList, Some(set[0]), (turn + 1) % 4)

        member this.IsEndOfGame(): bool = 
            failwith "Not Implemented"
