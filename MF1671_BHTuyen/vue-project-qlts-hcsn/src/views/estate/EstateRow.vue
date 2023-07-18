<template>
    <section class="table__row h-40 grid center-y" :class="{ 'row-active': checked }">
        <section class="table__checkbox pl-16">
            <input type="checkbox" class="t-checkbox" :checked="checked" @input="changeChecked" />
        </section>
        <section class="table__stt pl-10">{{ index + 1 }}</section>
        <section class="table__code">{{ itemEstate.codeEstate }}</section>
        <section class="table__name">{{ itemEstate.nameEstate }}</section>
        <section class="table__kind">{{ itemEstate.nameKindEstate }}</section>
        <section class="table__part">{{ itemEstate.namePart }}</section>
        <section class="table__count pl-20">{{ itemEstate.countEstate }}</section>
        <section class="table__price">{{ itemEstate.originalPrice }}</section>
        <section class="table__cumulative">{{ itemEstate.cumulative }}</section>
        <section class="table__remaining">{{ itemEstate.remaining }}</section>
        <section class="table__option center-y">
            <section class="wrapper-icon" @click="showEdit">
                <section class="icon edit"></section>
            </section>
            <section class="wrapper-icon">
                <section class="icon copy"></section>
            </section>
        </section>
    </section>

    <!-- Form Edit Estate -->
    <section class="blur" v-if="isShowEdit">
        <section class="assets__edit flex-column px-16 br-4 bg-white pt-16">
            <m-popup
                title="Sửa tài sản"
                @closePopup="closeEdit"
                :itemEstate="itemEstate"
                @editEstate="editEstate"
                @showToastSuccess="showToastSuccess"
            />
        </section>
    </section>
    <!--  -->
</template>

<script>
export default {
    name: 'EstateRow',
    /**
     * Author: Bùi Huy Tuyền (11/07/2023)
     * Khai báo các component sử dụng
     */
    components: {},
    /**
     * Author: Bùi Huy Tuyền (11/07/2023)
     * Định nghĩa props cho component
     */
    props: {
        itemEstate: Object,
        checkAll: Boolean,
        listEstateChoose: Array,
        index: Number
    },
    /**
     * Author: Bùi Huy Tuyền (11/07/2023)
     * Định nghĩa data cho component
     */
    data() {
        return {
            isShowEdit: false,
            checked: false
        }
    },
    /**
     * Author: Bùi Huy Tuyền (11/07/2023)
     * Khai báo các emit
     */
    emits: ['changeChecked', 'editEstate', 'showToastSuccess'],
    /**
     * Author: Bùi Huy Tuyền (11/07/2023)
     * Khai báo các method sử dụng
     */
    methods: {
        /**
         * Hiển thị form edit
         * @param
         * Author: Bùi Huy Tuyền (01/07/2023)
         */
        showEdit() {
            this.isShowEdit = true
        },
        /**
         * Ẩn thị form edit
         * @param
         * Author: Bùi Huy Tuyền (01/07/2023)
         */
        closeEdit() {
            this.isShowEdit = false
        },
        /**
         * check Checkbox
         * @param
         * Author: Bùi Huy Tuyền (11/07/2023)
         */
        changeChecked() {
            this.checked = !this.checked
            this.$emit(
                'changeChecked',
                this.itemEstate.id,
                this.checked,
                this.itemEstate.isDocument
            )
        },
        /**
         * Author: Bùi Huy Tuyền (11/07/2023)
         * @param {*} estate
         * Sửa tài sản
         */
        editEstate(estate) {
            this.$emit('editEstate', estate)
        },
        /**
         * Author: Bùi Huy Tuyền (11/07/2023)
         * @param {*} estate
         * Hiển thị thông báo thành công
         */
        showToastSuccess(value, message) {
            this.$emit('showToastSuccess', value, message)
        }
    },
    /**
     * Author: Bùi Huy Tuyền (11/07/2023)
     * Xử lý khi có sự thay đổi dữ liệu
     */
    watch: {
        /**
         * Author: Bùi Huy Tuyền (11/07/2023)
         * @param {*} estate
         * Cập nhật trạng thái checkbox khi chọn tất cả
         */
        listEstateChoose() {
            this.checked = this.listEstateChoose.includes(this.itemEstate.id)
        }
    }
}
</script>
