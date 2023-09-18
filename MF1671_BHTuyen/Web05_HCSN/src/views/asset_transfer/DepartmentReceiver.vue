<template>
    <section class="department-receive flex flex-col item-center w-100 fadeIn">
        <section class="label h-36 grid w-100 item-center col-gap-20">
            <h1>{{ index }}</h1>
            <h1>{{ fullname }}</h1>
            <h1>{{ delegate }}</h1>
            <h1>{{ position }}</h1>
        </section>
        <section class="wrapper flex flex-col w-100 row-gap-10" ref="wrapper">
            <section
                class="body h-36 grid w-100 item-center col-gap-20"
                v-for="(receiver, index) in receivers"
                :key="receiver.ReceiverId"
            >
                <section class="index flex center h-100 br-4">{{ index + 1 }}</section>
                <MISAInput
                    :placeholder="fullname_input"
                    placeholder-italic
                    padding="0 15px"
                    v-model.trim="receiver.Fullname"
                />
                <MISAInput
                    :placeholder="delegate_input"
                    placeholder-italic
                    padding="0 15px"
                    v-model.trim="receiver.Delegate"
                />
                <MISAInput
                    :placeholder="position_input"
                    placeholder-italic
                    padding="0 15px"
                    v-model.trim="receiver.Position"
                />
                <section class="flex item-center col-gap-6">
                    <MISAButton
                        type-button="icon"
                        width="30px"
                        height="30px"
                        circle
                        v-if="receiver.ReceiverOrder >= 2"
                        @click="swapReceiver(index, index - 1)"
                        tooltip
                        tooltip-content="Chuyển lên"
                        tooltip-type="right"
                    >
                        <template #icon>
                            <section class="icon up"></section>
                        </template>
                    </MISAButton>
                    <MISAButton
                        type-button="icon"
                        width="30px"
                        height="30px"
                        circle
                        v-if="receiver.ReceiverOrder < receivers.length"
                        @click="swapReceiver(index, index + 1)"
                        tooltip
                        tooltip-content="Chuyển xuống"
                        tooltip-type="right"
                    >
                        <template #icon>
                            <section class="icon down"></section>
                        </template>
                    </MISAButton>
                    <MISAButton
                        type-button="icon"
                        width="30px"
                        height="30px"
                        circle
                        @click="addReceiver"
                        tooltip
                        tooltip-content="Thêm"
                        tooltip-type="right"
                    >
                        <template #icon>
                            <section class="icon add"></section>
                        </template>
                    </MISAButton>
                    <MISAButton
                        type-button="icon"
                        width="30px"
                        height="30px"
                        circle
                        @click="deleteReceiver(index)"
                        v-if="receivers.length > 1"
                        tooltip
                        tooltip-content="Xóa"
                        tooltip-type="right"
                    >
                        <template #icon>
                            <section class="icon delete"></section>
                        </template>
                    </MISAButton>
                </section>
            </section>
        </section>
    </section>
</template>

<script setup lang="ts">
import { useResource } from '@/hook'
import type { DepartmentReceiverProps, Receiver, ReceiverFlag } from '@/types'
import { onMounted, ref, watch } from 'vue'
import { PopupDetailMode } from '@/enum'

const receivers = defineModel<Receiver[]>('receivers', { local: true, default: [] })
const receiverFlags = defineModel<ReceiverFlag[]>('receiverFlags', { local: true, default: [] })

const props = withDefaults(defineProps<DepartmentReceiverProps>(), {})

const addReceiver = () => {
    receivers.value.push({ ReceiverOrder: receivers.value.length + 1 } as Receiver)
    const timmer = setTimeout(() => {
        wrapper.value?.scrollTo(0, wrapper.value.scrollHeight)
        clearTimeout(timmer)
    }, 0)
}

const swapReceiver = (indexPrev: number, indexNext: number) => {
    const receiverPrev = receivers.value[indexPrev]
    const receiverNext = receivers.value[indexNext]
    receivers.value[indexPrev] = JSON.parse(JSON.stringify(receiverNext))
    receivers.value[indexNext] = JSON.parse(JSON.stringify(receiverPrev))
    receivers.value[indexPrev].ReceiverOrder = indexPrev + 1
    receivers.value[indexNext].ReceiverOrder = indexNext + 1
    console.log(receivers.value)
}

const wrapper = ref<HTMLElement | null>(null)

const deleteReceiver = (index: number) => {
    if (props.popupDetailMode == PopupDetailMode.UPDATE) {
        const receiver = receivers.value[index]
        if (receiver.ReceiverId && receiver.TransferAssetId) {
            receiverFlags.value.push({ Receiver: receiver, ActionMode: 2 })
        }
    }
    receivers.value.splice(index, 1)
    receivers.value.forEach((receiver, index) => {
        receiver.ReceiverOrder = index + 1
    })
}

onMounted(() => {
    if (receivers.value.length === 0) receivers.value.push({ ReceiverOrder: 1 } as Receiver)
})

watch(
    () => receivers.value,
    () => {}
)

const {
    department_receive: {
        index,
        delegate,
        fullname,
        position,
        delegate_input,
        position_input,
        fullname_input
    }
} = useResource()
</script>

<style scoped lang="scss">
@import './DepartmentReceiver.scss';
</style>
