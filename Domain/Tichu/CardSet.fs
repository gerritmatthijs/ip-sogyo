namespace Tichu
open System

type CardSet = 
    | Multiple of card: Card * number: int
    | FullHouse of triple: Card * double: Card
    | NonExistant

module CardSet = 
    let CardSetToString(set: CardSet): string = 
        match set with
        | Multiple(card, number) -> seq { for i in 1..number -> card.value} |> String.Concat
        | FullHouse(triple, double) -> 
            if triple.IntValue() > double.IntValue() 
                then seq {double.value; double.value; triple.value; triple.value; triple.value} |> String.Concat
            else seq {triple.value; triple.value; triple.value; double.value; double.value} |> String.Concat
        | NonExistant -> failwith "Cannot convert invalid CardSet to string."

    let _getCounts(str: string): list<int> = 
        str |> Seq.countBy(fun x -> x) |> Seq.map(fun (x, y) -> y) |> Seq.sort |> Seq.toList

    let _IsMultiple(setstring: string): bool = 
        _getCounts(setstring) |> List.length = 1

    let _IsFullHouse(setstring: string): bool = 
        _getCounts(setstring).Equals([2; 3]) 

    let StringToCardSet(setstring: string): CardSet = 
        if (setstring |> _IsMultiple) then Multiple(Card.Card(setstring[0]), setstring.Length)
        else if (setstring |> _IsFullHouse) then 
            let double = if setstring[0] = setstring[2] then Card.Card(setstring[4]) else Card.Card(setstring[0])
            FullHouse(Card.Card(setstring[2]), double)
        else NonExistant

    let CardSetToCardList(set: CardSet): Card list = 
        match set with
        | Multiple (card, number) -> [for i in 1..number -> card]
        | NonExistant -> failwith "Cannot convert invalid cardset to card list."

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