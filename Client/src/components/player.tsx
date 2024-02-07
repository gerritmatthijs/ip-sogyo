import { useTichuContext } from "../context/TichuGameContext"

type Props = {
    index: number
}

export default function Player(props: Props) {
    const { index } = props;
    const { gameState } = useTichuContext();
    const player = gameState?.players[index];
    const name = player? player.name : "";
    const textColor = getColor();
    const { columnPlacement, rowPlacement } = getGridPlacement(index);

    function getColor(){
        const isActivePlayer = index === gameState?.turn;
        const isCurrentLeader = name == gameState?.currentLeader;
        if (isActivePlayer){
            return 'darkred';
        }
        if (isCurrentLeader){
            return 'green';
        }
        return 'black';
    }

    return <div className="player" 
        style = {{color: textColor, gridColumn: columnPlacement, gridRow: rowPlacement}}
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
