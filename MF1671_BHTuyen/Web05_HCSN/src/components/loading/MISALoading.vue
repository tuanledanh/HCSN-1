<template>
    <!-- Loading -->
    <section class="t-loading modal flex center" v-if="loading">
        <section class="loader"></section>
    </section>
</template>

<script setup lang="ts">
import type { LoadingProps } from '@/types'

withDefaults(defineProps<LoadingProps>(), {
    loading: false
})
</script>

<style scoped lang="scss">
@import './MISALoading.scss';
</style>
