namespace Tichu

type TichuFactory() = 
    interface ITichuFactory with
        member this.createNewGame(playerNames: seq<string>): ITichu = 
            new TichuGame("23357KA")
