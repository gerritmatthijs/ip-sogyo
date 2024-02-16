namespace Tichu

type Card = 
    | Normal of value: char
    | Dragon 
    | Mahjong
    | Phoenix of cardDeclared: Option<Card> * isSingle: bool
    | Hound

    member this.NumericValue (): float = 
        match this with
            | Dragon -> 100
            | Mahjong -> 1
            | Hound -> 4949 // Just to always sort it on the right of the hand. The Hound never actually gets compared
            | Phoenix(cardDeclared, isSingle) -> 
                match cardDeclared, isSingle with 
                | None, _ -> 99 
                | Some(card), false ->  card.NumericValue()
                | Some(card), true -> card.NumericValue() + 0.5
            | Normal(value) -> 
                match value with 
                | 'T' -> 10
                | 'J' -> 11
                | 'Q' -> 12
                | 'K' -> 13
                | 'A' -> 14
                | x -> float (int x - int '0')

    member this.CharValue (): char = 
        match this with 
            | Dragon -> 'D'
            | Mahjong -> '1'
            | Hound -> 'H'
            | Phoenix(_) -> 'P'
            | Normal(value) -> value


module Card = 
    let Card(value: char) = 
        if ['1'; '2'; '3'; '4'; '5'; '6'; '7'; '8'; '9'; 'T'; 'J'; 'Q'; 'K'; 'A'; 'P'; 'D'; 'H'] |> List.contains(value) then
            match value with 
                | 'D' -> Dragon
                | '1' -> Mahjong
                | 'H' -> Hound
                | 'P' -> Phoenix(None, true)
                | x -> Normal(x)

        else failwith "Invalid card type"
