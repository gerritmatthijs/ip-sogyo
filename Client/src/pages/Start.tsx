import NameInputField from '../components/NameInputField';
import { createGame, getGame } from '../services/api';
import '../style/start.css';
import { useState } from 'react';
import { TichuGameState, isTichuGameState } from '../types';
import { useTichuContext } from '../context/TichuGameContext';
import { Alert } from '../components/alert';

export default function Start() {
    const [player1, setPlayer1] = useState("Gerrit")
    const [player2, setPlayer2] = useState("Daniel")
    const [player3, setPlayer3] = useState("Wesley")
    const [player4, setPlayer4] = useState("Hanneke")  
    const { setGameState } = useTichuContext()
    const [alert, setAlert] = useState<string | null>(null);
    const gameID = localStorage.getItem("gameID");

    async function onSubmit() {
        const result = await createGame([player1, player2, player3, player4]);
        setUpGame(result);
    }

    async function onContinue(){
        const result = await getGame(gameID as string);
        setUpGame(result);
    }

    function setUpGame(result: TichuGameState | {statusCode: number; statusText: string; }){
        if (isTichuGameState(result)) {
            localStorage.setItem("gameID", result.gameID);
            setGameState(result);
        }
        else {
            setAlert(`${result.statusCode} ${result.statusText}`);
        }
    }

    return (
        <div className='environment'>
            <h1>Welcome to Tichu!</h1>
            <h3>Don't know the rules? Read up on them <a href="https://en.wikipedia.org/wiki/Tichu" target="_blank">here!</a></h3>
            {gameID !== null && <button className='continue-button' onClick={onContinue}>Continue Previous Game</button>}
            <NameInputField id={1} value={player1} onChange={e => setPlayer1(e.target.value)}/>
            <NameInputField id={2} value={player2} onChange={e => setPlayer2(e.target.value)}/>
            <NameInputField id={3} value={player3} onChange={e => setPlayer3(e.target.value)}/>
            <NameInputField id={4} value={player4} onChange={e => setPlayer4(e.target.value)}/>
            <br/>
            <button onClick={onSubmit}>Start game</button>
            {alert && <Alert text = {alert} onClick={() => setAlert(null)}/>}
        </div>
    )
}