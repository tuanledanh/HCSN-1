<template>
    <!-- Filter -->
    <header class="layout__header w-100 center-y justify-between">
        <!-- Left -->
        <section class="layout__header__left center-y col-gap-11">
            <m-input
                placeholder="Tìm kiếm tài sản"
                placeholderItalics
                width="180px"
                v-model="searchEstate"
            >
                <template #iconLeft>
                    <section class="icon search"></section>
                </template>
            </m-input>

            <m-combobox
                placeholder="Loại tài sản"
                width="220px"
                classInput="input-filter"
                :listSelect="listFilterKindEstate"
                v-model="filterKindEstate"
            >
                <template #iconLeft>
                    <section class="icon filter"></section>
                </template>
                <template #iconRight>
                    <section class="icon down"></section>
                </template>
            </m-combobox>

            <m-combobox
                placeholder="Bộ phận sử dụng"
                width="220px"
                classInput="input-filter"
                :listSelect="listFilterNamePart"
                v-model="filterNamePart"
            >
                <template #iconLeft>
                    <section class="icon filter"></section>
                </template>
                <template #iconRight>
                    <section class="icon down"></section>
                </template>
            </m-combobox>
        </section>
        <!--  -->

        <!-- Right -->
        <section class="layout__header__right center-y col-gap-11">
            <!-- Button Add -->
            <m-button @clickButton="showFormAdd">
                <template #icon>
                    <section class="icon add"></section>
                </template>
                Thêm tài sản
            </m-button>
            <!--  -->

            <!-- Button export -->
            <m-button typeButton="icon">
                <section class="icon store"></section>
                <m-tooltip content="Export" />
            </m-button>
            <!--     -->

            <!-- Button Delete -->
            <m-button typeButton="icon" @clickButton="showWarningDelete">
                <section class="icon delete"></section>
                <m-tooltip content="Xóa" />
            </m-button>
            <!--  -->
        </section>
        <!--  -->
    </header>
    <!--  -->

    <!-- Table -->
    <section class="layout__table flex-column">
        <!-- Header -->
        <header class="table__header h-40 grid center-y">
            <section class="table__checkbox center-y pl-16">
                <input
                    type="checkbox"
                    class="t-checkbox"
                    :checked="chooseAll"
                    @change="changeChooseAll"
                />
            </section>
            <section class="table__stt">STT</section>
            <section class="table__code">Mã tài sản</section>
            <section class="table__name">Tên tài sản</section>
            <section class="table__kind">Loại tài sản</section>
            <section class="table__part">Bộ phận sử dụng</section>
            <section class="table__count">Số lượng</section>
            <section class="table__price">Nguyên giá</section>
            <section class="table__cumulative relative">
                HM/KH lũy kế
                <m-tooltip content="Hao mòn/Khấu hao lũy kế" type="top" />
            </section>
            <section class="table__remaining">Giá trị còn lại</section>
            <section class="table__option center">Chức năng</section>
        </header>

        <!-- Body -->
        <main
            class="table__body"
            @keydown="
                (event) => {
                    console.log(event.key)
                }
            "
        >
            <section v-if="isLoading">
                <section
                    v-if="listEstateFilter.length === 0"
                    class="w-100 h-100 center fw-700 font-size-14 mt-200"
                >
                    Không có dữ liệu
                </section>
                <EstateRow
                    v-for="(itemEstate, index) in listEstateFilter"
                    :itemEstate="itemEstate"
                    :key="itemEstate.id"
                    @changeChecked="checkedCheckbox"
                    :check-all="chooseAll"
                    :list-estate-choose="listEstateChoose"
                    :index="index"
                    @editEstate="editEstate"
                    @showToastSuccess="toastSuccess"
                />
            </section>

            <LoadingSkeleton v-else />
        </main>

        <!-- Footer -->
        <footer class="table__footer h-40 grid">
            <section class="table__footer_left center-y">
                <section class="footer__left__info">Tổng số: <span>200</span> bản ghi</section>
                <section class="footer__left__number-row center-y br-4">
                    <span>20</span>
                    <section class="wrapper-icon">
                        <section class="icon footer__left__icon-down"></section>
                    </section>
                </section>
                <section class="footer__left__number-page center-y">
                    <section class="wrapper-icon">
                        <section class="icon footer__left__icon-prev"></section>
                    </section>

                    <section class="number__page center-y">
                        <section class="wrapper-icon number__page--active">1</section>
                        <section class="wrapper-icon">2</section>
                        <section class="wrapper-icon">...</section>
                        <section class="wrapper-icon">10</section>
                    </section>

                    <section class="wrapper-icon">
                        <section class="icon footer__left__icon-next"></section>
                    </section>
                </section>
            </section>
            <section class="table__footer_right grid">
                <section class="total__count pl-20 fw-700 center-y">{{ sumCountEstate }}</section>
                <section class="total__price fw-700 center-y">{{ sumOriginalPrice }}</section>
                <section class="total__cumulative fw-700 center-y">{{ sumCumulative }}</section>
                <section class="total__remaining fw-700 center-y">{{ sumRemaining }}</section>
                <section class="table__footer_right--visibility"></section>
            </section>
        </footer>
        <!--  -->

        <!-- Form Add Estate -->
        <section class="blur" v-if="isShowFormAdd">
            <section class="assets__edit flex-column px-16 br-4 bg-white pt-16">
                <m-popup
                    title="Thêm tài sản"
                    @closePopup="hiddenFormAdd"
                    @showToastSuccess="toastSuccess"
                    :lengthListEstate="listEstate.length"
                    @addEstate="addEstate"
                />
            </section>
        </section>
        <!--  -->

        <!-- Toast Warning Delete -->
        <section class="blur center" v-if="showToast">
            <m-toast
                typeToast="warning"
                :content="contentDelete"
                :messageRight="messageRight"
                :messageLeft="messageLeft"
            >
                <m-button typeButton="outline" v-if="isDelete" @clickButton="cancelDelete"
                    >Không</m-button
                >
                <m-button @clickButton="deleteEstateChoose">{{ labelButtonDelete }}</m-button>
            </m-toast>
        </section>
        <!--  -->

        <!-- Toast Success -->
        <section class="success__message" v-if="isShowToastSuccess">
            <m-toast typeToast="success" :content="successMessage" />
        </section>
        <!--  -->
    </section>
    <!--  -->
