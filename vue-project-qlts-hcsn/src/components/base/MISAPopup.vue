<template>
    <form class="t-popup" ref="form" @keydown="(event) => submitWithEnter(event)">
        <header class="t-popup__header center-y justify-between w-100 h-36">
            <h1 class="t-popup__header--title">{{ title }}</h1>
            <section class="wrapper-icon" @click="showToast">
                <section class="icon t-popup__header--close"></section>
            </section>
        </header>
        <main class="t-popup__header--body edit__body">
            <m-input
                classLabel="edit__code"
                :label="labelForm.codeEstate"
                :placeholder="placeholderForm.codeEstate"
                placeholderItalics
                focus
                v-model="codeEstate"
            />
            <m-input
                classLabel="edit__name"
                :label="labelForm.nameEstate"
                :placeholder="placeholderForm.nameEstate"
                placeholderItalics
                v-model="nameEstate"
            />

            <m-combobox
                classLabel="edit__code-part"
                :label="labelForm.codePart"
                :placeholder="placeholderForm.codePart"
                v-model="codePart"
                :listSelect="listCodePart"
                required
            />

            <m-input
                classLabel="edit__name-part"
                :label="labelForm.namePart"
                readonly
                tabindex="-1"
                v-model="namePart"
            />

            <m-combobox
                classLabel="edit__code-assets"
                :label="labelForm.codeKindEstate"
                :placeholder="placeholderForm.codeKindEstate"
                v-model="codeKindEstate"
                :listSelect="listCodeKindEstate"
                required
            />

            <m-input
                classLabel="edit__name-assets"
                required
                :label="labelForm.nameKindEstate"
                readonly
                tabindex="-1"
                v-model="nameKindEstate"
            />

            <m-input
                classLabel="edit__count"
                :label="labelForm.countEstate"
                v-model="countEstate"
                textRight
                type="number"
            >
                <template #iconRight>
                    <section class="wrapper-icon flex-column row-gap-3">
                        <section class="choose-year-up icon"></section>
                        <section class="choose-year-down icon"></section>
                    </section>
                </template>
            </m-input>

            <m-input
                classLabel="edit__price"
                :label="labelForm.originalPrice"
                v-model="originalPrice"
                type="number"
                textRight
            />
            <m-input
                classLabel="edit__year-number"
                :label="labelForm.yearUse"
                textRight
                type="number"
                v-model="yearUse"
            />

            <m-input
                classLabel="edit__ratio-atrophy"
                :label="labelForm.ratioAtrophy"
                v-model="ratioAtrophy"
                type="percent"
                textRight
            >
                <template #iconRight>
                    <section class="wrapper-icon flex-column row-gap-3">
                        <section class="choose-year-up icon"></section>
                        <section class="choose-year-down icon"></section>
                    </section>
                </template>
            </m-input>

            <m-input
                classLabel="edit_value-atrophy"
                :label="labelForm.atrophyYear"
                v-model="atrophyYear"
                type="number"
                textRight
            />

            <m-input
                classLabel="edit__year-follow"
                :label="labelForm.yearFollow"
                readonly
                tabindex="-1"
                v-model="yearFollow"
                type="number"
                textRight
            />

            <label class="flex-column edit__day-buy row-gap-10">
                <p>{{ labelForm.dayBuy }} <span class="require">*</span></p>
                <section class="relative center-y">
                    <input
                        type="date"
                        class="t-input w-100"
                        placeholder="dd/mm/yyyy"
                        v-model="dayBuy"
                    />
                    <section class="wrapper-icon absolute r-6">
                        <section class="icon calendar"></section>
                    </section>
                </section>
            </label>

            <label class="flex-column edit__day-start-use row-gap-10">
                <p>{{ labelForm.dayStartUse }} <span class="require">*</span></p>
                <section class="relative center-y">
                    <input
                        type="date"
                        class="t-input w-100"
                        placeholder="dd/mm/yyyy"
                        v-model="dayStartUse"
                    />
                    <section class="wrapper-icon absolute r-6">
                        <section class="icon calendar"></section>
                    </section>
                </section>
            </label>
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
            <m-button width="100px" @clickButton="submit">Lưu</m-button>
        </footer>
    </form>

    <section class="blur center" v-if="isShowToastCancel">
        <m-toast typeToast="warning" :content="$_MISAResource.VN.warningCancel.noEdit">
            <m-button typeButton="outline" @clickButton="isShowToastCancel = false">Không</m-button>
            <m-button @clickButton="closePopup">Hủy bỏ</m-button>
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
    <section class="blur center" v-if="isShowToastValidateDate">
        <m-toast
            typeToast="warning"
            :content="contentValidate.message"
            :messageLeft="messageValidate"
        >
            <m-button @clickButton="closeToastValidate">Đóng</m-button>
        </m-toast>
    </section>

    <!-- Validate Nghiệp vụ -->
    <section class="blur center" v-if="isShowToastValidate">
        <m-toast typeToast="warning" :content="messageValidate">
            <m-button @clickButton="closeToastValidate">Đóng</m-button>
        </m-toast>
    </section>
