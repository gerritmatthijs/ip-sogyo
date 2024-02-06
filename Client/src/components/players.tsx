import { useTichuContext } from "../context/TichuGameContext"

type Props = {
    index: number
}

export const Player = (props: Props) => {
    const { index } = props;
    const { gameState } = useTichuContext();
    const player = gameState?.players[index];
    const name = player? player.name : "";
    const { columnStart, columnEnd, rowStart, rowEnd } = getGridPlacement(index);
    return <div className="player" 
    style = {{'gridColumnStart': columnStart, 'gridColumnEnd': columnEnd, 'gridRowStart': rowStart, 'gridRowEnd': rowEnd}}
    >
        <h2>{name}</h2>
        <span>Cards left: {player?.hand.length}</span>
    </div>
}

function getGridPlacement(index: number): { columnStart: number; columnEnd: number; rowStart: number; rowEnd: number; } {
    switch (index){
        case 0: return {columnStart: 2, columnEnd: 3, rowStart: 1, rowEnd: 2};
        case 1: return {columnStart: 3, columnEnd: 4, rowStart: 2, rowEnd: 3};
        case 2: return {columnStart: 2, columnEnd: 3, rowStart: 3, rowEnd: 4};
        case 3: return {columnStart: 1, columnEnd: 2, rowStart: 2, rowEnd: 3};
        default: throw new Error("Invalid index.")
    }
}
