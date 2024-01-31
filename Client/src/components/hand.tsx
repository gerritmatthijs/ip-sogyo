import { Card } from './card.tsx'

type Props = {
    hand: string
};

export const Hand = (props: Props) => {
    const {hand} = props;
    return <div className="hand">
        {createCards(hand)}
        {/* <br/>
        <button className='playCardsButton'>
            Play cards
        </button> */}
    </div>
}

function createCards(hand: string){
    const cardList = [];
    for (let i = 0; i < hand.length; i++){
        cardList.push(<Card value={hand[i]} key={i}/>);
    }
    return cardList;
}