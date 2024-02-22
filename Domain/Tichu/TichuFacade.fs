namespace Tichu

// The TichuFacade implements the ITichu interface and is mostly meant to translate F#-specific objects to strings and vice versa
type TichuFacade(tichu: TichuGame) = 

    new(playerNames: string list, playerHands: string list, leader: string, lastPlayed: string, turn: int) = 
        let hands = playerHands |> List.map(CardList.StringToCardList)
        let players = List.map2(fun name hand -> {name = name; hand = hand}) playerNames hands
        let lastPlay = if leader.Length = 0 then None else Some(lastPlayed |> CardList.StringToCardList, leader)
        new TichuFacade({players = players; lastPlay= lastPlay; turn = turn; status = NoText})

    new(playerNames: string list, playerHands: string list) = 
        let hands = playerHands |> List.map(CardList.StringToCardList)
        let players = List.map2(fun name hand -> {name = name; hand = hand}) playerNames hands
        let startingTurn = [0;1;2;3] |> List.find(fun i -> (hands[i] |> List.contains(Mahjong)))
        new TichuFacade({players = players; lastPlay = None; turn = startingTurn; status = NoText})


    interface ITichuFacade with
        member _.GetPlayerName(playerNumber: int): string = 
            tichu.players[playerNumber].name

        member _.GetPlayerHand(playerNumber: int): string = 
            CardList.CardListToString(tichu.players[playerNumber].hand)

        member _.GetLastPlayed(): string = 
            match tichu.lastPlay with 
            | None -> ""
            | Some(set, _) -> set |> CardList.CardListToString

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