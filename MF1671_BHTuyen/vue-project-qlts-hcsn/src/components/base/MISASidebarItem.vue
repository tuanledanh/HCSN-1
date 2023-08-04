<template>
    <section
        class="sidebar__body-item w-100 center-y pointer col-gap-6 relative"
        :class="{ 'sidebar__body-item--active': active }"
    >
        <section class="wrap-icon-sidebar center">
            <section :class="classIconSidebar"></section>
        </section>
        <h1 class="sidebar__item--title">
            <slot></slot>
        </h1>
        <section class="wrapper-icon absolute r-4 sidebar-icon-hidden" v-if="iconDown">
            <section class="icon down-icon-sidebar"></section>
        </section>
    </section>
</template>

<script>
export default {
    name: 'MISASidebarItem',
    /**
     * Author: Bùi Huy Tuyền (04/07/2023)
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
        }
    },
    computed: {
        /**
         * Author: Bùi Huy Tuyền (04/07/2023)
         * Định nghĩa class cho icon sidebar
         */
        classIconSidebar() {
            return `sidebar__body__icon-${this.order} icon`
        }
    }
}
</script>
