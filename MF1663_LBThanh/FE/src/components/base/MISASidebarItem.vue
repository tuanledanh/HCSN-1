<template>
    <div>
        <section class="sidebar__body-item w-100 center-y pointer col-gap-6 relative">
            <section class="wrap-icon-sidebar center">
                <section :class="classIconSidebar"></section>
            </section>
            <h1 class="sidebar__item--title" @click="toggleSubMenu">
                <slot></slot>
            </h1>
            <section class="wrapper-icon absolute r-4 sidebar-icon-hidden" v-if="iconDown" @click="toggleSubMenu">
                <section class="icon down-icon-sidebar"></section>
            </section>
        </section>
        <section class="sub-menu" v-if="iconDown && showSubMenu">
            <router-link
                :to="'/' + subItem.url"
                v-for="subItem in subItems"
                :key="subItem.url"
                class="w-100 center-y pointer col-gap-6 relative sub-menu-item"
            >
                <section class="wrap-icon-sidebar center">
                    <section class="sub-menu-icon"></section>
                </section>
                <h1 class="sidebar__item--title">
                    <span>{{ subItem.content }}</span>
                </h1>
            </router-link>
        </section>
    </div>
</template>

<script>
export default {
    name: 'MISASidebarItem',
    /**
     * Author: LB.Thành (04/07/2023)
     * ĐỊnh nghĩa props cho component
     */
    props: {
        iconDown: {
            type: Boolean,
            default: false
        },
        order: {
            type: String,
            default: '1'
        },
        active: {
            type: Boolean,
            default: false
        },
        // Danh sách sub item
        subItems: {
            type: Array,
            default: null
        }
    },
    data() {
        return {
            showSubMenu: false
        }
    },
    computed: {
        /**
         * Author: LB.Thành (04/07/2023)
         * Định nghĩa class cho icon sidebar
         */
        classIconSidebar() {
            return `sidebar__body__icon-${this.order} icon`
        }
    },
    methods: {
        toggleSubMenu() {
            if (this.iconDown) {
                this.showSubMenu = !this.showSubMenu
            }
        }
    }
}
</script>
