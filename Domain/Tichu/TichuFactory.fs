namespace Tichu
open System

type TichuFactory() = 
    let generateRandomInput(): String = 
        let orderedHand = "22223333444455556666777788889999TTTTJJJJQQQQKKKKAAAA"
        let array = orderedHand |> Seq.toArray
        let random = Random()
        for i in 0 .. array.Length - 1 do
            let j = random.Next(i, array.Length)
            let pom = array.[i]
            array.[i] <- array.[j]
            array.[j] <- pom
        array |> Array.toSeq |> String.Concat
    
    let GetPlayers(names: string seq, hands: seq<list<Card>>): Player list = 
        Seq.map2(fun name hand -> {name = name; hand = hand}) names hands |> Seq.toList

    interface ITichuFactory with
        member _.CreateNewGame(playerNames: string seq): ITichuFacade = 
            let hands = generateRandomInput() |> Seq.chunkBySize(13) |> Seq.map(
                    Seq.map(fun c -> Card.Card(c)) >> Seq.sortBy(fun card -> card.IntValue()) >> Seq.toList
                )
            let players = GetPlayers(playerNames, hands) 
            new TichuFacade(players)
        
        member _.CreateExistingGame(playerNames: string seq, playerHands: string seq, leader: string, lastPlayed: string, turn: int): ITichuFacade = 
            let hands = playerHands |> Seq.map(Card.StringToCardList)
            let players = GetPlayers(playerNames, hands);
            new TichuFacade(players) // TODO finish

        