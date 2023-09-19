<template>
    <div class="pick-popup z-999">
        <form class="t-popup" ref="form" @keyup.esc="showToast">
            <header class="t-popup__header center-y justify-between w-100 h-36">
                <h1 class="t-popup__header--title">Chọn tài sản điều chuyển</h1>
                <section class="wrapper-icon" @click="$emit('hidden')" title="Thoát">
                    <section class="icon t-popup__header--close"></section>
                </section>
            </header>
            <main class="t-popup__header--body edit__body pick-popup-body">
                <section class="table-data">
                    <!-- ------------------------Table start------------------------ -->
                    <div class="table">
                        <!-- ------------------------Header------------------------ -->
                        <div class="header--row row pr-4">
                            <div
                                class="header cell display--center-center border--right border--bottom"
                            >
                                <input type="checkbox" />
                            </div>
                            <div
                                class="header cell display--center-center font-weight--700 border--right border--bottom"
                            >
                                STT
                            </div>
                            <div
                                class="header cell display--center-left font-weight--700 border--right border--bottom pl-10"
                            >
                                Mã tài sản
                            </div>
                            <div
                                class="header cell display--center-left font-weight--700 border--right border--bottom pl-10"
                            >
                                Tên tài sản
                            </div>
                            <div
                                class="header cell display--center-left font-weight--700 border--right border--bottom pl-10"
                            >
                                Bộ phận sử dụng
                            </div>
                            <div
                                class="header cell display--center-right font-weight--700 border--right border--bottom pr-10"
                            >
                                Nguyên giá
                            </div>
                            <div
                                class="header cell display--center-right font-weight--700 border--right border--bottom pr-10"
                            >
                                Giá trị còn lại
                            </div>
                            <div
                                class="header cell display--center-right font-weight--700 border--right border--bottom pr-10"
                            >
                                Năm theo dõi
                            </div>
                        </div>

                        <!-- ------------------------Body------------------------ -->
                        <div class="body-data">
                            <div
                                class="body--row row tr--body"
                                v-for="(asset, index) in assets"
                                :key="asset.FixedAssetId"
                                :class="{ 'checked--row': selectedRows.includes(asset) }"
                                @click.exact.stop="callRowOnClick(asset)"
                                @click.ctrl.stop="callRowOnCtrlClick(asset)"
                            >
                                <div
                                    class="cell display--center-center border--top border--right border--bottom"
                                    @click.stop="callRowOnClickByCheckBox(asset)"
                                >
                                    <input
                                        type="checkbox"
                                        :checked="selectedRowsByCheckBox.includes(asset)"
                                    />
                                </div>
                                <div
                                    class="cell display--center-center border--top border--right border--bottom"
                                >
                                    {{ index + 1 }}
                                </div>
                                <div
                                    class="cell display--center-left border--top border--right border--bottom pl-10"
                                >
                                    {{ asset.FixedAssetCode }}
                                </div>
                                <div
                                    class="cell display--center-left border--top border--right border--bottom pl-10"
                                >
                                    {{ asset.FixedAssetName }}
                                </div>
                                <div
                                    class="cell display--center-left border--top border--right border--bottom pl-10"
                                >
                                    {{ asset.DepartmentName }}
                                </div>
                                <div
                                    class="cell display--center-right border--top border--right border--bottom pr-10"
                                >
                                    {{ formatNumber(asset.Cost) }}
                                </div>
                                <div
                                    class="cell display--center-right border--top border--right border--bottom pr-10"
                                >
                                    {{ formatNumber(FixedAssetRemaining(asset)) }}
                                </div>
                                <div
                                    class="cell display--center-right border--top border--right border--bottom pr-10"
                                >
                                    {{ asset.TrackedYear }}
                                </div>
                            </div>
                        </div>
                        <div class="body-data__footer pr-4">
                            <div class="row">
                                <div class="cell display--center-center border--bottom"></div>
                                <div class="cell display--center-center border--bottom"></div>
                                <div class="cell display--center-left border--bottom pl-10"></div>
                                <div
                                    class="cell display--center-center border--right border--bottom"
                                ></div>
                                <div
                                    class="cell display--center-right font-weight--700 border--right border--bottom pr-10"
                                ></div>
                                <div
                                    class="cell display--center-right font-weight--700 border--right border--bottom pr-10"
                                >
                                    {{ formatNumber(costTotal()) }}
                                </div>
                                <div
                                    class="cell display--center-right font-weight--700 border--right border--bottom pr-10"
                                >
                                    {{ formatNumber(remainingTotal()) }}
                                </div>
                            </div>
                        </div>
                        <!-- Footer -->
                        <footer class="table__footer h-40 grid bg-white">
                            <!-- Footer left -->
                            <section class="table__footer_left center-y">
                                <!-- Tổng số tài sản -->
                                <section class="footer__left__info">
                                    Tổng số: <span>{{ this.totalRecords }}</span> bản ghi
                                </section>
                                <!--  -->

                                <!-- Chọn số tài sản trong một trang -->
                                <m-dropdown v-model="pageLimit" />
                                <!--  -->

                                <!-- Thay đổi trang -->
                                <m-paging
                                    classPaging="footer__left__number-page"
                                    v-model="pageNumber"
                                    :numberEnd="pageNumberEnd"
                                />
                                <!--  -->
                            </section>
                        </footer>
                        <!--  -->
                    </div>
                    <!-- ------------------------Table end------------------------ -->
                </section>
                <section class="table-details">
                    <div class="mr-20">
                        <m-combobox
                            :label="labelForm.TransferDepartmentName"
                            required
                            labelStyle="fw-500 font-14"
                            :styleCombobox="400"
                            placeholder="Bộ phận sử dụng"
                            width="280px"
                            classInput="input-filter"
                            :listSelect="listDepartmentName()"
                            v-model="TransferDepartmentName"
                        >
                            <template #iconLeft>
                                <section class="icon filter">
                                    <m-tooltip content="Lọc" type="bottom" />
                                </section>
                            </template>
                            <template #iconRight>
                                <section class="icon down"></section>
                            </template>
                        </m-combobox>
                    </div>
                    <div>
                        <m-input
                            labelStyle="fw-500 font-14"
                            classLabel="fixedAssetCode"
                            :label="labelForm.Note"
                            placeholderItalics
                            maxlength="255"
                            v-model="Description"
                        />
                    </div>
                </section>
            </main>

            <footer class="t-popup__footer center-y">
                <m-button
                    typeButton="outline"
                    width="100px"
                    style="border: none"
                    @clickButton="showToast"
                    tabindex="12"
                >
                    Hủy
                </m-button>
                <m-button width="100px" @clickButton="saveAsset" tabindex="11">Đồng ý</m-button>
            </footer>
        </form>
    </div>
