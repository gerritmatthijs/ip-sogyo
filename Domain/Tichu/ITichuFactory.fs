namespace Tichu

type ITichuFactory = 
    abstract member CreateNewGame: playerNames: seq<string> -> ITichuFacade

    abstract member CreateExistingGame: playerNames: seq<string> * playerHands: seq<string> * 
        leader: string * lastPlayed: string * turn: int -> ITichuFacade

