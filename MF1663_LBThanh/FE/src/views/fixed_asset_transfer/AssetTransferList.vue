<template>
    <div class="content">
        <div class="content--top" ref="contentTop">
            <div class="top-left">
                <section class="center-y" v-if="selectedRows.length == 0">
                    <span class="font-weight--700">Điều Chuyển</span>
                    <section class="icon reload" @click="this.loadAllTransferAsset"></section>
                </section>
                <section class="header__right-delete" v-if="selectedRowsByCheckBox.length >= 1">
                    <span
                        >Đã chọn:
                        <span class="font-weight--700">{{
                            selectedRowsByCheckBox.length
                        }}</span></span
                    >
                    <span class="uncheck-option" @click="btnUncheckedAll()">Bỏ chọn</span>
                    <button class="delete-option">Xóa</button>
                </section>
            </div>
            <section class="layout__header__right center-y col-gap-11">
                <!-- Button thêm chứng từ -->
                <m-button @click=";(isShowPopup = true), (this.action = 'create')">
                    <template #icon>
                        <section class="icon addBox"></section>
                    </template>
                    Thêm chứng từ
                </m-button>
                <!--  -->

                <!-- Button nhắn tin -->
                <m-button typeButton="icon">
                    <section class="icon contact"></section>
                    <m-tooltip content="Nhắn tin" />
                </m-button>
                <!--     -->

                <!-- Button hỏi đáp -->
                <m-button typeButton="icon">
                    <section class="icon help"></section>
                    <m-tooltip content="Hỏi đáp" />
                </m-button>
                <!--  -->
            </section>
        </div>
        <div class="content--body border--left border--right border--bottom" ref="contentBody">
            <div class="body-top" ref="bodyTop">
                <!-- ------------------------Table start------------------------ -->
                <div class="table">
                    <!-- ------------------------Header------------------------ -->
                    <div class="header--row row pr-4">
                        <div
                            class="header cell display--center-center border--top border--right border--bottom"
                        >
                            <input type="checkbox" />
                        </div>
                        <div
                            class="header cell display--center-center font-weight--700 border--top border--right border--bottom"
                        >
                            STT
                        </div>
                        <div
                            class="header cell display--center-left font-weight--700 border--top border--right border--bottom pl-10"
                        >
                            Mã chứng từ
                        </div>
                        <div
                            class="header cell display--center-center font-weight--700 border--top border--right border--bottom"
                        >
                            Ngày điều chuyển
                        </div>
                        <div
                            class="header cell display--center-center font-weight--700 border--top border--right border--bottom"
                        >
                            Ngày chứng từ
                        </div>
                        <div
                            class="header cell display--center-right font-weight--700 border--top border--right border--bottom pr-10"
                        >
                            Nguyên giá
                        </div>
                        <div
                            class="header cell display--center-right font-weight--700 border--top border--right border--bottom pr-10"
                        >
                            Giá trị còn lại
                        </div>
                        <div
                            class="header cell display--center-left font-weight--700 border--top border--right border--bottom pl-10"
                        >
                            Ghi chú
                        </div>
                        <div
                            class="header cell display--center-center font-weight--700 border--top border--bottom"
                        >
                            Chức năng
                        </div>
                    </div>

                    <!-- ------------------------Body------------------------ -->
                    <div class="body-data">
                        <section v-if="!isLoadingDataTable">
                            <div
                                class="body--row row tr--body"
                                v-for="(transferAsset, index) in transferAssets"
                                :key="transferAsset.TransferAssetId"
                                :class="{ 'checked--row': selectedRows.includes(transferAsset) }"
                                @dblclick="loadDataforEditForm(transferAsset)"
                                @click.exact.stop="
                                    loadTransferAssetDetail(transferAsset.TransferAssetId)
                                "
                                @click.ctrl.stop="callRowOnCtrlClick(transferAsset)"
                            >
                                <div
                                    class="cell display--center-center border--right border--bottom"
                                    @click.stop="callRowOnClickByCheckBox(transferAsset)"
                                >
                                    <input
                                        type="checkbox"
                                        @click.stop
                                        :checked="selectedRowsByCheckBox.includes(transferAsset)"
                                    />
                                </div>
                                <div
                                    class="cell display--center-center border--right border--bottom"
                                >
                                    {{ index + 1 }}
                                </div>
                                <div
                                    class="cell display--center-left border--right border--bottom pl-10"
                                >
                                    {{ transferAsset.TransferAssetCode }}
                                </div>
                                <div
                                    class="cell display--center-center border--right border--bottom"
                                >
                                    {{ formatDate(transferAsset.TransferDate) }}
                                </div>
                                <div
                                    class="cell display--center-center border--right border--bottom"
                                >
                                    {{ formatDate(transferAsset.TransactionDate) }}
                                </div>
                                <div
                                    class="cell display--center-right border--right border--bottom pr-10"
                                >
                                    {{ formatNumber(transferAsset.Cost) }}
                                </div>
                                <div
                                    class="cell display--center-right border--right border--bottom pr-10"
                                >
                                    {{ formatNumber(transferAsset.RemainCost) }}
                                </div>
                                <div
                                    class="cell display--center-left border--right border--bottom pl-10"
                                >
                                    {{ transferAsset.Note }}
                                </div>
                                <div
                                    class="cell display--center-center border--right border--bottom"
                                >
                                    <div class="center col-gap-16">
                                        <section
                                            class="icon edit"
                                            @click="loadDataforEditForm(transferAsset)"
                                        ></section>
                                        <section
                                            class="icon delete"
                                            @click="deleteSingleTransferAsset(transferAsset)"
                                        ></section>
                                    </div>
                                </div>
                            </div>
                        </section>
                        <section v-else><LoadingSkeleton v-for="i in 5" :key="i" /></section>
                    </div>
                    <div class="body-data__footer pr-4">
                        <div class="body--row row">
                            <div
                                class="cell display--center-center border--right border--bottom"
                            ></div>
                            <div
                                class="cell display--center-center border--right border--bottom"
                            ></div>
                            <div
                                class="cell display--center-left border--right border--bottom pl-10"
                            ></div>
                            <div
                                class="cell display--center-center border--right border--bottom"
                            ></div>
                            <div
                                class="cell display--center-center border--right border--bottom"
                            ></div>
                            <div
                                class="cell font-weight--700 display--center-right border--right border--bottom pr-10"
                            >
                                {{ formatNumber(costTotal()) }}
                            </div>
                            <div
                                class="cell font-weight--700 display--center-right border--right border--bottom pr-10"
                            >
                                {{ formatNumber(remainTotal()) }}
                            </div>
                            <div
                                class="cell display--center-left border--right border--bottom pl-10"
                            ></div>
                            <div
                                class="cell display--center-center border--right border--bottom"
                            ></div>
                        </div>
                    </div>
                </div>
                <!-- ------------------------Table end------------------------ -->

                <!-- Footer -->
                <footer class="table__footer h-40 grid bg-white">
                    <!-- Footer left -->
                    <section class="table__footer_left center-y">
                        <!-- Tổng số tài sản -->
                        <section class="footer__left__info">
                            Tổng số: <span>{{ TotalRecords }}</span> bản ghi
                        </section>
                        <!--  -->

                        <!-- Chọn số tài sản trong một trang -->
                        <m-dropdown v-model="pageLimitOfTrans" />
                        <!--  -->

                        <!-- Thay đổi trang -->
                        <!-- v-if="pageNumberEnd > 0" -->
                        <m-paging
                            classPaging="footer__left__number-page"
                            v-model="pageNumberOfTrans"
                            :numberEnd="pageNumberEndOfTrans"
                        />
                        <!--  -->
                    </section>
                </footer>
                <!--  -->
            </div>

            <!-- ------------------------Resize bar------------------------ -->
            <div class="resize-bar border--top border--bottom" @mousedown="startResize"></div>

            <!-- ------------------------Table start------------------------ -->
            <div class="resizable-table z-999" ref="resizableTable">
                <div class="content--top" ref="tableTop">
                    <div class="top-left">
                        <!-- Button thông tin chi tiết -->
                        <m-button> Thông tin chi tiết </m-button>
                        <!--  -->
                    </div>
                    <div class="top-right">
                        <MISAIcon drop_down></MISAIcon>
                    </div>
                </div>
                <div class="table table-bot">
                    <!-- ------------------------Header------------------------ -->
                    <div class="header--row resizable-table__row">
                        <div
                            class="header cell display--center-center font-weight--700 border--top border--right border--bottom"
                        >
                            STT
                        </div>
                        <div
                            class="header cell display--center-left font-weight--700 border--top border--right border--bottom pl-10"
                        >
                            Mã tài sản
                        </div>
                        <div
                            class="header cell display--center-left font-weight--700 border--top border--right border--bottom pl-10"
                        >
                            Tên tài sản
                        </div>
                        <div
                            class="header cell display--center-right font-weight--700 border--top border--right border--bottom pr-10"
                        >
                            Nguyên giá
                        </div>
                        <div
                            class="header cell display--center-right font-weight--700 border--top border--right border--bottom pr-10"
                        >
                            Giá trị còn lại
                        </div>
                        <div
                            class="header cell display--center-left font-weight--700 border--top border--right border--bottom pl-10"
                        >
                            Bộ phận sử dụng
                        </div>
                        <div
                            class="header cell display--center-left font-weight--700 border--top border--right border--bottom pl-10"
                        >
                            Bộ phận điều chuyển đến
                        </div>
                        <div
                            class="header cell display--center-left font-weight--700 border--top border--bottom pl-10"
                        >
                            Lý do
                        </div>
                    </div>

                    <!-- ------------------------Body------------------------ -->
                    <div class="body-data">
                        <div
                            class="body--row resizable-table__row"
                            v-for="(transferAssetDetail, index) in details"
                            :key="transferAssetDetail.TransferAssetDetailId"
                        >
                            <div class="cell display--center-center border--right border--bottom">
                                {{ index + 1 }}
                            </div>
                            <div
                                class="cell display--center-left border--right border--bottom pl-10"
                            >
                                {{ transferAssetDetail.FixedAssetCode }}
                            </div>
                            <div
                                class="cell display--center-left border--right border--bottom pl-10"
                            >
                                {{ transferAssetDetail.FixedAssetName }}
                            </div>
                            <div
                                class="cell display--center-right border--right border--bottom pr-10"
                            >
                                {{ formatNumber(transferAssetDetail.Cost) }}
                            </div>
                            <div
                                class="cell display--center-right border--right border--bottom pr-10"
                            >
                                {{ formatNumber(transferAssetDetail.RemainderCost) }}
                            </div>
                            <div
                                class="cell display--center-left border--right border--bottom pl-10"
                            >
                                {{ transferAssetDetail.OldDepartmentName }}
                            </div>
                            <div
                                class="cell display--center-left border--right border--bottom pl-10"
                            >
                                {{ transferAssetDetail.TransferDepartmentName }}
                            </div>
                            <div
                                class="cell display--center-left border--right border--bottom pl-10"
                            >
                                {{ transferAssetDetail.Reason }}
                            </div>
                        </div>
                    </div>
                </div>
                <!-- ------------------------Table end------------------------ -->

                <!-- Footer -->
                <footer class="table__footer h-40 grid bg-white">
                    <!-- Footer left -->
                    <section class="table__footer_left center-y">
                        <!-- Tổng số tài sản -->
                        <section class="footer__left__info">
                            Tổng số: <span>{{ details.length }}</span> bản ghi
                        </section>
                        <!--  -->

                        <!-- Chọn số tài sản trong một trang -->
                        <m-dropdown v-model="pageLimit" />
                        <!--  -->

                        <!-- Thay đổi trang -->
                        <m-paging
                            v-if="pageNumberEnd > 0"
                            classPaging="footer__left__number-page"
                            v-model="pageNumber"
                            :numberEnd="pageNumberEnd"
                        />
                        <!--  -->
                    </section>
                </footer>
                <!--  -->
            </div>
        </div>
        <AssetTransferPopup
            v-if="isShowPopup"
            @close="isShowPopup = false"
            @updateListTransferAsset="
                (action) => {
                    updateListTransferAsset(action)
                }
            "
            :action="action"
            :transferAssetDetails="transferAssetDetails"
        />

        <!-- Thông báo thành công -->
        <section class="toast__message z-999" v-if="isShowToastMessage">
            <m-toast :typeToast="toastMessageType" :content="toastMessageContent" />
        </section>
        <!--  -->
    </div>
