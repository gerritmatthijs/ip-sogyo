namespace Tichu

// [<CustomComparison>]
type Card = 
    {value: char}
    member this.IntValue () = 
        match this.value with 
        | 'T' -> 10
        | 'J' -> 11
        | 'Q' -> 12
        | 'K' -> 13
        | 'A' -> 14
        | x -> int x - int '0'

module Card = 
    let CheckAllowed(lastSet: Option<Card>)(action: string): string = 
        match lastSet, action with
            | None, "pass" -> "You cannot pass when opening a trick."
            | None, _ -> "OK"
            | _, "pass" -> "OK"
            | Some(card), setstring -> 
                let cardPlayed = {value = setstring[0]};
                if cardPlayed.IntValue() > card.IntValue() then "OK" else "Your card has to be higher than the last played card."

    // interface IComparable with
    //     member this.CompareTo other = 
    //         match other with
    //         | :? Card as c -> this.intValue().CompareTo(c.intValue())
    //         | _ -> failwith "Cannot compare card to non-card object"