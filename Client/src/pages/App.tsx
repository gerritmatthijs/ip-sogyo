import { useState } from 'react'
import '../style/App.css'
import '../style/card.css'
import { sendGreetings } from '../services/api.ts'

function App() {
  const [count, setCount] = useState(0)

  return (
    <>
      <h1>Vite + React</h1>
      <div>
        <button className="card" onClick={() => sendGreetings()}>
        </button>
        <p>
          Edit <code>src/App.tsx</code> and save to test HMR
        </p>
      </div>
    </>
  )
}

export default App
