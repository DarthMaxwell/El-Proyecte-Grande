import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'

// https://vite.dev/config/
export default defineConfig({
  plugins: [react()],
  server: {
    proxy: {
      "^/api/movie": {
        target: "http://localhost:5132",
        secure: false
      },
      "^/api/reply": {
        target: "http://localhost:5132",
        secure: false,
      },
      "^/api/posts": {
        target: "http://localhost:5132",
        secure: false,
      },
      "^/api/auth": {
        target: "http://localhost:5132",
        secure: false,
      },
    },
  },
})