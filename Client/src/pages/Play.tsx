import '../style/play.css'
import '../style/card.css'
import { ActiveHand } from '../components/activeHand.tsx'
import { useTichuContext } from '../context/TichuGameContext.tsx';
import { getPicture } from '../components/card.tsx';

function Play() {
    const { gameState } = useTichuContext();
    const lastPlayed = gameState? gameState.lastPlayed : "";
    const player = gameState? gameState.players[0].name : "";

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