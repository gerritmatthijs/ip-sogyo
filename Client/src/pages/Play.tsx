import { useState } from 'react'
import '../style/play.css'
import '../style/card.css'
import { Hand } from '../components/hand.tsx'

function Play() {
    return (
        <div className='environment' >
            <h1>Tichu</h1>
            <div>
                <Hand hand="2357TJKA"/>
            </div>
        </div>
    )
}

export default Play
