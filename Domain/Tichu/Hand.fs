namespace Tichu
module Hand = 

    let StringToCardList(handstring: string): Card list = 
        handstring |> Seq.map(fun c -> {value = c}) |> Seq.toList

    let CardListToString(hand: Card list): string = 
        System.String.Concat(hand |> List.map(fun card -> card.value))

    let rec RemoveCards(hand: Card list, set: Card list): Card list = 
        match hand, set with
        | hand, [] -> hand
        | handcard::tailhand, setcard::tailset -> if handcard.Equals(setcard) then RemoveCards(tailhand, tailset) else handcard::RemoveCards(tailhand, set)
        | [], _ -> failwith "Card set is not contained in hand."

    let RemoveCardsStringVersion(hand: string, set: string): string = 
        CardListToString(RemoveCards(StringToCardList(hand), StringToCardList(set)))