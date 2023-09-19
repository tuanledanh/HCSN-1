<template>
    <section
        class="t-button flex item-center br-4 pointer justify-center"
        :class="`t-button-${typeButton} ${circle ? 'circle' : ''} ${disable ? 'disable' : ''} ${
            tooltip ? 'tooltip' : ''
        }`"
        :style="`height: ${height};padding: ${padding};width: ${width};border: ${
            border ? '' : 'none'
        }`"
        :tabindex="disable ? '-1' : tabindex"
        ref="button"
        @click="clickButton"
        @keydown.enter.stop="clickButton"
    >
        <!-- icon-left -->
        <section
            class="flex center t-button__icon-left h-100 ratio-1"
            :class="`t-button-${typeButton}__icon-left`"
            v-if="!!slots['icon-left']"
        >
            <slot name="icon-left"></slot>
        </section>

        <!-- Button title -->
        <p class="pointer t-button__title" :class="`t-button-${typeButton}__title`" v-if="!!title">
            {{ title }}
        </p>

        <!-- Icon Right -->
        <section
            class="flex center t-button__icon-right h-100 ratio-1"
            :class="`t-button-${typeButton}__icon-right`"
            v-if="slots['icon-right']"
        >
            <slot name="icon-right"></slot>
        </section>

        <!-- Button Icon -->
        <section
            class="flex center col-gap-10 h-100"
            :class="`t-button-${typeButton}__icon`"
            v-if="slots['icon']"
        >
            <slot name="icon"></slot>
        </section>
        <MISATooltip :content="tooltipContent" :type="tooltipType" />
    </section>
</template>

<script setup lang="ts">
import { TooltipType } from '@/enum'
import type { ButtonProps } from '@/types'
import { ref, type SlotsType } from 'vue'

// Định nghĩa props
withDefaults(defineProps<ButtonProps>(), {
    title: '',
    typeButton: 'main',
    tabindex: '0',
    height: '36px',
    width: 'auto',
    padding: '0',
    circle: false,
    border: true,
    disable: false,
    tooltip: false,
    tooltipContent: '',
    tooltipType: TooltipType.BOTTOM
})

// Định nghĩa Slot
const slots = defineSlots<{
    'icon-right': SlotsType
    'icon-left': SlotsType
    icon: SlotsType
}>()

// Ref đến Button
const button = ref<HTMLElement | null>(null)

const focus = () => {
    button.value?.focus()
}

// Khai báo Emit
const emit = defineEmits<{
    click: []
}>()

// Method clickButton
const clickButton = () => {
    button.value?.focus()
    emit('click')
}

defineExpose({
    focus
})
</script>

<style scoped lang="scss">
@import './MISAButton.scss';
</style>
