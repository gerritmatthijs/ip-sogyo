namespace Tichu
open System

type CardSet = 
    | Multiple of card: Card * number: int
    | FullHouse of triple: Card * double: Card
    | NonExistant

module CardSet = 
    let _getCounts(cards: Card list): list<int> = 
        cards |> List.countBy(fun x -> x) |> List.map(fun (x, y) -> y) |> List.sort

    let _IsMultiple(cards: Card list): bool = 
        _getCounts(cards) |> List.length = 1

    let _IsFullHouse(cards: Card list): bool = 
        _getCounts(cards).Equals([2; 3]) 

    let ToCardSet(cards: Card list): CardSet = 
        if (cards |> _IsMultiple) then Multiple(cards[0], cards.Length)
        else if (cards |> _IsFullHouse) then 
            let double = if cards[0].Equals cards[2] then cards[4] else cards[0]
            FullHouse(cards[2], double)
        else NonExistant

    let IsSameTypeAs(setOne: CardSet)(setTwo: CardSet): bool = 
        match setOne, setTwo with
        | Multiple(_, numberOne), Multiple(_, numberTwo) -> numberOne = numberTwo
        | FullHouse(_, _), FullHouse(_, _) -> true
        | _, _ -> false

    // Note that this function is used in the format: setTwo |> IsHigherThen(setOne)
    let IsHigherThen(setOne: CardSet)(setTwo: CardSet): bool = 
        if not (setOne |> IsSameTypeAs(setTwo)) then failwith "different types of card sets are incomparable"
        
        match setOne, setTwo with
        | Multiple(cardOne, _), Multiple(cardTwo, _) -> cardTwo.IntValue() > cardOne.IntValue()
        | _, _ -> failwith "different types of card sets are incomparable"