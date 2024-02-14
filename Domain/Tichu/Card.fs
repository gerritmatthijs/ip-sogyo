namespace Tichu

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
    let Card(value: char) = 
        if ['2'; '3'; '4'; '5'; '6'; '7'; '8'; '9'; 'T'; 'J'; 'Q'; 'K'; 'A'] |> List.contains(value) 
            then {value = value}
        else failwith "Invalid card type"

    let StringToCardList(handstring: string): Card list = 
        handstring |> Seq.map(fun c -> Card(c)) |> Seq.toList |> List.sortBy(fun card -> card.IntValue())

    let CardListToString(hand: Card list): string = 
        hand |> List.map(fun card -> card.value) |> System.String.Concat