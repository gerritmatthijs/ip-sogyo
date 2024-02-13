import { useTichuContext } from "../context/TichuGameContext";
import Play from "./Play";
import Start from "./Start";

export default function Tichu() {
    const { gameState } = useTichuContext() 
    return gameState? <Play/> : <Start/>;
}