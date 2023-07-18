<template>
    <label class="flex-column t-combobox row-gap-10" :class="classLabel" :style="styleCombobox">
        <p v-if="!!label">{{ label }} <span class="require" v-if="required">*</span></p>
        <section class="relative center-y" @mouseleave="hiddenListSelect">
            <section class="wrapper-icon absolute l-7" v-if="!!$slots.iconLeft">
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
                    }
                ]"
                :placeholder="placeholder"
                :required="required"
                v-model="inputValue"
                @keydown="(event) => selectWithKeyArrow(event)"
                ref="input"
            />
            <section class="wrapper-icon absolute r-6" @click="toggleIcon">
                <section class="icon down"></section>
            </section>

            <ul
                class="combobox__select--list absolute br-4 w-100"
                v-show="isShowListSelect"
                ref="list"
            >
                <section v-if="renderListSelect.length > 0">
                    <li
                        class="combobox__select--item"
                        v-for="(item, index) in renderListSelect"
                        :key="item"
                        :class="{ active: indexActive === index }"
                        @mouseenter="this.indexActive = index"
                        @click="selectItem(item)"
                    >
                        {{ item }}
                    </li>
                </section>
                <li class="combobox__select--item" v-else>{{ this.$_MISAResource.VN.noResult }}</li>
            </ul>
        </section>
    </label>
</template>

<script>
export default {
    name: 'MISACombobox',
    /**
     * Author: Bùi Huy Tuyền (04/07/2023)
     * ĐỊnh nghĩa props cho component
     */
    props: {
        label: {
            type: String,
            default: ''
        },
        required: {
            type: Boolean,
            default: false
        },
        placeholder: {
            type: String,
            default: ''
        },
        placeholderItalics: {
            type: Boolean,
            default: false
        },
        listSelect: {
            type: Array,
            default: () => []
        },
        classLabel: {
            type: String,
            default: ''
        },
        width: {
            type: String,
            default: '100%'
        },
        classInput: {
            type: String,
            default: ''
        },
        // Two way binding
        modelValue: {
            type: String,
            default: ''
        }
    },
    data() {
        return {
            isShowListSelect: false,
            listItemView: this.listSelect,
            indexActive: 0,
            inputValue: ''
        }
    },
    created() {
        this.inputValue = this.modelValue
    },
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
                        this.indexActive =
                            this.indexActive === this.renderListSelect.length - 1
                                ? 0
                                : this.indexActive + 1
                        this.$refs.list.scroll({
                            top: 36 * this.indexActive,
                            behavior: 'smooth'
                        })
                        break

                    case 'ArrowUp':
                        this.indexActive =
                            this.indexActive === 0
                                ? this.renderListSelect.length - 1
                                : this.indexActive - 1
                        this.$refs.list.scroll({
                            top: 36 * this.indexActive,
                            behavior: 'smooth'
                        })
                        break

                    case 'Enter':
                        this.inputValue =
                            this.$refs.list.children[0].children[this.indexActive].innerText

                        this.isShowListSelect = false
                        this.indexActive = 0

                        break

                    case 'Escape':
                        this.isShowListSelect = false
                        break

                    default:
                        break
                }
            } else {
                if (event.key !== 'Tab') this.isShowListSelect = true
            }
        },
        /**
         * Author: Bùi Huy Tuyền (04/07/2023)
         * @param {*} item
         * Chọn item trong list select
         */
        selectItem(item) {
            this.inputValue = item
            this.isShowListSelect = false
        }
    },
    computed: {
        /**
         * Author: Bùi Huy Tuyền (04/07/2023)
         * Render lại list select khi có sự thay đổi
         */
        renderListSelect() {
            if (this.inputValue === '') return this.listItemView
            return this.listItemView.filter((item) => {
                return item.toLowerCase().startsWith(this.inputValue.toLowerCase())
            })
        },
        /**
         * Author: Bùi Huy Tuyền (04/07/2023)
         * Style cho combobox
         */
        styleCombobox() {
            return {
                width: this.width
            }
        }
    },
    watch: {
        /**
         * Author: Bùi Huy Tuyền (11/07/2023)
         * @param {*} value
         * Tưo way binding
         */
        inputValue(value) {
            this.$emit('update:modelValue', value)
        },
        /**
         * Author: Bùi Huy Tuyền (11/07/2023)
         * @param {*} value
         * Tưo way binding
         */
        modelValue(value) {
            this.inputValue = value
        }
    }
}
</script>
