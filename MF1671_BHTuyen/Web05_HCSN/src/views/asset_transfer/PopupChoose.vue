<template>
    <section class="popup-choose modal flex center">
        <section class="wrapper-popup relative">
            <MISAPopup
                :heading="heading"
                @close="close"
                @submit="submit"
                :disable-submit="fixedAssetPaging.FixedAssets.length == 0"
            >
                <section class="popup-choose__body flex flex-col row-gap-10">
                    <section class="table">
                        <MISATable
                            border
                            id="FixedAssetId"
                            :data="fixedAssetPaging.FixedAssets"
                            :total="fixedAssetPaging.FixedAssetTotal"
                            :loading="loading"
                            :empty-data-title="transfer_asset_no_more"
                            v-model:rowIdFocus="rowIdFocus"
                            v-model:pageLimit="fixedAssetFilter.PageLimit"
                            v-model:pageNumber="fixedAssetFilter.PageNumber"
                            v-model:listRowIdSelected="listRowIdSelected"
                        >
                            <MISATableColumn type="selection" />
                            <MISATableColumn type="index" />
                            <MISATableColumn
                                :label="fixed_asset_code"
                                column-key="FixedAssetCode"
                                width="150px"
                                text-align="left"
                            />
                            <MISATableColumn
                                :label="fixed_asset_name"
                                column-key="FixedAssetName"
                                width="200px"
                                text-align="left"
                            />
                            <MISATableColumn
                                :label="department_name"
                                column-key="DepartmentName"
                                width="200px"
                                text-align="left"
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
                                column-key="RemainderCost"
                                width="150px"
                                text-align="right"
                                has-format-number
                                has-total
                            />
                            <MISATableColumn
                                :label="tracked_year"
                                width="150px"
                                column-key="TrackedYear"
                                scroll
                            />
                        </MISATable>
                    </section>

                    <!--  -->
                    <section class="new-department grid px-20 col-gap-20 item-center">
                        <MISACombobox
                            required
                            placeholder-italic
                            :label="transfer_asset_name"
                            :placeholder="transfer_asset_name_input"
                            :list="
                                useDepartment().departments.map(
                                    (department) => department.DepartmentName
                                )
                            "
                            v-model="transferDepartment.DepartmentName"
                            ref="combobox"
                        />
                        <MISAInput
                            :label="note_input"
                            :placeholder="note"
                            placeholder-italic
                            v-model="reason"
                        />
                    </section>
                </section>
            </MISAPopup>

            <section class="wrapper-select-info absolute">
                <MISASelectInfo
                    v-if="listRowIdSelected.length > 0"
                    :selected-total="listRowIdSelected.length"
                    :button-delete="false"
                    @unselected="listRowIdSelected = []"
                />
            </section>

            <!-- Thông báo validate -->
            <MISAToastMessage v-model="showWarningMessageValidate" @click-main="confirmToast">
                <p class="pr-20">{{ warningMessageValidate }}</p>
            </MISAToastMessage>
        </section>
    </section>
</template>

<script setup lang="ts">
import { useResource } from '@/hook'
import type {
    Department,
    FixedAsset,
    FixedAssetFilter,
    FixedAssetPaging,
    PopupChooseProps,
    TransferAssetDetail
} from '@/types'
import { ref, onMounted, watch, computed } from 'vue'
import { fixedAssetAPI } from '@/api'
import { useDepartment } from '@/stores'
import type MISACombobox from '@/components/combobox'

const props = defineProps<PopupChooseProps>()

const emits = defineEmits<{
    close: []
    submit: [transferAssetDetails: TransferAssetDetail[]]
}>()

const fixedAssetPaging = ref<FixedAssetPaging>({
    FixedAssets: [],
    FixedAssetTotal: 0
})

const fixedAssetFilter = ref<FixedAssetFilter>({
    PageLimit: 10,
    PageNumber: 1,
    FixedAssetIdIgnores: props.fixedAssetIdIgnores,
    TransferAssetDetailIdIgnores: props.transferAssetDetailIdIgnores
})

const transferDepartment = ref<Department>({
    DepartmentId: '',
    DepartmentName: '',
    DepartmentCode: ''
})
const reason = ref<string>('')

const loading = ref<boolean>(false)

const rowIdFocus = ref<string>('')

// Danh sách mã Id các tài sản đã chọn
const listRowIdSelected = ref<string[]>([])

const fixedAssetSelected = ref<FixedAsset[]>([])

