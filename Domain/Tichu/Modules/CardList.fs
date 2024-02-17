namespace Tichu
open System

module CardList = 

    let rec RemoveCards(set: Card list)(hand: Card list): Card list = 
        match hand, set with
        | hand, [] -> hand
        | handcard::tailhand, setcard::tailset -> 
        if handcard.Equals setcard then RemoveCards tailset tailhand else handcard::RemoveCards set tailhand
        | [], _ -> failwith "Card set is not contained in hand."

    let Sort(set: Card list) : Card list = 
        set |> List.sortBy(fun card -> card.NumericValue())

    let StringToCardList(handstring: string): Card list = 
        handstring |> Seq.map(fun c -> Card.Card(c)) |> Seq.toList |> Sort

    let CardListToString(hand: Card list): string = 
        hand |> List.map(fun card -> card.CharValue()) |> String.Concat