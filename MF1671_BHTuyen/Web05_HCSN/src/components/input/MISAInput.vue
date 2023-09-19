<template>
    <label class="t-input flex flex-col row-gap-10 br-4" :style="`width: ${width}; `">
        <p v-if="!!label" class="t-input__label pointer">
            {{ label }} <span class="required" v-if="required">*</span>
        </p>

        <section
            class="t-input__wrap flex item-center br-4 h-36 w-100"
            :class="{ readonly: readonly }"
            @click="input?.select()"
            @keydown.up.prevent="type === InputType.NUMBER ? increaseInputValue() : null"
            @keydown.down="type === InputType.NUMBER ? decreaseInputValue() : null"
        >
            <!-- Icon Left -->
            <section
                v-if="!!slots['icon-left']"
                class="flex center ratio-1 h-100 pointer icon-left br-left-4"
                @click.prevent.stop="$emit('click-icon-left')"
            >
                <slot name="icon-left"></slot>
            </section>

            <!-- Input -->
            <input
                type="text"
                class="input br-4 h-100"
                v-model="inputValue"
                :class="{
                    'text-right': textRight,
                    'placeholder--italic': placeholderItalic,
                    invalid: invalid
                }"
                ref="input"
                :style="`padding: ${hasIconChangeNumber ? '0 0 0 10px' : padding};`"
                :placeholder="placeholder"
                :readonly="readonly"
                :tabindex="readonly ? '-1' : tabindex"
                @input="$emit('input')"
            />
            <section v-if="hasIconChangeNumber" class="h-100 flex item-center justify-center w-30">
                <section class="wrapper-icon flex-col row-gap-5">
                    <section
                        class="chevron-up icon"
                        @click.prevent.stop="increaseInputValue"
                    ></section>
                    <section
                        class="chevron-down icon"
                        @click.prevent.stop="decreaseInputValue"
                    ></section>
                </section>
            </section>

            <!-- Icon Right -->
            <section
                v-if="!!slots['icon-right']"
                class="flex center h-100 ratio-1 pointer icon-right br-right-4"
            >
                <slot name="icon-right"></slot>
            </section>
        </section>
    </label>
</template>

<script setup lang="ts">
import type { InputProps } from '@/types'
import { type SlotsType, ref, onMounted, watch, computed } from 'vue'
import { InputType } from '@/enum'

const invalid = ref<boolean>(false)

const props = withDefaults(defineProps<InputProps>(), {
    padding: '0 10px',
    width: '100%',
    tabindex: '0',
    type: InputType.TEXT,
    isFocus: false,
    hasIconChangeNumber: false,
    maxLength: 255
})

const slots = defineSlots<{
    'icon-left': SlotsType
    'icon-right': SlotsType
}>()

defineEmits<{
    'click-icon-left': []
    input: []
}>()

const inputValue = defineModel<string | number>({ local: true, default: '' })

const isFocus = computed(() => props.isFocus)

const focus = () => {
    input.value?.focus()
}

defineExpose({ focus })

watch(isFocus, (value) => {
    if (value) {
        input.value?.focus()
    }
})

onMounted(() => {
    if (isFocus.value) input.value?.focus()
    if (props.type === InputType.NUMBER || props.type === InputType.PERCENT) {
        inputValue.value = '0'
    }
})

watch(inputValue, (value) => {
    if (props.type === InputType.NUMBER) {
        inputValue.value = formatNumber(value)

        if (inputValue.value.length == 1) inputValue.value = '0' + inputValue.value
    }

    if (props.type === InputType.PERCENT) {
        inputValue.value = value.toString().replace(/\./g, ',')
    }

    if (props.type === InputType.CODE) {
        if (inputValue.value.toString().length <= props.maxLength) {
            inputValue.value = formatCode(value)
        } else {
            inputValue.value = formatCode(value.toString().slice(0, props.maxLength))
        }
    }

    if (props.type === InputType.TEXT) {
        if (inputValue.value.toString().length > props.maxLength) {
            inputValue.value = value.toString().slice(0, props.maxLength)
        }
    }
})

const increaseInputValue = () => {
    inputValue.value = Number(inputValue.value.toString().replace(/\./g, '')) + 1
    inputValue.value = formatNumber(inputValue.value)
}
const decreaseInputValue = () => {
    if (Number(inputValue.value.toString().replace(/\./g, '')) <= 0) return
    inputValue.value = Number(inputValue.value.toString().replace(/\./g, '')) - 1
    inputValue.value = formatNumber(inputValue.value)
}

const formatNumber = (number: string | number) =>
    number
        .toString()
        .replace(/^0/g, '')
        .replace(/([^0-9])/g, '')
        .replace(/\./g, '')
        .replace(/\B(?=(\d{3})+(?!\d))/g, '.')

const formatCode = (text: number | string) => text.toString().replace(/([^0-9,a-z,A-Z])/g, '')

const input = ref<HTMLInputElement | null>(null)
</script>

<style scoped lang="scss">
@import './MISAInput.scss';
</style>
