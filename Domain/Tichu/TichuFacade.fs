namespace Tichu

// The TichuFacade implements the ITichu interface and is mostly meant to translate F#-specific objects to strings and vice versa
type TichuFacade(tichu: TichuGame) = 

    let GetPlayers(names: string seq, hands: seq<list<Card>>): Player list = 
        Seq.map2(fun name hand -> {name = name; hand = hand}) names hands |> Seq.toList

    new(playerNames: string list, playerHands: string list, leader: string, lastPlayed: string, turn: int) = 
        let hands = playerHands |> Seq.map(Card.StringToCardList)
        let players = Seq.map2(fun name hand -> {name = name; hand = hand}) playerNames hands |> Seq.toList
        let lastPlay = if leader.Length = 0 then None else Some(lastPlayed |> Card.StringToCardList, leader)
        new TichuFacade({players = players; lastPlay= lastPlay; turn = turn; status = NoText})

    new(playerNames: string list, playerHands: string list) = 
        let hands = playerHands |> Seq.map(Card.StringToCardList)
        let players = Seq.map2(fun name hand -> {name = name; hand = hand}) playerNames hands |> Seq.toList
        new TichuFacade({players = players; lastPlay = None; turn = 0; status = NoText})

    // TODO adapt Tichu tests so that the next constuctor can be removed
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
            | Some(_, playerName) -> playerName

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