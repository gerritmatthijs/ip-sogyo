import NameInputField from '../components/NameInputField';
import { createGame } from '../services/api';
import '../style/play.css';
import { useState } from 'react';
import { isTichuGameState } from '../types';
import { useTichuContext } from '../context/TichuGameContext';

export default function Start() {
    const [player1, setPlayer1] = useState("Gerrit")
    const [player2, setPlayer2] = useState("Daniel")
    const [player3, setPlayer3] = useState("Wesley")
    const [player4, setPlayer4] = useState("Hanneke")  
    const { setGameState } = useTichuContext()

    async function onSubmit() {
        const result = await createGame([player1, player2, player3, player4]);
        if (isTichuGameState(result)) {
            setGameState(result);
        }
        // else {
        //     setAlert(`${result.statusCode} ${result.statusText}`);
        // }
    }

    return (
        <div className='environment'>
            <h1>Welcome to Tichu!</h1>
            <NameInputField id={1} value={player1} onChange={e => setPlayer1(e.target.value)}/>
            <NameInputField id={2} value={player2} onChange={e => setPlayer2(e.target.value)}/>
            <NameInputField id={3} value={player3} onChange={e => setPlayer3(e.target.value)}/>
            <NameInputField id={4} value={player4} onChange={e => setPlayer4(e.target.value)}/>
            <br/>
            <button onClick={onSubmit}>Start game</button>
        </div>
    )
}