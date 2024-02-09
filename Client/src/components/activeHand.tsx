// import { Card } from './card.tsx'
import { useState } from 'react';
import { useTichuContext } from '../context/TichuGameContext.tsx';
import { getPicture } from './card.tsx';

type Props = {
    onPlay: (cardset: string) => void;
    onPass: () => void;
};

export default function ActiveHand(props: Props) {
    const { onPlay, onPass } = props;
    const { gameState } = useTichuContext();
    const hand = gameState? gameState.players[gameState.turn].hand : "";
    const [cardsClicked, setCardsClicked] = useState<Array<number>>([]);
    const lastPlayed = gameState? gameState.lastPlayed : "";
    const endOfGame = gameState? gameState.gameStatus.endOfGame: false;

    function onCardClicked(cardnumber: number){
        let index = cardsClicked.findIndex((n) => n == cardnumber)
        if (index == -1){
            let newArray = cardsClicked.concat(cardnumber)
            newArray.sort()
            setCardsClicked(newArray)
        }
        else {
            let newArray = cardsClicked.slice(0, index).concat(cardsClicked.slice(index + 1))
            setCardsClicked(newArray)
        }
    }

    function onPlayButtonClicked(){
        const cardset = cardsClicked.map((i) => hand[i]).join("");
        setCardsClicked([]);
        onPlay(cardset)
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
    {createCards(hand)}
    <br/>
    <button className="play-button" onClick={onPlayButtonClicked} disabled={cardsClicked.length == 0}>Play Selected Cards</button>
    {!endOfGame && <button className="pass-button" onClick={onPass} disabled={!lastPlayed}>Pass</button>}
    </div>
}
