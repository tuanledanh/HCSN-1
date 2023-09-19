<template>
    <section class="flex item-center col-gap-16">
        <section class="flex item-center col-gap-6 t-select-info">
            <span>{{ selected }}</span>
            <span class="fw-500">{{ selectedTotal }}</span>
        </section>

        <section class="pointer t-select-info__unselected" @click="$emit('unselected')">
            {{ unselected }}
        </section>

        <section
            class="pointer br-4 t-select-info__delete"
            tabindex="0"
            @click="$emit('delete')"
            v-if="buttonDelete"
        >
            {{ to_delete }}
        </section>
    </section>
</template>

<script setup lang="ts">
import type { SelectInfoProps } from '@/types'
import { useResource } from '@/hook'

withDefaults(defineProps<SelectInfoProps>(), {
    selectedTotal: 0,
    buttonDelete: true
})

defineEmits(['delete', 'unselected'])

const {
    select_info: { selected, unselected, to_delete }
} = useResource()
</script>

<style scoped lang="scss">
@import './MISASelectInfo.scss';
</style>
