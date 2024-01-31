namespace Tichu

// [<CustomComparison>]
type Card = 
    {value: char}
    member this.intValue () = 
        match this.value with 
        | 'T' -> 10
        | 'J' -> 11
        | 'Q' -> 12
        | 'K' -> 13
        | 'A' -> 14
        | x -> int x - int '0'

    // interface IComparable with
    //     member this.CompareTo other = 
    //         match other with
    //         | :? Card as c -> this.intValue().CompareTo(c.intValue())
    //         | _ -> failwith "Cannot compare card to non-card object"