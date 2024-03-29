namespace Tichu

type TichuGame = 
    {players: Player list; lastPlay: Option<Card list * string>; turn: int; status: StatusText}

    member x.GetActivePlayer(): Player = x.players[x.turn]

    member x.TurnAfterHound(): int = 
        x.turn |> TichuGame.IncreasePlayerIndex |> TichuGame.IncreasePlayerIndex |> x.NextTurnHelper

    member x.NextTurn(): int = 
         x.turn |> TichuGame.IncreasePlayerIndex |> x.NextTurnHelper

    member x.NextTurnHelper(index: int): int = 
        if x.players[index] |> Player.isFinished then x.NextTurnHelper(index |> TichuGame.IncreasePlayerIndex)
        else index

    member x.TrickIsWonUponPass(): bool = x.TrickWonHelper(TichuGame.IncreasePlayerIndex x.turn)

    member x.TrickWonHelper(index: int): bool = 
        let (_, leaderName) = x.lastPlay.Value
        if x.players[index].name.Equals leaderName then true
        else if x.players[index] |> Player.isFinished then x.TrickWonHelper(index |> TichuGame.IncreasePlayerIndex)
        else false

    static member IncreasePlayerIndex(index: int) = (index + 1) % 4
    
module TichuGame = 
    
    let private CheckSameTypeAndHigher(lastSet: CardSet, newSet: CardSet) = 
        if not (newSet |> CardSet.IsSameTypeAs(lastSet)) then "You can only play sets of the same type as the leading set."
        else if not (newSet |> CardSet.IsHigherThen(lastSet)) then "Your card set has to be higher than the last played card set."
        else "OK"

    let private PlaySet(set: Card list)(tichu: TichuGame): TichuGame = 
        let updatedPlayer = tichu.GetActivePlayer() |> Player.PlayCards(set)
        let updatedPlayerList = tichu.players |> List.mapi(fun i p -> if i = tichu.turn then updatedPlayer else p)
        
        let status: StatusText = if updatedPlayer.hand.IsEmpty then Message(tichu.GetActivePlayer().name + " has played all their cards!") else NoText
        let nextTurn = if set.Equals([Hound]) then tichu.TurnAfterHound() else tichu.NextTurn()
        let lastPlayed = Phoenix.DeclareSet(set, tichu.lastPlay |> Option.map(fst))
        
        {players = updatedPlayerList; lastPlay = Some(lastPlayed, updatedPlayer.name); turn = nextTurn; status = status}

    let private TryPlaySet(set: Card list)(tichu: TichuGame): TichuGame = 
        let declaredSet = Phoenix.DeclareSet(set, tichu.lastPlay |> Option.map(fst))
        let alertText = 
            if (declaredSet |> CardSet.ToCardSet).Equals(Invalid) then "Invalid card set." 
            else 
                match tichu.lastPlay, declaredSet with 
                | None, _ | Some([Hound], _), _ -> "OK"
                | Some([Dragon], _), [Phoenix(_, _)] -> "Phoenix cannot be played over the dragon."
                | Some(lastSet, _), _ -> CheckSameTypeAndHigher(lastSet |> CardSet.ToCardSet, declaredSet |> CardSet.ToCardSet)
        match alertText with 
        | "OK" -> tichu |> PlaySet(set)
        | _ -> {tichu with status = Alert(alertText)}

    let private Pass(tichu: TichuGame): TichuGame = 
        if tichu.TrickIsWonUponPass() then
            let (_, leader) = tichu.lastPlay.Value
            let status = Message(leader + " has won the trick!")
            {tichu with lastPlay = None; turn = tichu.NextTurn(); status = status}
        else 
            {tichu with turn = tichu.NextTurn(); status = NoText}

    let private TryPass(tichu: TichuGame): TichuGame = 
        match tichu.lastPlay with 
        | None | Some([Hound], _) -> {tichu with status = Alert("You cannot pass when opening a trick.")}
        | _ -> tichu |> Pass
        

    let DoTurn(action: Action)(tichu: TichuGame): TichuGame = 
        match action with 
        | Pass -> tichu |> TryPass
        | Set(set) -> tichu |> TryPlaySet(set)