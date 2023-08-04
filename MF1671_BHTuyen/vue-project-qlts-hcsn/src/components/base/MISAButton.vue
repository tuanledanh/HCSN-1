import { watch } from 'vue';
<template>
    <button
        class="center-y pointer br-4 outline-none m-button relative"
        :class="[classButton, { hasIcon: !!$slots.icon }, { buttonIcon: typeButton === 'icon' }]"
        :disabled="disable"
        :style="styleButton"
        :type="type"
        @click="$emit('clickButton')"
        ref="button"
    >
        <section class="wrapper-btn-icon center" v-if="!!$slots.icon">
            <slot name="icon"></slot>
        </section>

        <section class="wrapper-btn-title button-text center" v-if="!!$slots.default">
            <slot></slot>
        </section>

        <div class="wrapper-btn-combo" v-if="!!$slots.combo">
            <slot name="combo" class="btn-combo"></slot>
        </div>
    </button>
</template>

<script>
export default {
    name: 'MISAButton',
    props: {
        /**
         * @type {String}
         * @default main
         * @description Kiểu của button (main, sub, outline, icon)
         * @author Bùi Huy Tuyền (27-07-2023)
         */
        typeButton: {
            type: String,
            default: 'main',
            validator: function (value) {
                return ['main', 'sub', 'outline', 'icon'].indexOf(value) !== -1
            }
        },
        /**
         * @type {Boolean}
         * @default false
         * @description Trạng thái disable của button
         * @Author Bùi Huy Tuyền (27/07/2023)
         */
        disable: {
            type: Boolean,
            default: false
        },
        /**
         * @type {String}
         * @default 36px
         * @description Chiều cao của button (px)
         * @Author Bùi Huy Tuyền (27/07/2023)
         */
        height: {
            type: String,
            default: '36px',
            validator: (value) => {
                return ['32px', '36px', '40px'].indexOf(value) !== -1
            }
        },
        /**
         * @type {String}
         * @default 80px
         * @description Chiều rộng của button (px)
         * @author Bùi Huy Tuyền (27/07/2023)
         */
        width: {
            type: String,
            default: '80px',
            validator: (value) => {
                return parseInt(value.replace('px', '')) >= 80
            }
        },
        /**
         * @type {String}
         * @default button
         * @description Kiểu của button (button, submit, reset)
         * @author Bùi Huy Tuyền (27/07/2023)
         */
        type: {
            type: String,
            default: 'button',
            validator: (value) => {
                return ['button', 'submit', 'reset'].indexOf(value) !== -1
            }
        },
        /**
         * @type {Boolean}
         * @default false
         * @description Trạng thái focus của button
         * @author Bùi Huy Tuyền (27/07/2023)
         */
        focus: {
            type: Boolean,
            default: false
        }
    },
    methods: {
        focusButton() {
            this.$refs.button.focus()
        }
    },
    computed: {
        styleButton() {
            return {
                height: this.height,
                minWidth: this.width
            }
        },
        classButton() {
            return `m-button-${this.typeButton}`
        }
    },
    mounted() {
        if (this.focus) {
            this.focusButton()
        }
    },
    watch: {
        focus(value) {
            console.log(value)
            if (value) {
                this.focusButton()
            }
        }
    }
}
</script>
