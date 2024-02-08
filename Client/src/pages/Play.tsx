import '../style/play.css';
import '../style/card.css';
import ActiveHand from '../components/activeHand.tsx';
import Player from '../components/player.tsx';
import { Alert, Message } from '../components/alert.tsx';
import { useTichuContext } from '../context/TichuGameContext.tsx';
import { getPicture } from '../components/card.tsx';
import { TichuGameState, isTichuGameState } from '../types.ts';
import { createGame, playerAction } from '../services/api.ts';
import { useEffect, useState } from 'react';

export default function Play() {
    useEffect(() => {getStartingHand();}, []);

    const { gameState, setGameState } = useTichuContext();
    const lastPlayed = gameState? gameState.lastPlayed : "";
    const activePlayer = gameState? gameState.players[gameState.turn].name : "";
    const [alert, setAlert] = useState<string | null>(null);
    const [message, setMessage] = useState<string | null>(null);

    async function getStartingHand(){
        const result = await createGame(["Gerrit", "Daniel", "Wesley", "Hanneke"]);
        updateState(result);
    }

    async function onCardPlayed(cardPlayed: string){
        const result = await playerAction(cardPlayed);
        if (isTichuGameState(result) && gameState?.players[gameState.turn].hand == cardPlayed){
            setMessage(activePlayer + " has played all their cards!");
        }
        updateState(result);
    }

    async function onPass(){
        const result = await playerAction("pass");
        if (isTichuGameState(result) && result["lastPlayed"] == ""){
            setMessage(gameState?.currentLeader + " has won the trick!");
        }
        updateState(result);
    }
    
    function updateState(result: string | TichuGameState | {statusCode: number; statusText: string;}) {
        if (isTichuGameState(result)) {
            setGameState(result);
            setAlert(null);
        }
        else if (typeof result == 'string') {
            setAlert(result);
        }
        else {
            setAlert(`${result.statusCode} ${result.statusText}`);
        }
    }

    return (
        <div className='environment' >
            <h1>Tichu</h1>
            <div className="grid-container">
                <Player index={0}/>
                <Player index={1}/>
                <Player index={2}/>
                <Player index={3}/>
                {lastPlayed && <button className="card" disabled={true} 
                style={{backgroundPosition: getPicture(lastPlayed), 
                    gridColumn: '2 / span 1', gridRow: '2 / span 1'}} />} 
                <div className="line"></div>
            </div>
 
            <br/>
            {message && <Message text = {message} onClick = {() => setMessage(null)}/>}
            <h2>{activePlayer}'s hand</h2> 
            {alert && <Alert text = {alert} onClick={() => setAlert(null)}/>}
            <div>
                <ActiveHand onClick={onCardPlayed}/>
            </div>
            <button className="pass-button" onClick={onPass}>Pass</button>
        </div>
    )
}
