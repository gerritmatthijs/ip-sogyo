import React from 'react'
import ReactDOM from 'react-dom/client'
import './index.css'
import { TichuGameProvider } from './context/TichuGameContext.tsx'
import Tichu from './pages/Tichu.tsx'

ReactDOM.createRoot(document.getElementById('root')!).render(
  <React.StrictMode>
    <TichuGameProvider>
      <Tichu />
    </TichuGameProvider>
  </React.StrictMode>,
)
