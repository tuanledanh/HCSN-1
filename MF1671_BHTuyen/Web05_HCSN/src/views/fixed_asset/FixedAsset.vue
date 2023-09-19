<template>
    <section class="fixed-asset">
        <header class="fixed-asset__header flex item-center justify-between px-20">
            <section class="fixed-asset__header-left flex item-center col-gap-10">
                <MISAInput
                    :placeholder="fixed_asset_search"
                    placeholder-italic
                    width="200px"
                    v-model="fixedAssetFilter.FixedAssetCodeOrName"
                    @click-icon-left="getFixedAssetPaging"
                >
                    <template #icon-left>
                        <section class="icon search"></section>
                    </template>
                </MISAInput>
                <MISACombobox
                    :placeholder="fixed_asset_category_name"
                    width="200px"
                    :list="fixedAssetCategoryNameList"
                    v-model="fixedAssetFilter.FixedAssetCategoryName"
                    @click-icon-left="getFixedAssetPaging"
                >
                    <template #icon-left>
                        <section class="icon filter"></section>
                    </template>
                </MISACombobox>
                <MISACombobox
                    :placeholder="department_name"
                    width="200px"
                    :list="departmentNameList"
                    v-model="fixedAssetFilter.DepartmentName"
                    @click-icon-left="getFixedAssetPaging"
                >
                    <template #icon-left>
                        <section class="icon filter"></section>
                    </template>
                </MISACombobox>

                <MISAButton
                    :title="data_update"
                    type-button="outline"
                    padding="0 15px 0 0"
                    @click="updateData"
                >
                    <template #icon-left>
                        <section class="icon async" :class="{ updateData: isUpdateData }"></section>
                    </template>
                </MISAButton>

                <MISASelectInfo
                    v-if="listRowIdSelected.length > 0"
                    :selected-total="listRowIdSelected.length"
                    @unselected="listRowIdSelected = []"
                    :button-delete="false"
                />
            </section>

            <section class="fixed-asset__header-right flex item-center col-gap-10">
                <MISAButton
                    :title="fixed_asset_add"
                    padding="0 15px 0 0"
                    @click="(isShowFixedAssetPopup = true), (fixedAssetPopupMode = 'CREATE')"
                >
                    <template #icon-left>
                        <section class="icon add"></section>
                    </template>
                </MISAButton>

                <MISAButton
                    type-button="icon"
                    width="36px"
                    height="36px"
                    tooltip
                    tooltip-content="Xuất Excel"
                >
                    <template #icon>
                        <section class="icon excel"></section>
                    </template>
                </MISAButton>
                <MISAButton
                    type-button="icon"
                    width="36px"
                    height="36px"
                    @click="deleteManyFixedAsset"
                    tooltip
                    tooltip-content="Xóa"
                >
                    <template #icon>
                        <section class="icon delete"></section>
                    </template>
                </MISAButton>
                <MISAButton
                    type-button="icon"
                    width="36px"
                    height="36px"
                    tooltip
                    tooltip-content="Cài đặt"
                >
                    <template #icon>
                        <section class="icon setting"></section>
                    </template>
                </MISAButton>
            </section>
        </header>
        <main class="fixed-asset__main px-20 pb-14">
            <MISATable
                border
                id="FixedAssetId"
                :data="fixedAssetPaging.FixedAssets"
                :total="fixedAssetPaging.FixedAssetTotal"
                :loading="loadingTable"
                v-model:listRowIdSelected="listRowIdSelected"
                v-model:rowIdFocus="rowIdFocus"
                v-model:pageLimit="fixedAssetFilter.PageLimit"
                v-model:pageNumber="fixedAssetFilter.PageNumber"
            >
                <MISATableColumn type="selection" />
                <MISATableColumn type="index" />
                <MISATableColumn
                    :label="fixed_asset_code"
                    width="150px"
                    text-align="left"
                    column-key="FixedAssetCode"
                />
                <MISATableColumn
                    :label="fixed_asset_name"
                    width="250px"
                    text-align="left"
                    column-key="FixedAssetName"
                />
                <MISATableColumn
                    :label="fixed_asset_category_name"
                    width="250px"
                    text-align="left"
                    column-key="FixedAssetCategoryName"
                />
                <MISATableColumn
                    :label="department_name"
                    width="250px"
                    text-align="left"
                    column-key="DepartmentName"
                />
                <MISATableColumn
                    :label="quantity"
                    width="150px"
                    text-align="right"
                    column-key="Quantity"
                    has-format-number
                    has-total
                />
                <MISATableColumn
                    :label="cost"
                    width="200px"
                    text-align="right"
                    column-key="Cost"
                    has-format-number
                    has-total
                />
                <MISATableColumn
                    :label="accumulation_depreciation"
                    width="200px"
                    text-align="right"
                    column-key="AccumulationDepreciation"
                    has-format-number
                    has-total
                    tooltip="Hao mòn khấu hao lũy kế"
                />
                <MISATableColumn
                    :label="remainder_cost"
                    width="200px"
                    text-align="right"
                    column-key="RemainderCost"
                    has-format-number
                    has-total
                />
                <MISATableColumn type="expand" :label="functional" width="150px" scroll>
                    <template
                        #[fixedAssetId]="{ checkTooltip }"
                        v-for="fixedAssetId in fixedAssetPaging.FixedAssets.map(
                            (fixedAsset) => fixedAsset.FixedAssetId
                        )"
                        :key="fixedAssetId"
                    >
                        <MISAButton
                            type-button="icon"
                            circle
                            width="30px"
                            height="30px"
                            @click="
                                (isShowFixedAssetPopup = true),
                                    (fixedAssetPopupMode = 'UPDATE'),
                                    (fixedAssetUpdate = fixedAssetPaging.FixedAssets.find(
                                        (fixedAsset) => fixedAsset.FixedAssetId === fixedAssetId
                                    ))
                            "
                            tooltip
                            tooltip-content="Sửa"
                            :tooltip-type="checkTooltip"
                        >
                            <template #icon>
                                <section class="icon edit"></section>
                            </template>
                        </MISAButton>
                        <MISAButton
                            type-button="icon"
                            circle
                            width="30px"
                            height="30px"
                            @click="
                                (isShowFixedAssetPopup = true),
                                    (fixedAssetPopupMode = 'REPLICATE'),
                                    (fixedAssetReplicate = fixedAssetPaging.FixedAssets.find(
                                        (fixedAsset) => fixedAsset.FixedAssetId === fixedAssetId
                                    ))
                            "
                            tooltip
                            tooltip-content="Nhân bản"
                            :tooltip-type="checkTooltip"
                        >
                            <template #icon>
                                <section class="icon replication"></section>
                            </template>
                        </MISAButton>
                        <MISAButton
                            type-button="icon"
                            circle
                            width="30px"
                            height="30px"
                            @click="deleteFixedAsset(fixedAssetId)"
                            tooltip
                            tooltip-content="Xóa"
                            :tooltip-type="checkTooltip"
                        >
                            <template #icon>
                                <section class="icon delete"></section>
                            </template>
                        </MISAButton>
                    </template>
                </MISATableColumn>
            </MISATable>
        </main>
    </section>

    <Transition name="fade">
        <FixedAssetPopup
            v-if="isShowFixedAssetPopup"
            @close="isShowFixedAssetPopup = false"
            @submit="popupSubmit"
            :fixed-asset-popup-mode="fixedAssetPopupMode"
            :department-list="departmentList"
            :fixed-asset-category-list="fixedAssetCategoryList"
            :fixed-asset-update="fixedAssetUpdate"
            :fixed-asset-replicate="fixedAssetReplicate"
        />
    </Transition>

    <MISAToastMessage
        :message="successMessage"
        v-model="showSuccessMessage"
        :type="ToastMessageType.SUCCESS"
    />

    <MISAToastMessage v-model="showWarningMessage" @click-main="showWarningMessage = false">
        <TransferAssetInfo :message="warningMessage" :more-info="moreInfoWaringMessage" />
    </MISAToastMessage>
