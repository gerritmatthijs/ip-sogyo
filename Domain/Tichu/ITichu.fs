namespace Tichu

type ITichu = 
    abstract member GetPlayerName: playerNumber: int -> string

    abstract member GetPlayerHand: name: string -> string

    abstract member GetLastPlayed: unit -> string

    abstract member HasTurn: name: string -> bool

    abstract member CheckAllowed: set: string -> string

    abstract member DoTurn: name: string * set: string -> ITichu
    
    abstract member IsEndOfGame: unit -> bool