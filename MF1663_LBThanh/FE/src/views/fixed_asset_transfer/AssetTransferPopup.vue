<template>
    <div class="popup__container z-999">
        <header class="t-popup__header center-y justify-between w-100 h-36">
            <h1 class="t-popup__header--title" v-if="action == 'create'">
                Thêm chứng từ điều chuyển
            </h1>
            <h1 class="t-popup__header--title" v-else>Sửa chứng từ điều chuyển</h1>
            <section class="wrapper-icon" @click="$emit('close')" title="Thoát">
                <section class="icon t-popup__header--close"></section>
            </section>
        </header>
        <form>
            <div class="sub-title">Thông tin chung</div>
            <section
                class="popup__content--area"
                :class="{ 'change-height-receiver': isOpenReceiver }"
            >
                <section class="content__area--input content__area--row">
                    <m-input
                        ref="transferAssetCode"
                        labelStyle="fw-500 font-14"
                        classLabel="transferAssetCode"
                        :label="labelForm.DocumentCode"
                        v-model="TransferAssetCode"
                        placeholderItalics
                        maxlength="50"
                        required
                        tabindex="1"
                    />

                    <m-input
                        classLabel="StartUsingDate"
                        labelStyle="fw-500 font-14"
                        :label="labelForm.DocumentDate"
                        v-model="TransactionDate"
                        type="date"
                        :placeholder="placeholderForm.time"
                        required
                        tabindex="2"
                    >
                        <template #iconRight>
                            <section class="wrapper-icon absolute r-6">
                                <section class="icon calendar"></section>
                            </section>
                        </template>
                    </m-input>

                    <m-input
                        classLabel="StartUsingDate"
                        labelStyle="fw-500 font-14"
                        :label="labelForm.TransferDate"
                        v-model="TransferDate"
                        type="date"
                        ref="StartUsingDate"
                        :placeholder="placeholderForm.time"
                        required
                        tabindex="3"
                    >
                        <template #iconRight>
                            <section class="wrapper-icon absolute r-6">
                                <section class="icon calendar"></section>
                            </section>
                        </template>
                    </m-input>

                    <m-input
                        labelStyle="fw-500 font-14"
                        :label="labelForm.Note"
                        v-model="Note"
                        maxlength="255"
                        tabindex="4"
                    />
                </section>
                <section class="table__checkbox flex-row col-gap-50 relative">
                    <section class="flex-row col-gap-8">
                        <m-checkbox
                            @change="toggleReceiver(), initReceiverForm()"
                            :checked="checked"
                            title="Chọn ban giao nhận"
                        />
                        <span>Chọn ban giao nhận</span>
                    </section>
                    <section class="flex-row col-gap-8" v-if="isOpenReceiver">
                        <m-checkbox title="Chọn ban giao nhận" />
                        <span>Thêm ban giao nhận từ lần nhập trước</span>
                    </section>
                </section>
                <section class="content__area--receiver" v-if="isOpenReceiver">
                    <section class="receiver-header receiver-row">
                        <div class="cell display--center-left font-14">STT</div>
                        <div class="cell display--center-left pl-10 font-14">Họ và tên</div>
                        <div class="cell display--center-left pl-10 font-14">Đại diện</div>
                        <div class="cell display--center-left pl-10 font-14">Chức vụ</div>
                    </section>

                    <section class="receiver-body">
                        <section
                            class="receiver-row"
                            v-for="(receiver, index) in receivers"
                            :key="index"
                        >
                            <div class="cell display--center-center font-14 box">
                                {{ index + 1 }}
                            </div>
                            <div class="cell display--center-left pl-10 font-14">
                                <m-input
                                    placeholderItalics
                                    maxlength="50"
                                    required
                                    tabindex="1"
                                    placeholder="Nhập họ và tên"
                                    classLabel="w-100 pr-10"
                                    v-model="receiver.FullName"
                                />
                            </div>
                            <div class="cell display--center-left pl-10 font-14">
                                <m-input
                                    placeholderItalics
                                    maxlength="50"
                                    required
                                    tabindex="1"
                                    placeholder="Nhập đại diện"
                                    classLabel="w-100 pr-10"
                                    v-model="receiver.Delegate"
                                />
                            </div>
                            <div class="cell display--center-left pl-10 font-14">
                                <m-input
                                    placeholderItalics
                                    maxlength="50"
                                    required
                                    tabindex="1"
                                    placeholder="Nhập chức vụ"
                                    classLabel="w-100 pr-10"
                                    v-model="receiver.Position"
                                />
                            </div>
                            <div class="cell display--center-left pl-10 font-14 icon-list">
                                <section class="icon arrow-up"></section>
                                <section class="icon arrow-down"></section>
                                <section
                                    class="icon addBoxGrey"
                                    @click="btnAddReceiver()"
                                ></section>
                                <section
                                    class="icon delete"
                                    v-if="index > 0"
                                    @click="btnDeleteReceiver(receiver)"
                                ></section>
                            </div>
                        </section>
                    </section>
                </section>
            </section>
            <!-- Thao tác tìm kiếm, thêm, xóa tài sản điều chuyển -->
            <section class="popup-detail-action">
                <section class="popup__detail-action--right center-y col-gap-16">
                    <span>Thông tin tài sản điều chuyển</span>
                    <m-input
                        placeholder="Tìm kiếm tài sản"
                        placeholderItalics
                        width="263px !important"
                        @Search="filterListFixedAsset"
                    >
                        <template #iconLeft>
                            <section class="icon search">
                                <m-tooltip content="Tìm kiếm" type="bottom" />
                            </section>
                        </template>
                    </m-input>
                </section>
                <section class="popup__detail-action--left" @click="btnShowFormChooseAsset()">
                    <m-button>
                        <template #icon>
                            <section class="icon addBoxGrey"></section>
                        </template>
                        Chọn tài sản
                    </m-button>
                </section>
            </section>
            <!-- Thông tin chi tiết tài sản điều chuyển  -->
            <section class="popup-data-list">
                <!-- ------------------------Table start------------------------ -->
                <div class="table">
                    <!-- ------------------------Header------------------------ -->
                    <div class="header--row row pr-4 bc-grey">
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
                            class="header cell display--center-center font-weight--700 border--top border--right border--bottom"
                        >
                            Bộ phận điều chuyển đến
                        </div>
                        <div
                            class="header cell display--center-left font-weight--700 border--top border--right border--bottom pl-10"
                        >
                            Lý do
                        </div>
                        <div
                            class="header cell display--center-center font-weight--700 border--top border--bottom"
                        >
                            Chức năng
                        </div>
                    </div>

                    <!-- ------------------------Body------------------------ -->
                    <div class="body-data">
                        <div
                            class="header--row row"
                            v-for="(asset, index) in assets"
                            :key="asset.FixedAssetId"
                        >
                            <div
                                class="cell display--center-center border--top border--right border--bottom"
                            >
                                <input type="checkbox" />
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
                                class="cell display--center-right border--top border--right border--bottom pr-10"
                            >
                                {{ formatNumber(asset.Cost) }}
                            </div>
                            <div
                                class="cell display--center-right border--top border--right border--bottom pr-10"
                            >
                                {{
                                    this.action === 'create'
                                        ? formatNumber(FixedAssetRemaining(asset))
                                        : formatNumber(asset.RemainderCost)
                                }}
                            </div>
                            <div
                                class="cell display--center-left border--top border--right border--bottom pl-10"
                            >
                                {{ asset.OldDepartmentName }}
                            </div>
                            <div
                                class="cell display--center-center border--top border--right border--bottom pl-4 pr-4"
                            >
                                <m-combobox
                                    required
                                    :styleCombobox="400"
                                    placeholder="Bộ phận sử dụng"
                                    width="200px"
                                    classInput="input-filter"
                                    :listSelect="listDepartmentName()"
                                    v-model="asset.TransferDepartmentName"
                                    @filter="getDepartmentFilter"
                                />
                            </div>
                            <div
                                class="cell display--center-left border--top border--right border--bottom pl-4 pr-4"
                            >
                                <m-input
                                    placeholderItalics
                                    maxlength="255"
                                    classLabel="w-100"
                                    v-model="asset.Reason"
                                />
                            </div>
                            <div class="cell display--center-center border--top border--bottom">
                                <section
                                    class="icon delete"
                                    @click="btnDeleteAsset(asset)"
                                ></section>
                            </div>
                        </div>
                    </div>
                    <div class="body-data__footer pr-4 bc-grey">
                        <div class="row bc-grey">
                            <div class="cell display--center-center border--bottom bc-grey"></div>
                            <div class="cell display--center-center border--bottom bc-grey"></div>
                            <div
                                class="cell display--center-left border--bottom pl-10 bc-grey"
                            ></div>
                            <div
                                class="cell display--center-center border--right border--bottom bc-grey"
                            ></div>
                            <div
                                class="cell display--center-right font-weight--700 border--right border--bottom pr-10 bc-grey"
                            >
                                {{ formatNumber(costTotal()) }}
                            </div>
                            <div
                                class="cell display--center-right font-weight--700 border--right border--bottom pr-10 bc-grey"
                            >
                                {{
                                    this.action === 'create'
                                        ? formatNumber(remainingTotal())
                                        : formatNumber(remainderTotal())
                                }}
                            </div>
                        </div>
                    </div>
                    <!-- Footer -->
                    <footer class="table__footer h-40 grid bg-white">
                        <!-- Footer left -->
                        <section class="table__footer_left center-y">
                            <!-- Tổng số tài sản -->
                            <section class="footer__left__info">
                                Tổng số: <span>{{ assets.length }}</span> bản ghi
                            </section>
                            <!--  -->

                            <!-- Chọn số tài sản trong một trang -->
                            <m-dropdown v-model="pageLimit" />
                            <!--  -->

                            <!-- Thay đổi trang -->
                            <m-paging
                                classPaging="footer__left__number-page"
                                v-model="pageNumber"
                                :numberEnd="5"
                            />
                            <!--  -->
                        </section>
                    </footer>
                    <!--  -->
                </div>
                <!-- ------------------------Table end------------------------ -->
            </section>
        </form>
        <footer class="popup-footer center-y col-gap-16">
            <m-button typeButton="outline" width="100px" tabindex="12"> Hủy </m-button>
            <m-button width="100px" @clickButton="submit" tabindex="11"> Lưu </m-button>
        </footer>
        <AssetTransferPick
            v-if="isOpenForm"
            @hidden="isOpenForm = false"
            @loadDataToDetails="loadDataToDetails"
            :existFixedAsset="existFixedAsset"
        />
    </div>
