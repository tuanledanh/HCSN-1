<template>
    <label
        class="t-combobox flex flex-col row-gap-10 relative br-4"
        :class="{ readonly: readonly }"
        @keydown="keyDown"
        ref="combobox"
        @click.prevent="input?.select()"
    >
        <p v-if="!!label" class="t-combobox__label pointer">
            {{ label }} <span class="required" v-if="required">*</span>
        </p>

        <section
            class="t-combobox__wrapper flex item-center relative br-4 h-36"
            :style="`width: ${width}`"
            ref="wrapper"
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
                v-model="comboboxValue"
                :class="{
                    'text-right': textRight,
                    'placeholder--italic': placeholderItalic,
                    invalid: invalid
                }"
                ref="input"
                :style="`padding: ${padding}`"
                :placeholder="placeholder"
                :readonly="readonly"
                :tabindex="tabindex"
                @click.prevent
                @select.prevent
            />
            <!--  -->
            <section
                class="icon-clear ratio-1 h-100 flex item-center justify-center w-10"
                v-if="isShowClear"
                @click.prevent="clickIconClear"
            >
                <section class="icon clear"></section>
            </section>

            <!-- Icon Right -->
            <section
                class="flex center ratio-1 h-100 pointer icon-right br-right-4"
                :class="{ readonly: readonly }"
                @click.prevent="clickIconRight"
            >
                <section class="icon arrow-down" :class="{ rotate: isShowList }"></section>
            </section>
        </section>

        <!-- Combobox list -->
        <Teleport to="body">
            <Transition :name="`list-${positionList}`">
                <section
                    class="t-combobox__list z-999 absolute br-4 px-10"
                    :style="styleList"
                    v-if="isShowList"
                    ref="listRef"
                    @scroll="
                        () => {
                            console.log('first')
                        }
                    "
                >
                    <section v-if="listShow.length > 0">
                        <li
                            class="h-36 flex item-center t-combobox__item br-4 pointer"
                            :class="{ hover: hoverIndex === index, active: activeIndex === index }"
                            v-for="(item, index) in listShow"
                            :key="item"
                            @mouseover="hoverIndex = index"
                            @click.prevent.stop="
                                (activeIndex = hoverIndex),
                                    (isShowList = false),
                                    input?.focus(),
                                    $emit('select', item)
                            "
                        >
                            <section class="h-100 ratio-1 flex center">
                                <section class="icon check" v-if="index == activeIndex"></section>
                            </section>
                            <section
                                class="h-100 flex item-center justify-left t-combobox__item-titel"
                            >
                                {{ item }}
                            </section>
                        </li>
                    </section>
                    <section v-else>
                        <li class="h-36 flex item-center t-combobox__item br-4 pointer">
                            <section class="h-100 ratio-1 flex center"></section>
                            <section
                                class="h-100 flex item-center justify-left t-combobox__item-titel"
                            >
                                {{ not_found_data }}
                            </section>
                        </li>
                    </section>
                </section>
            </Transition>
        </Teleport>
    </label>
</template>

<script setup lang="ts">
import { KeyName } from '@/enum'
import { useOnClickOutside, useResource } from '@/hook'
import type { ComboBoxProps } from '@/types'
import { computed, watch, ref, type SlotsType } from 'vue'

//
const invalid = ref<boolean>(false)

const emits = defineEmits<{
    'click-icon-left': []
    select: [value: string]
}>()

const props = withDefaults(defineProps<ComboBoxProps>(), {
    padding: '0 10px',
    width: '100%',
    list: () => []
})

const slots = defineSlots<{
    'icon-left': SlotsType
}>()

const list = computed(() => props.list)

const comboboxValue = defineModel<string>({ local: true, default: '' })

const isShowList = ref<boolean>(false)

const isShowClear = ref<boolean>(false)

const hoverIndex = ref<number>(-1)

const activeIndex = ref<number>(-1)

const positionList = ref<string>('bottom')

const input = ref<HTMLInputElement>()

const wrapper = ref<HTMLElement | null>(null)
const combobox = ref<HTMLElement | null>(null)

const rect = ref<DOMRect | null>(null)

const listRef = ref<HTMLElement | null>(null)

const isChoose = ref<boolean>(false)

const styleList = ref<{
    top: string
    left: string
    bottom: string
    width: string
}>({
    width: '0px',
    top: '0px',
    left: '0px',
    bottom: '0px'
})

const keydownUp = () => {
    if (isShowList.value) {
        if (!hoverIndex.value) hoverIndex.value = listShow.value.length - 1
        else hoverIndex.value--
        listRef.value?.scroll({
            top: !hoverIndex.value ? 0 : 20 * hoverIndex.value + 10,
            behavior: 'smooth'
        })
    } else clickIconRight()
}

const keydownDown = () => {
    if (isShowList.value) {
        if (hoverIndex.value == listShow.value.length - 1) hoverIndex.value = 0
        else hoverIndex.value++
        listRef.value?.scrollTo({
            top: !hoverIndex.value ? 0 : 20 * hoverIndex.value + 10,
            behavior: 'smooth'
        })
    } else clickIconRight()
}

const focus = () => {
    combobox.value?.focus()
}

const keyDown = (event: KeyboardEvent) => {
    switch (event.key) {
        case KeyName.ARROW_DOWN:
            keydownDown()
            break
        case KeyName.ARROW_UP:
            keydownUp()
            break
        case KeyName.ENTER:
            activeIndex.value = hoverIndex.value
            isShowList.value = false
            emits('select', comboboxValue.value)
            break
        default:
            isChoose.value = false
            hoverIndex.value = 0
            break
    }
}

//
const clickIconRight = () => {
    if (activeIndex.value == -1) hoverIndex.value = 0
    else hoverIndex.value = activeIndex.value

    isShowList.value = !isShowList.value
    input!.value?.focus()

    setPosition()
}

const setPosition = () => {
    rect.value = wrapper.value!.getBoundingClientRect()
    const check = window.innerHeight - rect.value!.top - rect.value!.height - 202 > 0

    styleList.value.left = rect.value!.left + 'px'
    styleList.value.width = rect.value!.width + 'px'

    if (check) {
        positionList.value = 'bottom'
        styleList.value.top = rect.value!.bottom + 2 + 'px'
        styleList.value.bottom = 'unset'
    } else {
        positionList.value = 'top'
        styleList.value.bottom = window.innerHeight - rect.value!.top + 2 + 'px'
        styleList.value.top = 'unset'
    }
}

const clickIconClear = () => {
    setPosition()
    comboboxValue.value = ' '
    input.value?.focus()
    hoverIndex.value = 0
    activeIndex.value = -1
    isShowList.value = true
}

//
const listShow = computed(() => {
    if (isChoose.value) {
        return list.value
    }
    return list.value.filter((item: string) => {
        return item.toLowerCase().includes(comboboxValue.value.toLowerCase())
    })
})

//
watch(comboboxValue, (value) => {
    isShowClear.value = !!value
})

watch(activeIndex, (value) => {
    comboboxValue.value = listShow.value[value]
})

watch(isShowList, (value) => {
    if (value && activeIndex.value != -1) {
        console.log('first')
    } else isChoose.value = true
})

useOnClickOutside(combobox, () => {
    isShowList.value = false
})

defineExpose({
    focus
})

const {
    combobox: { not_found_data }
} = useResource()
</script>

<style scoped lang="scss">
@import './MISACombobox.scss';
</style>
