<template>
    <section
        class="table__row h-40 grid center-y"
        :class="{ 'row-active': checked }"
        @dblclick=";(isShowForm = true), (action = 'update')"
        @click.ctrl="checked = !checked"
    >
        <!-- checkbox -->
        <section class="row__checkbox center">
            <m-checkbox :checked="checked" @change="changeChecked" />
        </section>
        <!-- Số thứ tự -->
        <section class="row__stt text-right pr-28">{{ index }}</section>
        <!-- Mã tài sản -->
        <section class="row__code pr-20" :title="fixedAsset.FixedAssetCode">
            {{ fixedAsset.FixedAssetCode }}
        </section>
        <!-- Tên tài sản -->
        <section class="row__name pr-20" :title="fixedAsset.FixedAssetName">
            {{ fixedAsset.FixedAssetName }}
        </section>
        <!-- Tên loại tài sản -->
        <section class="row__category pr-20">{{ fixedAsset.FixedAssetCategoryName }}</section>
        <!-- Tên phòng ban -->
        <section class="row__department pr-20">{{ fixedAsset.DepartmentName }}</section>
        <!-- Số lượng -->
        <section class="row__quantity text-right pl-20">
            {{ formatNumber(fixedAsset.Quantity) }}
        </section>
        <!-- Nguyên giá -->
        <section class="row__cost text-right pl-20" :title="fixedAsset.Cost">
            {{ formatNumber(fixedAsset.Cost) }}
        </section>
        <!-- Hao mòn lũy kế -->
        <section class="row__cumulative text-right pl-20">
            {{ formatNumber(FixedAssetCumulative) }}
        </section>
        <!-- Giá trị còn lại -->
        <section class="row__remaining text-right pl-20">
            {{ formatNumber(FixedAssetRemaining) }}
        </section>
        <!-- Chức năng -->
        <section class="row__option center-y">
            <!-- Update -->
            <section class="wrapper-icon" @click=";(isShowForm = true), (action = 'update')">
                <section class="icon edit"></section>
            </section>
            <!-- Nhân bản -->
            <section class="wrapper-icon" @click=";(isShowForm = true), (action = 'replicate')">
                <section class="icon copy"></section>
            </section>
        </section>
    </section>

    <!-- Form cập nhật và nhân bản -->
    <section class="blur" v-if="isShowForm">
        <section class="assets__edit flex-column px-16 br-4 bg-white pt-16">
            <m-popup
                title="Sửa tài sản"
                @closePopup="isShowForm = false"
                @updateListFixedAsset="$emit('updateListFixedAsset', $event)"
                :fixedAsset="fixedAsset"
                :action="action"
                :listDepartment="listDepartment"
                :listFixedAssetCategory="listFixedAssetCategory"
            />
        </section>
    </section>
</template>

<script>
export default {
    name: 'FixedAssetRow',
    /**
     * Author: Bùi Huy Tuyền (11/07/2023)
     * Định nghĩa props cho component
     */
    props: {
        // Dữ liệu tài sản
        fixedAsset: {
            type: Object,
            default: () => ({})
        },
        // Chọn tất cả
        checkAll: {
            type: Boolean,
            default: false
        },
        // Danh sách tài sản được chọn
        listFixedAssetChoose: {
            type: Array,
            default: () => []
        },
        // Số thứ tự của dòng
        index: {
            type: Number,
            default: 0
        },
        // Danh sách phòng ban
        listDepartment: {
            type: Array,
            default: () => []
        },
        // Danh sách loại tài sản
        listFixedAssetCategory: {
            type: Array,
            default: () => []
        }
    },
    /**
     * Author: Bùi Huy Tuyền (11/07/2023)
     * Định nghĩa data cho component
     */
    data() {
        return {
            // Hiển thị form
            isShowForm: false,
            // Chọn checkbox
            checked: false,
            // Hành động
            action: ''
        }
    },
    /**
     * Author: Bùi Huy Tuyền (11/07/2023)
     * Khai báo các emit
     */
    emits: ['changeChecked', 'updateListFixedAsset'],
    methods: {
        /**
         * check Checkbox
         * @param
         * Author: Bùi Huy Tuyền (11/07/2023)
         */
        changeChecked(value) {
            this.checked = value
            this.$emit('changeChecked', this.fixedAsset.FixedAssetId, value)
        },
        /**
         * Format số
         * @param {*} number
         * Author: Bùi Huy Tuyền (11/07/2023)
         */
        formatNumber(number) {
            return number.toString().replace(/\B(?=(\d{3})+(?!\d))/g, '.')
        }
    },
    watch: {
        /**
         * Author: Bùi Huy Tuyền (11/07/2023)
         * Kiểm tra xem tài sản có nằm trong danh sách được chọn hay không
         */
        listFixedAssetChoose() {
            this.checked = this.listFixedAssetChoose.includes(this.fixedAsset.FixedAssetId)
        }
    },
    computed: {
        /**
         * Author: Bùi Huy Tuyền (11/07/2023)
         * Tính giá trị hao mòn lũy kế
         */
        FixedAssetCumulative() {
            return ((this.fixedAsset.Cost * this.fixedAsset.DepreciationRate) / 100).toFixed(0)
        },
        /**
         * Author: Bùi Huy Tuyền (11/07/2023)
         * Tính giá trị hao mòn còn lại
         */
        FixedAssetRemaining() {
            return this.fixedAsset.Cost - this.FixedAssetCumulative
        }
    },
    // Khởi tạo
    mounted() {
        // Kiểm tra xem có trong danh sách tài sản được chọn không
        if (this.listFixedAssetChoose.includes(this.fixedAsset.FixedAssetId)) {
            this.checked = true
        }
    }
}
</script>
