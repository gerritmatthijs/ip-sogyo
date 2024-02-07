namespace Tichu

type TichuGame = 
    {players: Player list; lastPlay: Option<Card * Player>; turn: int}

    member x.GetPlayer(name: string): Player = 
        x.players |> List.find(fun player -> player.name.Equals name)

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

    let PlaySet(name: string, setstring: string)(tichu: TichuGame): TichuGame = 
        let set = setstring |> Hand.StringToCardList 
        let updatedPlayer = tichu.GetPlayer name |> Player.PlayCards(set)
        let updatedPlayerList = tichu.players |> List.map(fun p -> if p.name.Equals name then updatedPlayer else p)
        {players = updatedPlayerList; lastPlay = Some(set[0], updatedPlayer); turn = tichu.NextTurn()}

    let Pass(name: string)(tichu: TichuGame): TichuGame = 
        match tichu.lastPlay with
        | None -> failwith "Cannot pass when starting a trick."
        | Some(_, _) -> 
            let updatedLastPlay = if tichu.TrickIsWonUponPass() then None else tichu.lastPlay
            {tichu with lastPlay = updatedLastPlay; turn = tichu.NextTurn()}

    // Is there a way to make this a let binding (i.e. private function), while still being able to call an interface member?
    let checkErrors(name: string, action: string)(tichu: TichuGame): unit = 
        if not (tichu.players[tichu.turn].name.Equals name)
            then failwith "It is not allowed to play out of turn."
        if not (action |> Card.CheckAllowed(tichu.lastPlay |> Option.map(fun (card, _) -> card)) = "OK") 
            then failwith "Move not allowed: call CheckAllowed function first."

    let DoTurn(name: string, action: string)(tichu: TichuGame): TichuGame = 
        tichu |> checkErrors(name, action)

        match action with 
        | "pass" -> tichu |> Pass(name)
        | setstring -> tichu |> PlaySet(name, setstring)