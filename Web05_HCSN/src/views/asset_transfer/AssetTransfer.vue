<template>
    <!-- View Điều chuyển tài sản -->
    <section class="asset-transfer flex flex-col w-100 h-100">
        <!-- Header -->
        <header class="asset-transfer__header flex item-center justify-between h-44 px-10">
            <!-- Header Trái -->
            <section class="asset-transfer__header-left flex item-center">
                <section class="flex item-center col-gap-10" v-if="listRowIdSelected.length == 0">
                    <h1 class="asset-transfer__header-left__title">
                        {{ title }}
                    </h1>
                    <MISAButton
                        type-button="icon"
                        disabled
                        circle
                        height="30px"
                        width="30px"
                        @click="dataSync"
                        tooltip
                        tooltip-content="Đồng bộ"
                        tooltip-type="right"
                    >
                        <template #icon>
                            <section class="icon sync" :class="{ 'data-sync': sync }"></section>
                        </template>
                    </MISAButton>
                </section>
                <MISASelectInfo
                    :selected-total="listRowIdSelected.length"
                    v-else
                    @unselected="listRowIdSelected = []"
                    @delete="deleteManyTransferAsset"
                />
            </section>

            <!-- Header Phải -->
            <section class="asset-transfer__header-right flex item-center col-gap-10">
                <!-- Thêm chứng từ -->
                <MISAButton
                    :title="button_title"
                    disabled
                    padding="0 10px 0 0"
                    @click="addTransferAsset"
                >
                    <template #icon-left>
                        <section class="icon add-document"></section>
                    </template>
                </MISAButton>

                <!-- Phản hồi -->
                <MISAButton type-button="icon" disabled circle height="30px" width="30px">
                    <template #icon>
                        <section class="icon feedback"></section>
                    </template>
                </MISAButton>

                <!-- Trợ giúp -->
                <MISAButton type-button="icon" disabled circle height="30px" width="30px">
                    <template #icon>
                        <section class="icon help"></section>
                    </template>
                </MISAButton>
            </section>
        </header>

        <!-- Body -->
        <main class="asset-transfer__body w-100 flex flex-col">
            <!-- Bảng chính -->
            <section
                class="main-table w-100"
                :class="`${isResize ? '' : 'animation'}`"
                ref="mainTable"
                :style="`height: ${mainTableHeight}px;`"
            >
                <MISATable
                    border
                    id="TransferAssetId"
                    :data="transferAssetPaging.TransferAssets"
                    :total="transferAssetPaging.TransferAssetTotal"
                    :loading="mainTableLoading"
                    @click-row="clickRow"
                    @dbclick-row="dbclickRow"
                    v-model:rowIdFocus="transferAssetDetailFilter.TransferAssetId"
                    v-model:pageLimit="transferAssetFilter.PageLimit"
                    v-model:pageNumber="transferAssetFilter.PageNumber"
                    v-model:listRowIdSelected="listRowIdSelected"
                >
                    <MISATableColumn :type="TableColumnType.SELECTION" />
                    <MISATableColumn :type="TableColumnType.INDEX" />
                    <MISATableColumn
                        :label="transfer_asset_code"
                        width="150px"
                        column-key="TransferAssetCode"
                        text-align="left"
                    />
                    <MISATableColumn
                        :label="transaction_date"
                        width="200px"
                        column-key="TransactionDate"
                        has-format-date
                    />
                    <MISATableColumn
                        :label="transfer_date"
                        width="200px"
                        column-key="TransferDate"
                        has-format-date
                    />
                    <MISATableColumn
                        :label="cost"
                        width="150px"
                        text-align="right"
                        column-key="Cost"
                        has-format-number
                        has-total
                    />
                    <MISATableColumn
                        :label="remainder_cost"
                        width="150px"
                        text-align="right"
                        column-key="RemainderCost"
                        has-format-number
                        has-total
                    />
                    <MISATableColumn
                        :label="note"
                        width="200px"
                        text-align="left"
                        column-key="Note"
                    />
                    <MISATableColumn
                        :label="functional"
                        width="150px"
                        :type="TableColumnType.EXPAND"
                        scroll
                    >
                        <template
                            #[item]="{ checkTooltip }"
                            v-for="item in transferAssetIds"
                            :key="item"
                        >
                            <MISAButton
                                type-button="icon"
                                tooltip
                                tooltip-content="Chỉnh sửa"
                                :tooltip-type="checkTooltip"
                                circle
                                width="30px"
                                height="30px"
                                @click="updateTransferAsset(item)"
                            >
                                <template #icon>
                                    <section class="icon edit"></section>
                                </template>
                            </MISAButton>
                            <MISAButton
                                type-button="icon"
                                tooltip
                                tooltip-content="Xóa"
                                :tooltip-type="checkTooltip"
                                circle
                                width="30px"
                                height="30px"
                                @click="deleteTransferAsset(item)"
                            >
                                <template #icon>
                                    <section class="icon delete"></section>
                                </template>
                            </MISAButton>
                        </template>
                    </MISATableColumn>
                </MISATable>
            </section>

            <section
                ref="resize"
                style="cursor: row-resize; width: 100%; height: 2.5px; background-color: #aaacae"
            ></section>

            <!-- Caption -->
            <section class="detail-table__caption h-40 flex item-center justify-between px-12">
                <MISAButton
                    :title="caption"
                    height="28px"
                    padding="0 10px"
                    @click="
                        mainTableHeight === TABLE_HEIGHT_MAX
                            ? (mainTableHeight = TABLE_HEIGHT_DEFAULT)
                            : null
                    "
                />
                <MISAButton
                    type-button="icon"
                    width="20px"
                    height="20px"
                    circle
                    :border="false"
                    @click="
                        mainTableHeight === TABLE_HEIGHT_MAX
                            ? (mainTableHeight = TABLE_HEIGHT_DEFAULT)
                            : (mainTableHeight = TABLE_HEIGHT_MAX)
                    "
                    tooltip
                    :tooltip-content="mainTableHeight === TABLE_HEIGHT_MAX ? expand : collapse"
                    :tooltip-type="TooltipType.LEFT"
                >
                    <template #icon>
                        <section
                            class="icon chevron-down"
                            :class="`${mainTableHeight === TABLE_HEIGHT_MAX ? 'rotate' : ''}`"
                        ></section>
                    </template>
                </MISAButton>
            </section>

            <!-- Bảng chi tiết -->
            <section
                class="detail-table w-100"
                ref="detailTable"
                :class="`${isResize ? '' : 'animation'}`"
                :style="`height: calc(100% - ${mainTableHeight}px - 42.5px);`"
                v-show="mainTable?.offsetHeight! <= TABLE_HEIGHT_MAX"
            >
                <!-- Bảng chi tiết -->
                <MISATable
                    border
                    :has-footer="false"
                    :loading="detailTableLoading"
                    ref="detailTable"
                    id="TransferAssetDetailId"
                    :data="transferAssetDetailPaging.TransferAssetDetails"
                    :total="transferAssetDetailPaging.TransferAssetDetailTotal"
                    v-model:pageLimit="transferAssetDetailFilter.PageLimit"
                    v-model:pageNumber="transferAssetDetailFilter.PageNumber"
                >
                    <MISATableColumn :type="TableColumnType.INDEX" />
                    <MISATableColumn
                        :label="fixed_asset_code"
                        column-key="FixedAssetCode"
                        width="150px"
                        text-align="left"
                    />
                    <MISATableColumn
                        :label="fixed_asset_name"
                        width="250px"
                        text-align="left"
                        column-key="FixedAssetName"
                    />
                    <MISATableColumn
                        :label="cost"
                        width="200px"
                        text-align="right"
                        column-key="Cost"
                        has-total
                        has-format-number
                    />
                    <MISATableColumn
                        :label="remainder_cost"
                        width="200px"
                        text-align="right"
                        column-key="RemainderCost"
                        has-total
                        has-format-number
                    />
                    <MISATableColumn
                        :label="department_name"
                        width="250px"
                        text-align="left"
                        column-key="DepartmentName"
                    />
                    <MISATableColumn
                        :label="transfer_department_name"
                        width="250px"
                        text-align="left"
                        column-key="TransferDepartmentName"
                    />
                    <MISATableColumn
                        :label="reason"
                        scroll
                        width="250px"
                        text-align="left"
                        column-key="Reason"
                    />
                </MISATable>
            </section>
        </main>

        <!-- Popup Detail -->

        <Transition name="fade">
            <PopupDetail
                v-if="isShowPopupDetail"
                @close="isShowPopupDetail = false"
                @submit="popupDetailSubmit"
                :popup-detail-mode="popupDetailMode"
                :transfer-asset-update="transferAssetUpdate"
            />
        </Transition>
    </section>

    <!-- Toast message khi xóa -->
    <MISAToastMessage v-model="showWarningMessage" @click-main="showWarningMessage = false">
        <TransferAssetInfo
            :message="warningMessage"
            :more-info="moreInfoWaringMessage"
            v-if="!!moreInfoWaringMessage"
        />
        <p class="pr-20"></p>
    </MISAToastMessage>
