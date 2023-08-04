<template>
    <label class="flex-column row-gap-10 t-input-misa" :class="classLabel">
        <p v-if="!!label">{{ label }} <span class="require">*</span></p>
        <section class="relative center-y">
            <section class="wrapper-icon absolute l-7 icon-left" v-if="!!$slots.iconLeft">
                <slot name="iconLeft"></slot>
            </section>

            <input
                :type="type"
                class="t-input w-100"
                :class="[
                    classInput,
                    {
                        't-input-placeholder': placeholderItalics,
                        't-input__icon--left': !!$slots.iconLeft,
                        't-input__icon--right': !!$slots.iconRight
                    },
                    { invalid: invalid },
                    { readonly: readonly }
                ]"
                :placeholder="placeholder"
                ref="input"
                :readonly="readonly"
                :tabindex="tabindex"
                :required="required"
                v-model="inputValue"
                @input="(event) => changeInput(event.target.value)"
                :style="styleInput"
                @focus.stop="$emit('focus')"
                @blur="blur"
                :maxlength="maxlength"
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
        // Class của label
        classLabel: {
            type: String,
            default: ''
        },
        // không được để trống
        required: {
            type: Boolean,
            default: false
        },
        // tiêu đề
        label: {
            type: String,
            default: ''
        },
        // tiêu đề placeholder
        placeholder: {
            type: String,
            default: ''
        },
        // Chữ nghiêng placeholder
        placeholderItalics: {
            type: Boolean,
            default: false
        },
        // chỉ đọc
        readonly: {
            type: Boolean,
            default: false
        },
        // thứ tự tab
        tabindex: {
            type: String,
            default: '0'
        },
        // Chiều rộng của input
        width: {
            type: String,
            default: 'auto'
        },
        // Class của input
        classInput: {
            type: String,
            default: ''
        },
        // Giá trị của input
        modelValue: {
            type: String
        },
        // Content right
        textRight: {
            type: Boolean,
            default: false
        },
        // Loại input (text, number, date, ...)
        typeOfInput: {
            type: String,
            default: 'text'
        },
        // loại input
        type: {
            type: String,
            default: 'text'
        },
        // Số ký tự tối đa
        maxlength: {
            type: String,
            default: '255'
        }
    },
    /**
     * Author: Bùi Huy Tuyền (04/07/2023)
     * Khai báo data cho component
     */
    data() {
        return {
            inputValue: '',
            invalid: false
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
        focus() {
            this.$refs.input.focus()
        },
        /**
         * Author: Bùi Huy Tuyền (11/07/2023)
         * @param {*} value
         * @description: Hàm thay đổi giá trị input
         */
        changeInput(value) {
            if (this.invalid && value !== '') this.invalid = false

            // Nếu là input kiểu số
            if (this.typeOfInput === 'number') {
                value = value
                    .replace(/^0/g, '')
                    .replace(/([^0-9])/g, '')
                    .replace(/\./g, '')
                    .replace(/\B(?=(\d{3})+(?!\d))/g, '.')

                if (value === '') value = '0'
                this.inputValue = value
                this.$emit('update:modelValue', value)
            }
            // Nếu là input kiểu tỷ lệ
            if (this.typeOfInput == 'percent') {
                value = value.replace(/^0/g, '').replace(/([^0-9,])/g, '')
                if (value.includes(',') && value.indexOf(',') !== value.length - 1) {
                    value = value.replace(/,$/g, '')
                }
                if (Number(value) > 100) value = '100'
                if (value === '') value = '0'
                this.inputValue = value
                this.$emit('update:modelValue', value)
            }
            // Nếu là input kiểu text
            if (this.typeOfInput === 'text') {
                this.inputValue = value
                this.$emit('update:modelValue', value)
            }
        },
        /**
         * Author: Bùi Huy Tuyền (04/07/2023)
         * @description: Hàm blur input
         */
        blur() {
            if (this.required) {
                if (this.inputValue === '') {
                    this.invalid = true
                } else {
                    this.invalid = false
                }
            }
        }
    },
    /**
     * Author: Bùi Huy Tuyền (04/07/2023)
     * @description: Hàm khởi tạo giá trị cho component
     */
    created() {
        this.inputValue = this.modelValue
    },
    /**
     * Author: Bùi Huy Tuyền (04/07/2023)
     * @description:  focus vào input khi component được mounted
     */
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
