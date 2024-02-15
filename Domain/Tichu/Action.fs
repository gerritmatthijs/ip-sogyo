namespace Tichu

type Action = 
    | Pass
    | Set of cards: Card list

module Action = 

    let ToAction(actionstring: string): Action = 
        match actionstring with 
            | "pass" -> Pass
            | setstring -> Set(setstring |> Card.StringToCardList)

    let _CheckSameTypeAndHigher(lastSet: CardSet, newSet: CardSet) = 
        if not (newSet |> CardSet.IsSameTypeAs(lastSet)) then "You can only play sets of the same type as the leading set."
        else if not (newSet |> CardSet.IsHigherThen(lastSet)) then "Your card set has to be higher than the last played card set."
        else "OK"

    let _CheckInvalidSetPlayed(action: Action) = 
        match action with 
            | Pass -> false
            | Set(set) -> (set |> CardSet.ToCardSet).Equals(Invalid)

    let GetAlertTextOrOK(lastSet: Option<Card list>)(action: Action): string = 
        if _CheckInvalidSetPlayed(action) then "Invalid card set." 
        else 
        match lastSet, action with
            | Some([Hound]), Set(_) -> "OK"
            | Some(lastSet), Set(newSet) -> _CheckSameTypeAndHigher(lastSet |> CardSet.ToCardSet, newSet |> CardSet.ToCardSet)
            | None, Pass | Some([Hound]), Pass -> "You cannot pass when opening a trick."
            | None, _ | _, Pass -> "OK"
