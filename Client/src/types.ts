export type TichuGameState = {
    hand: string
    lastPlayed: string
}

export function isTichuGameState(gameState: unknown): gameState is TichuGameState {
    return (gameState as TichuGameState).hand !== undefined;
}