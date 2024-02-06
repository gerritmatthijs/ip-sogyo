import '../style/play.css'
import '../style/card.css'
import { ActiveHand } from '../components/activeHand.tsx'
import { Alert } from '../components/alert.tsx';
import { useTichuContext } from '../context/TichuGameContext.tsx';
import { getPicture } from '../components/card.tsx';
import { Player } from '../components/players.tsx';
import { TichuGameState, isTichuGameState } from '../types.ts';
import { createGame, playCard } from '../services/api.ts'
import { useEffect, useState } from 'react';

function Play() {
    useEffect(() => {getStartingHand();}, [])

    const { gameState, setGameState } = useTichuContext();
    const lastPlayed = gameState? gameState.lastPlayed : "";
    const activePlayer = gameState? gameState.players[gameState.turn].name : "";
    const [alert, setAlert] = useState<string | null>(null);

    const onCardPlayed = async (cardPlayed: string) => {
        const result = await playCard(activePlayer, cardPlayed);
        updateState(result);
    }
    
    function updateState(result: string | TichuGameState | {statusCode: number; statusText: string;}) {
        if (isTichuGameState(result)) {
            setGameState(result);
        }
        else if (typeof result == 'string') {
            setAlert(result);
        }
        else {
            setAlert(`${result.statusCode} ${result.statusText}`);
        }
    }

    async function getStartingHand(){
        const result = await createGame(["Gerrit", "Daniel", "Wesley", "Hanneke"]);
        updateState(result);
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
                style={{'backgroundPosition': getPicture(lastPlayed), 'justifySelf': 'center',
                    'gridColumnStart': 2, 'gridColumnEnd': 3, 'gridRowStart': 2, 'gridRowEnd': 3}} />} 
            </div>
 
            <br/>
            <div className='activeplayername'> {activePlayer}'s hand</div>  
            {alert && <Alert text = {alert} onClick={() => setAlert(null)}/>}
            <div>
                <ActiveHand onClick={onCardPlayed}/>
            </div>
        </div>
    )
}

export default Play