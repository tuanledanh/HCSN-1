import { createRouter, createWebHistory } from 'vue-router'

const router = createRouter({
    history: createWebHistory(import.meta.env.BASE_URL),
    routes: [
        {
            path: '/',
            name: 'home',
            component: () => import('/src/views/home/ViewHome.vue')
        },
        {
            path: '/estate',
            name: 'estate',
            component: () => import('/src/views/estate/EstateTable.vue')
        },
        {
            path: '/report',
            name: 'report',
            component: () => import('/src/views/report/ViewReport.vue')
        },
        {
            path: '/tool',
            name: 'tool',
            component: () => import('/src/views/tool/ViewTool.vue')
        },
        {
            path: '/report',
            name: 'report',
            component: () => import('/src/views/report/ViewReport.vue')
        },
        {
            path: '/research',
            name: 'research',
            component: () => import('/src/views/research/ViewResearch.vue')
        },
        {
            path: '/category',
            name: 'category',
            component: () => import('/src/views/category/ViewCategory.vue')
        },
        {
            path: '/estatehtdb',
            name: 'estatehtdb',
            component: () => import('/src/views/estatehtdb/ViewEstateHtdb.vue')
        }
    ]
})

export default router
