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
        array[..13] |> Array.toSeq |> Seq.sortBy(fun c -> {value = c}.IntValue()) |> String.Concat
    
    interface ITichuFactory with
        member this.createNewGame(playerNames: string seq): ITichu = 
            let hand = generateRandomInput()
            new TichuGame(playerNames |> Seq.head, hand, None)