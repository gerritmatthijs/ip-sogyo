// import { Card } from './card.tsx'
import { createGame, playCard } from '../services/api.ts'
import { useContext, useEffect, useState } from 'react';
import { TichuGameState, isTichuGameState } from '../types.ts';
import { Alert } from './alert.tsx';
import { useTichuContext } from '../context/TichuGameContext.tsx';

// type Props = {
//     startingHand: string
// };

export const ActiveHand = () => {
    // const {startingHand} = props;
    const { gameState, setGameState } = useTichuContext();
    const hand = gameState? gameState.player.hand : "";
    const [alert, setAlert] = useState<string | null>(null);

    useEffect(() => {getStartingHand();}, [])

    const onCardPlayed = async (cardPlayed: string) => {
        const result = await playCard(cardPlayed);
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
    return <div className="hand">
        {createCards(hand)}
        {alert && <Alert text = {alert} onClick={() => setAlert(null)}/>}
        {/* <br/>
        <button className='playCardsButton'>
            Play cards
        </button> */}
    </div>

    function createCards(hand: string){
        const cardList = [];
        for (let i = 0; i < hand.length; i++){
            cardList.push(<button className="card" key = {i} style={{'backgroundPosition': getPicture(hand[i])}} onClick={() => onCardPlayed(hand[i])}/>);
        }
        return cardList;
    }

    async function getStartingHand(){
        const result = await createGame("Gerrit");
        updateState(result);
    }
}

function getPicture(card: string) {
    switch(card) {
        case "T": return "-900% 0%";
        case "J": return "-1000% 0%";
        case "Q": return "-1100% 0%";
        case "K": return "-1200% 0%";
        case "A": return "-1300% 0%";
        default: 
            const numberValue = Number(card);
            const horizontalPosition = (numberValue - 1) * -100;
            return horizontalPosition + "% 0%";
    }
}