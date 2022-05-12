import App from './App.vue'
import {createApp} from 'vue'
import vueX from 'vuex'

import router from './router'

import storeConfigs from './store'

import './assets/font/font-awesome-4.7.0/css/font-awesome.min.css'
import './assets/css/commom/reset.css'
import './assets/css/commom/base.css'
import './assets/css/commom/grid.css'
import './assets/css/style.css'
import './assets/css/commom/reponsive.css'

var store = new vueX.Store(storeConfigs)
const app = createApp(App)
app.use(router)
app.use(store)
app.mount('#app')
