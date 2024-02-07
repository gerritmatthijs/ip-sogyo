namespace Tichu

type TichuGame(players: Player list, lastPlay: Option<Card * Player>, turn: int) = 
    
    let GetPlayer(name: string): Player = 
        players |> List.find(fun player -> player.name.Equals name)

    let IncreasePlayerIndex(index: int) = (index + 1) % 4

    let NextTurn(): int = 
        let nextPlayerIndex = turn |> IncreasePlayerIndex
        match players[nextPlayerIndex].hand.Length with
        | 0 -> 
                let opposingPlayerIndex = nextPlayerIndex |> IncreasePlayerIndex
                match players[opposingPlayerIndex].hand.Length with
                | 0 -> opposingPlayerIndex |> IncreasePlayerIndex
                | _ -> opposingPlayerIndex
        | _ -> nextPlayerIndex

    let NextPlayer(): Player = players[IncreasePlayerIndex turn]

    let OppositePlayer(): Player = players[turn |> IncreasePlayerIndex |> IncreasePlayerIndex]

    let PlaySet(name: string, setstring: string): ITichu = 
        let set = setstring |> Hand.StringToCardList 
        let updatedPlayer = GetPlayer name |> Player.PlayCards(set)
        let updatedPlayerList = players |> List.map(fun p -> if p.name.Equals name then updatedPlayer else p)
        new TichuGame(updatedPlayerList, Some(set[0], updatedPlayer), NextTurn())

    let rec TrickWonHelper(leader: Player, index: int): bool = 
        if players[index].Equals leader then true
        else if players[index] |> Player.isFinished then TrickWonHelper(leader, IncreasePlayerIndex index)
        else false

    let TrickWon(leader: Player): bool = TrickWonHelper(leader, IncreasePlayerIndex turn)

    let Pass(name: string): ITichu = 
        match lastPlay with
        | None -> failwith "Cannot pass when starting a trick."
        | Some(_, leader) -> 
            let updatedLastPlay = if TrickWon(leader) then None else lastPlay
            new TichuGame(players, updatedLastPlay, NextTurn())

    // Is there a way to make this a let binding (i.e. private function), while still being able to call an interface member?
    member this.checkErrors(name: string, setstring: string): unit = 
        if not (players[turn].name.Equals name)
            then failwith "It is not allowed to play out of turn."
        if not ((this :> ITichu).CheckAllowed(setstring) = "OK") 
            then failwith "Move not allowed: call CheckAllowed function first."
       
    interface ITichu with
        member x.GetPlayerName(playerNumber: int): string = 
            players[playerNumber].name

        member x.GetPlayerHand(name: string): string = 
            Hand.CardListToString(GetPlayer(name).hand)

        member x.GetLastPlayed(): string = 
            match lastPlay with 
            | None -> ""
            | Some(card, _) -> card.value.ToString()

        member x.GetCurrentLeader(): string = 
            match lastPlay with
            | None -> ""
            | Some(_, player) -> player.name

        member x.GetTurn(): int = turn

        member x.CheckAllowed(action: string): string = 
            match lastPlay, action with
            | None, "pass" -> "You cannot pass when opening a trick."
            | None, _ -> "OK"
            | _, "pass" -> "OK"
            | Some(card, _), setstring -> 
                let cardPlayed = {value = setstring[0]};
                if cardPlayed.IntValue() > card.IntValue() then "OK" else "Your card has to be higher than the last played card."

        member this.DoTurn(name: string, action: string): ITichu = 
            this.checkErrors(name, action)

            match action with 
            | "pass" -> Pass(name)
            | setstring -> PlaySet(name, setstring)


        member x.IsEndOfGame(): bool = 
            failwith "Not Implemented"