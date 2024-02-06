export type TichuGameState = {
    players: Player[]
    lastPlayed: string
    currentLeader: string
    turn: number
}

export type Player = {
    name: string
    hand: string
}

export function isTichuGameState(gameState: unknown): gameState is TichuGameState {
    return (gameState as TichuGameState).players !== undefined;
}