</template>

<script>
import { useYearFollow } from '/src/stores/yearFollow.js'

export default {
    name: 'MISAPopup',
    /**
     * Author: Bùi Huy Tuyền (04/07/2023)
     * @param
     * ĐỊnh nghĩa props cho component
     */
    props: {
        title: {
            type: String,
            default: 'Popup'
        },
        itemEstate: Object,
        lengthListEstate: Number
    },

    data() {
        return {
            // Data from
            codeEstate: '',
            nameEstate: '',
            codePart: '',
            namePart: '',
            codeKindEstate: '',
            nameKindEstate: '',
            countEstate: '',
            originalPrice: '',
            yearUse: '',
            ratioAtrophy: '',
            atrophyYear: '',
            yearFollow: '',
            dayBuy: '',
            dayStartUse: '',
            //
            isShowToastCancel: false,
            isShowToastCancelEdit: false,
            isShowToastValidateDate: false,
            isShowToastValidate: false,
            listPart: [
                {
                    code: 'BP01',
                    name: 'Bộ phận 01'
                },
                {
                    code: 'BP02',
                    name: 'Bộ phận 02'
                },
                {
                    code: 'BP03',
                    name: 'Bộ phận 03'
                }
            ],
            listKindEstate: [
                {
                    code: 'TSCĐ',
                    name: 'Tài sản cố định',
                    yearUse: '20',
                    ratioAtrophy: '6.67'
                },
                {
                    code: 'TSDĐ',
                    name: 'Tài sản di động',
                    yearUse: '10',
                    ratioAtrophy: '2.25'
                },
                {
                    code: 'TSPT',
                    name: 'Tài sản phân tán',
                    yearUse: '5',
                    ratioAtrophy: '4.5'
                }
            ],
            messageValidate: ''
        }
    },
    emits: ['closePopup', 'showToastSuccess', 'addEstate', 'editEstate'],
    methods: {
        /**
         * Author: Bùi Huy Tuyền (04/07/2023)
         * @param
         * Ẩn popup
         */
        closePopup() {
            this.$emit('closePopup')
            this.isShowToastCancel = false
            this.isShowToastCancelEdit = false
        },
        updateValue(value) {
            this.value = value
        },
        validate() {
            // Validate Dữ liệu
            switch ('') {
                case this.codeEstate:
                    this.isShowToastValidateDate = true
                    this.messageValidate = this.labelForm.codeEstate
                    return
                case this.nameEstate:
                    this.isShowToastValidateDate = true
                    this.messageValidate = this.labelForm.nameEstate
                    return
                case this.codePart:
                    this.isShowToastValidateDate = true
                    this.messageValidate = this.labelForm.codePart
                    return
                case this.codeKindEstate:
                    this.isShowToastValidateDate = true
                    this.messageValidate = this.labelForm.codeKindEstate
                    return
                default:
                    break
            }

            // Validate Nghiệp vụ
            // Validate Tỷ lệ hao mòn !== 1 / Số năm sử dụng
            let numberRatioAtrophy = Number(this.ratioAtrophy.replace(',', '.')) / 100
            let numberYearUse = Number(this.yearUse)
            if (numberRatioAtrophy !== 1 / numberYearUse) {
                console.log('object')
                this.isShowToastValidate = true
                this.messageValidate = this.contentValidate.ratioAtrophyYearUse
            }
            // Validate Hao mòn năm phải nhỏ hơn hoặc bằng nguyên giá
            let numberAtrophyYear = Number(this.atrophyYear.replace(/\./g, ''))
            let numberOriginalPrice = Number(this.originalPrice.replace(/\./g, ''))
            if (numberAtrophyYear > numberOriginalPrice) {
                this.isShowToastValidate = true
                this.messageValidate = this.contentValidate.atrophyYearOriginalPrice
            }
        },
        showToast() {
            if (this.isEdit) {
                this.isShowToastCancelEdit = true
            } else {
                this.isShowToastCancel = true
            }
        },
        closeToastValidate() {
            this.isShowToastValidateDate = false
            this.isShowToastValidate = false
        },
        submit() {
            this.validate()
            if (!this.isShowToastValidateDate && !this.isShowToastValidate) {
                if (this.itemEstate) {
                    const estate = {
                        id: this.itemEstate.id,
                        codeEstate: this.codeEstate,
                        nameEstate: this.nameEstate,
                        codePart: this.codePart,
                        namePart: this.namePart,
                        codeKindEstate: this.codeKindEstate,
                        nameKindEstate: this.nameKindEstate,
                        countEstate: this.countEstate,
                        originalPrice: this.originalPrice,
                        yearUse: this.yearUse,
                        ratioAtrophy: this.ratioAtrophy,
                        atrophyYear: this.atrophyYear,
                        yearFollow: this.yearFollow,
                        dayBuy: this.dayBuy,
                        dayStartUse: this.dayStartUse,
                        cumulative: '0',
                        remaining: this.originalPrice,
                        isChungTu: false
                    }
                    this.$emit('editEstate', estate)
                    this.$emit('showToastSuccess', true, this.$_MISAResource.VN.success.edit)
                } else {
                    const estate = {
                        id: this.codeEstate,
                        codeEstate: this.codeEstate,
                        nameEstate: this.nameEstate,
                        codePart: this.codePart,
                        namePart: this.namePart,
                        codeKindEstate: this.codeKindEstate,
                        nameKindEstate: this.nameKindEstate,
                        countEstate: this.countEstate,
                        originalPrice: this.originalPrice,
                        yearUse: this.yearUse,
                        ratioAtrophy: this.ratioAtrophy,
                        atrophyYear: this.atrophyYear,
                        yearFollow: this.yearFollow,
                        dayBuy: this.dayBuy,
                        dayStartUse: this.dayStartUse,
                        cumulative: '0',
                        remaining: this.originalPrice,
                        isChungTu: false
                    }
                    this.$emit('addEstate', estate)
                    this.$emit('showToastSuccess', true, this.$_MISAResource.VN.success.add)
                }
                this.$emit('closePopup')
            }
        },
        submitWithEnter(event) {
            if (event.key === 'Enter') {
                this.submit()
            }
        }
    },
    computed: {
        yearFollowStore() {
            return useYearFollow()
        },
        year() {
            return this.yearFollowStore.yearFollow.toString()
        },
        listCodePart() {
            return this.listPart.map((item) => item.code)
        },
        listNamePart() {
            return this.listPart.map((item) => item.name)
        },
        listCodeKindEstate() {
            return this.listKindEstate.map((item) => item.code)
        },
        listNameKindEstate() {
            return this.listKindEstate.map((item) => item.name)
        },
        labelForm() {
            return this.$_MISAResource.VN.labelForm
        },
        placeholderForm() {
            return this.$_MISAResource.VN.placeholderForm
        },
        contentValidate() {
            return this.$_MISAResource.VN.validate
        },

        isEdit() {
            if (this.itemEstate) {
                let t = this.itemEstate.codeEstate !== this.codeEstate
                t = t || this.itemEstate.nameEstate !== this.nameEstate
                t = t || this.itemEstate.codePart !== this.codePart
                t = t || this.itemEstate.codeKindEstate !== this.codeKindEstate
                t = t || this.itemEstate.countEstate !== this.countEstate
                t = t || this.itemEstate.originalPrice !== this.originalPrice
                t = t || this.itemEstate.yearUse !== this.yearUse
                t = t || this.itemEstate.ratioAtrophy !== this.ratioAtrophy
                t = t || this.itemEstate.yearFollow !== this.yearFollow
                t = t || this.itemEstate.dayBuy !== this.dayBuy
                t = t || this.itemEstate.dayStartUse !== this.dayStartUse
                return t
            }
            return false
        }
    },
    mounted() {
        // Edit Estate
        if (this.itemEstate) {
            this.codeEstate = this.itemEstate.codeEstate
            this.nameEstate = this.itemEstate.nameEstate
            this.codePart = this.itemEstate.codePart
            this.namePart = this.itemEstate.namePart
            this.codeKindEstate = this.itemEstate.codeKindEstate
            this.nameKindEstate = this.itemEstate.nameKindEstate
            this.countEstate = this.itemEstate.countEstate
            this.originalPrice = this.itemEstate.originalPrice
            this.ratioAtrophy = this.itemEstate.ratioAtrophy
            this.ratioAtrophy.replace(/\./g, ',')
            this.yearUse = this.itemEstate.yearUse
            this.yearFollow = this.itemEstate.yearFollow
            this.dayBuy = this.itemEstate.dayBuy
            this.dayStartUse = this.itemEstate.dayStartUse
        }
        // Add Estate
        else {
            if (this.lengthListEstate.toString().length === 1)
                this.codeEstate = 'TS0000' + (this.lengthListEstate + 1).toString()
            if (this.lengthListEstate.toString().length === 2)
                this.codeEstate = 'TS000' + (this.lengthListEstate + 1).toString()
            if (this.lengthListEstate.toString().length === 3)
                this.codeEstate = 'TS00' + (this.lengthListEstate + 1).toString()
            if (this.lengthListEstate.toString().length === 4)
                this.codeEstate = 'TS0' + (this.lengthListEstate + 1).toString()
            if (this.lengthListEstate.toString().length === 5)
                this.codeEstate = 'TS' + (this.lengthListEstate + 1).toString()

            this.countEstate = '01'
            this.originalPrice = '0'
            this.yearUse = '0'
            this.ratioAtrophy = '0'
            this.yearFollow = this.year
            const date = new Date()
            const day = date.getDate()
            const month = date.getMonth() + 1
            const year = date.getFullYear()
            this.dayBuy = `${year}-0${month}-${day}`
            this.dayStartUse = `${year}-0${month}-${day}`
        }
    },
    watch: {
        codePart(value) {
            this.namePart = this.listPart.find((item) => item.code === value)?.name
        },
        codeKindEstate(value) {
            this.nameKindEstate = this.listKindEstate.find((item) => item.code === value)?.name
            this.yearUse = this.listKindEstate.find((item) => item.code === value)?.yearUse
            this.ratioAtrophy = this.listKindEstate.find(
                (item) => item.code === value
            )?.ratioAtrophy
        },
        originalPrice(value) {
            value = value.replace(/\./g, '')
            this.ratioAtrophy = this.ratioAtrophy.replace(/,/g, '.')
            let numberValue = Number(value)
            let numberRatioAtrophy = Number(this.ratioAtrophy)
            this.atrophyYear = (numberValue * (numberRatioAtrophy / 100))
                .toFixed(0)
                .toString()
                .replace(/\./g, ',')
                .replace(/\./g, '')
                .replace(/\B(?=(\d{3})+(?!\d))/g, '.')
            this.ratioAtrophy = this.ratioAtrophy.replace(/\./g, ',')
        },
        ratioAtrophy(value) {
            if (value[value.length - 1] == ',') {
                this.ratioAtrophy = value.slice(0, value.length - 1)
            }

            value = value.replace(/,/g, '.')
            let numberRatioAtrophy = Number(value)
            this.originalPrice = this.originalPrice.replace(/\./g, '')
            let numberOriginalPrice = Number(this.originalPrice)
            this.atrophyYear = (numberOriginalPrice * (numberRatioAtrophy / 100))
                .toFixed(0)
                .toString()
                .replace(/\./g, ',')
                .replace(/\./g, '')
                .replace(/\B(?=(\d{3})+(?!\d))/g, '.')

            this.originalPrice = this.originalPrice
                .replace(/\./g, '')
                .replace(/\B(?=(\d{3})+(?!\d))/g, '.')
        }
    }
}
</script>