</template>

<script>
import EstateRow from './EstateRow.vue'
import LoadingSkeleton from './LoadingSkeleton.vue'

export default {
    name: 'EstateTable',
    /**
     * Author: Bùi Huy Tuyền (11/07/2023)
     * Khai báo các component sử dụng
     */
    components: {
        EstateRow,
        LoadingSkeleton
    },
    /**
     * Author: Bùi Huy Tuyền (11/07/2023)
     * Khai báo các data sử dụng
     */
    data() {
        return {
            chooseAll: false,
            isLoading: false,
            listFilterKindEstate: ['Tài sản cố định', 'Tài sản di động', 'Tài sản phân tán'],
            listFilterNamePart: ['Bộ phận 01', 'Bộ phận 02', 'Bộ phận 03'],
            listEstate: [
                {
                    id: '001',
                    codeEstate: 'TS00001',
                    nameEstate: 'Máy tính',
                    codePart: 'BP01',
                    namePart: 'Bộ phận 01',
                    codeKindEstate: 'TSCĐ',
                    nameKindEstate: 'Tài sản cố định',
                    countEstate: '10',
                    originalPrice: '20.000.000',
                    yearUse: '20',
                    ratioAtrophy: '6.67',
                    atrophyYear: '',
                    yearFollow: '2021',
                    dayBuy: '2020-10-20',
                    dayStartUse: '2020-10-20',
                    cumulative: '1.000.000',
                    remaining: '19.000.000',
                    isDocument: true
                },
                {
                    id: '002',
                    codeEstate: 'TS00002',
                    nameEstate: 'Xe đạp',
                    codePart: 'BP02',
                    namePart: 'Bộ phận 02',
                    codeKindEstate: 'TSDĐ',
                    nameKindEstate: 'Tài sản di động',
                    countEstate: '10',
                    originalPrice: '20.000.000',
                    yearUse: '10',
                    ratioAtrophy: '2.25',
                    atrophyYear: '',
                    yearFollow: '2022',
                    dayBuy: '2021-12-09',
                    dayStartUse: '2021-12-09',
                    cumulative: '1.000.000',
                    remaining: '19.000.000',
                    isDocument: true
                },
                {
                    id: '003',
                    codeEstate: 'TS00003',
                    nameEstate: 'Ô tô',
                    codePart: 'BP03',
                    namePart: 'Bộ phận 03',
                    codeKindEstate: 'TSPT',
                    nameKindEstate: 'Tài sản phân tán',
                    countEstate: '10',
                    originalPrice: '20.000.000',
                    yearUse: '5',
                    ratioAtrophy: '4.5',
                    atrophyYear: '',
                    yearFollow: '2023',
                    dayBuy: '2023-05-05',
                    dayStartUse: '2023-05-05',
                    cumulative: '1.000.000',
                    remaining: '19.000.000',
                    isDocument: false
                },
                {
                    id: '004',
                    codeEstate: 'TS00004',
                    nameEstate: 'Ô tô',
                    codePart: 'BP03',
                    namePart: 'Bộ phận 02',
                    codeKindEstate: 'TSPT',
                    nameKindEstate: 'Tài sản phân tán',
                    countEstate: '10',
                    originalPrice: '20.000.000',
                    yearUse: '5',
                    ratioAtrophy: '4.5',
                    atrophyYear: '',
                    yearFollow: '2023',
                    dayBuy: '2023-05-05',
                    dayStartUse: '2023-05-05',
                    cumulative: '1.000.000',
                    remaining: '19.000.000',
                    isDocument: false
                }
            ],
            listEstateChoose: [],
            listEstateChooseDocument: [],
            showToast: false,
            isDelete: false,
            isShowFormAdd: false,
            isShowToastSuccess: false,
            saveTimer: Number,
            successMessage: '',
            contentDelete: '',
            labelButtonDelete: '',
            messageLeft: '',
            messageRight: '',
            filterKindEstate: '',
            filterNamePart: '',
            listEstateFilter: [],
            searchEstate: ''
        }
    },
    /**
     * Author: Bùi Huy Tuyền (11/07/2023)
     * Khai báo các method sử dụng
     */
    methods: {
        /**
         * Author: Bùi Huy Tuyền (11/07/2023)
         * @param {id, checked, isDocument}
         * Xử lý logic khi check checkbox
         */
        checkedCheckbox(id, checked, isDocument) {
            if (checked) {
                if (!this.listEstateChoose.includes(id)) {
                    this.listEstateChoose.push(id)
                }
                if (isDocument) {
                    this.listEstateChooseDocument.push(id)
                }
                if (this.listEstateChoose.length === this.listEstate.length) {
                    this.chooseAll = true
                }
            } else {
                if (this.listEstateChoose.includes(id)) {
                    this.listEstateChoose.splice(this.listEstateChoose.indexOf(id), 1)
                }

                if (this.listEstateChooseDocument.includes(id)) {
                    this.listEstateChooseDocument.splice(
                        this.listEstateChooseDocument.indexOf(id),
                        1
                    )
                }
                if (this.chooseAll) {
                    this.chooseAll = false
                }
            }
            if (this.listEstateChooseDocument.length >= 1) {
                this.isDelete = false
            } else {
                this.isDelete = true
            }
        },
        /**
         * Author: Bùi Huy Tuyền (11/07/2023)
         * @param
         * Xử lý logic khi check all
         */
        changeChooseAll() {
            this.chooseAll = !this.chooseAll
            if (this.chooseAll) {
                this.listEstateChoose = this.listEstate.map((item) => item.id)
                this.listEstateChooseDocument = this.listEstate
                    .filter((item) => item.isDocument)
                    .map((item) => item.id)
            } else {
                this.listEstateChoose = []
                this.listEstateChooseDocument = []
            }
        },
        /**
         * Author: Bùi Huy Tuyền (11/07/2023)
         * @param
         * Hiển thị cảnh báo khi xóa tài sản
         */
        showWarningDelete() {
            this.showToast = true
            if (this.isDelete) {
                if (this.listEstateChoose.length === 1) {
                    this.contentDelete = 'Bạn có muốn xóa tài sản '
                    let codeEstate = this.listEstate.find(
                        (item) => item.id === this.listEstateChoose[0]
                    ).codeEstate

                    let nameEstate = this.listEstate.find(
                        (item) => item.id === this.listEstateChoose[0]
                    ).nameEstate

                    this.messageRight = `${codeEstate} - ${nameEstate}`
                } else {
                    this.contentDelete = this.$_MISAResource.VN.btnDeleteWarning.deleteMany
                    if (this.listEstateChoose.length > 10) {
                        this.messageLeft = `${this.listEstateChoose.length} `
                    } else {
                        this.messageLeft = `0${this.listEstateChoose.length} `
                    }
                }
                this.labelButtonDelete = 'Xóa'
            } else {
                if (this.listEstateChoose.length == 0) {
                    this.contentDelete = 'Bạn chưa chọn tài sản nào'
                }

                if (this.listEstateChooseDocument.length === 1) {
                    if (this.listEstateChoose.length >= 1) {
                        this.contentDelete = this.$_MISAResource.VN.btnDeleteWarning.noDeleteMany
                        this.messageLeft = `01 `
                    } else {
                        this.contentDelete = this.$_MISAResource.VN.btnDeleteWarning.noDeleteOne
                    }
                }

                if (this.listEstateChooseDocument.length >= 2) {
                    if (this.listEstateChooseDocument.length > 10) {
                        this.messageLeft = `${this.listEstateChooseDocument.length} `
                    } else {
                        this.messageLeft = `0${this.listEstateChooseDocument.length} `
                    }
                    this.contentDelete = this.$_MISAResource.VN.btnDeleteWarning.noDeleteMany
                }
                this.labelButtonDelete = 'Đóng'
            }
        },
        /**
         * Author: Bùi Huy Tuyền (11/07/2023)
         * @param
         * Xử lý logic khi xóa tài sản
         */
        deleteEstateChoose() {
            if (this.isDelete) {
                this.listEstateChoose.forEach((item) => {
                    this.listEstate.splice(
                        this.listEstate.findIndex((estate) => estate.id === item),
                        1
                    )
                    this.listEstateFilter = [...this.listEstate]
                })
                this.listEstateChoose = []
                this.listEstateChooseDocument = []

                this.isDelete = false
                this.toastSuccess(true, 'Xóa thành công')
            }
            this.showToast = false
            this.contentDelete = ''
            this.messageLeft = ''
            this.messageRight = ''
        },
        /**
         * Author: Bùi Huy Tuyền (11/07/2023)
         * @param
         * Xử lý logic khi hủy xóa tài sản
         */
        cancelDelete() {
            this.showToast = false
            this.contentDelete = ''
            this.messageLeft = ''
            this.messageRight = ''
        },
        /**
         * Author: Bùi Huy Tuyền (11/07/2023)
         * @param
         * Hiện form để thêm tài sản
         */
        showFormAdd() {
            this.isShowFormAdd = true
        },
        /**
         * Author: Bùi Huy Tuyền (11/07/2023)
         * @param
         * Ẩn form để thêm tài sản
         */
        hiddenFormAdd() {
            this.isShowFormAdd = false
        },
        /**
         * Author: Bùi Huy Tuyền (11/07/2023)
         * @param {value, message}
         * Hiện thông báo khi xử lý logic thành công
         */
        toastSuccess(value, message) {
            this.isShowToastSuccess = value
            this.successMessage = message
            setTimeout(() => {
                this.isShowToastSuccess = false
            }, 3000)
        },
        /**
         * Author: Bùi Huy Tuyền (11/07/2023)
         * @param {estate}
         * Xử lý logic khi thêm tài sản
         */
        addEstate(estate) {
            this.listEstate.push(estate)
            this.listEstateFilter = [...this.listEstate]
        },
        /**
         * Author: Bùi Huy Tuyền (11/07/2023)
         * @param {estate}
         * Xử lý logic khi sửa tài sản
         */
        editEstate(estate) {
            this.listEstate.splice(
                this.listEstate.findIndex((item) => item.id === estate.id),
                1,
                estate
            )
        }
    },
    /**
     * Author: Bùi Huy Tuyền (11/07/2023)
     * Xử lý khi component được mounted vào DOM
     */
    mounted() {
        this.listEstateFilter = [...this.listEstate]
        /**
         * Author: Bùi Huy Tuyền (11/07/2023)
         * Tạo timer để hiện thị loading khi tải dữ liệu
         */
        this.saveTimer = setTimeout(() => {
            this.isLoading = true
        }, 1000)
    },
    /**
     * Author: Bùi Huy Tuyền (11/07/2023)
     * Xử lý khi có sự thay đổi dữ liệu
     */
    watch: {
        /**
         * Author: Bùi Huy Tuyền (11/07/2023)
         * @param {value}}
         * Hiện thị danh sách tài sản khi được lọc theo loại tài sản
         */
        filterKindEstate(value) {
            if (value === '') {
                this.listEstateFilter = [...this.listEstate]
                return
            }
            this.listEstateFilter = this.listEstateFilter.filter(
                (item) => item.nameKindEstate === value
            )
        },
        /**
         * Author: Bùi Huy Tuyền (11/07/2023)
         * @param {value}}
         * Hiện thị danh sách tài sản khi được lọc theo tên bộ phận
         */
        filterNamePart(value) {
            if (value === '') {
                this.listEstateFilter = [...this.listEstate]
                return
            }
            this.listEstateFilter = this.listEstateFilter.filter((item) => item.namePart === value)
        },
        searchEstate(value) {
            this.listEstateFilter = this.listEstate.filter((item) => {
                return (
                    item.nameEstate.toLowerCase().includes(value.toLowerCase()) ||
                    item.codeEstate.toLowerCase().includes(value.toLowerCase())
                )
            })
        }
    },
    /**
     * Author: Bùi Huy Tuyền (11/07/2023)
     * Xử lý khi component được unmounted khỏi DOM
     */
    unmounted() {
        /**
         * Author: Bùi Huy Tuyền (11/07/2023)
         * Xóa timer khi component bị hủy
         */
        clearTimeout(this.saveSideEffect)
    },
    /**
     * Author: Bùi Huy Tuyền (11/07/2023)
     * Xử lý data
     */
    computed: {
        /**
         * Author: Bùi Huy Tuyền (11/07/2023)
         * Tính tổng số lượng tài sản
         */
        sumCountEstate() {
            return this.listEstate.reduce((sum, item) => {
                return sum + Number(item.countEstate)
            }, 0)
        },
        /**
         * Author: Bùi Huy Tuyền (11/07/2023)
         * Tính tổng giá trị tài sản
         */
        sumOriginalPrice() {
            let sum = this.listEstate.reduce((sum, item) => {
                return sum + Number(item.originalPrice.replace(/\./g, ''))
            }, 0)
            return sum.toString().replace(/\B(?=(\d{3})+(?!\d))/g, '.')
        },
        /**
         * Author: Bùi Huy Tuyền (11/07/2023)
         * Tính tổng giá trị hao mòn khấu hao
         */
        sumCumulative() {
            let sum = this.listEstate.reduce((sum, item) => {
                return sum + Number(item.cumulative.replace(/\./g, ''))
            }, 0)
            return sum.toString().replace(/\B(?=(\d{3})+(?!\d))/g, '.')
        },
        /**
         * Author: Bùi Huy Tuyền (11/07/2023)
         * Tính tổng giá trị còn lại
         */
        sumRemaining() {
            let sum = this.listEstate.reduce((sum, item) => {
                return sum + Number(item.remaining.replace(/\./g, ''))
            }, 0)
            return sum.toString().replace(/\B(?=(\d{3})+(?!\d))/g, '.')
        }
    }
}
</script>
