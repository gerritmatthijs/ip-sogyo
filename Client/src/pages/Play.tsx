import '../style/play.css'
import '../style/card.css'
import { ActiveHand } from '../components/activeHand.tsx'
import { useTichuContext } from '../context/TichuGameContext.tsx';
import { getPicture } from '../components/card.tsx';
import { Player } from '../components/players.tsx';

function Play() {
    const { gameState } = useTichuContext();
    const lastPlayed = gameState? gameState.lastPlayed : "";
    const player = gameState? gameState.players[0].name : "";

    return (
        <div className='environment' >
            <h1>Tichu</h1>
            <div className="grid-container">
                <Player index={0}/>
                <Player index={1}/>
                <Player index={2}/>
                <Player index={3}/>
                {lastPlayed && <button className="card" disabled={true} 
                style={{'backgroundPosition': getPicture(lastPlayed), 'justifySelf': 'center',
                    'gridColumnStart': 2, 'gridColumnEnd': 3, 'gridRowStart': 2, 'gridRowEnd': 3}} />} 
            </div>
 
            <br/>
            <div className='activeplayername'> {player}'s hand</div>  
            <div>
                <ActiveHand />
            </div>
        </div>
    )
}

export default Play