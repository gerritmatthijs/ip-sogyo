namespace Tichu

type TichuGame(hand: string) = 
    

    interface ITichu with
        member this.getPlayerName(playerNumber: int) = "Gerrit"

        member this.getPlayerHand(name: string): string = 
            hand

        member this.getLastPlayed(): string = 
            failwith "Not Implemented"

        member this.hasTurn(name: string): bool = 
            failwith "Not Implemented"

        member this.doTurn(name: string, set: string): ITichu = 
            failwith "Not Implemented"

        member this.isEndOfGame(): bool = 
            failwith "Not Implemented"
        