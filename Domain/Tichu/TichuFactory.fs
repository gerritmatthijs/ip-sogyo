namespace Tichu
open System

type TichuFactory() = 
    let generateRandomInput(): String = 
        let allCards = "122223333444455556666777788889999TTTTJJJJQQQQKKKKAAAAPDH"
        let array = allCards |> Seq.toArray
        let random = Random()
        for i in 0 .. array.Length - 1 do
            let j = random.Next(i, array.Length)
            let pom = array.[i]
            array.[i] <- array.[j]
            array.[j] <- pom
        array |> Array.toSeq |> String.Concat

    interface ITichuFactory with
        member _.CreateNewGame(playerNames: string seq): ITichuFacade = 
            let playerList = playerNames |> Seq.toList
            let hands = generateRandomInput() |> Seq.chunkBySize(14) |> Seq.map(String.Concat) |> Seq.toList
            new TichuFacade(playerList, hands)
        
        member _.CreateExistingGame(playerNames: string seq, playerHands: string seq, leader: string, lastPlayed: string, turn: int): ITichuFacade = 
            let playerList = playerNames |> Seq.toList
            let handList = playerHands |> Seq.toList
            new TichuFacade(playerList, handList, leader, lastPlayed, turn)

        