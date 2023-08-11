<template>
    <aside
        class="sidebar flex-column"
        :style="styleSidebar"
        :class="{ sidebar__grow: isSidebarGrow }"
    >
        <!-- Sidebar Header -->
        <header class="sidebar__header px-15 pt-10 pb-4 center-y col-gap-16">
            <section class="wh-36 center pointer">
                <section class="sidebar__header__icon icon"></section>
            </section>

            <h1 class="sidebar__header__title">MISA QLTS</h1>
        </header>

        <!-- Sidebar body -->
        <main
            class="sidebar__body flex-column px-11 pt-15"
            :class="{ sidebar__shrink: !isSidebarGrow }"
        >
            <router-link to="/" class="relative">
                <m-sidebarItem>Tổng quan</m-sidebarItem>
                <m-tooltip content="Tổng quan" type="right" />
            </router-link>
            <router-link to="/fixed_asset" class="relative">
                <m-sidebarItem iconDown order="2">Tài sản</m-sidebarItem>
                <m-tooltip content="Tài sản" type="right" />
            </router-link>

            <router-link to="/fixed_asset_htdb" class="relative">
                <m-sidebarItem iconDown order="3">Tài sản HT-ĐB</m-sidebarItem>
                <m-tooltip content="Tài sản HT-ĐB" type="right" />
            </router-link>
            <router-link to="/tool" class="relative">
                <m-sidebarItem iconDown order="4">Công cụ dụng cụ</m-sidebarItem>
                <m-tooltip content="Công cụ dụng cụ" type="right" />
            </router-link>
            <router-link to="/category" class="relative">
                <m-sidebarItem order="5">Danh mục</m-sidebarItem>
                <m-tooltip content="Danh mục" type="right" />
            </router-link>
            <router-link to="/research" class="relative">
                <m-sidebarItem iconDown order="6">Tra cứu</m-sidebarItem>
                <m-tooltip content="Tra cứu" type="right" />
            </router-link>
            <router-link to="/report" class="relative">
                <m-sidebarItem iconDown order="7">Báo cáo</m-sidebarItem>
                <m-tooltip content="Báo cáo" type="right" />
            </router-link>
        </main>

        <!-- Sidebar Footer -->
        <footer class="sidebar__footer h-40 w-100 center-y">
            <section class="wrapper-icon relative" @click="toggleIconSidebar">
                <section class="icon sidebar__footer-icon"></section>
                <m-tooltip :content="isSidebarGrow ? 'Thu hẹp' : 'Bung rộng'" type="right" />
            </section>
        </footer>
    </aside>
</template>

<script>
export default {
    name: 'TheSidebar',
    data() {
        return {
            isSidebarGrow: false
        }
    },
    methods: {
        /**
         * Author: Bùi Huy Tuyền (11/07/2023)
         * @param
         * Bung rộng Sidebar
         *
         */
        toggleIconSidebar() {
            this.isSidebarGrow = !this.isSidebarGrow
        }
    },
    computed: {
        /**
         *  Author: Bùi Huy Tuyền (11/07/2023)
         *  Style của sidebar
         */
        styleSidebar() {
            return {
                width: this.isSidebarGrow ? '200px' : '66px',
                transition: 'width 0.3s ease-in-out'
            }
        }
    }
}
</script>
