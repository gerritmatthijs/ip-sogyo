namespace Tichu

// The TichuFacade implements the ITichu interface and is mostly meant to translate F#-specific objects to strings and vice versa
type TichuFacade(tichu: TichuGame) = 

    new(players: Player list) = new TichuFacade({players = players; lastPlay = None; turn = 0; status = NoText})

    interface ITichuFacade with
        member _.GetPlayerName(playerNumber: int): string = 
            tichu.players[playerNumber].name

        member _.GetPlayerHand(name: string): string = 
            Card.CardListToString(tichu.GetPlayer(name).hand)

        member _.GetLastPlayed(): string = 
            match tichu.lastPlay with 
            | None -> ""
            | Some(set, _) -> set |> Card.CardListToString

        member _.GetCurrentLeader(): string = 
            match tichu.lastPlay with
            | None -> ""
            | Some(_, player) -> player.name

        member _.GetTurn(): int = tichu.turn

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

        member x.DoTurn(actionstring: string): ITichuFacade = 
            let action = actionstring |> Action.ToAction
            new TichuFacade(tichu |> TichuGame.DoTurn(action))