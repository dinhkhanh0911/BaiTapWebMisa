import {createRouter,createWebHistory} from 'vue-router'

import TheEmployees from '@/components/layout/TheEmployees.vue'
import TheSuppliers from '@/components/layout/TheSuppliers.vue'
import TheDeposits from '@/components/layout/TheDeposits.vue'
import TheProcedure from "@/components/layout/TheProcedure.vue"
import TheReiceive_Payment from "@/components/layout/TheReiceive_Payment.vue"
const routes = [
    {
        path: '/employees',
        component:TheEmployees,
        meta:{
            title:'Quản lý nhân viên'
        }
    },
    {
        path: '/suppliers',
        component:TheSuppliers,
        meta:{
            title:'Quản lý nhà cung cấp'
        }
    },
    // {
    //     path: '/CA/CADeposits',
    //     component:TheProcedure,
    //     meta:{
    //         title:'Tiền gửi'
    //     }
    // },
    // {
    //     path:'/CA/CAReceipt',
    //     component:TheReiceive_Payment,
    //     meta:{
    //         title:'Tiền gửi'
    //     }
    // },
    {
        path: "/CA", component: TheDeposits,
        children: [
            {path: "",  redirect: '/CA/CAProcess' },
            {
                path: "CAProcess",
                component: TheProcedure
            },
            {
                path: "ReceiptPaymentList",
                component: TheReiceive_Payment,
            },
        ]
    },

    {path: '/:pathmatch(.*)*',component:TheEmployees}

]
const router = createRouter({
    history:createWebHistory(),
    routes
})

export default router