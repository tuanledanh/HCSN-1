<template>
    <Transition name="fade">
        <section v-if="type === ToastMessageType.WARNING && show" class="modal flex center z-999">
            <section class="t-toast-warning flex flex-col br-4">
                <main class="t-toast-warning__body flex">
                    <section class="t-toast-warning__body--icon">
                        <section class="icon warning"></section>
                    </section>
                    <section class="t-toast-warning__body--message flex item-center">
                        <slot></slot>
                    </section>
                </main>
                <footer class="t-toast-warning__footer flex justify-right item-center col-gap-10">
                    <MISAButton
                        type-button="outline"
                        :title="outlineButton"
                        width="95px"
                        padding="0 16px"
                        v-if="!!outlineButton"
                        @click="$emit('click-outline')"
                    />
                    <MISAButton
                        type-button="sub"
                        :title="subButton"
                        width="95px"
                        padding="0 16px"
                        v-if="!!subButton"
                        @click="$emit('click-sub')"
                    />
                    <MISAButton
                        :title="mainButton || confirm"
                        width="95px"
                        padding="0 16px"
                        @click="$emit('click-main')"
                    />
                </footer>
            </section>
        </section>
    </Transition>

    <Transition name="toast">
        <section v-if="type === ToastMessageType.SUCCESS && show" class="t-toast-success br-4 flex">
            <section class="t-toast-success--icon flex center h-100">
                <section class="wrapper-success flex center">
                    <section class="icon success"></section>
                </section>
            </section>
            <section class="t-toast-success--message flex item-center h-100">
                {{ message }}
            </section>
        </section>
    </Transition>
</template>

<script setup lang="ts">
import { ToastMessageType } from '@/enum'
import type { ToastMessageProps } from '@/types'
import { watch } from 'vue'
import { useResource } from '@/hook'

const props = withDefaults(defineProps<ToastMessageProps>(), {
    type: ToastMessageType.WARNING
})

const show = defineModel<boolean>({ local: true, default: false, required: false })

defineEmits<{
    'click-outline': []
    'click-sub': []
    'click-main': []
}>()

const {
    toast_message: { confirm }
} = useResource()

watch(
    () => show.value,
    (value) => {
        if (
            (props.type == ToastMessageType.SUCCESS || props.type == ToastMessageType.ERROR) &&
            value
        ) {
            const timmer = setTimeout(() => {
                show.value = false
                clearTimeout(timmer)
            }, 3000)
        }
    }
)
</script>

<style scoped lang="scss">
@import './MISAToastMessage.scss';
</style>
