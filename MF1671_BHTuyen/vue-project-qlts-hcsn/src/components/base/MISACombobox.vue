<template>
    <label
        class="flex-column t-combobox row-gap-10 pointer"
        :class="classLabel"
        :style="styleCombobox"
    >
        <p v-if="!!label">{{ label }} <span class="require" v-if="required">*</span></p>
        <section class="relative center-y" v-click-outside="() => (isShowListSelect = false)">
            <section class="wrapper-icon absolute l-7 left" v-if="!!$slots.iconLeft">
                <slot name="iconLeft"></slot>
            </section>
            <input
                type="text"
                class="t-input w-100"
                :class="[
                    classInput,
                    {
                        't-combobox-placeholder': placeholderItalics,
                        't-combobox__icon--left': !!$slots.iconLeft,
                        't-combobox__icon--right': !!$slots.iconRight
                    },
                    { invalid: invalid }
                ]"
                :placeholder="placeholder"
                :required="required"
                :value="inputValue"
                @input="
                    (event) => {
                        inputValueChange(event.target.value)
                    }
                "
                @keyup.stop="(event) => selectWithKeyArrow(event)"
                @keydown.esc="inputValue !== '' ? (inputValue = '') : null"
                @blur.stop="blurInput"
                :maxlength="maxLength"
                @dblclick="isShowListSelect = !isShowListSelect"
                ref="input"
                :title="inputValue"
            />

            <section
                class="wrapper-icon icon-close absolute r-6"
                style="margin-right: 20px"
                v-if="inputValue !== ''"
                @click="inputValue = ''"
                z-index="0"
            >
                <section class="icon close"></section>
            </section>

            <section class="wrapper-icon icon-down absolute r-6" @click="toggleIcon">
                <section class="icon down"></section>
            </section>

            <ul
                class="combobox__select--list absolute br-4 w-100"
                v-show="isShowListSelect"
                ref="list"
            >
                <section v-if="listItemView.length > 0">
                    <li
                        class="combobox__select--item center-y col-gap-6"
                        v-for="(item, index) in listItemView"
                        :key="item"
                        :class="{ active: indexFocusActive === index || indexChoose === index }"
                        @mouseenter="this.indexFocusActive = index"
                        @click="selectItem(item)"
                    >
                        <section class="wrapper-icon">
                            <section class="icon check" v-if="indexChoose === index"></section>
                        </section>
                        <span class="title" :title="item">{{ item }}</span>
                    </li>
                </section>
                <li class="combobox__select--item" v-else>{{ this.$_MISAResource.VN.noResult }}</li>
            </ul>
        </section>
    </label>
</template>

