namespace Tichu

type TichuGame = 
    {players: Player list; lastPlay: Option<CardSet * Player>; turn: int; status: StatusText}

    member x.GetPlayer(name: string): Player = 
        x.players |> List.find(fun player -> player.name.Equals name)

    member x.GetActivePlayer(): Player = x.players[x.turn]

    member x.NextTurn(): int = x.NextTurnHelper(TichuGame.IncreasePlayerIndex x.turn)

    member x.NextTurnHelper(index: int): int = 
        if x.players[index] |> Player.isFinished then x.NextTurnHelper(index |> TichuGame.IncreasePlayerIndex)
        else index

    member x.TrickIsWonUponPass(): bool = x.TrickWonHelper(TichuGame.IncreasePlayerIndex x.turn)

    member x.TrickWonHelper(index: int): bool = 
        let (_, leader) = x.lastPlay.Value
        if x.players[index].Equals leader then true
        else if x.players[index] |> Player.isFinished then x.TrickWonHelper(index |> TichuGame.IncreasePlayerIndex)
        else false

    static member IncreasePlayerIndex(index: int) = (index + 1) % 4
    
module TichuGame = 

    let PlaySet(set: CardSet)(tichu: TichuGame): TichuGame = 
        let updatedPlayer = tichu.GetActivePlayer() |> Player.PlayCards(set)
        let updatedPlayerList = tichu.players |> List.mapi(fun i p -> if i = tichu.turn then updatedPlayer else p)
        let status: StatusText = if updatedPlayer.hand.IsEmpty then Message(tichu.GetActivePlayer().name + " has played all their cards!") else NoText
        {players = updatedPlayerList; lastPlay = Some(set, updatedPlayer); turn = tichu.NextTurn(); status = status}

    let Pass(tichu: TichuGame): TichuGame = 
        if tichu.TrickIsWonUponPass() then
            let (_, leader) = tichu.lastPlay.Value
            let status = Message(leader.name + " has won the trick!")
            {tichu with lastPlay = None; turn = tichu.NextTurn(); status = status}
        else 
            {tichu with turn = tichu.NextTurn(); status = NoText}
        // let updatedLastPlay = if tichu.TrickIsWonUponPass() then None else tichu.lastPlay
        // let status: StatusText = if tichu.TrickIsWonUponPass() then Message(leader.name + " has won the trick!") else NoText
        // {tichu with lastPlay = updatedLastPlay; turn = tichu.NextTurn(); status = status}

    let DoTurn(action: Action)(tichu: TichuGame): TichuGame = 
        let errorStatus = action |> Action.CheckAllowed(tichu.lastPlay |> Option.map(fun (card, _) -> card))
        if not (errorStatus.Equals "OK" )
            then {tichu with status = Alert(errorStatus)} else

        match action with 
        | Pass -> tichu |> Pass
        | Set(set) -> tichu |> PlaySet(set)