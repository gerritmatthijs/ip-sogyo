import { useState } from 'react';
import { useTichuContext } from '../context/TichuGameContext.tsx';
import { getPicture } from './card.tsx';
import { parseCardSelection } from '../services/api.ts';
import { isTichuGameState, isTichuGameStatus } from '../types.ts';

type Props = {
    onPlay: (cardset: string) => void;
    onPass: () => void;
    onAlert: (result: {statusCode: number; statusText: string;}) => void;
};

export default function ActiveHand(props: Props) {
    const { onPlay, onPass, onAlert } = props;
    const { gameState } = useTichuContext();
    const hand = gameState? gameState.players[gameState.turn].hand : "";
    const [cardsClicked, setCardsClicked] = useState<Array<number>>([]);
    const [hoverMessage, setHoverMessage] = useState("");
    const lastPlayed = gameState? gameState.lastPlayed : "";
    const endOfGame = gameState? gameState.gameStatus.endOfGame: false;
    const activePlayer = gameState? gameState.players[gameState.turn].name : "";

    async function checkAllowedCardSet(newArray: Array<number>) {
        const result = await parseCardSelection(
            newArray.map((i) => hand[i]).join(""), 
            gameState?.gameID as string
            );
        if (isTichuGameStatus(result)){
            setHoverMessage(result.alert);
        }
        else {
            onAlert(result);
        }
    }

    function onCardClicked(cardnumber: number){
        let index = cardsClicked.findIndex((n) => n == cardnumber);
        let newArray: Array<number>;
        if (index == -1){
            newArray = cardsClicked.concat(cardnumber);
        }
        else {
            newArray = cardsClicked.slice(0, index).concat(cardsClicked.slice(index + 1));
        }
        setCardsClicked(newArray);
        checkAllowedCardSet(newArray);
    }

    function onPlayButtonClicked(){
        const currentCardSet = cardsClicked.map((i) => hand[i]).join("");
        setCardsClicked([]);
        onPlay(currentCardSet);
    }

    function onPassButtonClicked(){
        setCardsClicked([]);
        onPass();
    }

    function createCards(hand: string){
        const cardList = [];
        for (let i = 0; i < hand.length; i++){
            cardList.push(<button 
                    className="card" key = {i} 
                    style={{backgroundPosition: getPicture(hand[i]), opacity: cardsClicked.includes(i) ? '0.5' : ' 1'}} 
                    onClick={() => onCardClicked(i)}
                    />);
        }
        return cardList;
    }

    return <div className="hand">   
    <h2>{activePlayer}'s hand</h2>
    {createCards(hand)}
    <br/>
    <button className="play-button" onClick={onPlayButtonClicked} 
        disabled={hoverMessage.length>0 || cardsClicked.length==0}>
        Play Selected Cards</button>
    {!endOfGame && <button className="pass-button" onClick={onPassButtonClicked} 
        disabled={!lastPlayed || lastPlayed=="H"}>Pass</button>}
    <div className="hovertext">{hoverMessage}</div>
    </div>
}