</template>

<script setup lang="ts">
import { ref, onMounted, watch, computed } from 'vue'
import { useResource } from '@/hook'
import PopupDetail from './PopupDetail.vue'
import { transferAssetAPI, transferAssetDetailAPI } from '@/api'
import type {
    TransferAssetFilter,
    TransferAsset,
    TransferAssetDetailFilter,
    PopupDetailMode,
    TransferAssetPaging,
    TransferAssetDetailPaging,
    MyResponse
} from '@/types'
import { PopupDetailMode as PopupDetailModeEnum, TableColumnType, TooltipType } from '@/enum'
import { isAxiosError } from 'axios'

const TABLE_HEIGHT_MIN = 118
const TABLE_HEIGHT_MAX = 595.1
const TABLE_HEIGHT_DEFAULT = 350

const isShowPopupDetail = ref<boolean>(false)

const listRowIdSelected = ref<string[]>([])

const isResize = ref<boolean>(false)

const mainTableHeight = ref<number>(TABLE_HEIGHT_DEFAULT)

const transferAssetFilter = ref<TransferAssetFilter>({ PageLimit: 10, PageNumber: 1 })

const transferAssetPaging = ref<TransferAssetPaging>({
    TransferAssets: [],
    TransferAssetTotal: 0
})

const transferAssetUpdate = ref<TransferAsset>()

