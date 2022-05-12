import {createRouter,createWebHistory} from 'vue-router'

import EmployeesManagement from '../components/EmployeesManagement.vue'
import Report from '../components/Report.vue'
import Error from '../components/Error.vue'
const routes = [
    {
        path: '/employees',
        component:EmployeesManagement,
        meta:{
            title:'Quản lý nhân viên'
        }
    },
    {path: '/report',component:Report},
    {path: '/:pathmatch(.*)*',component:Error}

]
const router = createRouter({
    history:createWebHistory(),
    routes
})

export default router