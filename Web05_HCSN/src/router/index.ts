import * as vRouter from 'vue-router'
import { createRouter, createWebHistory } from 'vue-router'

const _router: Array<vRouter.RouteRecordRaw> = [
    {
        path: '/',
        redirect: '/overview'
    },
    {
        path: '/overview',
        component: () => import('@/views/overview')
    },
    // list asset
    {
        path: '/fixed-asset',
        component: () => import('@/views/fixed_asset')
    },
    {
        path: '/fixed-asset-increase',
        component: () => import('@/views/fixed_asset_increase')
    },
    {
        path: '/information-change',
        component: () => import('@/views/information_change')
    },
    {
        path: '/fixed-asset-reassessment',
        component: () => import('@/views/fixed_asset_reassessment')
    },
    {
        path: '/depreciation-fixed-asset',
        component: () => import('@/views/depreciation_fixed_asset')
    },
    {
        path: '/asset-transfer',
        component: () => import('@/views/asset_transfer')
    },
    {
        path: '/reducing-asset',
        component: () => import('@/views/reducing_asset')
    },
    {
        path: '/fixed-asset-inventory',
        component: () => import('@/views/fixed_asset_inventory')
    },
    {
        path: '/other',
        component: () => import('@/views/other')
    },
    // list infrastructure
    {
        path: '/infrastructure-asset',
        component: () => import('@/views/infrastructure_asset')
    },
    {
        path: '/tool',
        component: () => import('@/views/tool')
    },
    {
        path: '/category',
        component: () => import('@/views/category')
    },
    {
        path: '/search',
        component: () => import('@/views/search')
    },
    {
        path: '/report',
        component: () => import('@/views/report')
    }
]

const router = createRouter({
    history: createWebHistory(import.meta.env.BASE_URL),
    routes: _router
})

export default router
