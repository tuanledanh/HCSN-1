<template>
    <section
        class="t-popup w-100 h-100 flex flex-col br-4"
        @keydown.esc="emit('close')"
        tabindex="0"
        ref="popup"
    >
        <!-- Header -->
        <header class="t-popup__header h-50 flex item-center justify-between px-20 br-top-4">
            <h2 class="t-popup__title">{{ heading }}</h2>
            <MISAButton
                type-button="icon"
                width="24px"
                height="24px"
                :border="false"
                circle
                tooltip
                :tooltip-content="popupResource.tootip_close"
                :tooltip-type="TooltipType.BOTTOM"
                @click="emit('close')"
            >
                <template #icon>
                    <section class="icon close"></section>
                </template>
            </MISAButton>
        </header>

        <!-- Body -->
        <main class="t-popup__body">
            <slot></slot>
        </main>

        <!-- Footer -->
        <footer
            class="t-popup__footer h-50 flex item-center justify-right col-gap-10 px-7 br-bottom-4"
        >
            <MISAButton
                :title="popupResource.title_button_cancel"
                type-button="outline"
                width="80px"
                @click="emit('close')"
            />
            <MISAButton
                :title="popupResource.title_button_save"
                width="80px"
                @click="emit('submit')"
                :disable="disableSubmit"
            />
        </footer>
    </section>
</template>

<script setup lang="ts">
import { TooltipType } from '@/enum'
import { useResource } from '@/hook'
import type { PopupProps } from '@/types'
import { onMounted, ref } from 'vue'

defineProps<PopupProps>()

const { popup: popupResource } = useResource()

const emit = defineEmits<{ close: []; submit: [] }>()

const popup = ref<HTMLElement | null>(null)

const focus = () => {
    popup.value?.focus()
}

defineExpose({ focus })

onMounted(() => {
    popup.value?.focus()
})
</script>

<style scoped lang="scss">
@import './MISAPopup.scss';
</style>
