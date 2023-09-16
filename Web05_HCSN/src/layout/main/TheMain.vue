<template>
    <section class="main-layout flex flex-col">
        <TheHeader :header-resource="header" />
        <main class="main-layout__router-view">
            <RouterView />
        </main>
    </section>
</template>

<script setup lang="ts">
import { useResource } from '@/hook'

import TheHeader from '@/layout/header'

const {
    main: { header }
} = useResource()
</script>

<style scoped lang="scss">
@import './TheMain.scss';
</style>
