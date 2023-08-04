<template>
    <form class="t-popup" ref="form" @keyup.esc="showToast">
        <header class="t-popup__header center-y justify-between w-100 h-36">
            <h1 class="t-popup__header--title">{{ title }}</h1>
            <section class="wrapper-icon" @click="showToast" title="Thoát">
                <section class="icon t-popup__header--close"></section>
            </section>
        </header>
        <main class="t-popup__header--body edit__body">
            <m-input
                ref="fixedAssetCode"
                classLabel="fixedAssetCode"
                :label="labelForm.FixedAssetCode"
                :placeholder="placeholderForm.FixedAssetCode"
                placeholderItalics
                v-model="FixedAssetCode"
                @focus="setInputFocus('fixedAssetCode')"
                maxlength="20"
                required
            />

            <m-input
                ref="fixedAssetName"
                classLabel="fixedAssetName"
                :label="labelForm.FixedAssetName"
                :placeholder="placeholderForm.FixedAssetName"
                placeholderItalics
                v-model="FixedAssetName"
                @focus="setInputFocus('fixedAssetName')"
                maxlength="100"
                required
            />

            <m-combobox
                ref="departmentCode"
                classLabel="departmentCode"
                :label="labelForm.DepartmentCode"
                :placeholder="placeholderForm.DepartmentCode"
                v-model="DepartmentCode"
                :listSelect="listDepartmentCode"
                @focus="setInputFocus('departmentCode')"
                :maxlength="20"
                required
            />

            <m-input
                classLabel="departmentName"
                :label="labelForm.DepartmentName"
                readonly
                tabindex="-1"
                v-model="DepartmentName"
                required
            />

            <m-combobox
                ref="fixedAssetCategoryCode"
                classLabel="fixedAssetCategoryCode"
                :label="labelForm.FixedAssetCategoryCode"
                :placeholder="placeholderForm.FixedAssetCategoryCode"
                :listSelect="listFixedAssetCategoryCode"
                v-model="FixedAssetCategoryCode"
                @focus="setInputFocus('fixedAssetCategoryCode')"
                :maxlength="20"
                required
            />

            <m-input
                classLabel="fixedAssetCategoryName"
                required
                :label="labelForm.FixedAssetCategoryName"
                readonly
                tabindex="-1"
                v-model="FixedAssetCategoryName"
            />

            <m-input
                ref="quantity"
                classLabel="quantity"
                :label="labelForm.Quantity"
                v-model="Quantity"
                textRight
                typeOfInput="number"
                @focus="setInputFocus('quantity')"
                required
                @keydown.up.stop="increaseCount"
                @keydown.down.stop="decreaseCount"
            >
                <template #iconRight>
                    <section class="wrapper-icon flex-column row-gap-3">
                        <section class="choose-year-up icon" @click="increaseCount"></section>
                        <section class="choose-year-down icon" @click="decreaseCount"></section>
                    </section>
                </template>
            </m-input>

            <m-input
                ref="cost"
                classLabel="cost"
                :label="labelForm.Cost"
                v-model="Cost"
                textRight
                typeOfInput="number"
                @focus="setInputFocus('cost')"
                required
            />
            <m-input
                ref="lifeTime"
                classLabel="lifeTime"
                :label="labelForm.LifeTime"
                textRight
                v-model="LifeTime"
                @focus="setInputFocus('lifeTime')"
                required
            />

            <m-input
                classLabel="depreciationRate"
                :label="labelForm.DepreciationRate"
                v-model="DepreciationRate"
                typeOfInput="percent"
                textRight
                readonly
                required
            >
                <template #iconRight>
                    <section class="wrapper-icon flex-column row-gap-3">
                        <section class="choose-year-up icon"></section>
                        <section class="choose-year-down icon"></section>
                    </section>
                </template>
            </m-input>

            <m-input
                ref="yearDepreciation"
                classLabel="yearDepreciation"
                :label="labelForm.YearDepreciation"
                v-model="YearDepreciation"
                typeOfInput="number"
                textRight
                @focus="setInputFocus('yearDepreciation')"
                required
            />

            <m-input
                classLabel="trackedYear"
                :label="labelForm.TrackedYear"
                readonly
                tabindex="-1"
                v-model="TrackedYear"
                typeOfInput="number"
                textRight
                required
            />

            <m-input
                classLabel="purchaseDate"
                :label="labelForm.PurchaseDate"
                v-model="PurchaseDate"
                type="date"
                ref="purchaseDate"
                @focus="setInputFocus('purchaseDate')"
                :placeholder="placeholderForm.time"
                required
            >
                <template #iconRight>
                    <section class="wrapper-icon absolute r-6">
                        <section class="icon calendar"></section>
                    </section>
                </template>
            </m-input>

            <m-input
                classLabel="usingStartedDate"
                :label="labelForm.UsingStartedDate"
                v-model="UsingStartedDate"
                type="date"
                ref="usingStartedDate"
                @focus="setInputFocus('usingStartedDate')"
                :placeholder="placeholderForm.time"
                required
            >
                <template #iconRight>
                    <section class="wrapper-icon absolute r-6">
                        <section class="icon calendar"></section>
                    </section>
                </template>
            </m-input>
        </main>

        <footer class="t-popup__footer center-y">
            <m-button
                typeButton="outline"
                width="100px"
                style="border: none"
                @clickButton="showToast"
            >
                Hủy
            </m-button>
            <m-button width="100px" @clickButton="submit">{{ contentBtnSubmit }}</m-button>
        </footer>
    </form>

    <section class="blur center" v-if="isShowToastCancel">
        <m-toast
            typeToast="warning"
            :content="
                action === 'update'
                    ? $_MISAResource.VN.warningCancel.noUpdate
                    : $_MISAResource.VN.warningCancel.noCreate
            "
            @keyup="
                (event) => {
                    if (
                        (event.key === 'ArrowLeft' || event.key === 'Tab') &&
                        focusToastWaring === 'cancel'
                    ) {
                        focusToastWaring = 'no'
                    }
                    if (
                        (event.key === 'ArrowRight' || event.key === 'Tab') &&
                        focusToastWaring === 'no'
                    ) {
                        focusToastWaring = 'cancel'
                    }
                }
            "
        >
            <m-button
                typeButton="outline"
                @clickButton="
                    ;(isShowToastCancel = false),
                        $refs[this.inputFocus].focus(),
                        (focusToastWaring = 'cancel')
                "
                :focus="focusToastWaring === 'no'"
                >Không</m-button
            >
            <m-button @clickButton="closePopup" :focus="focusToastWaring === 'cancel'"
                >Hủy bỏ</m-button
            >
        </m-toast>
    </section>

    <section class="blur center" v-if="isShowToastCancelEdit">
        <m-toast typeToast="warning" :content="$_MISAResource.VN.warningCancel.edit">
            <m-button typeButton="outline" @clickButton="isShowToastCancelEdit = false"
                >Hủy bỏ</m-button
            >
            <m-button typeButton="sub" @clickButton="closePopup">Không lưu</m-button>
            <m-button @clickButton="submit">Lưu</m-button>
        </m-toast>
    </section>

    <!-- Warning Validate Dữ liệu-->
    <section class="blur center" v-if="isShowToastValidateData">
        <m-toast
            typeToast="warning"
            :content="errorMessage === '' ? contentValidate.message : errorMessage"
            :messageRight="messageValidateData"
        >
            <m-button @clickButton="closeToastValidate" focus>Đóng</m-button>
        </m-toast>
    </section>

    <!-- Validate Nghiệp vụ -->
    <section class="blur center" v-if="isShowToastValidate">
        <m-toast typeToast="warning" :content="messageValidate">
            <m-button @clickButton="closeToastValidate" focus>Đóng</m-button>
        </m-toast>
    </section>
