import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [react()],
  server: {
    proxy: {
      "/tichu": {
        target: "http://localhost:5036/",
        changeOrigin: true,
        secure: false
      }
    }
  }
})
