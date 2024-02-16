namespace Tichu

type Action = 
    | Pass
    | Set of cards: Card list

module Action = 

    let ToAction(actionstring: string): Action = 
        match actionstring with 
            | "pass" -> Pass
            | setstring -> Set(setstring |> CardList.StringToCardList)
