namespace Tichu
module Hand = 

    let StringToCardList(handstring: string): Card list = 
        handstring |> Seq.map(fun c -> {value = c}) |> Seq.toList

    let CardListToString(hand: Card list): string = 
        System.String.Concat(hand |> List.map(fun card -> card.value))

    let rec RemoveCards(set: Card list)(hand: Card list): Card list = 
        match hand, set with
        | hand, [] -> hand
        | handcard::tailhand, setcard::tailset -> 
        if handcard.Equals(setcard) then RemoveCards(tailset)(tailhand) else handcard::RemoveCards(set)(tailhand)
        | [], _ -> failwith "Card set is not contained in hand."

    let RemoveCardsStringVersion(hand: string, set: string): string = 
        CardListToString(RemoveCards(StringToCardList(set))(StringToCardList(hand)))