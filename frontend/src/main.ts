import { createApp } from 'vue'
import Antd from 'ant-design-vue';
import App from './App.vue'
import router from './router'
import {createPinia} from 'pinia' 
import 'ant-design-vue/dist/antd.less';
import './assets/main.css'

const pinia = createPinia()
const app = createApp(App)

app.use(router)
app.use(pinia)
app.use(Antd)

app.mount('#app')
