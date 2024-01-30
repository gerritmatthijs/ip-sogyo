namespace Tichu

type ITichuFactory = 
    abstract member createNewGame: playerNames: list<string> -> ITichu

