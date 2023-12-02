import './assets/main.css'

import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import tdesign from 'tdesign-mobile-vue'
import 'tdesign-mobile-vue/es/style/index.css';
import TDesign from 'tdesign-mobile-vue';
const app = createApp(App)

app.use(router)
app.use(TDesign)

app.mount('#app')
