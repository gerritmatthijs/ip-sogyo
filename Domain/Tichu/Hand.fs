namespace Tichu
module Hand = 

    let rec RemoveCards(set: Card list)(hand: Card list): Card list = 
        match hand, set with
        | hand, [] -> hand
        | handcard::tailhand, setcard::tailset -> 
        if handcard.Equals setcard then RemoveCards tailset tailhand else handcard::RemoveCards set tailhand
        | [], _ -> failwith "Card set is not contained in hand."