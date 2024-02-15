namespace Tichu
open System

type Card = 
    | Normal of value: char
    | Dragon 
    | Mahjong
    | Phoenix of cardCopied: Option<Card>
    | Hound

    member this.IntValue (): int = 
        match this with
            | Dragon -> 100
            | Mahjong -> 1
            | Hound -> 4949 // Just to always sort it on the right of the hand. The Hound never actually gets compared
            | Phoenix(cardCopied) -> 
                match cardCopied with 
                | None -> 99 
                | Some(card) ->  card.IntValue()
            | Normal(value) -> 
                match value with 
                | 'T' -> 10
                | 'J' -> 11
                | 'Q' -> 12
                | 'K' -> 13
                | 'A' -> 14
                | x -> int x - int '0'

    member this.CharValue (): char = 
        match this with 
            | Dragon -> 'D'
            | Mahjong -> '1'
            | Hound -> 'H'
            | Phoenix(_) -> 'P'
            | Normal(value) -> value


module Card = 
    let Card(value: char) = 
        if ['H'; '1'; '2'; '3'; '4'; '5'; '6'; '7'; '8'; '9'; 'T'; 'J'; 'Q'; 'K'; 'A'; 'P'; 'D'] |> List.contains(value) then
            match value with 
                | 'D' -> Dragon
                | '1' -> Mahjong
                | 'H' -> Hound
                | 'P' -> Phoenix(None)
                | x -> Normal(x)

        else failwith "Invalid card type"

    let GetSinglePhoenix(lastSet: Option<Card list>) = 
        match lastSet with 
            | None -> Phoenix(Some(Mahjong))
            | Some(set) -> Phoenix(Some(set[0]))

    let StringToCardList(handstring: string): Card list = 
        handstring |> Seq.map(fun c -> Card(c)) |> Seq.toList |> List.sortBy(fun card -> card.IntValue())

    let CardListToString(hand: Card list): string = 
        hand |> List.map(fun card -> card.CharValue()) |> String.Concat