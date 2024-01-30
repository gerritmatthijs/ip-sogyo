﻿namespace Tichu

type ITichu = 
    abstract member getPlayerName: playerNumber: int -> string

    abstract member getPlayerHand: name: string -> string

    abstract member hasTurn: name: string -> bool

    abstract member getLastPlayed: unit -> string

    abstract member doTurn: name: string * set: string -> ITichu
    
    abstract member isEndOfGame: unit -> bool