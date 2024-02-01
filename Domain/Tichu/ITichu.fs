namespace Tichu

type ITichu = 
    abstract member getPlayerName: playerNumber: int -> string

    abstract member getPlayerHand: name: string -> string

    abstract member getLastPlayed: unit -> string

    abstract member hasTurn: name: string -> bool

    abstract member doTurn: name: string * set: string -> Result<ITichu, string>
    
    abstract member isEndOfGame: unit -> bool