</template>

<script>
import FixedAssetAPI from '/src/api/FixedAsset.API'
import { useIsLoading } from '/src/stores/isLoading.js'

export default {
    name: 'MISAPopup',
    /**
     * @author Bùi Huy Tuyền (04-07-2023)
     * @param
     * @description Đinh nghĩa props cho component
     */
    props: {
        /**
         * @description Tiêu đề popup
         * @type String
         * @default Popup
         * @Author Bùi Huy Tuyền (04/07/2021)
         */
        title: {
            type: String,
            default: 'Popup'
        },
        fixedAsset: {
            type: Object,
            default: () => ({})
        },
        listDepartment: {
            type: Array,
            default: () => []
        },
        listFixedAssetCategory: {
            type: Array,
            default: () => []
        },
        action: {
            type: String,
            default: 'create'
        },
        contentBtnSubmit: {
            type: String,
            default: 'Lưu'
        }
    },
    data() {
        return {
            // Data from
            FixedAssetCode: '',
            FixedAssetName: '',
            DepartmentCode: '',
            DepartmentName: '',
            FixedAssetCategoryCode: '',
            FixedAssetCategoryName: '',
            Quantity: '',
            Cost: '',
            LifeTime: '',
            TrackedYear: '',
            DepreciationRate: '',
            YearDepreciation: '',
            PurchaseDate: '',
            UsingStartedDate: '',
            //
            FixedAssetCategoryId: '',
            DepartmentId: '',
            //
            isShowToastCancel: false,
            isShowToastCancelEdit: false,
            isShowToastValidateData: false,
            isShowToastValidate: false,
            messageValidate: '',
            messageValidateData: '',
            inputFocus: 'fixedAssetCode',
            isValidate: true,
            fieldInvalidate: '',
            errorMessage: '',
            focusToastWaring: 'cancel'
        }
    },
    /**
     * @author Bùi Huy Tuyền (04-07-2023)
     * @description Định nghĩa các emit sử dụng trong component
     */
    emits: ['closePopup', 'updateListFixedAsset'],
    /**
     * @author Bùi Huy Tuyền (04-07-2023)
     * @description Định nghĩa các method sử dụng trong component
     */
    methods: {
        /**
         * @author Bùi Huy Tuyền (04-07-2023)
         * @param {*} fixedAsset Tài sản cần thêm
         * @description Thêm tài sản mới
         */
        async createFixedAsset(fixedAsset) {
            try {
                this.setIsLoading(true)
                await FixedAssetAPI.createFixedAsset(fixedAsset)
                setTimeout(() => {
                    this.setIsLoading(false)
                    this.$emit('closePopup')
                    this.$emit('updateListFixedAsset', this.action)
                }, 500)
            } catch (error) {
                console.log(error)
                this.setIsLoading(false)
                this.isShowToastValidateData = true
                this.errorMessage = error.response.data.UserMessage
            }
        },
        /**
         * @author Bùi Huy Tuyền (11-07-2023)
         * @param {*} fixedAssetId tài sản cần cập nhật
         * @param {*} fixedAsset tài sản cần cập nhật
         * @description Xử lý logic khi cập nhật tài sản
         */
        async updateFixedAsset(fixedAssetId, fixedAsset) {
            try {
                this.setIsLoading(true)
                await FixedAssetAPI.updateFixedAsset(fixedAssetId, fixedAsset)
                setTimeout(() => {
                    this.setIsLoading(false)
                    this.$emit('closePopup')
                    this.$emit('updateListFixedAsset', this.action)
                }, 500)
            } catch (error) {
                this.setIsLoading(false)
                this.isShowToastValidateData = true
                this.errorMessage = error.response.data.UserMessage
            }
        },
        /**
         * @description Lấy mã code tài sản gợi ý
         * @param
         * @author Bùi Huy Tuyền (11-07-2023)
         */
        async getFixedAssetCode() {
            try {
                this.setIsLoading(true)
                const res = await FixedAssetAPI.getFixedAssetCode()
                setTimeout(() => {
                    this.setIsLoading(false)
                    this.FixedAssetCode = res.data
                }, 500)
            } catch (error) {
                console.log(error)
            }
        },
        /**
         * @description Đóng form
         * @author Bùi Huy Tuyền (11-07-2023)
         */
        closePopup() {
            this.$emit('closePopup')
            this.isShowToastCancel = false
            this.isShowToastCancelEdit = false
        },
        /**
         * @description Set focus cho input
         * @param {*} value tên input cần focus
         * @author Bùi Huy Tuyền (11-07-2023)
         */
        setInputFocus(value) {
            this.inputFocus = value
        },
        /**
         * @description Tăng số lượng tài sản lên 1
         * @author Bùi Huy Tuyền (11-07-2023)
         */
        increaseCount() {
            if (Number(this.Quantity) < 9)
                this.Quantity = '0' + (Number(this.Quantity) + 1).toString()
            else {
                this.Quantity = (Number(this.Quantity) + 1).toString()
            }
        },
        /**
         * @description Giảm số lượng tài sản xuống 1
         * @author Bùi Huy Tuyền (11-07-2023)
         */
        decreaseCount() {
            if (this.Quantity === '01') return
            if (Number(this.Quantity) < 11)
                this.Quantity = '0' + (Number(this.Quantity) - 1).toString()
            else this.Quantity = (Number(this.Quantity) - 1).toString()
        },
        /**
         * @description Kiểm tra dữ liệu form
         * @author Bùi Huy Tuyền (11-07-2023)
         */
        validate() {
            // Validate Dữ liệu
            this.isValidate = true
            switch ('') {
                // Khi chưa nhập mã tài sản
                case this.FixedAssetCode:
                    this.isShowToastValidateData = true
                    this.messageValidateData = this.labelForm.FixedAssetCode
                    this.inputFocus = 'fixedAssetCode'
                    this.isValidate = false
                    return
                // Khi chưa nhập tên tài sản
                case this.FixedAssetName:
                    this.isShowToastValidateData = true
                    this.messageValidateData = this.labelForm.FixedAssetName
                    this.inputFocus = 'fixedAssetName'
                    this.isValidate = false
                    return
                // Khi chưa nhập mã phòng ban
                case this.DepartmentCode:
                    this.isShowToastValidateData = true
                    this.messageValidateData = this.labelForm.DepartmentCode
                    this.inputFocus = 'departmentCode'
                    this.isValidate = false
                    return
                // Khi chưa nhập tên phòng ban
                case this.DepartmentName:
                    this.isShowToastValidate = true
                    this.messageValidateData = this.contentValidate.DepartmentCode
                    this.inputFocus = 'departmentCode'
                    this.isValidate = false
                    return
                // Khi chưa nhâp mã loại tài sản
                case this.FixedAssetCategoryCode:
                    this.isShowToastValidateData = true
                    this.messageValidateData = this.labelForm.FixedAssetCategoryCode
                    this.inputFocus = 'fixedAssetCategoryCode'
                    this.isValidate = false
                    return
                // Khi chưa nhập tên loại tài sản
                case this.FixedAssetCategoryName:
                    console.log('object')
                    this.isShowToastValidate = true
                    this.messageValidateData = this.contentValidate.FixedAssetCategoryCode
                    this.inputFocus = 'fixedAssetCategoryCode'
                    this.isValidate = false
                    return
                default:
                    break
            }

            // Validate Nghiệp vụ
            // Validate Tỷ lệ hao mòn !== 1 / Số năm sử dụng
            let numberDepreciationRate = Number(this.DepreciationRate.replace(',', '.')) / 100
            let numberLifeTime = Number(this.lifeTime)
            if (Math.abs(numberDepreciationRate - 1 / numberLifeTime) > 0.1) {
                this.isShowToastValidate = true
                this.inputFocus = 'lifeTime'
                this.messageValidate = this.contentValidate.DepreciationRateLifeTime
                this.isValidate = false
                return
            }
            // Validate Hao mòn năm phải nhỏ hơn hoặc bằng nguyên giá
            let numberYearDepreciation = Number(this.YearDepreciation.replace(/\./g, ''))
            let numberCost = Number(this.Cost.replace(/\./g, ''))
            if (numberYearDepreciation > numberCost) {
                this.isShowToastValidate = true
                this.inputFocus = 'cost'
                this.messageValidate = this.contentValidate.YearDepreciationCost
                this.isValidate = false
                return
            }
            // Validate Ngày bắt đầu sử dụng phải nhỏ hơn hoặc bằng ngày mua
            let datePurchaseDate = new Date(this.PurchaseDate)
            let dateUsingStartedDate = new Date(this.UsingStartedDate)
            if (dateUsingStartedDate < datePurchaseDate) {
                this.isShowToastValidate = true
                this.inputFocus = 'usingStartedDate'
                this.messageValidate = this.contentValidate.UsingStartedDatePurchaseDate
                this.isValidate = false
                return
            }
        },
        /**
         * @description Hiển thị cảnh báo khi người dùng muốn hủy thao tác
         * @author Bùi Huy Tuyền (11-07-2023)
         */
        showToast() {
            if (this.isUpdate && (this.action === 'update' || this.action === 'replicate')) {
                this.isShowToastCancelEdit = true
            } else {
                this.isShowToastCancel = true
            }
        },
        /**
         * @description Đóng cảnh báo khi người dùng muốn hủy thao tác và tiếp tục thao tác trên form
         * @author Bùi Huy Tuyền (11-07-2023)
         */
        closeToastValidate() {
            this.$refs[this.inputFocus].focus()
            this.isShowToastValidateData = false
            this.isShowToastValidate = false
            this.messageValidate = ''
            this.messageValidateData = ''
            this.errorMessage = ''
        },
        /**
         * @description Submit form
         * @author Bùi Huy Tuyền (11-07-2023)
         */
        submit() {
            this.validate()
            if (this.isValidate) {
                if (!this.isShowToastValidateData && !this.isShowToastValidate) {
                    let newFixedAsset = {
                        FixedAssetCode: this.FixedAssetCode,
                        FixedAssetName: this.FixedAssetName,
                        FixedAssetCategoryId: this.FixedAssetCategoryId,
                        FixedAssetCategoryCode: this.FixedAssetCategoryCode,
                        FixedAssetCategoryName: this.FixedAssetCategoryName,
                        DepartmentId: this.DepartmentId,
                        DepartmentCode: this.DepartmentCode,
                        DepartmentName: this.DepartmentName,
                        DepreciationRate: Number(this.DepreciationRate.replace(',', '.')),
                        Quantity: Number(this.Quantity.replace(/\./g, '')),
                        Cost: Number(this.Cost.replace(/\./g, '')),
                        LifeTime: Number(this.LifeTime.replace(/\./g, '')),
                        PurchaseDate: new Date(this.PurchaseDate).toISOString(),
                        UsingStartedDate: new Date(this.UsingStartedDate).toISOString(),
                        TrackedYear: Number(this.TrackedYear)
                    }
                    if (this.action === 'update') {
                        this.updateFixedAsset(this.fixedAsset.FixedAssetId, newFixedAsset)
                    } else {
                        this.createFixedAsset(newFixedAsset)
                    }
                }
            }
        },
        /**
         * @description Submit form khi nhấn Enter
         * @param {*} event
         * @author Bùi Huy Tuyền (11-07-2023)
         */
        submitWithEnter(event) {
            if (event.key === 'Enter') {
                this.submit()
            }
        },
        /**
         * @description Chuyển đổi ngày tháng năm thành chuỗi ngày tháng năm
         * @param {*} date Ngày cần chuyển đổi
         * @author Bùi Huy Tuyền (11-07-2023)
         */
        convertDate(date) {
            date = new Date(date)
            const day = date.getDate()
            const month = date.getMonth() + 1
            const year = date.getFullYear()
            if (day <= 9 && month <= 9) return `${year}-0${month}-0${day}`
            if (day <= 9) return `${year}-${month}-0${day}`
            if (month <= 9) return `${year}-0${month}-${day}`
            return `${year}-${month}-${day}`
        }
    },
    computed: {
        /**
         * @description Lấy danh sách mã phòng ban
         * @author Bùi Huy Tuyền (27-07-2023)
         */
        listDepartmentCode() {
            return this.listDepartment.map((item) => item.DepartmentCode)
        },
        /**
         * @description Lấy danh sách mã loại tài sản
         * @author Bùi Huy Tuyền (27-07-2023)
         */
        listFixedAssetCategoryCode() {
            return this.listFixedAssetCategory.map((item) => item.FixedAssetCategoryCode)
        },
        /**
         * @description Lấy nội dung label trên form nhập liệu
         * @author Bùi Huy Tuyền (27-07-2023)
         */
        labelForm() {
            return this.$_MISAResource.VN.labelForm
        },
        /**
         * @description Lấy nội dung placeholder trên form nhập liệu
         * @author Bùi Huy Tuyền (27-07-2023)
         */
        placeholderForm() {
            return this.$_MISAResource.VN.placeholderForm
        },
        /**
         * @description Lấy nội dung cảnh bảo validate dữ liệu trên form
         * @author Bùi Huy Tuyền (27-07-2023)
         */
        contentValidate() {
            return this.$_MISAResource.VN.validate
        },
        /**
         * @description Kiểm tra xem có phải thao dữ liệu trên form đã được thay đổi hay chưa
         * @author Bùi Huy Tuyền (27-07-2023)
         */
        isUpdate() {
            let t = this.fixedAsset.FixedAssetName !== this.FixedAssetName.trim()
            t = t || this.fixedAsset.DepartmentCode !== this.DepartmentCode.trim()
            t = t || this.fixedAsset.FixedAssetCategoryCode !== this.FixedAssetCategoryCode.trim()
            t = t || this.fixedAsset.Quantity != Number(this.Quantity)
            t = t || this.fixedAsset.Cost != this.Cost.replace(/\./g, '')
            t = t || this.fixedAsset.LifeTime != this.LifeTime.replace(/\./g, '')
            t = t || this.convertDate(this.fixedAsset.PurchaseDate) !== this.PurchaseDate
            t = t || this.convertDate(this.fixedAsset.UsingStartedDate) !== this.UsingStartedDate

            if (this.action === 'update') {
                t = t || this.fixedAsset.FixedAssetCode !== this.FixedAssetCode.trim()
                return t
            } else if (this.action === 'replicate') {
                return t
            } else if (this.action === 'create') {
                return true
            }
            return false
        },
        /**
         * @description Kiểm tra xem có đang load dữ liệu hay không
         * @author Bùi Huy Tuyền (27-07-2023)
         */
        isLoading() {
            return useIsLoading().isLoading
        },
        /**
         * @description Thay đổi trạng thái load dữ liệu
         * @author Bùi Huy Tuyền (27-07-2023)
         */
        setIsLoading() {
            return useIsLoading().setIsLoading
        }
    },
    /**
     * @description Xử lý khi component được mount vào DOM
     * @author Bùi Huy Tuyền (27-07-2023)
     */
    mounted() {
        this.$refs.fixedAssetCode.focus()
        // Add FixedAsset
        if (this.action === 'create') {
            this.getFixedAssetCode()
            this.Quantity = '01'
            this.Cost = '0'
            this.LifeTime = '0'
            this.DepreciationRate = '0'
            this.YearDepreciation = '0'
            const date = new Date()
            const day = date.getDate() > 9 ? date.getDate() : '0' + date.getDate()
            const month =
                date.getMonth() + 1 > 9 ? date.getMonth() + 1 : '0' + (date.getMonth() + 1)
            const year = date.getFullYear()
            this.TrackedYear = year.toString()
            this.PurchaseDate = `${year}-${month}-${day}`
            this.UsingStartedDate = `${year}-${month}-${day}`
        }
        // Update FixedAsset
        else {
            this.FixedAssetName = this.fixedAsset.FixedAssetName
            this.DepartmentCode = this.fixedAsset.DepartmentCode
            this.DepartmentName = this.fixedAsset.DepartmentName
            this.FixedAssetCategoryCode = this.fixedAsset.FixedAssetCategoryCode
            this.FixedAssetCategoryName = this.fixedAsset.FixedAssetCategoryName
            this.Quantity =
                this.fixedAsset.Quantity > 9
                    ? this.fixedAsset.Quantity.toString()
                    : '0' + this.fixedAsset.Quantity.toString()
            this.Cost = this.fixedAsset.Cost.toString()
            this.LifeTime = this.fixedAsset.LifeTime.toString()
            this.TrackedYear = this.fixedAsset.TrackedYear.toString()
            this.PurchaseDate = this.convertDate(this.fixedAsset.PurchaseDate)
            this.UsingStartedDate = this.convertDate(this.fixedAsset.UsingStartedDate)
            if (this.action === 'update') {
                this.FixedAssetCode = this.fixedAsset.FixedAssetCode
            } else {
                this.getFixedAssetCode()
            }
        }
    },
    watch: {
        /**
         * @description Xử lý khi thay đổi mã tài sản
         * @param {*} value
         * @author Bùi Huy Tuyền (27-07-2023)
         */
        DepartmentCode(value) {
            this.DepartmentName =
                this.listDepartment.find((item) => item.DepartmentCode === value)?.DepartmentName ||
                ''
            this.DepartmentId = this.listDepartment.find(
                (item) => item.DepartmentCode === value
            )?.DepartmentId
        },
        /**
         * @description Xử lý khi thay đổi mã loại tài sản
         * @param {*} value
         * @author Bùi Huy Tuyền (27-07-2023)
         */
        FixedAssetCategoryCode(value) {
            this.FixedAssetCategoryName =
                this.listFixedAssetCategory.find((item) => item.FixedAssetCategoryCode === value)
                    ?.FixedAssetCategoryName || ''

            this.FixedAssetCategoryId = this.listFixedAssetCategory.find(
                (item) => item.FixedAssetCategoryCode === value
            )?.FixedAssetCategoryId

            this.LifeTime = this.isUpdate
                ? this.listFixedAssetCategory
                      .find((item) => item.FixedAssetCategoryCode === value)
                      ?.LifeTime.toString() || '0'
                : this.fixedAsset.LifeTime.toString()

            this.DepreciationRate = this.isUpdate
                ? this.listFixedAssetCategory
                      .find((item) => item.FixedAssetCategoryCode === value)
                      ?.DepreciationRate.toString()
                      .replace(/\./g, ',') || '0'
                : this.fixedAsset.DepreciationRate.toString()
        },
        /**
         * @description Xử lý khi thay đổi nguyên giá
         * @param {*} value
         * @author Bùi Huy Tuyền (27-07-2023)
         */
        Cost(value) {
            let numberValue = Number(value.replace(/\./g, ''))
            let numberDepreciationRate = Number(this.DepreciationRate.replace(/,/g, '.'))
            this.YearDepreciation = (numberValue * (numberDepreciationRate / 100))
                .toFixed(0)
                .toString()
                .replace(/\./g, ',')
                .replace(/\./g, '')
                .replace(/\B(?=(\d{3})+(?!\d))/g, '.')
        },
        /**
         * @description Xử lý khi thay đổi thời gian sử dụng
         * @param {*} value
         * @author Bùi Huy Tuyền (27-07-2023)
         */
        LifeTime(value) {
            value = value.replace(/\./g, '')
            if (Number(value) === 0) {
                this.DepreciationRate = '0'
            } else {
                this.DepreciationRate = ((1 / Number(value)) * 100)
                    .toFixed(2)
                    .toString()
                    .replace(/\./g, ',')
            }
        },
        /**
         * @description Xử lý khi thay đổi tỷ lệ hao mòn
         * @param {*} value
         * @author Bùi Huy Tuyền (27-07-2023)
         */
        DepreciationRate(value) {
            if (value[value.length - 1] == ',') {
                this.DepreciationRate = value.slice(0, value.length - 1)
            }

            value = value.replace(/,/g, '.')
            let numberDepreciationRate = Number(value)
            this.Cost = this.Cost.replace(/\./g, '')
            let numberCost = Number(this.Cost)
            this.YearDepreciation = (numberCost * (numberDepreciationRate / 100))
                .toFixed(0)
                .toString()
                .replace(/\./g, ',')
                .replace(/\./g, '')
                .replace(/\B(?=(\d{3})+(?!\d))/g, '.')

            this.Cost = this.Cost.replace(/\./g, '').replace(/\B(?=(\d{3})+(?!\d))/g, '.')
        }
    }
}
</script>
