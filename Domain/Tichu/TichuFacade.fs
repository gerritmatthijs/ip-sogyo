namespace Tichu

// The TichuFacade implements the ITichu interface and is mostly meant to translate F#-specific objects to C#-suitable objects
type TichuFacade(tichu: TichuGame) = 

    new(players: Player list) = new TichuFacade({players = players; lastPlay = None; turn = 0; status = NoText})

    interface ITichu with
        member _.GetPlayerName(playerNumber: int): string = 
            tichu.players[playerNumber].name

        member _.GetPlayerHand(name: string): string = 
            Hand.CardListToString(tichu.GetPlayer(name).hand)

        member _.GetLastPlayed(): string = 
            match tichu.lastPlay with 
            | None -> ""
            | Some(card, _) -> card.value.ToString()

        member _.GetCurrentLeader(): string = 
            match tichu.lastPlay with
            | None -> ""
            | Some(_, player) -> player.name

        member _.GetTurn(): int = tichu.turn

        member _.DoTurn(actionstring: string): ITichu = 
            printfn $"action received from server: {actionstring}." 
            let action = actionstring |> Action.ToAction
            new TichuFacade(tichu |> TichuGame.DoTurn(action))

        member _.GetMessage(): string = 
            match tichu.status with
                | Message(text) -> text
                | _ -> ""

        member _.GetAlert(): string = 
            match tichu.status with
                | Alert(text) -> text
                | _ -> ""
    
        member _.IsEndOfGame(): bool = 
            (tichu.players |> List.filter(Player.isFinished)).Length = 3