// import { Card } from './card.tsx'
import { useTichuContext } from '../context/TichuGameContext.tsx';
import { getPicture } from './card.tsx';

type Props = {
    onClick: (card: string) => void;
};

export default function ActiveHand(props: Props) {
    const { onClick } = props;
    const { gameState } = useTichuContext();
    const hand = gameState? gameState.players[gameState.turn].hand : "";

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
            cardList.push(<button className="card" key = {i} 
                    style={{backgroundPosition: getPicture(hand[i])}} 
                    onClick={() => onClick(hand[i])}/>
                    );
        }
        return cardList;
    }
}
