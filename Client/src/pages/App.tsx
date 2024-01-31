import { useState } from 'react'
import '../style/App.css'
import '../style/card.css'
import { Hand } from '../components/hand.tsx'

function App() {
  return (
    <>
      <h1>Tichu</h1>
      <div>
        <Hand hand="2357TJKA"/>
        <p>
          Edit <code>src/App.tsx</code> and save to test HMR
        </p>
      </div>
    </>
  )
}

export default App
