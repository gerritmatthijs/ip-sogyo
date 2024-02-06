import { useTichuContext } from "../context/TichuGameContext"

type Props = {
    index: number
}

export const Player = (props: Props) => {
    const { index } = props;
    const { gameState } = useTichuContext();
    const player = gameState?.players[index];
    const name = player? player.name : "";
    return <div className="player">
        <h2>{name}</h2>
        <span>Cards left: {player?.hand.length}</span>
    </div>
}