const transferAssetDetailFilter = ref<TransferAssetDetailFilter>({
    PageLimit: 10,
    PageNumber: 1,
    TransferAssetId: ''
})

const transferAssetDetailPaging = ref<TransferAssetDetailPaging>({
    TransferAssetDetails: [],
    TransferAssetDetailTotal: 0
})

const sync = ref<boolean>(false)

const mainTableLoading = ref<boolean>(false)

const detailTableLoading = ref<boolean>(false)

const popupDetailMode = ref<PopupDetailMode>(PopupDetailModeEnum.CREATE)

const warningMessage = ref<string>('')
const showWarningMessage = ref<boolean>(false)
const moreInfoWaringMessage = ref<string[]>([])
// Computed
const transferAssetIds = computed(() => {
    return transferAssetPaging.value.TransferAssets.map((transferAsset) => {
        return transferAsset.TransferAssetId
    })
})

// Methods
const getTransferAssetPaging = async () => {
    try {
        mainTableLoading.value = true
        const response = await transferAssetAPI.getTransferAssetPaging(transferAssetFilter.value)
        if (response.data.TransferAssets.length > 0) {
            transferAssetPaging.value = response.data
            transferAssetDetailFilter.value.TransferAssetId =
                transferAssetPaging.value.TransferAssets[0].TransferAssetId
            await getTransferAssetDetailPaging()
        }

        const timmer = setTimeout(() => {
            mainTableLoading.value = false
            clearTimeout(timmer)
        }, 500)
    } catch (error) {
        console.log(error)
    }
}

const deleteTransferAsset = async (transferAssetId: string) => {
    try {
        warningMessage.value = String(3).padStart(2, '0')

        await transferAssetAPI.deleteTransferAsset(transferAssetId)
        transferAssetPaging.value.TransferAssets = transferAssetPaging.value.TransferAssets.filter(
            (transferAsset) => transferAsset.TransferAssetId !== transferAssetId
        )
    } catch (error) {
        if (isAxiosError<MyResponse>(error)) {
            showWarningMessage.value = true
            warningMessage.value = error.response?.data?.UserMessage ?? ''
            moreInfoWaringMessage.value = error.response?.data?.MoreInfo ?? []
        }
    }
}

