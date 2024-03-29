namespace Tichu

type CardSet = 
    | Multiple of card: Card * number: int
    | FullHouse of triple: Card // Since we only use CardSets to compare sets, we don't need to know what the pair is.
    | Straight of lowest: Card * length: int
    | SubsequentPairs of lowest: Card * length: int
    | Invalid

module CardSet = 
    let private getCounts(cards: Card list): list<int> = 
        cards |> List.countBy(fun x -> x) |> List.map(fun (x, y) -> y) |> List.sort

    let private IsMultiple(cards: Card list): bool = 
        getCounts(cards) |> List.length = 1

    let private IsFullHouse(cards: Card list): bool = 
        getCounts(cards).Equals([2; 3]) 

    let rec private checkConsecutive(cards: Card list): bool = 
        match cards.Tail with
        | [] -> true
        | second::_ -> int(second.NumericValue()) - int(cards.Head.NumericValue()) = 1 && checkConsecutive(cards.Tail)

    let private IsStraight(cards: Card list): bool = 
        cards.Length >= 5 && checkConsecutive(cards)

    let private IsSubsequentPairs(cards: Card list): bool = 
        let distinctCounts = getCounts(cards) |> List.distinct 
        if not (cards.Length >= 4 && distinctCounts[0] = 2 && distinctCounts.Length = 1) then false
        else checkConsecutive(List.distinct cards)
        
    let private replacePhoenix(card: Card): Card = 
        match card with 
        | Phoenix(Some(declaredCard), false) -> declaredCard
        | _ -> card

    let ToCardSet(cards: Card list): CardSet = 
        let declaredCards = cards |> List.map(replacePhoenix)
        if (declaredCards |> IsMultiple) then Multiple(declaredCards[0], cards.Length)
        else if (declaredCards |> IsFullHouse) then FullHouse(declaredCards[2])
        else if (declaredCards |> IsStraight) then Straight(declaredCards.Head, cards.Length)
        else if (declaredCards |> IsSubsequentPairs) then SubsequentPairs(declaredCards.Head, cards.Length/2)
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
        | Multiple(cardOne, _), Multiple(cardTwo, _) -> cardTwo.NumericValue() > cardOne.NumericValue()
        | FullHouse(cardOne), FullHouse(cardTwo) -> cardTwo.NumericValue() > cardOne.NumericValue()
        | Straight(lowestOne, _), Straight(lowestTwo, _) -> lowestTwo.NumericValue() > lowestOne.NumericValue()
        | SubsequentPairs(lowestOne, _), SubsequentPairs(lowestTwo, _) -> lowestTwo.NumericValue() > lowestOne.NumericValue()
        | _, _ -> failwith "different types of card sets are incomparable" // This line cannot be reached
