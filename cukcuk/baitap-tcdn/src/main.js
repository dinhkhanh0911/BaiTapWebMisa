import { createApp } from "vue";
import App from "./App.vue";

import router from '@/router/index.js' 

import './assets/font/font-awesome-4.7.0/css/font-awesome.min.css'
import './assets/css/commom/reset.css'
import './assets/css/commom/base.css'
import './assets/css/commom/grid.css'
import './assets/css/style.css'
import './assets/css/commom/reponsive.css'
import messageResources from "@/assets/js/resources/messageResources.js";



let app = createApp(App)

app.config.globalProperties.popupMsg = messageResources.popupMsg
app.config.globalProperties.toastMsg = messageResources.toastMsg
app.config.globalProperties.errorMsg = messageResources.errorMsg
app.config.globalProperties.typePopup = messageResources.typePopup
app.config.globalProperties.typePopupName = messageResources.typePopupName
app.config.globalProperties.validateMsg = messageResources.validateMsg

app.use(router)
app.mount('#app')
