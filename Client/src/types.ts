export type TichuGameState = {
    player: Player
    lastPlayed: string
}

export type Player = {
    name: string
    hand: string
}

export function isTichuGameState(gameState: unknown): gameState is TichuGameState {
    return (gameState as TichuGameState).player !== undefined;
}