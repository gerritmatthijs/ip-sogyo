namespace Tichu

type ITichuFactory = 
    abstract member createNewGame: playerNames: seq<string> -> ITichuFacade

