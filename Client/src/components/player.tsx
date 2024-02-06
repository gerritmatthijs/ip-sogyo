import { useTichuContext } from "../context/TichuGameContext"

type Props = {
    index: number
}

export default function Player(props: Props) {
    const { index } = props;
    const { gameState } = useTichuContext();
    const player = gameState?.players[index];
    const name = player? player.name : "";
    const activePlayer = index === gameState?.turn;
    const { columnPlacement, rowPlacement } = getGridPlacement(index);
    return <div className="player" 
        style = {{color: activePlayer? 'darkred': 'black', 
            gridColumn: columnPlacement, gridRow: rowPlacement}}
        >
            <h2>{name}</h2>
            <span>Cards left: {player?.hand.length}</span>
        </div>
}

function getGridPlacement(index: number): { columnPlacement: string; rowPlacement: string; } {
    switch (index){
        case 0: return {columnPlacement: '2 / span 1', rowPlacement: '1 / span 1'};
        case 1: return {columnPlacement: '3 / span 1', rowPlacement: '2 / span 1'};
        case 2: return {columnPlacement: '2 / span 1', rowPlacement: '3 / span 1'};
        case 3: return {columnPlacement: '1 / span 1', rowPlacement: '2 / span 1'};
        default: throw new Error("Invalid index.")
    }
}
