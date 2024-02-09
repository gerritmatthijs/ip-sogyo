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
    useEffect(() => {getNewGame();}, []);

    const { gameState, setGameState } = useTichuContext();
    const lastPlayed = gameState? gameState.lastPlayed : "";
    const activePlayer = gameState? gameState.players[gameState.turn].name : "";
    const [alert, setAlert] = useState<string | null>(null);
    const [message, setMessage] = useState<string | null>(null);
    const endOfGame = gameState? gameState.gameStatus.endOfGame: false;

    async function getNewGame(){
        const result = await createGame(["Gerrit", "Daniel", "Wesley", "Hanneke"]);
        updateState(result);
    }

    async function onPlaySet(cardset: string){
        const result = await playerAction(cardset);
        updateState(result);
    }

    async function onPass(){
        const result = await playerAction("pass");
        updateState(result);
    }
    
    function updateState(result: TichuGameState | {statusCode: number; statusText: string;}) {
        if (isTichuGameState(result)) {
            setGameState(result);
            setAlertOrMessage(result);
        }
        else {
            setAlert(`${result.statusCode} ${result.statusText}`);
        }
    }

    function setAlertOrMessage(result: TichuGameState) {
        if (result.gameStatus.alert){
            setAlert(result.gameStatus.alert)
        }
        else {
            setAlert(null)
        }
        if (result.gameStatus.message){
            setMessage(result.gameStatus.message)
        }
        else {
            setMessage(null)
        }
    }

    function createLastPlayed(cardset: string){
        const cardList = [];
        console.log(cardset)
        for (let i = 0; i < cardset.length; i++){
            cardList.push(<button 
                    className="card" key = {i} 
                    style={{backgroundPosition: getPicture(cardset[i])}} 
                    disabled={true}
                    />);
        }
        return cardList;
    }

    return (
        <div className='environment' >
            <h1>Tichu</h1>
            <div className="grid-container">
                <Player index={0}/>
                <Player index={1}/>
                <Player index={2}/>
                <Player index={3}/>
                {lastPlayed && !endOfGame && 
                <div style={{gridColumn: '2 / span 1', gridRow: '2 / span 1'}}>{createLastPlayed(lastPlayed)}</div>} 
                {endOfGame && <div style = {{gridColumn: '2 / span 1', gridRow: '2 / span 1'}}>
                    <h2> Game finished! </h2>
                    <button className='newGameButton' onClick={getNewGame}>Start New Game</button>
                </div>}
                <div className="line"></div>
            </div>
 
            <br/>
            {message && <Message text = {message} onClick = {() => setMessage(null)}/>}
            {!endOfGame && <h2>{activePlayer}'s hand</h2>}
            {alert && <Alert text = {alert} onClick={() => setAlert(null)}/>}
            {!endOfGame && <div>
                <ActiveHand onPlay={onPlaySet}/>
            </div>}
            {!endOfGame && <button className="pass-button" onClick={onPass} disabled={!lastPlayed}>Pass</button>}
        </div>
    )
}

