import '../style/play.css'
import '../style/card.css'
import { ActiveHand } from '../components/activeHand.tsx'

function Play() {
    return (
        <div className='environment' >
            <h1>Tichu</h1>
            <div>
                <ActiveHand />
            </div>
        </div>
    )
}

export default Play