</template>

<script>
import AssetTransferPick from './AssetTransferPick.vue'
import TransferAssetAPI from '../../api/TransferAsset.API'
import TransferAssetDetailAPI from '../../api/TransferAssetDetail.API'
import ReceiverAPI from '../../api/Receiver.API'
import DepartmentAPI from '../../api/Department.API'
import { useIsLoading } from '../../../src/stores/isLoading.js'
export default {
    name: 'AssetTransferPopup',
    components: {
        AssetTransferPick
    },
    props: {
        action: {
            type: String,
            default: 'create'
        },
        transferAssetDetails: {
            type: Object,
            default: () => ({})
        }
    },
    data() {
        return {
            assets: [],
            unchangeDetailsList: null, // Theo dõi sự thay đổi của các item trong assets, 0 thêm hoạc xóa, chỉ cập nhật
            createDetailList: [],
            originalAssets: [], // danh sách nguyên thủy 0 bao giờ thay đổi
            updateDetailsList: null, // danh sách lưu trữ nhưng bản ghi cần cập nhật
            deleteDetailsList: [], // danh sách nhưng bản ghi cần xóa
            receivers: [],
            createReceivers: [],
            updateListReceiver: [], // danh sách những bản ghi cần cập nhật
            deleteReceiversList: [],
            originalReceiver: [],
            checked: false,
            isOpenForm: false,
            isOpenReceiver: false,
            // Các trường của chứng từ
            TransferAssetCode: '',
            TransactionDate: '',
            TransferDate: '',
            Cost: 0,
            RemainderCost: 0,
            Note: '',
            // End Các trường của chứng từ
            existFixedAsset: null,
            Reason: ''
        }
    },
    emits: ['close', 'updateListTransferAsset'],
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
        },
        /**
         * @description Kiểm tra xem có đang load dữ liệu hay không
         * @author LB.Thành (27-07-2023)
         */
        isLoading() {
            return useIsLoading().isLoading
        },
        /**
         * @description Thay đổi trạng thái load dữ liệu
         * @author LB.Thành (27-07-2023)
         */
        setIsLoading() {
            return useIsLoading().setIsLoading
        }
    },
    created() {
        this.getAllDepartment()
    },
    async mounted() {
        this.$refs.transferAssetCode.focus()
        if (this.action == 'create') {
            this.getNewTransferAssetCode()
            const date = new Date()
            const day = date.getDate() > 9 ? date.getDate() : '0' + date.getDate()
            const month =
                date.getMonth() + 1 > 9 ? date.getMonth() + 1 : '0' + (date.getMonth() + 1)
            const year = date.getFullYear()
            this.TransactionDate = `${year}-${month}-${day}`
            this.TransferDate = `${year}-${month}-${day}`
        }
        if (this.action == 'update') {
            let data = this.transferAssetDetails
            this.TransferAssetCode = data.TransferAssetCode
            this.TransactionDate = this.convertDate(data.TransactionDate)
            this.TransferDate = this.convertDate(data.TransferDate)
            this.Note = data.Note
            await this.getAllDetailsFromTransferAsset(data.TransferAssetId)
            await this.getAllReceiverByTransferAsset(data.TransferAssetId)
            if (this.receivers.length > 0) {
                this.checked = true
                this.isOpenReceiver = true
            }
        }
    },
    methods: {
        toggleReceiver() {
            this.isOpenReceiver = !this.isOpenReceiver
        },
        initReceiverForm() {
            // Kiểm tra nếu mảng receivers rỗng, thêm một phần tử mặc định
            if (this.receivers.length === 0) {
                this.receivers.push({
                    ReceiverOrder: this.receivers.length + 1,
                    FullName: '',
                    Delegate: '',
                    Position: ''
                })
            }
        },
        btnShowFormChooseAsset() {
            this.assets.forEach((asset) => {
                if (!Object.prototype.hasOwnProperty.call(asset, 'DepartmentCode')) {
                    asset.Quantity = 1000
                    asset.DepartmentCode = 'raw'
                    asset.DepartmentName = 'raw'
                    asset.FixedAssetCategoryCode = 'raw'
                    asset.FixedAssetCategoryName = 'raw'
                }
            })
            this.existFixedAsset = this.assets
            this.isOpenForm = true
        },
        async createNewTransferAsset(transferAsset) {
            try {
                await TransferAssetAPI.createTransferAsset(transferAsset)
                setTimeout(() => {
                    this.$emit('close')
                    this.$emit('updateListTransferAsset', 'create')
                }, 500)
            } catch (error) {
                console.log(error)
            }
        },
        async updateTransferAssetAsync(id, obj) {
            try {
                await TransferAssetAPI.updateTransferAsset(id, obj)
                setTimeout(() => {
                    this.$emit('close')
                    this.$emit('updateListTransferAsset', 'update')
                }, 500)
            } catch (error) {
                console.log(error)
            }
        },
        /**
         * @description Lấy mã chứng từ mới
         * @returns {string} mã chứng từ mới
         * @author LB.Thành (10/09/2023)
         */
        async getNewTransferAssetCode() {
            try {
                this.setIsLoading(true)
                const res = await TransferAssetAPI.getTransferAssetCode()
                setTimeout(() => {
                    this.setIsLoading(false)
                    this.TransferAssetCode = res.data
                }, 500)
            } catch (error) {
                console.log(error)
            }
        },
        //------------------------------------------- ASSET Detail-------------------------------------------
        // load data từ form chọn tài sản chứng từ
        // TODO: làm thêm api lấy danh sách tài sản không chứa những tài sản đã chọn
        loadDataToDetails(items) {
            if (!this.assets || this.assets.length <= 0) {
                this.assets = items
                this.Reason = items[0].Description
            } else {
                // Kiểm tra nếu this.action là 'update', gán Flag bằng 1 cho các bản ghi mới
                if (this.action === 'update') {
                    items = items.map((item) => {
                        return {
                            ...item,
                            Flag: 0 // Gán Flag bằng 1 cho bản ghi mới
                        }
                    })
                    this.createDetailList = items
                }

                this.assets = this.assets.concat(items)
            }
        },
        async getAllDetailsFromTransferAsset(id) {
            let res = await TransferAssetDetailAPI.getAllTransferAssetDetailPaging(id)
            this.assets = res.data
            this.originalAssets = JSON.parse(JSON.stringify(this.assets))
        },
        //======================================================================================================

        //------------------------------------------- DEPARTMNET -------------------------------------------
        async getAllDepartment() {
            const res = await DepartmentAPI.getAllDepartment()
            this.listDepartment = res.data
        },
        listDepartmentName() {
            return this.listDepartment.map((item) => item.DepartmentName)
        },
        //======================================================================================================

        //============================================= RECEIVER ============================================
        async getAllReceiverByTransferAsset(id) {
            let res = await ReceiverAPI.getAllReceiverByTransfer(id)
            this.receivers = res.data
            this.originalReceiver = JSON.parse(JSON.stringify(this.receivers))
        },

        //======================================================================================================
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
        remainderTotal() {
            return this.assets.reduce((total, item) => {
                return total + item.RemainderCost
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
        convertDate(date) {
            date = new Date(date)
            const day = date.getDate()
            const month = date.getMonth() + 1
            const year = date.getFullYear()
            if (day <= 9 && month <= 9) return `${year}-0${month}-0${day}`
            if (day <= 9) return `${year}-${month}-0${day}`
            if (month <= 9) return `${year}-0${month}-${day}`
            return `${year}-${month}-${day}`
        },

        prepareDataForSubmit() {
            // Details =========================================
            let listUpdate = this.assets.filter(
                (asset) =>
                    asset.TransferAssetDetailId !== null &&
                    asset.TransferAssetDetailId !== '' &&
                    typeof asset.TransferAssetDetailId !== 'undefined' &&
                    Object.prototype.hasOwnProperty.call(asset, 'TransferAssetDetailId')
            )

            let listChange = listUpdate
                .filter((newItem) => {
                    return this.originalAssets.find(
                        (item) =>
                            item.TransferAssetDetailId === newItem.TransferAssetDetailId &&
                            (item.Reason !== newItem.Reason ||
                                item.TransferDepartmentName !== newItem.TransferDepartmentName)
                    )
                })
                .map((newItem) => ({
                    ...newItem,
                    Flag: 1
                }))
            this.updateDetailsList = listChange
            // Details =========================================

            // Receivers =========================================
            let listReceiverUpdate = this.receivers.filter(
                (receiver) =>
                    receiver.TransferAssetId !== null &&
                    receiver.TransferAssetId !== '' &&
                    typeof receiver.TransferAssetId !== 'undefined' &&
                    Object.prototype.hasOwnProperty.call(receiver, 'TransferAssetId')
            )
            this.updateListReceiver = listReceiverUpdate
                .filter((newItem) => {
                    return this.originalReceiver.find(
                        (item) =>
                            item.ReceiverId === newItem.ReceiverId &&
                            (item.Delegate !== newItem.Delegate ||
                                item.Position !== newItem.Position ||
                                item.FullName !== newItem.FullName)
                    )
                })
                .map((newItem) => ({
                    ...newItem,
                    Flag: 1
                }))
            let listReceiverCreate = this.receivers.filter(
                (receiver) =>
                    receiver.TransferAssetId === null ||
                    receiver.TransferAssetId === '' ||
                    (typeof receiver.TransferAssetId === 'undefined' ||
                        !Object.prototype.hasOwnProperty.call(receiver, 'TransferAssetId'))
            )
            this.createReceivers = listReceiverCreate.map((newItem) => ({
                ...newItem,
                Flag: 0
            }))
        },

        submit() {
            if (this.action == 'create') {
                let transferAsset = {
                    TransferAssetCode: this.TransferAssetCode,
                    TransactionDate: this.TransactionDate,
                    TransferDate: this.TransferDate,
                    Cost: this.costTotal(),
                    RemainCost: this.remainingTotal(),
                    Note: this.Note
                }
                let detailsAssets = this.assets.map((item) => ({
                    FixedAssetId: item.FixedAssetId,
                    FixedAssetCode: item.FixedAssetCode,
                    FixedAssetName: item.FixedAssetName,
                    Cost: item.Cost,
                    RemainderCost: this.FixedAssetRemaining(item),
                    OldDepartmentId: item.DepartmentId,
                    OldDepartmentName: item.DepartmentName,
                    TransferDepartmentName: item.TransferDepartmentName,
                    TransferDepartmentId: this.listDepartment.find(
                        (i) => i.DepartmentName == item.TransferDepartmentName
                    )?.DepartmentId,
                    Reason: item.Reason
                }))
                let newTransferAsset = {
                    TransferAsset: transferAsset,
                    ListTransferAssetDetail: detailsAssets,
                    ListReceiver: this.receivers
                }
                this.createNewTransferAsset(newTransferAsset)
            } else if (this.action == 'update') {
                this.prepareDataForSubmit()
                let updateData = {
                    TransferAsset: {
                        TransferAssetCode: this.TransferAssetCode,
                        TransactionDate: this.TransactionDate,
                        TransferDate: this.TransferDate,
                        Cost: this.costTotal(),
                        RemainCost: this.remainderTotal(),
                        Note: this.Note
                    },
                    ListTransferAssetDetail: [
                        ...this.createDetailList,
                        ...this.deleteDetailsList,
                        ...this.updateDetailsList
                    ],
                    ListReceiver: [
                        ...this.updateListReceiver,
                        ...this.deleteReceiversList,
                        ...this.createReceivers
                    ]
                }
                this.updateTransferAssetAsync(this.transferAssetDetails.TransferAssetId, updateData)
                console.log(updateData)
            }
        },

        //=========================================Button Handler===========================================
        btnAddReceiver() {
            const newReceiver = {
                ReceiverOrder: this.receivers.length + 1,
                FullName: '',
                Delegate: '',
                Position: ''
            }
            this.receivers.push(newReceiver)
        },
        btnDeleteAsset(asset) {
            const indexAssets = this.assets.findIndex(
                (item) => item.TransferAssetDetailId === asset.TransferAssetDetailId
            )
            if (indexAssets !== -1) {
                this.assets.splice(indexAssets, 1)
            }
            if (asset.TransferAssetDetailId !== null && asset.TransferAssetDetailId !== '') {
                this.deleteDetailsList.push({
                    ...asset,
                    Flag: 2
                })
            }
        },
        btnDeleteReceiver(receiver) {
            const indexReceivers = this.receivers.findIndex(
                (item) => item.ReceiverId === receiver.ReceiverId
            )
            if (indexReceivers !== -1) {
                this.receivers.splice(indexReceivers, 1)
            }
            if (receiver.TransferAssetId !== null && receiver.TransferAssetId !== '') {
                this.deleteReceiversList.push({
                    ...receiver,
                    Flag: 2
                })
            }
        },
        removeReceiver(index) {
            // Xóa receiver khỏi mảng
            this.receivers.splice(index, 1)

            // Cập nhật lại giá trị ReceiverOrder cho các receiver còn lại trong mảng
            for (let i = 0; i < this.receivers.length; i++) {
                this.receivers[i].ReceiverOrder = i + 1
            }
        }
    }
}
</script>

<style scoped>
@import '../../css/views/AssetTransferPopup.css';
</style>
