<template>
    <section
        class="t-dropdown flex center br-4 pointer relative"
        :style="{
            height: height,
            width: width
        }"
        tabindex="0"
        ref="dropdown"
        @click="clickDropdown"
        @keydown.enter="isShowDropdownList ? selectItem(list[indexHover]) : null"
        @keydown.esc.stop="isShowDropdownList = false"
        @keydown.up.stop="keyupArrowUp"
        @keydown.down.stop="keyupArrowDown"
    >
        <input
            type="text"
            class="t-dropdown__input h-100 ratio-1 text-right pointer"
            v-model="dropdownValue"
            readonly
            tabindex="-1"
        />
        <section class="t-dropdown__wrap-icon h-100 ratio-1 flex center">
            <section
                class="icon chevron-down"
                :style="`transform: rotate(${isShowDropdownList ? 180 : 0}deg)`"
            ></section>
        </section>

        <!-- Dropdown list -->
        <section
            class="t-dropdown__list absolute w-100 br-4 flex flex-col row-gap-3"
            :class="`t-dropdown__list--${position}`"
            ref="dropdownList"
            v-if="isShowDropdownList"
        >
            <section
                v-for="(item, index) in list"
                :key="item"
                class="t-dropdown__item br-4 flex center"
                :class="{ 't-dropdown__item--hover': indexHover === index }"
                @click.prevent.stop="selectItem(item)"
                @mouseenter="indexHover = index"
            >
                {{ item }}
            </section>
        </section>
    </section>
</template>

<script setup lang="ts">
import { ref } from 'vue'
import { useOnClickOutside } from '@/hook'
import type { Dropdown } from '@/types'

// Định nghĩa props cho dropdown
const props = withDefaults(defineProps<Dropdown>(), {
    width: '80px',
    height: '36px',
    list: () => [],
    position: 'bottom'
})

// Refs tham chiếu đến dropdown
const dropdown = ref<HTMLElement | null>(null)

// Refs tham chiếu đến dropdown list
const dropdownList = ref<HTMLElement | null>(null)

// Trạng thái hiển thị dropdown list
const isShowDropdownList = ref<boolean>(false)

const indexHover = ref<number>(0)

// Định nghĩa giá trị binding cho dropdown
const dropdownValue = defineModel<number>({ local: true })

// Hàm click vào dropdown
const clickDropdown = () => {
    isShowDropdownList.value = !isShowDropdownList.value
    dropdown.value?.focus()
}

// Hàm chọn item trong dropdown list
const selectItem = (value: number) => {
    dropdownValue.value = value
    console.log(value)
    isShowDropdownList.value = false
}

// Khi ấn nút ArrowDown
const keyupArrowDown = () => {
    if (isShowDropdownList.value) {
        if (indexHover.value == props.list.length - 1) {
            indexHover.value = 0
        } else {
            indexHover.value++
        }
        dropdownList.value?.scroll({
            top: indexHover.value * 15,
            behavior: 'smooth'
        })
    } else {
        isShowDropdownList.value = true
    }
}

// Khi ấn nút ArrowUp
const keyupArrowUp = () => {
    if (isShowDropdownList.value) {
        if (indexHover.value == 0) {
            indexHover.value = props.list.length - 1
        } else {
            indexHover.value--
        }
    } else {
        isShowDropdownList.value = true
    }
    dropdownList.value?.scroll({
        top: indexHover.value * 15,
        behavior: 'smooth'
    })
}

// Ẩn dropdown list khi click ra ngoài
useOnClickOutside(dropdown, () => {
    isShowDropdownList.value = false
})
</script>

<style scoped lang="scss">
@import './MISADropdown.scss';
</style>
