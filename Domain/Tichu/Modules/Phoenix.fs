namespace Tichu

module Phoenix = 

    let private GetSinglePhoenixDeclaration(lastSet: Option<Card list>) = 
        match lastSet with 
            | None -> Phoenix(Some(Mahjong), true)
            | Some(set) -> Phoenix(Some(set[0]), true)

    let private CreatesValidSetWith(set: Card list)(card: Card): bool = 
        let joinedSet = card::set |> CardList.Sort
        match CardSet.ToCardSet(joinedSet) with 
        | Invalid | Multiple(_, 4) -> false
        | _ -> true

    let private GetPhoenixDeclarationInSet(set: Card list): Card = 
        // Here the card list is the remainder of the set without the phoenix
        let possibleCards = "23456789TJQKA" |> CardList.StringToCardList
        try 
            let declarationValue = possibleCards |> List.findBack(CreatesValidSetWith(set))
            Phoenix(Some(declarationValue), false)
        with 
        | :? System.Collections.Generic.KeyNotFoundException -> Phoenix(None, false)

    let DeclareSet(set: Card list, lastSet: Option<Card list>): Card list = 
        if set.Equals([Phoenix(None, true)]) then [GetSinglePhoenixDeclaration(lastSet)]
        else if set |> List.contains(Phoenix(None, true)) then 
                let setWithoutPhoenix = set |> List.removeAt(set.Length - 1)
                let declaredPhoenix = GetPhoenixDeclarationInSet(setWithoutPhoenix)
                declaredPhoenix::setWithoutPhoenix |> CardList.Sort
        else set
    