</template>

<script setup lang="ts">
import { useResource } from '@/hook'
import FixedAssetPopup from './popup'
import { ref, onMounted, computed, watch } from 'vue'
import type {
    FixedAssetPopupMode,
    FixedAsset,
    FixedAssetFilter,
    Department,
    FixedAssetCategory,
    FixedAssetPaging,
    MyResponse
} from '@/types'
import { departmentAPI, fixedAssetAPI, fixedAssetCategoryAPI } from '@/api'
import { FixedAssetPopupMode as FixedAssetPopupModeEnum, ToastMessageType } from '@/enum'
import { isAxiosError } from 'axios'

const fixedAssetUpdate = ref<FixedAsset>()

const fixedAssetReplicate = ref<FixedAsset>()

const departmentList = ref<Department[]>([])

const fixedAssetCategoryList = ref<FixedAssetCategory[]>([])

const isShowFixedAssetPopup = ref<boolean>()

const fixedAssetPopupMode = ref<FixedAssetPopupMode>('CREATE')

const rowIdFocus = ref<string>('')

const listRowIdSelected = ref<Array<string>>([])

const successMessage = ref<string>('')
const showSuccessMessage = ref<boolean>(false)

const warningMessage = ref<string>('')
const showWarningMessage = ref<boolean>(false)
const moreInfoWaringMessage = ref<Array<string>>([])

const loadingTable = ref<boolean>(false)
const isUpdateData = ref<boolean>(false)

