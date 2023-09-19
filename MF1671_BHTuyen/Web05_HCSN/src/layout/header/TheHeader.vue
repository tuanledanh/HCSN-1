<template>
    <header class="header h-44 item-center flex justify-between px-20">
        <!-- Tiêu đề -->
        <section class="header-left flex item-center">
            <h1 class="header-left__title">{{ headerResource.title }}</h1>
        </section>

        <section class="header-right flex item-center">
            <!-- Organization -->
            <section class="header-right__organization">
                {{ headerResource.organization }}
            </section>

            <!-- Năm hiện tại -->
            <section class="header-right__year flex item-center br-4">
                <span class="header-right__year__title">{{
                    headerResource.year
                }}</span>
                <span
                    class="header-right__year__value fw-700 flex item-center"
                    >{{ year }}</span
                >

                <section class="wrapper-icon flex-col row-gap-3">
                    <section class="chevron-up icon" @click="year++"></section>
                    <section
                        class="chevron-down icon"
                        @click="year--"
                    ></section>
                </section>
            </section>

            <!-- Icon -->
            <section class="header-right__icon flex item-center col-gap-20">
                <section class="wrapper-icon relative">
                    <section class="bell icon"></section>
                    <!-- <m-tooltip content="Thông báo" /> -->
                </section>
                <section class="wrapper-icon relative">
                    <section class="grid icon"></section>
                    <!-- <m-tooltip content="Tùy chọn" /> -->
                </section>
                <section class="wrapper-icon relative">
                    <section class="circle-question icon"></section>
                    <!-- <m-tooltip content="Trợ giúp" /> -->
                </section>
                <section class="user-section flex item-center col-gap-8">
                    <section class="wrapper-icon relative">
                        <section class="circle-user icon"></section>
                        <!-- <m-tooltip content="Avatar" /> -->
                    </section>
                    <section class="chevron-down icon"></section>
                </section>
            </section>
        </section>
    </header>
</template>

<script setup lang="ts">
import type { ResourceData } from '@/types'
import { ref } from 'vue'

interface HeaderProps {
    headerResource: ResourceData['main']['header']
}
defineProps<HeaderProps>()

const year = ref<number>(new Date().getFullYear())
</script>

<style scoped lang="scss">
@import './TheHeader.scss';
</style>
