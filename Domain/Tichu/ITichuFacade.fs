namespace Tichu

type ITichuFacade = 
    abstract member GetPlayerName: playerNumber: int -> string

    abstract member GetPlayerHand: name: string -> string

    abstract member GetLastPlayed: unit -> string

    abstract member GetCurrentLeader: unit -> string

    abstract member GetTurn: unit -> int

    abstract member CheckAllowed: string -> bool

    abstract member DoTurn: string -> ITichuFacade

    abstract member GetMessage: unit -> string

    abstract member GetAlert: unit -> string
    
    abstract member IsEndOfGame: unit -> bool