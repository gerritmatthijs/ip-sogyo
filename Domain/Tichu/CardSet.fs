namespace Tichu
open System

type CardSet = 
    | Multiple of card: Card * number: int
    | NonExistant

module CardSet = 
    let CardSetToString(set: CardSet): string = 
        match set with
        | Multiple(card, number) -> seq { for i in 1..number -> card.value} |> String.Concat
        | NonExistant -> failwith "Cannot convert invalid CardSet to string."

    let IsValidSet(setstring: string): bool = 
        (setstring |> Seq.distinct |> Seq.length) = 1

    let StringToCardSet(setstring: string): CardSet = 
        if not (setstring |> IsValidSet) then NonExistant
        else Multiple({value = setstring[0]}, setstring.Length)

    let CardSetToCardList(set: CardSet): Card list = 
        match set with
        | Multiple (card, number) -> [for i in 1..number -> card]
        | NonExistant -> failwith "Cannot convert invalid cardset to card list."

    let IsSameTypeAs(setOne: CardSet)(setTwo: CardSet): bool = 
        match setOne, setTwo with
        | Multiple(_, numberOne), Multiple(_, numberTwo) -> numberOne = numberTwo
        | _, _ -> false

    // Note that this function is used in the format: setTwo |> IsHigherThen(setOne)
    let IsHigherThen(setOne: CardSet)(setTwo: CardSet): bool = 
        if not (setOne |> IsSameTypeAs(setTwo)) then failwith "different types of card sets are incomparable"
        
        match setOne, setTwo with
        | Multiple(cardOne, _), Multiple(cardTwo, _) -> cardTwo.IntValue() > cardOne.IntValue()
        | _, _ -> failwith "different types of card sets are incomparable"