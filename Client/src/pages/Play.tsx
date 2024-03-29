import '../style/play.css';
import '../style/card.css';
import ActiveHand from '../components/activeHand.tsx';
import Player from '../components/player.tsx';
import { Alert, Message } from '../components/alert.tsx';
import { useTichuContext } from '../context/TichuGameContext.tsx';
import { getPicture } from '../components/card.tsx';
import { TichuGameState, isTichuGameState } from '../types.ts';
import { createGame, playerAction } from '../services/api.ts';
import { useState } from 'react';

export default function Play() {
    const { gameState, setGameState } = useTichuContext();

    const lastPlayed = gameState? gameState.lastPlayed : "";
    const activePlayer = gameState? gameState.players[gameState.turn].name : "";
    const [alert, setAlert] = useState<string | null>(null);
    const [message, setMessage] = useState<string | null>(null);
    const [IsChangeOver, setIsChangeOver] = useState(false);
    const endOfGame = gameState? gameState.gameStatus.endOfGame: false;
    if (endOfGame){
        localStorage.removeItem("gameID");
    }

    function backToStart(){
        setGameState(undefined);
    }

    async function getNewGame(){
        const result = await createGame(gameState? gameState.players.map(p => p.name): 
            ["Gerrit", "Daniel", "Wesley", "Hanneke"]);
        if (isTichuGameState(result)){
            localStorage.setItem("gameID", result.gameID);
        } 
        updateState(result);
    }

    async function onPlaySet(cardset: string){
        const result = await playerAction(cardset, gameState?.gameID as string);
        updateState(result);
    }

    async function onPass(){
        const result = await playerAction("pass", gameState?.gameID as string);
        updateState(result);
    }
    
    function updateState(result: TichuGameState | {statusCode: number; statusText: string;}) {
        if (isTichuGameState(result)) {
            setGameState(result);
            setAlertOrMessage(result);

            // Comment the next 3 lines to disable intermediate screen
            if (!(result.gameStatus.alert)){
                setIsChangeOver(true);
            }
        }
        else {
            displayServerAlert(result);
        }
    }

    function displayServerAlert(result: {statusCode: number; statusText: string;}){
        setAlert(`${result.statusCode} ${result.statusText}`);
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
            <button className='newgame-button' onClick={getNewGame}>Start New Game</button>
            <button className='backtostart-button' onClick={backToStart}>Back To Start</button>
            <div className="grid-container">
                <Player index={0}/>
                <Player index={1}/>
                <Player index={2}/>
                <Player index={3}/>
                {lastPlayed && !endOfGame && 
                <div style={{gridColumn: '2 / span 1', gridRow: '2 / span 1'}}>{createLastPlayed(lastPlayed)}</div>} 
                {endOfGame && <div style = {{gridColumn: '2 / span 1', gridRow: '2 / span 1'}}>
                    <h2> Game finished! </h2>
                </div>}
                <div className="line"></div>
            </div>
 
            <br/>
            {message && <Message text = {message} onClick = {() => setMessage(null)}/>}
            {alert && <Alert text = {alert} onClick={() => setAlert(null)}/>}
            {!IsChangeOver && !endOfGame && <div>
                <ActiveHand onPlay={onPlaySet} onPass={onPass} onAlert={displayServerAlert}/>
            </div>}
            {IsChangeOver && <button className="changeover-button" onClick={() => setIsChangeOver(false)}>
                Show {activePlayer}'s Hand</button>}
        </div>
    )
}