</template>

<script>
import { rowOnClick, rowOnClickByCheckBox, rowOnCtrlClick } from '../../helper/table/rowSelect'
import FixedAssetAPI from '../../api/FixedAsset.API'
import DepartmentAPI from '../../api/Department.API'
export default {
    name: 'AssetTransferPick',
    props: {
        existFixedAsset: {
            type: Array,
            default: () => []
        }
    },
    data() {
        return {
            isOpenPopup: true,
            assets: [],
            listDepartment: [],
            totalRecords: 0,
            TransferDepartmentName: '',
            Description: '',

            // Tick ô checkbox input
            isCheckboxChecked: false,
            // Danh sách các bản ghi đã chọn
            selectedRowsByCheckBox: [],
            // Danh sách các bản ghi đã chọn
            selectedRows: [],
            departmentFilter: null,
            pageNumberEnd: 0,
            pageNumber: 1,
            pageLimit: 20
        }
    },
    emits: ['hidden'],
    // async created() {
    //     await this.loadData()
    //     await this.getAllDepartment()
    // },
    async mounted() {
        await this.loadData()
        await this.getAllDepartment()
    },
    watch: {
        /**
         * @author LB.Thành (11/07/2023)
         * @description Xử lý khi thay đổi số bản ghi trong một trang
         */
         pageLimit() {
            this.pageNumber = 1
            this.loadData()
        },
        async pageNumber(value){
            let dataFilter = {
                pageNumber: value,
                pageLimit: this.pageLimit,
                FixedAssetDtos: this.existFixedAsset
            }
            this.loadData()
        }
    },
    computed: {
        /**
         * @description Lấy nội dung label trên form nhập liệu
         * @author LB.Thành (27-07-2023)
         */
        labelForm() {
            return this.$_MISAResource.VN.labelForm
        },
        /**
         * @description Lấy nội dung placeholder trên form nhập liệu
         * @author LB.Thành (27-07-2023)
         */
        placeholderForm() {
            return this.$_MISAResource.VN.placeholderForm
        }
    },
    methods: {
        async loadData() {
            let dataFilter = {
                pageNumber: this.pageNumber,
                pageLimit: this.pageLimit,
                FixedAssetDtos: this.existFixedAsset,
                transferAssetDetailIds: []
            }
            let res = await FixedAssetAPI.getFixedAssetForTransfer(dataFilter)
            this.assets = res.data.Data
            this.totalRecords = res.data.TotalRecords
            this.pageNumberEnd = Math.ceil(res.data.TotalRecords / this.pageLimit) 
        },
        async getAllDepartment() {
            const res = await DepartmentAPI.getAllDepartment()
            this.listDepartment = res.data
        },
        listDepartmentName() {
            return this.listDepartment.map((item) => item.DepartmentName)
        },
        FixedAssetCumulative(asset) {
            // Lấy thời điểm hiện tại
            var currentDate = new Date()

            // Chuyển định dạng StartUsingDate từ chuỗi sang đối tượng Date
            var startUsingDate = new Date(asset.StartUsingDate)

            // Tính số năm đã trôi qua từ thời điểm StartUsingDate đến thời điểm hiện tại
            var yearsPassed = currentDate.getFullYear() - startUsingDate.getFullYear()

            // Tính giá trị khấu hao lũy kế theo công thức
            var cumulativeDepreciation = yearsPassed * ((asset.DepreciationRate / 100) * asset.Cost)

            return cumulativeDepreciation.toFixed(0)
        },

        /**
         * Author: LB.Thành (11/07/2023)
         * Tính giá trị hao mòn còn lại
         */
        FixedAssetRemaining(asset) {
            return asset.Cost - this.FixedAssetCumulative(asset)
        },

        /**
         * @description Tổng giá trị tất cả tài sản hiện có trong trang hiện tại
         * @returns {number} tổng giá trị tài sản trong trang hiện tại
         * @author LB.Thành (08/08/2023)
         */
        costTotal() {
            return this.assets.reduce((total, item) => {
                return total + item.Cost
            }, 0)
        },

        depreciationTotal() {
            // Lấy thời điểm hiện tại
            var currentDate = new Date()

            // Sử dụng phương thức reduce để tính tổng giá trị khấu hao lũy kế
            var totalDepreciation = this.assets.reduce((total, item) => {
                // Chuyển định dạng StartUsingDate từ chuỗi sang đối tượng Date
                var startUsingDate = new Date(item.StartUsingDate)

                // Tính số năm đã trôi qua từ thời điểm StartUsingDate đến thời điểm hiện tại
                var yearsPassed = currentDate.getFullYear() - startUsingDate.getFullYear()

                // Tính giá trị khấu hao lũy kế cho từng tài sản và cộng vào tổng
                return total + yearsPassed * ((item.DepreciationRate / 100) * item.Cost)
            }, 0)

            return totalDepreciation.toFixed(0)
        },

        remainingTotal() {
            return this.costTotal() - this.depreciationTotal()
        },

        formatNumber(number) {
            if (number !== undefined && number !== null) {
                return number
                    .toString()
                    .replace(/\./g, '')
                    .replace(/\B(?=(\d{3})+(?!\d))/g, '.')
            } else {
                // Xử lý khi biến number là undefined hoặc null
                return '0' // Hoặc giá trị mặc định khác tùy bạn chọn
            }
        },
        //------------------------------------------- CLICK ROW -------------------------------------------

        /**
         * Thực hiện call hàm rowOnClick từ file js để bôi xanh 1 dòng nếu click vào dòng đó
         * @param {object} asset thông tin tài sản
         * Author: LB.Thành (02/08/2023)
         */
        callRowOnClick(asset) {
            rowOnClick.call(this, asset)
        },

        callRowOnClickByCheckBox(asset) {
            rowOnClickByCheckBox.call(this, asset)
        },

        callRowOnCtrlClick(asset) {
            rowOnCtrlClick.call(this, asset)
        },

        btnUncheckedAll() {
            this.selectedRows = []
            this.selectedRowsByCheckBox = []
        },

        saveAsset() {
            let departmentName = this.TransferDepartmentName
            let description = this.Description
            const assetsWithNewDepartment = this.selectedRowsByCheckBox.map((asset) => ({
                ...asset,
                NewDepartmentId: this.listDepartment.find(
                    (item) => item.DepartmentName === departmentName
                )?.DepartmentId,
                OldDepartmentName: asset.DepartmentName,
                OldDepartmentId: asset.DepartmentId,
                RemainderCost: this.FixedAssetRemaining(asset),
                TransferDepartmentName: departmentName,
                Reason: description
            }))
            this.$emit('loadDataToDetails', assetsWithNewDepartment)
            this.$emit('hidden')
        }
    }
}
</script>

<style scoped>
@import '../../css/views/AssetTransferPick.css';
</style>
