import React from 'react'
import ReactDOM from 'react-dom/client'
import Play from './pages/Play.tsx'
import './index.css'
import { TichuGameProvider } from './context/TichuGameContext.tsx'

ReactDOM.createRoot(document.getElementById('root')!).render(
  <React.StrictMode>
    <TichuGameProvider>
      <Play />
    </TichuGameProvider>
  </React.StrictMode>,
)
