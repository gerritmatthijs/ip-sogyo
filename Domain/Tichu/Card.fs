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
