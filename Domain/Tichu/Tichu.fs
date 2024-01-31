namespace Tichu

type TichuGame(hand: Card list) = 
    
    new(handstring: string) = new TichuGame(handstring |> Seq.map(fun c -> {value = c}) |> Seq.toList)

    interface ITichu with
        member this.getPlayerName(playerNumber: int): string = "Gerrit"

        member this.getPlayerHand(name: string): string = 
            System.String.Concat(hand |> List.map(fun card -> card.value))

        member this.getLastPlayed(): string = 
            failwith "Not Implemented"

        member this.hasTurn(name: string): bool = 
            failwith "Not Implemented"

        member this.doTurn(name: string, set: string): ITichu = 
            failwith "Not Implemented"

        member this.isEndOfGame(): bool = 
            failwith "Not Implemented"
        