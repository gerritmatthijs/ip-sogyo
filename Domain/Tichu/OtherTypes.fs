namespace Tichu

type StatusText = 
    | NoText
    | Alert of text: string
    | Message of text: string

type Action = 
    | Pass
    | Set of cards: Card

module Action = 

    let ToAction(actionstring: string): Action = 
        match actionstring with 
            | "pass" -> Pass
            | cardstring -> Set({value = cardstring[0]})

    let CheckAllowed(lastSet: Option<Card>)(action: Action): string = 
        match lastSet, action with
            | None, Pass -> "You cannot pass when opening a trick."
            | None, _ -> "OK"
            | _, Pass -> "OK"
            | Some(lastCard), Set(cardPlayed) -> 
                if cardPlayed.IntValue() > lastCard.IntValue() then "OK" else "Your card has to be higher than the last played card."