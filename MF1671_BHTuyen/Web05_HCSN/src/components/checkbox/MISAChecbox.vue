<template>
    <section
        class="t-checkbox icon br-4"
        :class="`${checked ? 't-checkbox--checked' : ''} 
        ${disable ? 't-checkbox--disable' : ''}`"
        @dblclick.stop
        @click.stop.prevent="onClick"
        tabindex="0"
        ref="checkbox"
        @keydown.enter.stop="onClick"
    ></section>
</template>

<script setup lang="ts">
import type { CheckboxProps } from '@/types'
import { ref, watch } from 'vue'

// Khai báo v-model
const props = withDefaults(defineProps<CheckboxProps>(), {
    checked: false
})

const checked = ref<boolean>(props.checked)

watch(
    () => props.checked,
    (value) => {
        checked.value = value
        console.log(checked.value)
    }
)

const emit = defineEmits<{
    onChecked: [checked: boolean]
}>()

// Method Click
const onClick = () => {
    checked.value = !checked.value
    emit('onChecked', checked.value)
}
</script>

<style scoped lang="scss">
@import './MISACheckbox.scss';
</style>
