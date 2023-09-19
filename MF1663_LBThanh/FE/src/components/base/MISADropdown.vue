<template>
    <section
        class="dropdown center-y br-4 relative"
        @click="isShowListSelect = !isShowListSelect"
        v-click-outside="() => (isShowListSelect = false)"
        title="Số bản ghi trên một trang"
    >
        <input :value="value" class="dropdown__input" readonly ref="input" />
        <section class="wrapper-icon icon__dropdown">
            <section class="icon footer__left__icon-down"></section>
        </section>
        <section class="dropdown__list absolute br-4" v-if="isShowListSelect">
            <section
                class="dropdown__item center"
                :class="{ active: index === indexActive }"
                v-for="(item, index) in listSelect"
                :key="index"
                @mouseenter="indexActive = index"
                @click.stop="selectValue(item)"
            >
                {{ item }}
            </section>
        </section>
    </section>
</template>

<script>
export default {
    name: 'MISADropdown',
    /**
     * @author LB.Thành (27-07-2023)
     * Khai báo các Props
     */
    props: {
        /**
         * Author: LB.Thành (08/08/2023)
         * Danh sách lựa chọn
         *
         */
        listSelect: {
            type: Array,
            default: () => [2, 5, 10, 15, 20, 30, 50, 100]
        },
        /**
         * Giá trị ban đầu
         * Author: LB.Thành (08/08/2023)
         */
        modelValue: {
            type: Number,
            default: 0
        }
    },
    /**
     * Khai báo các data
     */
    data: function () {
        return { value: '', isShowListSelect: false, indexActive: 0 }
    },
    methods: {
        /**
         * Author: LB.Thành (08/08/2023)
         * @param {*} item
         * Chọn giá trị
         */
        selectValue(item) {
            this.value = item
            this.isShowListSelect = false
            this.$emit('selectValue', item)
        }
    },
    watch: {
        /**
         * Author: LB.Thành (08/08/2023)
         * @param {*} value
         * Theo dõi sự thay đổi của props modelValue
         */
        modelValue(value) {
            this.value = value
        },
        /**
         * Author: LB.Thành (08/08/2023)
         * @param {*} value
         * Theo dõi sự thay đổi của data value
         */
        value(value) {
            this.$emit('update:modelValue', value)
        }
    },
    /**
     * Author: LB.Thành (08/08/2023)
     * Khởi tạo giá trị ban đầu cho data value
     */
    mounted() {
        this.value = this.modelValue
    }
}
</script>
