namespace Tichu
open System

type CardSet = 
    {card: Card; number: int}

module CardSet = 
    let CardSetToString(set: CardSet): string = 
        seq { for i in 1..set.number -> set.card.value} |> String.Concat

    let StringToCardSet(setstring: string): CardSet = 
        {card = {value = setstring[0]}; number = setstring.Length}

    let CardSetToCardList(set: CardSet): Card list = 
        [for i in 1..set.number -> set.card]

    let IsSameTypeAs(setOne: CardSet)(setTwo: CardSet): bool = 
        setOne.number = setTwo.number

    // Note that this function is used in the format: setTwo |> IsHigherThen(setOne)
    let IsHigherThen(setOne: CardSet)(setTwo: CardSet): bool = 
        if not (setOne |> IsSameTypeAs(setTwo)) then failwith "different types of card sets are incomparable"
        setTwo.card.IntValue() > setOne.card.IntValue()