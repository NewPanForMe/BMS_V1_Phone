import { fileURLToPath, URL } from 'node:url'

import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'
import vueJsx from '@vitejs/plugin-vue-jsx'

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [
    vue(),
    vueJsx(),
  ],
  resolve: {
    alias: {
      '@': fileURLToPath(new URL('./src', import.meta.url))
    },
    
  },
  server: {
    proxy: {
      "/BMSV2Service": {
        target: "http://localhost:20000",
        changeOrigin: true, //必须要开启跨域
      },
    },
    fs: {
      allow: ['..'],
    }
  }
})
