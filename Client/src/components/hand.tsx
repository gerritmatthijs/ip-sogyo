// import { Card } from './card.tsx'
import { createGame, playCard } from '../services/api.ts'
import { useEffect, useState } from 'react';
import { isTichuGameState } from '../types.ts';

// type Props = {
//     startingHand: string
// };

export const Hand = () => {
    // const {startingHand} = props;
    const [hand, setHand] = useState("");

    useEffect(() => {getStartingHand();}, [])

    const onCardPlayed = async (cardPlayed: string) => {
        const result = await playCard(cardPlayed);
        if (isTichuGameState(result)){
            setHand(result["hand"]);
        }
        else {
            console.log("Invalid result obtained:" + result.statusCode + result.statusText);
        }
    }

    return <div className="hand">
        {createCards(hand)}
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
        if (isTichuGameState(result)){
            setHand(result.hand);
        }
        else {
            console.log("Invalid result obtained:" + result.statusCode + result.statusText);
            return "";
        }
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