namespace Tichu

type TichuFactory() = 
    interface ITichuFactory with
        member this.createNewGame(playerNames: string list): ITichu = 
            new TichuGame("23357KA")
