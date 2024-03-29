﻿namespace Tichu

type ITichuFacade = 
    abstract member GetPlayerName: playerNumber: int -> string

    abstract member GetPlayerHand: playerNumber: int -> string

    abstract member GetLastPlayed: unit -> string

    abstract member GetCurrentLeader: unit -> string

    abstract member GetTurn: unit -> int

    abstract member GetMessage: unit -> string

    abstract member GetAlert: unit -> string
    
    abstract member IsEndOfGame: unit -> bool
    
    abstract member DoTurn: string -> ITichuFacade