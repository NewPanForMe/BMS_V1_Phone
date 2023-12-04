import './assets/main.css'
import { createApp } from 'vue'
import App from './App.vue'
import router from './router/index'
import mobile from 'tdesign-mobile-vue'
import instance from './utils/http'
import cookies from './utils/cookies'
import utils from './utils/message'
import api from './api/api'

const app = createApp(App)

app.use(router)
app.use(mobile)
app.use(instance)


app.provide('$instance', instance)
app.provide('$Utils', utils)
app.provide('$Api', api)
app.provide('$Cookies', cookies)
app.provide('$Router', router)





app.mount('#app')
