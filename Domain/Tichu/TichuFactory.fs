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
    
    interface ITichuFactory with
        member this.createNewGame(playerNames: string seq): ITichu = 
            let hands = generateRandomInput() |> Seq.chunkBySize(13) |> Seq.map(Seq.map(fun c -> {value = c}) >> Seq.sortBy(fun card -> card.IntValue()) >> Seq.toList)
            let players = Seq.map2(fun name hand -> {name = name; hand = hand}) playerNames hands |> Seq.toList
            new TichuGame(players, None, 0)