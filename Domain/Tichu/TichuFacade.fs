namespace Tichu

// The TichuFacade implements the ITichu interface and is mostly meant to translate F#-specific objects to C#-suitable objects
type TichuFacade(tichu: TichuGame) = 

    new(players: Player list) = new TichuFacade({players = players; lastPlay = None; turn = 0})

    interface ITichu with
        member x.GetPlayerName(playerNumber: int): string = 
            tichu.players[playerNumber].name

        member x.GetPlayerHand(name: string): string = 
            Hand.CardListToString(tichu.GetPlayer(name).hand)

        member x.GetLastPlayed(): string = 
            match tichu.lastPlay with 
            | None -> ""
            | Some(card, _) -> card.value.ToString()

        member x.GetCurrentLeader(): string = 
            match tichu.lastPlay with
            | None -> ""
            | Some(_, player) -> player.name

        member x.GetTurn(): int = tichu.turn

        member x.CheckAllowed(action: string): string = 
            action |> Card.CheckAllowed(tichu.lastPlay |> Option.map(fun (card, _) -> card))

        member this.DoTurn(name: string, action: string): ITichu = 
            new TichuFacade(tichu |> TichuGame.DoTurn(name, action))

        member x.IsEndOfGame(): bool = 
            failwith "Not Implemented"