const warningMessageValidate = ref<string>('')
const showWarningMessageValidate = ref<boolean>(false)

const combobox = ref<InstanceType<typeof MISACombobox> | null>(null)

const getFixedAssetPaging = async () => {
    loading.value = true
    const response = await fixedAssetAPI.getTransferAssetPaging(fixedAssetFilter.value)
    fixedAssetPaging.value = response.data
    if (fixedAssetPaging.value.FixedAssets.length > 0)
        rowIdFocus.value = fixedAssetPaging.value.FixedAssets[0].FixedAssetId
    console.log(fixedAssetPaging.value.FixedAssets)
    const timmer = setTimeout(() => {
        loading.value = false
        clearTimeout(timmer)
    }, 500)
}

const transferAssetDetails = computed<TransferAssetDetail[]>(() => {
    console.log('update')
    return fixedAssetSelected.value.map(
        (fixedAsset): TransferAssetDetail => ({
            FixedAssetId: fixedAsset.FixedAssetId,
            FixedAssetCode: fixedAsset.FixedAssetCode,
            FixedAssetName: fixedAsset.FixedAssetName,
            Cost: fixedAsset.Cost,
            DepartmentId: fixedAsset.DepartmentId,
            DepartmentName: fixedAsset.DepartmentName,
            RemainderCost: fixedAsset.RemainderCost,
            TrackedYear: fixedAsset.TrackedYear,
            TransferAssetId: props.transferAssetId,
            TransferAssetDetailId: props.transferAssetId,
            Reason: reason.value,
            TransferDepartmentId: transferDepartment.value.DepartmentId,
            TransferDepartmentName: transferDepartment.value.DepartmentName
        })
    )
})

const confirmToast = () => {
    showWarningMessageValidate.value = false

    if (transferDepartment.value.DepartmentName == '') {
        combobox.value?.focus()
    }
}

const check = (): boolean => {
    if (listRowIdSelected.value.length == 0) {
        warningMessageValidate.value = transfer_asset_no_choose
        showWarningMessageValidate.value = true
        return false
    }

    if (transferDepartment.value.DepartmentName == '') {
        warningMessageValidate.value = choose_transfer_department
        showWarningMessageValidate.value = true
        return false
    }

    if (
        transferAssetDetails.value.some(
            (transferAssetDetail) =>
                transferAssetDetail.DepartmentId == transferDepartment.value.DepartmentId
        )
    ) {
        console.log(transferAssetDetails.value)
        warningMessageValidate.value = transfer_department_check
        showWarningMessageValidate.value = true
        return false
    }
    return true
}

const submit = () => {
    console.log(transferAssetDetails.value)
    if (check()) {
        emits('submit', transferAssetDetails.value)
        emits('close')
    }
}

const close = () => {
    emits('close')
}

watch(
    () => transferDepartment.value.DepartmentName,
    (value) => {
        console.log('first')
        console.log(useDepartment().departments)
        transferDepartment.value = useDepartment().getDepartmentByName(value)
        console.log(transferDepartment.value)
    }
)

watch([listRowIdSelected, listRowIdSelected.value, () => listRowIdSelected.value.length], () => {
    fixedAssetSelected.value = fixedAssetSelected.value.filter((fixedAsset) =>
        listRowIdSelected.value.includes(fixedAsset.FixedAssetId)
    )

    const fixedAsset = fixedAssetPaging.value.FixedAssets.filter((fixedAsset) =>
        listRowIdSelected.value.includes(fixedAsset.FixedAssetId)
    )

    fixedAsset.forEach((fixedAsset) => {
        if (!fixedAssetSelected.value.includes(fixedAsset)) {
            fixedAssetSelected.value.push(fixedAsset)
        }
    })
})
watch([() => fixedAssetFilter.value.PageLimit, () => fixedAssetFilter.value.PageNumber], () =>
    getFixedAssetPaging()
)

onMounted(() => {
    getFixedAssetPaging()
})

const {
    asset_transfer_popup_choose: {
        heading,
        cost,
        fixed_asset_code,
        fixed_asset_name,
        tracked_year,
        note,
        remainder_cost,
        department_name,
        transfer_asset_name_input,
        transfer_asset_name,
        note_input,
        transfer_asset_no_more,
        choose_transfer_department,
        transfer_department_check,
        transfer_asset_no_choose
    }
} = useResource()
</script>

<style scoped lang="scss">
@import './PopupChoose.scss';
</style>
