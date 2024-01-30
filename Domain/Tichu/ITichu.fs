namespace Tichu

type ITichu = 
    abstract member getPlayerHand: name: string -> string

    abstract member hasTurn: name: string -> bool

    abstract member getLastPlayed: unit -> string

    abstract member doTurn: name: string * set: string -> ITichu