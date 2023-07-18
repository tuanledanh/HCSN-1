<template>
    <label class="flex-column row-gap-10" :class="classLabel">
        <p v-if="!!label">{{ label }} <span class="require">*</span></p>
        <section class="relative center-y">
            <section class="wrapper-icon absolute l-7" v-if="!!$slots.iconLeft">
                <slot name="iconLeft"></slot>
            </section>

            <input
                type="text"
                class="t-input w-100"
                :class="[
                    classInput,
                    {
                        't-input-placeholder': placeholderItalics,
                        't-input__icon--left': !!$slots.iconLeft,
                        't-input__icon--right': !!$slots.iconRight
                    }
                ]"
                :placeholder="placeholder"
                ref="input"
                :readonly="readonly"
                :tabindex="tabindex"
                :required="required"
                v-model="inputValue"
                @input="(event) => changeInput(event.target.value)"
                :style="styleInput"
            />

            <section class="wrapper-icon absolute r-6" v-if="!!$slots.iconRight">
                <slot name="iconRight"> </slot>
            </section>
        </section>
    </label>
</template>

<script>
export default {
    name: 'MISAInput',
    /**
     * Author: Bùi Huy Tuyền (04/07/2023)
     * ĐỊnh nghĩa props cho component
     */
    props: {
        classLabel: {
            type: String,
            default: ''
        },
        required: {
            type: Boolean,
            default: false
        },
        label: {
            type: String,
            default: ''
        },
        placeholder: {
            type: String,
            default: ''
        },
        focus: {
            type: Boolean,
            default: false
        },
        placeholderItalics: {
            type: Boolean,
            default: false
        },
        readonly: {
            type: Boolean,
            default: false
        },
        tabindex: {
            type: String,
            default: '0'
        },
        width: {
            type: String,
            default: 'auto'
        },
        classInput: {
            type: String,
            default: ''
        },
        modelValue: {
            type: String
        },
        textRight: {
            type: Boolean,
            default: false
        },
        type: {
            type: String,
            default: 'text'
        }
    },
    /**
     * Author: Bùi Huy Tuyền (04/07/2023)
     * Khai báo data cho component
     */
    data() {
        return {
            inputValue: ''
        }
    },
    /**
     * Author: Bùi Huy Tuyền (04/07/2023)
     * Khởi tạo giá trị cho component
     */
    watch: {
        modelValue(value) {
            this.inputValue = value
        }
    },
    methods: {
        /**
         * Author: Bùi Huy Tuyền (04/07/2023))
         * @param
         * @description: Hàm focus vào input
         */
        focusInput() {
            this.$refs.input.focus()
        },
        /**
         * Author: Bùi Huy Tuyền (11/07/2023)
         * @param {*} value
         * @description: Hàm thay đổi giá trị input
         */
        changeInput(value) {
            if (this.type === 'number') {
                value = value
                    .replace(/^0/g, '')
                    .replace(/([^0-9])/g, '')
                    .replace(/\./g, '')
                    .replace(/\B(?=(\d{3})+(?!\d))/g, '.')

                if (value === '') value = '0'
                this.inputValue = value
                this.$emit('update:modelValue', value)
            }
            if (this.type == 'percent') {
                value = value.replace(/^0/g, '').replace(/([^0-9,])/g, '')
                if (value.includes(',') && value.indexOf(',') !== value.length - 1) {
                    value = value.replace(/,$/g, '')
                }
                if (Number(value) > 100) value = '100'
                if (value === '') value = '0'
                this.inputValue = value
                this.$emit('update:modelValue', value)
            }
            if (this.type === 'text') {
                this.inputValue = value
                this.$emit('update:modelValue', value)
            }
        }
    },
    created() {
        this.inputValue = this.modelValue
    },
    /**
     * Author: Bùi Huy Tuyền (04/07/2023)
     * @description:  focus vào input khi component được mounted
     */
    mounted() {
        if (this.focus) {
            this.$refs.input.focus()
        }
    },
    computed: {
        /**
         * Author: Bùi Huy Tuyền (04/07/2023)
         * @description: Hàm định dạng style cho input
         */
        styleInput() {
            return {
                width: this.width,
                textAlign: this.textRight ? 'right' : 'left'
            }
        }
    }
}
</script>
