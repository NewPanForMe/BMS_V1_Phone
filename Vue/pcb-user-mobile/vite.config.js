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
    cors: true,//为开发服务器配置 CORS , 默认启用并允许任何源
    proxy: {
      '/BMSV2Service': {
          target: 'http://127.0.0.1:20000/',   //代理接口
          changeOrigin: true,
          rewrite: (path) => path.replace(/^\/BMSV2Service/, 'BMSV2Service')
    }
  }
  }
  
})