const deleteManyTransferAsset = async () => {
    try {
        await transferAssetAPI.deleteManyTransferAsset(listRowIdSelected.value)
        transferAssetPaging.value.TransferAssets = transferAssetPaging.value.TransferAssets.filter(
            (transferAsset) => !listRowIdSelected.value.includes(transferAsset.TransferAssetId)
        )
        listRowIdSelected.value = []
    } catch (error) {
        if (isAxiosError<MyResponse>(error)) {
            showWarningMessage.value = true
            warningMessage.value = error.response?.data?.UserMessage ?? ''
            moreInfoWaringMessage.value = error.response?.data?.MoreInfo ?? []
        }
    }
}

const getTransferAssetDetailPaging = async () => {
    detailTableLoading.value = true
    const response = await transferAssetDetailAPI.getTransferAssetDetailPaging(
        transferAssetDetailFilter.value
    )

    transferAssetDetailPaging.value = response.data
    const timmer = setTimeout(() => {
        detailTableLoading.value = false
        clearTimeout(timmer)
    }, 500)
}

const addTransferAsset = () => {
    popupDetailMode.value = PopupDetailModeEnum.CREATE
    isShowPopupDetail.value = true
}

const dataSync = async () => {
    sync.value = true
    await getTransferAssetPaging()
    const timmer = setTimeout(() => {
        sync.value = false
        clearTimeout(timmer)
    }, 500)
}

const popupDetailSubmit = () => {
    transferAssetFilter.value.PageNumber = 1
}

const updateTransferAsset = (transferAssetId: string) => {
    transferAssetUpdate.value = transferAssetPaging.value.TransferAssets.find(
        (transferAsset) => transferAsset.TransferAssetId === transferAssetId
    )
    popupDetailMode.value = PopupDetailModeEnum.UPDATE
    isShowPopupDetail.value = true
}

const clickRow = (transferAssetId: string) => {
    transferAssetDetailFilter.value.TransferAssetId = transferAssetId
}

const dbclickRow = (transferAssetId: string) => {
    updateTransferAsset(transferAssetId)
}

// Watch
watch(
    transferAssetFilter.value,
    () => {
        getTransferAssetPaging()
    },
    {
        immediate: true
    }
)

watch(transferAssetDetailFilter.value, () => {
    getTransferAssetDetailPaging()
})

// Ref DOM
const mainTable = ref<HTMLElement | null>(null)

const detailTable = ref<HTMLElement | null>(null)

const resize = ref<HTMLElement | null>(null)

onMounted(async () => {
    const resizing = (event: MouseEvent) => {
        isResize.value = true
        if (
            mainTableHeight.value >= TABLE_HEIGHT_MIN &&
            mainTableHeight.value <= TABLE_HEIGHT_MAX
        ) {
            mainTableHeight.value = event.clientY
            mainTableHeight.value = event.clientY - mainTable.value!.offsetTop
        }
    }

    const stopResizing = () => {
        isResize.value = false
        if (mainTableHeight.value <= TABLE_HEIGHT_MIN) {
            mainTableHeight.value = TABLE_HEIGHT_MIN
        }
        if (mainTableHeight.value >= TABLE_HEIGHT_MAX) {
            mainTableHeight.value = TABLE_HEIGHT_MAX
        }
        document.removeEventListener('mousemove', resizing, false)
        document.removeEventListener('mouseup', stopResizing, false)
    }

    const startResizing = () => {
        isResize.value = true
        document.addEventListener('mousemove', resizing, false)
        document.addEventListener('mouseup', stopResizing, false)
    }
    resize.value?.addEventListener('mousedown', startResizing, false)
})

const {
    asset_transfer: {
        caption,
        cost,
        transfer_asset_code,
        transaction_date,
        fixed_asset_code,
        fixed_asset_name,
        functional,
        header: { button_title, title },
        note,
        reason,
        remainder_cost,
        transfer_date,
        transfer_department_name,
        department_name,
        collapse,
        expand
    }
} = useResource()
</script>

<style scoped lang="scss">
@import './AssetTransfer.scss';
</style>
