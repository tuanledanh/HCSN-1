<template>
    <section class="t-datepicker pointer br-4" :style="`width: ${width}`" ref="wrapper">
        <p class="t-datepicker__title" @click="clickIcon">
            {{ label }} <span style="color: red">*</span>
        </p>

        <section
            class="t-datepicker__wrap flex item-center justify-center h-36 br-4 w-100"
            :class="{ invalid: invalid, focus: isFocus }"
        >
            <ElDatePicker
                v-model="datepickerValue"
                placeholder="dd/mm/yyyy"
                format="DD/MM/YYYY"
                value-format="YYYY-MM-DD"
                type="date"
                size="default"
                ref="datepicker"
                style="width: 100%"
            />
            <section
                class="t-datepicker__icon ratio-1 flex center h-100"
                @click.stop.prevent="clickIcon"
            >
                <section class="icon datepicker"></section>
            </section>
        </section>
    </section>
</template>

<script setup lang="ts">
import { useOnClickOutside } from '@/hook'
import type { DatePickerProps } from '@/types'
import { ElDatePicker, type DatePickerInstance } from 'element-plus'
import { ref } from 'vue'

withDefaults(defineProps<DatePickerProps>(), {
    label: 'Date',
    width: '100%',
    invalid: false
})

const datepicker = ref<DatePickerInstance | null>(null)
const wrapper = ref<HTMLElement | null>(null)
const isShow = ref<boolean>(false)
const isFocus = ref<boolean>(false)
const datepickerValue = defineModel<string>({ local: true, default: '' })

const clickIcon = () => {
    console.log('ll')
    isFocus.value = true
    if (isShow.value) {
        datepicker.value?.handleClose()
        isShow.value = false
    } else {
        datepicker.value?.handleOpen()
        isShow.value = true
    }
}

useOnClickOutside(wrapper, () => {
    isFocus.value = false
    isShow.value = false
})
</script>

<style scoped lang="scss">
@import './MISADatePicker.scss';
</style>
