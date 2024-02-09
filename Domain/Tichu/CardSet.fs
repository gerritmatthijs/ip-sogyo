namespace Tichu
open System

type CardSet = 
    {card: Card; number: int}

module CardSet = 
    let CardSetToString(set: CardSet): string = 
        seq { for i in 1..set.number -> set.card.value} |> String.Concat

    let IsValidSet(setstring: string): bool = 
        (setstring |> Seq.distinct |> Seq.length) = 1

    // Perhaps replace Option<CardSet> by Result<CardSet> later to avoid confusion with the lastPlayed Option.
    let StringToCardSet(setstring: string): Option<CardSet> = 
        if not (setstring |> IsValidSet) then None 
        else Some({card = {value = setstring[0]}; number = setstring.Length})

    let CardSetToCardList(set: CardSet): Card list = 
        [for i in 1..set.number -> set.card]

    let IsSameTypeAs(setOne: CardSet)(setTwo: CardSet): bool = 
        setOne.number = setTwo.number

    // Note that this function is used in the format: setTwo |> IsHigherThen(setOne)
    let IsHigherThen(setOne: CardSet)(setTwo: CardSet): bool = 
        if not (setOne |> IsSameTypeAs(setTwo)) then failwith "different types of card sets are incomparable"
        setTwo.card.IntValue() > setOne.card.IntValue()