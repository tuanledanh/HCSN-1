<template>
    <button
        class="center-y pointer br-4 outline-none m-button relative"
        :class="[classButton, { hasIcon: !!slots.icon }]"
        :disabled="disable"
        :style="styleButton"
        :type="type"
        @click="$emit('clickButton')"
    >
        <section class="wrapper-btn-icon center" v-if="!!slots.icon">
            <slot name="icon"></slot>
        </section>

        <section class="wrapper-btn-title button-text center" v-if="!!slots.default">
            <slot></slot>
        </section>

        <div class="wrapper-btn-combo" v-if="!!slots.combo">
            <slot name="combo" class="btn-combo"></slot>
        </div>
    </button>
</template>

<script setup>
import { computed, useSlots } from 'vue'

/**
 * Author: Bùi Huy Tuyền (04/07/2023)
 * Lấy ra các slot của component
 */
const slots = useSlots()

/**
 * Author: Bùi Huy Tuyền (04/07/2023)
 * Định nghĩa props cho component
 */
const props = defineProps({
    // Kiểu button
    typeButton: {
        type: String,
        default: 'main',
        validator: function (value) {
            return ['main', 'sub', 'outline', 'icon'].indexOf(value) !== -1
        }
    },
    // Vô hiệu hóa button
    disable: {
        type: Boolean,
        default: false
    },

    // The `height` property is defined as a prop for the Vue component. It accepts a string value and
    // has a default value of `'36px'`.
    height: {
        type: String,
        default: '36px',
        validator: (value) => {
            return ['32px', '36px', '40px'].indexOf(value) !== -1
        }
    },
    // The `width` property is defined as a prop for the Vue component. It accepts a string value and
    // has a default value of `'80px'`.
    width: {
        type: String,
        default: '80px',
        validator: (value) => {
            return parseInt(value.replace('px', '')) >= 80
        }
    },
    // The `type` property is defined as a prop for the Vue component. It accepts a string value and has
    // a default value of `'button'`.
    type: {
        type: String,
        default: 'button',
        validator: (value) => {
            return ['button', 'submit', 'reset'].indexOf(value) !== -1
        }
    }
})

/**
 * Author: Bùi Huy Tuyền (04/07/2023)
 * Tính style thông qua các props truyền vào
 */
const styleButton = computed(() => ({
    height: props.height,
    minWidth: props.width
}))

/**
 * Author: Bùi Huy Tuyền (04/07/2023)
 * Tính class thông qua các props truyền vào
 */
const classButton = computed(() => `m-button-${props.typeButton}`)
</script>