<script>
import { useIsLoading } from '/src/stores/isLoading.js'
export default {
    name: 'MISACombobox',
    /**
     * Author: Bùi Huy Tuyền (04/07/2023)
     * ĐỊnh nghĩa props cho component
     */
    props: {
        // Label của combobox
        label: {
            type: String,
            default: ''
        },
        // Bắt buộc nhập
        required: {
            type: Boolean,
            default: false
        },
        // Placeholder cho combobox
        placeholder: {
            type: String,
            default: ''
        },
        // Placeholder in italics
        placeholderItalics: {
            type: Boolean,
            default: false
        },
        // Danh sách item hiển thị
        listSelect: {
            type: Array,
            default: () => []
        },
        // Class cho Label
        classLabel: {
            type: String,
            default: ''
        },
        // Chiều rộng của combobox
        width: {
            type: String,
            default: '100%'
        },
        // Style cho combobox
        classInput: {
            type: String,
            default: ''
        },
        // Two way binding
        modelValue: {
            type: String,
            default: ''
        },
        // Số ký tự tối đa
        maxLength: {
            type: String,
            default: '20'
        }
    },
    /**
     * Author: Bùi Huy Tuyền (04/07/2023)
     * Data của component
     */
    data() {
        return {
            // Hiển thị list select
            isShowListSelect: false,
            // Danh sách item hiển thị
            listItemView: this.listSelect,
            // Vị trí focus active
            indexFocusActive: 0,
            // Giá trị input
            inputValue: '',
            // Trạng thái input
            invalid: false,
            // vị trí được chọn
            indexChoose: null
        }
    },
    /**
     * Author: Bùi Huy Tuyền (04/07/2023)
     * Khởi tạo khi component được mounted
     */
    mounted() {
        this.inputValue = this.modelValue
        if (this.modelValue === '') this.indexFocusActive = 0
        else this.indexFocusActive = this.listItemView.findIndex((item) => item === this.modelValue)

        this.$refs.list.scroll({
            top: 36 * this.indexFocusActive,
            behavior: 'smooth'
        })
        console.log('object')

        this.listItemView = this.listSelect
    },
    /**
     * Author: Bùi Huy Tuyền (04/07/2023)
     * Khai báo các method
     */
    methods: {
        /**
         * Author: Bùi Huy Tuyền (11/07/2023)
         * @param
         *  Hiển thị list select khi click vào icon
         */
        toggleIcon() {
            this.isShowListSelect = !this.isShowListSelect
        },
        /**
         * Author: Bùi Huy Tuyền (04/07/2023)
         * @param {*} event
         * Ẩn list select khi click ra ngoài
         */
        hiddenListSelect() {
            this.isShowListSelect = false
        },
        /**
         * Author: Bùi Huy Tuyền (04/07/2023)
         * @param {*} event
         * Chọn item trong list select bằng phím mũi tên và enter
         */
        selectWithKeyArrow(event) {
            if (this.isShowListSelect) {
                switch (event.key) {
                    case 'ArrowDown':
                        this.indexFocusActive =
                            this.indexFocusActive === this.listItemView.length - 1
                                ? 0
                                : this.indexFocusActive + 1

                        this.$refs.list.scroll({
                            top: 36 * this.indexFocusActive,
                            behavior: 'smooth'
                        })
                        break

                    case 'ArrowUp':
                        this.indexFocusActive =
                            this.indexFocusActive === 0
                                ? this.listItemView.length - 1
                                : this.indexFocusActive - 1

                        this.$refs.list.scroll({
                            top: 36 * this.indexFocusActive,
                            behavior: 'smooth'
                        })

                        break

                    case 'Enter':
                        this.inputValue =
                            this.$refs.list.children[0].children[this.indexFocusActive]?.innerText
                        this.indexChoose = this.indexFocusActive
                        this.listItemView = this.listSelect
                        this.isShowListSelect = false
                        break
                    case 'Tab':
                    case 'Escape':
                        this.isShowListSelect = false
                        break
                    default:
                        break
                }
            } else {
                if (event.key !== 'Tab' && event.key !== 'Escape') this.isShowListSelect = true
            }
        },
        /**
         * Author: Bùi Huy Tuyền (04/07/2023)
         * @param {*} item
         * Chọn item trong list select
         */
        selectItem(item) {
            this.inputValue = item
            this.indexChoose = this.indexFocusActive
            this.isShowListSelect = false
        },
        /**
         * Author: Bùi Huy Tuyền (04/07/2023)
         * Focus vào input
         */
        focus() {
            this.$refs.input.focus()
            this.$emit('focus')
        },
        /**
         * Author: Bùi Huy Tuyền (04/07/2023)
         * @param {*} value
         * Input value change
         */
        inputValueChange(value) {
            this.$emit('update:modelValue', value)
            this.indexFocusActive = 0
            this.listItemView = this.listSelect.filter((item) =>
                item.toLowerCase().includes(value.toLowerCase())
            )
        },
        /**
         * Author: Bùi Huy Tuyền (04/07/2023)
         * Blur input
         */
        blurInput() {
            if (this.required) {
                if (this.inputValue === '') this.invalid = true
                else this.invalid = false
            }
        }
    },
    computed: {
        /**
         * Author: Bùi Huy Tuyền (04/07/2023)
         * Style cho combobox
         */
        styleCombobox() {
            return {
                width: this.width
            }
        },
        /**
         * Author: Bùi Huy Tuyền (04/07/2023)
         * Kiểm tra loading dữ liệu
         */
        isLoading() {
            return useIsLoading().isLoading
        }
    },
    watch: {
        /**
         * Author: Bùi Huy Tuyền (11/07/2023)
         * @param {*} value
         * Two way binding
         */
        modelValue(value) {
            this.inputValue = value
        },
        /**
         * Author: Bùi Huy Tuyền (11/07/2023)
         * @param {*} value
         * Two way binding
         */
        inputValue(value) {
            if (this.invalid && value !== '') this.invalid = false
            if (value === '') this.isShowListSelect = false
            this.$emit('update:modelValue', value)
        },
        /**
         * Author: Bùi Huy Tuyền (11/07/2023)
         * @param {*} value
         * Theo dõi sự thay đổi của list select
         */
        indexFocusActive() {
            // this.inputValue = this.$refs.list.children[0].children[value].innerText
            // this.$refs.list.children[0].children[value].scrollIntoView({
            //     behavior: 'smooth',
            //     block: 'center'
            // })
            // this.$refs.list.scroll({
            //     top: 36 * this.indexFocusActive,
            //     behavior: 'smooth'
            // })
        },
        /**
         * Author: Bùi Huy Tuyền (11/07/2023)
         * @param {*} value
         * Theo dõi sự thay đổi của list select
         */
        listSelect(value) {
            this.listItemView = value
        }
    }
}
</script>
