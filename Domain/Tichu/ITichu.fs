namespace Tichu

type ITichu = 
    abstract member GetPlayerName: playerNumber: int -> string

    abstract member GetPlayerHand: name: string -> string

    abstract member GetLastPlayed: unit -> string

    abstract member GetCurrentLeader: unit -> string

    abstract member GetTurn: unit -> int

    abstract member CheckAllowed: set: string -> string

    abstract member DoTurn: name: string * set: string -> ITichu
    
    abstract member IsEndOfGame: unit -> bool