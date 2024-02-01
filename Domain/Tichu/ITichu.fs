namespace Tichu

type ITichu = 
    abstract member GetPlayerName: playerNumber: int -> string

    abstract member GetPlayerHand: name: string -> string

    abstract member GetLastPlayed: unit -> string

    abstract member HasTurn: name: string -> bool

    abstract member DoTurn: name: string * set: string -> Result<ITichu, string>
    
    abstract member IsEndOfGame: unit -> bool