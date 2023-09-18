<template>
    <section
        class="absolute t-tooltip br-4"
        :class="`t-tooltip--${type}`"
        :style="{
            top: top ? top + 'px' : '',
            left: left ? left + 'px' : '',
            display: show ? 'block' : 'none'
        }"
    >
        <section class="t-tooltip__wrapper relative br-4 center">
            <h1 class="t-tooltip__content">{{ content }}</h1>
        </section>
    </section>
</template>

<script setup lang="ts">
import type { TooltipProps } from '@/types'
import { TooltipType } from '@/enum'
withDefaults(defineProps<TooltipProps>(), {
    content: '',
    type: TooltipType.BOTTOM
})
</script>

<style scoped lang="scss">
@import './MISATooltip.scss';
</style>