</template>

<script>
import { rowOnClick, rowOnClickByCheckBox, rowOnCtrlClick } from '../../helper/table/rowSelect'
import FixedAssetAPI from '../../api/FixedAsset.API'
import TransferAssetAPI from '../../api/TransferAsset.API'
import TransferAssetDetailAPI from '../../api/TransferAssetDetail.API'
import LoadingSkeleton from '../fixed_asset/LoadingSkeleton.vue'
import AssetTransferPopup from './AssetTransferPopup.vue'

export default {
    name: 'AssetTransferList',
    components: {
        LoadingSkeleton,
        AssetTransferPopup
    },
    props: {
        isChangeWidth: {
            type: Boolean,
            default: false
        }
    },
    data() {
        return {
            assets: [],
            TotalRecords: 0,
            transferAssets: [],
            transferAssetDetails: {},
            action: '',
            //=====================Toast=============================
            isShowToastMessage: false,
            toastMessageContent: '',
            toastMessageType: 'success',
            //=====================END Toast=============================
            details: [],
            pageLimitOfTrans: 20,
            pageNumberOfTrans: 1,
            pageNumberEndOfTrans: 1,
            pageLimit: 20,
            pageNumber: 1,
            pageNumberEnd: 0,
            isLoadingDataTable: false,
            isLoadingDataDetail: false,
            isShowPopup: false,
            startY: 0,
            initialHeight: 0,
            // ----------------------------- Resize table -----------------------------
            // Hiển thị tooltip của icon resize
            visible_tool_tip: false,
            // Kiểm tra coi đã nhấn giữ icon resize chưa
            isMouseDown: false,
            // Điểm bắt đầu của chuột khi click vào khối div resize bar
            startY: null,
            // Chiều cao ban đầu của resizable table
            initialHeight: null,
            // Chiều cao ban đầu của resizable table (giá trị không thể đổi)
            initialHeightFix: null,
            // Chiều cao của resizable table khi đang resize
            initialHeightAfterResize: null,
            // Chiều cao ban đầu của table top (giá trị không thể đổi)
            tableTopHeightFix: null,
            // Thu gọn table bằng icon
            isNarrow: false,

            selectedRowsByCheckBox: [],
            selectedRows: []
        }
    },
    mounted() {
        this.loadData()
        this.loadAllTransferAsset()
    },
    methods: {
        async loadData() {
            try {
                this.isLoadingDataTable = true
                let res = await FixedAssetAPI.getAllFixedAsset()
                this.assets = res.data
            } catch (error) {
                console.log(error)
            }
            setTimeout(() => {
                this.isLoadingDataTable = false
            }, 1000)
        },
        async loadAllTransferAsset() {
            try {
                this.isLoadingDataTable = true
                let res = await TransferAssetAPI.getAllTransferAssetPaging(
                    this.pageLimitOfTrans,
                    this.pageNumber
                )
                this.transferAssets = res.data.TransferAssets
                this.TotalRecords = res.data.TotalRecords
                this.pageNumberEndOfTrans = Math.ceil(res.data.TotalRecords / this.pageLimitOfTrans)
            } catch (error) {
                console.log(error)
            }
            setTimeout(() => {
                this.isLoadingDataTable = false
            }, 1000)
        },
        async loadTransferAssetDetail(transferAssetId) {
            try {
                this.isLoadingDataDetail = true
                let res = await TransferAssetDetailAPI.getAllTransferAssetDetailPaging(
                    transferAssetId
                )
                this.details = res.data
            } catch (error) {
                console.log(error)
            }
            setTimeout(() => {
                this.isLoadingDataDetail = false
            }, 1000)
        },
        async loadDataforEditForm(transferAsset) {
            this.action = 'update'
            this.isShowPopup = true
            this.transferAssetDetails = transferAsset
        },
        startResize(event) {
            event.preventDefault() // Ngăn chặn chọn văn bản khi kéo
            document.addEventListener('mousemove', this.resizing)
            document.addEventListener('mouseup', this.stopResize)

            this.startY = event.clientY // Lưu vị trí chuột xuất phát
            this.initialHeight = this.$refs.resizableTable.clientHeight // Lưu kích thước ban đầu của resizable-table
        },
        resizing(event) {
            const movementY = this.startY - event.clientY // Tính toán khoảng cách di chuyển của chuột
            const newHeight = this.initialHeight + movementY // Tính toán chiều cao mới dựa trên kích thước ban đầu và khoảng cách di chuyển

            const contentBodyHeight = this.$refs.contentBody.clientHeight
            const minHeight = this.$refs.tableTop.clientHeight // Độ cao tối thiểu
            const maxHeight = contentBodyHeight // Độ cao tối đa
            // Áp dụng độ cao mới vào resizable-table và giữ nguyên con trỏ
            this.$refs.resizableTable.style.height = `${Math.min(
                Math.max(newHeight, minHeight),
                maxHeight
            )}px`
            this.initialHeightAfterResize = this.$refs.resizableTable.clientHeight
            this.$refs.bodyTop.style.height = `${
                contentBodyHeight - Math.min(Math.max(newHeight, minHeight), maxHeight)
            }px`
            document.body.style.cursor = 'ns-resize'
        },

        stopResize() {
            document.removeEventListener('mousemove', this.resizing)
            document.removeEventListener('mouseup', this.stopResize)
            document.body.style.cursor = 'auto' // Trả lại con trỏ mặc định
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
        formatDate(inputDate) {
            const date = new Date(inputDate)
            const day = String(date.getDate()).padStart(2, '0')
            const month = String(date.getMonth() + 1).padStart(2, '0') // Lưu ý: Tháng bắt đầu từ 0
            const year = date.getFullYear()
            return `${day}/${month}/${year}`
        },
        /**
         * @author LB.Thành (08/08/2023)
         * @param {*} action Cập nhật lại danh sách chứng từ sau khi thêm, sửa, xóa
         */
        updateListTransferAsset(action) {
            // Cập nhật lại trang
            this.loadAllTransferAsset()
            setTimeout(() => {
                this.showToastMessage(true, this.$_MISAResource.VN.success[action], 'success')
            }, 1000)
        },

        deleteSingleTransferAsset(transferAsset) {
            this.isLoadingDataTable = true
            var listIds = []
            listIds.push(transferAsset.TransferAssetId)
            console.log(listIds)
            TransferAssetAPI.deleteManyTransferAssets(listIds)
            this.isLoadingDataTable = false
        },
        /**
         * @author LB.Thành (11/07/2023)
         * @param {isShow, message, type}
         * Hiện thông báo khi xử lý logic thành công
         */
        showToastMessage(isShow, content, type) {
            this.isShowToastMessage = isShow
            this.toastMessageContent = content
            this.toastMessageType = type
            this.saveTimer = setTimeout(() => {
                this.isShowToastMessage = false
            }, 3000)
        },
        //======================================== HANDLE ROW SELECTION =====================================//
        callRowOnClick(transferAsset) {
            rowOnClick.call(this, transferAsset)
        },

        callRowOnClickByCheckBox(transferAsset) {
            rowOnClickByCheckBox.call(this, transferAsset)
        },

        callRowOnCtrlClick(transferAsset) {
            rowOnCtrlClick.call(this, transferAsset)
            console.log(this.selectedRowsByCheckBox)
        },

        btnUncheckedAll() {
            this.selectedRows = []
            this.selectedRowsByCheckBox = []
        },
        //======================================== HANDLE CALCULATE ==========================================//
        /**
         * @description Tổng giá trị tất cả chứng từ hiện có trong trang hiện tại
         * @returns {number} tổng giá trị chứng từ trong trang hiện tại
         * @author LB.Thành (08/08/2023)
         */
        costTotal() {
            return this.transferAssets.reduce((total, item) => {
                return total + item.Cost
            }, 0)
        },
        /**
         * @description Tổng giá trị tất cả chứng từ hiện có trong trang hiện tại
         * @returns {number} tổng giá trị chứng từ trong trang hiện tại
         * @author LB.Thành (08/08/2023)
         */
        remainTotal() {
            return this.transferAssets.reduce((total, item) => {
                return total + item.RemainCost
            }, 0)
        }
    }
}
</script>

<style scoped>
@import '../../css/views/AssetTransfer.css';
</style>
