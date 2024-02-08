export type TichuGameState = {
    players: Player[]
    gameStatus: GameStatus
    lastPlayed: string
    currentLeader: string
    turn: number
}

export type Player = {
    name: string
    hand: string
}

export type GameStatus = {
    message: string
    alert: string
}

export function isTichuGameState(gameState: unknown): gameState is TichuGameState {
    return (gameState as TichuGameState).players !== undefined;
}