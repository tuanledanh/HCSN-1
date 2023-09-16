<template>
    <aside
        class="sidebar flex flex-col"
        :class="{
            'sidebar--grow': isGrow
        }"
    >
        <!-- Sidebar Header -->
        <header class="sidebar__header px-15 pt-10 pb-4 flex item-center col-gap-16 h-50">
            <section class="wh-36 flex center pointer">
                <section class="sidebar__header__icon icon"></section>
            </section>

            <h1 class="sidebar__header__title">
                {{ sidebar.header }}
            </h1>
        </header>

        <!-- Sidebar body -->
        <main class="sidebar__body flex-col px-11 pt-15">
            <!-- Tổng quan -->
            <MISASidebarItem order="1" :title="sidebar.overview" to="/overview" :is-grow="isGrow" />

            <!-- Tài sản -->
            <MISASidebarItem
                order="2"
                :title="sidebar.fixed_asset"
                to="/fixed-asset"
                hasChevronDown
                :isGrow="isGrow"
                has-menu
                :list-item-menu="fixedAssetList"
            />

            <!-- Tài sản hạ tầng đường bộ -->
            <MISASidebarItem
                order="3"
                :title="sidebar.infrastructure_asset"
                to="/infrastructure-asset"
                hasChevronDown
                :isGrow="isGrow"
                :content-tooltip="sidebar.infrastructure_asset_tooltip"
            />

            <!-- Công cụ dụng cụ -->
            <MISASidebarItem
                order="4"
                :title="sidebar.tool"
                to="/tool"
                hasChevron-down
                :isGrow="isGrow"
            />

            <!-- Danh mục -->
            <MISASidebarItem order="5" :title="sidebar.category" to="/category" :is-grow="isGrow" />

            <!-- Tra cứu -->
            <MISASidebarItem
                order="6"
                :title="sidebar.search"
                to="/search"
                hasChevronDown
                :isGrow="isGrow"
            />

            <!-- Báo cáo -->
            <MISASidebarItem
                order="7"
                :title="sidebar.report"
                to="/report"
                hasChevronDown
                :isGrow="isGrow"
            />
        </main>

        <!-- Sidebar Footer -->
        <footer class="sidebar__footer h-40 flex item-center justify-right">
            <section class="wrapper-icon tooltip" @click="isGrow = !isGrow">
                <section class="icon square-arrow"></section>
                <MISATooltip type="right" :content="isGrow ? sidebar.collapse : sidebar.expand" />
            </section>
        </footer>
    </aside>
</template>

<script setup lang="ts">
import { ref } from 'vue'
import { useResource } from '@/hook'

const isGrow = ref<boolean>(false)

const { sidebar } = useResource()

const fixedAssetList = Object.entries(sidebar.fixed_asset_list)
</script>

<style scoped lang="scss">
@import './TheSidebar.scss';
</style>
