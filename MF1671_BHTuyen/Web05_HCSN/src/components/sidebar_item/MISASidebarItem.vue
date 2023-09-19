<template>
    <section :class="{ tooltip: !isGrow }">
        <RouterLink
            class="sidebar-item sidebar-item-link flex item-center pointer col-gap-6 relative br-4"
            :class="{
                'sidebar-item--active': active
            }"
            :to="to"
            active-class="sidebar-item--active"
            @dblclick="isGrow ? (isShowMenu = !isShowMenu) : (isShowMenu = false)"
        >
            <!-- SidebarItem Icon -->
            <section class="sidebar-item__icon center flex">
                <section class="icon" :class="`sidebar-item__icon-${order}`"></section>
            </section>

            <!-- SidebarItem Title -->
            <h1 class="sidebar-item__title">{{ title }}</h1>

            <!-- Chevron-down Icon -->
            <section
                class="sidebar-item__icon-chevron-down wrapper-icon absolute r-4"
                v-if="hasChevronDown"
                @click="isGrow ? (isShowMenu = !isShowMenu) : (isShowMenu = false)"
            >
                <section
                    class="icon chevron-down"
                    :class="{ show: isGrow, 'show-menu': isShowMenu }"
                ></section>
            </section>
        </RouterLink>

        <!-- SidebarItem Menu -->
        <Transition name="list-bottom">
            <section
                class="sidebar-item__menu w-100 br-4 flex flex-col"
                v-if="isGrow && hasMenu && isShowMenu"
                ref="sidebarItemMenu"
            >
                <RouterLink
                    v-for="item in listItemMenu"
                    :key="item[0]"
                    :to="item[0].replace(/_/g, '-')"
                    class="h-40 flex item-center pointer col-gap-6 relative sidebar-item__menu__link"
                    active-class="link--active"
                >
                    <section class="sidebar-item__menu__icon flex justify-right item-center">
                        <section class="icon arrow-hidden"></section>
                    </section>

                    <h1 class="sidebar-item__menu__title">{{ item[1] }}</h1>
                </RouterLink>
            </section>
        </Transition>

        <MISATooltip type="right" :content="contentTooltip ?? title" />
    </section>
</template>

<script setup lang="ts">
import type { SidebarItemProps } from '@/types'
import { ref, watch, computed } from 'vue'
import { RouterLink, useRoute, type RouteLocationNormalizedLoaded } from 'vue-router'

const props = defineProps<SidebarItemProps>()

const isShowMenu = ref<boolean>(false)
const sidebarItemMenu = ref<HTMLElement | null>(null)

const route = ref<RouteLocationNormalizedLoaded>(useRoute())

const active = computed(() => {
    if (props.listItemMenu) {
        return props.listItemMenu
            ?.map((item) => `/${item[0].replace(/_/g, '-')}`)
            .includes(route.value.path)
    }
    return route.value.path === props.to
})

watch(
    () => route.value.path,
    (value) => {
        if (
            value !== props.to &&
            !props.listItemMenu?.map((item) => `/${item[0].replace(/_/g, '-')}`).includes(value)
        ) {
            isShowMenu.value = false
        }
    }
)

watch(props, () => {
    if (!props.isGrow) {
        isShowMenu.value = false
    }
})
</script>

<style scoped lang="scss">
@import './MISASidebarItem.scss';
</style>