const fixedAssetFilter = ref<FixedAssetFilter>({
    PageLimit: 20,
    PageNumber: 1,
    FixedAssetCategoryName: '',
    DepartmentName: '',
    FixedAssetCodeOrName: ''
})

const fixedAssetPaging = ref<FixedAssetPaging>({
    FixedAssets: [],
    FixedAssetTotal: 0
})

const departmentNameList = computed(() => departmentList.value.map((item) => item.DepartmentName))

const fixedAssetCategoryNameList = computed(() =>
    fixedAssetCategoryList.value.map((item) => item.FixedAssetCategoryName)
)

const getFixedAssetPaging = async () => {
    try {
        loadingTable.value = true
        const response = await fixedAssetAPI.getFixedAssetPaging(fixedAssetFilter.value)
        fixedAssetPaging.value = response.data
        rowIdFocus.value = fixedAssetPaging.value.FixedAssets[0].FixedAssetId
        const timmer = setTimeout(() => {
            loadingTable.value = false
            clearTimeout(timmer)
        }, 500)
    } catch (error) {
        console.log(error)
    }
}

const updateData = async () => {
    isUpdateData.value = true
    await getFixedAssetPaging()
    const timmer = setTimeout(() => {
        successMessage.value = success_update_data
        showSuccessMessage.value = true
        isUpdateData.value = false
        clearTimeout(timmer)
    }, 500)
}

const getDepartment = async () => {
    const response = await departmentAPI.getAllDepartment()
    departmentList.value = response.data
}

const getFixedAssetCategory = async () => {
    const response = await fixedAssetCategoryAPI.getAllFixedAssetCategory()
    fixedAssetCategoryList.value = response.data
}

const deleteFixedAsset = async (fixedAssetId: string) => {
    try {
        await fixedAssetAPI.deleteFixedAsset(fixedAssetId)
        fixedAssetFilter.value.PageNumber = 1
        fixedAssetPaging.value.FixedAssets = fixedAssetPaging.value.FixedAssets.filter(
            (item) => item.FixedAssetId !== fixedAssetId
        )
        successMessage.value = success_delete
        showSuccessMessage.value = true
    } catch (error) {
        if (isAxiosError<MyResponse>(error)) {
            showWarningMessage.value = true
            warningMessage.value = error.response?.data?.UserMessage ?? ''
            moreInfoWaringMessage.value = error.response?.data?.MoreInfo ?? []
        }
    }
}

const deleteManyFixedAsset = async () => {
    await fixedAssetAPI.deleteManyFixedAsset(listRowIdSelected.value)
    fixedAssetPaging.value.FixedAssets = fixedAssetPaging.value.FixedAssets.filter(
        (item) => !listRowIdSelected.value.includes(item.FixedAssetId)
    )
    fixedAssetFilter.value.PageNumber = 1
    listRowIdSelected.value = []
    successMessage.value = success_delete
    showSuccessMessage.value = true
}

const popupSubmit = (fixedAsset: FixedAsset, fixedAssetPopupMode: FixedAssetPopupMode) => {
    getFixedAssetPaging()
    fixedAssetFilter.value.PageNumber = 1
    switch (fixedAssetPopupMode) {
        case FixedAssetPopupModeEnum.CREATE:
            fixedAssetPaging.value.FixedAssets.unshift(fixedAsset)
            successMessage.value = success_create
            break
        case FixedAssetPopupModeEnum.UPDATE:
            fixedAssetPaging.value.FixedAssets = fixedAssetPaging.value.FixedAssets.filter(
                (item) => item.FixedAssetId !== fixedAsset.FixedAssetId
            )
            fixedAssetPaging.value.FixedAssets.unshift(fixedAsset)
            successMessage.value = success_update
            break
        case FixedAssetPopupModeEnum.REPLICATE:
            successMessage.value = success_replicate
            break
        default:
            break
    }
    showSuccessMessage.value = true
}

watch([() => fixedAssetFilter.value.PageLimit, () => fixedAssetFilter.value.PageNumber], () => {
    getFixedAssetPaging()
})

// Gọi API lấy dữ liệu khi component được mounted
onMounted(() => {
    getFixedAssetPaging()
    getDepartment()
    getFixedAssetCategory()
})

// Lấy resource
const {
    fixed_asset: {
        cost,
        accumulation_depreciation,
        data_update,
        fixed_asset_add,
        fixed_asset_category_name,
        department_name,
        fixed_asset_code,
        fixed_asset_name,
        fixed_asset_search,
        functional,
        quantity,
        remainder_cost,
        success_create,
        success_delete,
        success_replicate,
        success_update,
        success_update_data
    }
} = useResource()
</script>

<style scoped lang="scss">
@import './FixedAsset.scss';
</style>
