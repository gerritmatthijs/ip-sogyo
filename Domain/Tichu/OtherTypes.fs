namespace Tichu

type StatusText = 
    | NoText
    | Alert of text: string
    | Message of text: string

type Action = 
    | Pass
    | Set of cards: CardSet

module Action = 

    let ToAction(actionstring: string): Action = 
        match actionstring with 
            | "pass" -> Pass
            | cardstring -> Set(cardstring |> CardSet.StringToCardSet)

    let CheckAllowed(lastSet: Option<CardSet>)(action: Action): string = 
        match lastSet, action with
            | None, Pass -> "You cannot pass when opening a trick."
            | None, _ -> "OK"
            | _, Pass -> "OK"
            | Some(lastSet), Set(newSet) -> 
                if not (newSet |> CardSet.IsSameTypeAs(lastSet)) then $"You can only play sets of {lastSet.number} cards of the same height in this trick." else
                if newSet |> CardSet.IsHigherThen(lastSet) then "OK" else "Your card has to be higher than the last played card."