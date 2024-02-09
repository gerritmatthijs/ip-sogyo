namespace Tichu

type Action = 
    | Pass
    | Invalid
    | Set of cards: CardSet

module Action = 

    let ToAction(actionstring: string): Action = 
        match actionstring with 
            | "pass" -> Pass
            | cardstring -> 
                match cardstring |> CardSet.StringToCardSet with 
                | None -> Invalid
                | Some(set) -> Set(set)

    let GetAlertTextOrOK(lastSet: Option<CardSet>)(action: Action): string = 
        match lastSet, action with
            | _, Invalid -> "Invalid set type: you can only play multiples of the same card height"
            | None, Pass -> "You cannot pass when opening a trick."
            | None, _ -> "OK"
            | _, Pass -> "OK"
            | Some(lastSet), Set(newSet) -> 
                if not (newSet |> CardSet.IsSameTypeAs(lastSet)) then $"You can only play sets of {lastSet.number} cards of the same height in this trick." else
                if newSet |> CardSet.IsHigherThen(lastSet) then "OK" else "Your card set has to be higher than the last played card set."