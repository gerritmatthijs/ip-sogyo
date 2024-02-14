namespace Tichu

type CardSet = 
    | Multiple of card: Card * number: int
    | FullHouse of triple: Card // Since we only use CardSets to compare sets, we don't need to know what the pair is.
    | Straight of lowest: Card * length: int
    | SubsequentPairs of lowest: Card * length: int
    | Invalid

module CardSet = 
    let _getCounts(cards: Card list): list<int> = 
        cards |> List.countBy(fun x -> x) |> List.map(fun (x, y) -> y) |> List.sort

    let _IsMultiple(cards: Card list): bool = 
        _getCounts(cards) |> List.length = 1

    let _IsFullHouse(cards: Card list): bool = 
        _getCounts(cards).Equals([2; 3]) 

    let rec _checkConsecutive(cards: Card list): bool = 
        match cards.Tail with
        | [] -> true
        | second::remainder -> second.IntValue() - cards.Head.IntValue() = 1 && _checkConsecutive(cards.Tail)

    let _IsStraight(cards: Card list): bool = 
        cards.Length >= 5 && _checkConsecutive(cards)

    let _IsSubsequentPairs(cards: Card list): bool = 
        let distinctCounts = _getCounts(cards) |> List.distinct 
        if not (cards.Length >= 4 && distinctCounts[0] = 2 && distinctCounts.Length = 1) then false
        else _checkConsecutive(List.distinct cards)
        
    let ToCardSet(cards: Card list): CardSet = 
        if (cards |> _IsMultiple) then Multiple(cards[0], cards.Length)
        else if (cards |> _IsFullHouse) then FullHouse(cards[2])
        else if (cards |> _IsStraight) then Straight(cards.Head, cards.Length)
        else if (cards |> _IsSubsequentPairs) then SubsequentPairs(cards.Head, cards.Length/2)
        else Invalid

    let IsSameTypeAs(setOne: CardSet)(setTwo: CardSet): bool = 
        match setOne, setTwo with
        | Multiple(_, numberOne), Multiple(_, numberTwo) -> numberOne = numberTwo
        | FullHouse(_), FullHouse(_) -> true
        | Straight(_, lengthOne), Straight(_, lengthTwo) -> lengthOne = lengthTwo
        | SubsequentPairs(_, lengthOne), SubsequentPairs(_, lengthTwo) -> lengthOne = lengthTwo
        | _, _ -> false

    // Note that this function is used in the format: setTwo |> IsHigherThen(setOne)
    let IsHigherThen(setOne: CardSet)(setTwo: CardSet): bool = 
        if not (setOne |> IsSameTypeAs(setTwo)) then failwith "different types of card sets are incomparable"
        
        match setOne, setTwo with
        | Multiple(cardOne, _), Multiple(cardTwo, _) -> cardTwo.IntValue() > cardOne.IntValue()
        | FullHouse(cardOne), FullHouse(cardTwo) -> cardTwo.IntValue() > cardOne.IntValue()
        | Straight(lowestOne, _), Straight(lowestTwo, _) -> lowestTwo.IntValue() > lowestOne.IntValue()
        | SubsequentPairs(lowestOne, _), SubsequentPairs(lowestTwo, _) -> lowestTwo.IntValue() > lowestOne.IntValue()
        | _, _ -> failwith "different types of card sets are incomparable"