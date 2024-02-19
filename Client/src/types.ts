export type TichuGameState = {
    players: Player[]
    gameStatus: GameStatus
    lastPlayed: string
    currentLeader: string
    turn: number
    gameID: string
}

export type Player = {
    name: string
    hand: string
}

export type GameStatus = {
    message: string
    alert: string
    endOfGame: boolean
}

export function isTichuGameState(gameState: unknown): gameState is TichuGameState {
    return (gameState as TichuGameState).players !== undefined;
}

export function isTichuGameStatus(gameStatus: unknown): gameStatus is GameStatus {
    return (gameStatus as GameStatus).message != undefined;
}