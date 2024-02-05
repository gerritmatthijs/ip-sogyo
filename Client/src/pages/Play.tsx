import '../style/play.css'
import '../style/card.css'
import { ActiveHand } from '../components/activeHand.tsx'
import { useTichuContext } from '../context/TichuGameContext.tsx';

function Play() {
    const { gameState } = useTichuContext();
    const lastPlayed = gameState? gameState.lastPlayed : "";
    const player = gameState? gameState.player.name : "";

    return (
        <div className='environment' >
            <h1>Tichu</h1>
            {lastPlayed && <button className="card" disabled={true} style={{'backgroundPosition': getPicture(lastPlayed)}} />} 
            <br/>
            <div className='playername'> {player}'s hand</div>  
            <div>
                <ActiveHand />
            </div>
        </div>
    )
}

export default Play

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