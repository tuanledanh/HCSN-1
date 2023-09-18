<template>
    <section class="modal flex center">
        <section class="wrapper-popup">
            <MISAPopup :heading="headingPopup" @close="$emit('close')" @submit="submit">
                <section class="popup__body grid row-gap-20 col-gap-20 px-20 py-20">
                    <section class="col-1 row-1">
                        <MISAInput
                            required
                            is-focus
                            :type="InputType.CODE"
                            :label="fixed_asset_code"
                            :placeholder="fixed_asset_code_input"
                            placeholder-italic
                            v-model="fixedAsset.FixedAssetCode"
                        />
                    </section>
                    <section class="col-23 row-1">
                        <MISAInput
                            required
                            :label="fixed_asset_name"
                            :placeholder="fixed_asset_name_input"
                            placeholder-italic
                            v-model="fixedAsset.FixedAssetName"
                        />
                    </section>
                    <section class="col-1 row-2">
                        <MISACombobox
                            required
                            :label="fixed_asset_category_code"
                            :placeholder="fixed_asset_category_code_choose"
                            :list="fixedAssetCategoryCodeList"
                            v-model="fixedAsset.FixedAssetCategoryCode"
                        />
                    </section>
                    <section class="col-23 row-2">
                        <MISAInput
                            required
                            :label="fixed_asset_category_name"
                            readonly
                            v-model="fixedAsset.FixedAssetCategoryName"
                        />
                    </section>
                    <section class="col-1 row 3">
                        <MISACombobox
                            required
                            :label="department_code"
                            :placeholder="department_code_choose"
                            :list="departmentCodeList"
                            v-model="fixedAsset.DepartmentCode"
                        />
                    </section>
                    <section class="col-23 row-3">
                        <MISAInput
                            required
                            :label="department_name"
                            readonly
                            v-model="fixedAsset.DepartmentName"
                        />
                    </section>
                    <section class="col-1 row-4">
                        <MISAInput
                            required
                            :label="quantity"
                            text-right
                            :type="InputType.NUMBER"
                            hasIconChangeNumber
                            placeholder="0"
                            v-model="fixedAsset.Quantity"
                        />
                    </section>
                    <section class="col-2 row-4">
                        <MISAInput
                            required
                            :label="cost"
                            text-right
                            :type="InputType.NUMBER"
                            placeholder="0"
                            v-model="fixedAsset.Cost"
                        />
                    </section>
                    <section class="col-3 row-4">
                        <MISAInput
                            required
                            :label="life_time"
                            text-right
                            :type="InputType.NUMBER"
                            placeholder="0"
                            v-model="fixedAsset.LifeTime"
                        />
                    </section>
                    <section class="col-1 row-5">
                        <MISAInput
                            required
                            :label="depreciation_rate"
                            readonly
                            text-right
                            :type="InputType.PERCENT"
                            placeholder="0"
                            v-model="fixedAsset.DepreciationRate"
                        />
                    </section>
                    <section class="col-2 row-5">
                        <MISAInput
                            required
                            :label="year_depreciation"
                            text-right
                            :type="InputType.NUMBER"
                            placeholder="0"
                            v-model="fixedAsset.YearDepreciation"
                        />
                    </section>
                    <section class="col-3 row-5">
                        <MISAInput
                            required
                            :label="tracked_year"
                            text-right
                            readonly
                            v-model="fixedAsset.TrackedYear"
                        />
                    </section>

                    <section class="col-1 row-6">
                        <MISADatePicker
                            required
                            :label="purchase_date"
                            v-model="fixedAsset.PurchaseDate"
                        />
                    </section>
                    <section class="col-2 row-6">
                        <MISADatePicker
                            required
                            :label="using_started_date"
                            v-model="fixedAsset.UsingStartedDate"
                        />
                    </section>
                </section>
            </MISAPopup>
        </section>
    </section>
</template>

<script setup lang="ts">
import { useResource } from '@/hook'
import type { FixedAsset, FixedAssetPopupProps, FixedAssetPopupMode } from '@/types'
import { FixedAssetPopupMode as FixedAssetPopupModeEnum, InputType } from '@/enum'
import { computed, ref, onMounted, watch } from 'vue'
import { fixedAssetAPI } from '@/api'

const props = withDefaults(defineProps<FixedAssetPopupProps>(), {
    fixedAssetPopupMode: 'CREATE',
    departmentList: () => [],
    fixedAssetCategoryList: () => []
})

const emits = defineEmits<{
    close: []
    submit: [fixedAsset: FixedAsset, fixedAssetPopupMode: FixedAssetPopupMode]
}>()

const fixedAssetPopupMode = computed(() => props.fixedAssetPopupMode)

const departmentCodeList = computed(() => props.departmentList.map((item) => item.DepartmentCode))

const fixedAssetCategoryCodeList = computed(() =>
    props.fixedAssetCategoryList.map((item) => item.FixedAssetCategoryCode)
)

const headingPopup = computed(() => {
    switch (fixedAssetPopupMode.value) {
        case FixedAssetPopupModeEnum.UPDATE:
            return update
        case FixedAssetPopupModeEnum.REPLICATE:
            return replicate
        default:
            return create
    }
})

const fixedAsset = ref<FixedAsset>({
    FixedAssetId: '',
    FixedAssetCode: '',
    FixedAssetName: '',
    FixedAssetCategoryCode: '',
    FixedAssetCategoryId: '',
    FixedAssetCategoryName: '',
    DepartmentId: '',
    DepartmentName: '',
    DepartmentCode: '',
    PurchaseDate: new Date().toISOString(),
    UsingStartedDate: new Date().toISOString(),
    Cost: 0,
    AccumulationDepreciation: 0,
    RemainderCost: 0,
    Quantity: 0,
    DepreciationRate: 0,
    YearDepreciation: 0,
    TrackedYear: 0,
    LifeTime: 0
})

onMounted(async () => {
    switch (fixedAssetPopupMode.value) {
        case FixedAssetPopupModeEnum.UPDATE:
            if (props.fixedAssetUpdate) {
                console.log(props.fixedAssetUpdate)
                fixedAsset.value = JSON.parse(JSON.stringify(props.fixedAssetUpdate))
                console.log(fixedAsset.value)
            }
            break
        case FixedAssetPopupModeEnum.REPLICATE:
            if (props.fixedAssetReplicate) {
                fixedAsset.value = JSON.parse(JSON.stringify(props.fixedAssetReplicate))
                await getFixedAssetNewCode()
            }
            break
        default:
            fixedAsset.value.TrackedYear = new Date().getFullYear()
            await getFixedAssetNewCode()
            break
    }
})

watch(
    () => fixedAsset.value.DepartmentCode,
    () => {
        fixedAsset.value.DepartmentId =
            props.departmentList.find(
                (item) => item.DepartmentCode === fixedAsset.value.DepartmentCode
            )?.DepartmentId || ''
        fixedAsset.value.DepartmentName =
            props.departmentList.find(
                (item) => item.DepartmentCode === fixedAsset.value.DepartmentCode
            )?.DepartmentName || ''
    }
)

watch(
    () => fixedAsset.value.FixedAssetCategoryCode,
    () => {
        console.log('first')
        fixedAsset.value.FixedAssetCategoryId =
            props.fixedAssetCategoryList.find(
                (item) => item.FixedAssetCategoryCode === fixedAsset.value.FixedAssetCategoryCode
            )?.FixedAssetCategoryId || ''
        fixedAsset.value.FixedAssetCategoryName =
            props.fixedAssetCategoryList.find(
                (item) => item.FixedAssetCategoryCode === fixedAsset.value.FixedAssetCategoryCode
            )?.FixedAssetCategoryName || ''

        fixedAsset.value.LifeTime =
            props.fixedAssetCategoryList.find(
                (item) => item.FixedAssetCategoryCode === fixedAsset.value.FixedAssetCategoryCode
            )?.LifeTime || 0

        fixedAsset.value.DepreciationRate =
            props.fixedAssetCategoryList.find(
                (item) => item.FixedAssetCategoryCode === fixedAsset.value.FixedAssetCategoryCode
            )?.DepreciationRate || 0
    }
)

watch(
    () => fixedAsset.value.LifeTime,
    (value) => {
        fixedAsset.value.DepreciationRate =
            Number((100 / converStringToNumber(value)).toFixed(2)) === Number.POSITIVE_INFINITY
                ? 0
                : Number((100 / converStringToNumber(value)).toFixed(2))

        fixedAsset.value.YearDepreciation = Number(
            (
                (converStringToNumber(fixedAsset.value.Cost) * fixedAsset.value.DepreciationRate) /
                100
            ).toFixed(0)
        )

        fixedAsset.value.AccumulationDepreciation = Number(
            (
                converStringToNumber(fixedAsset.value.YearDepreciation) *
                (new Date().getFullYear() -
                    new Date(fixedAsset.value.UsingStartedDate).getFullYear())
            ).toFixed(0)
        )

        fixedAsset.value.RemainderCost = Number(
            (
                converStringToNumber(fixedAsset.value.Cost) -
                converStringToNumber(fixedAsset.value.AccumulationDepreciation)
            ).toFixed(0)
        )
    }
)

watch(
    () => fixedAsset.value.YearDepreciation,
    () => {
        fixedAsset.value.AccumulationDepreciation = Number(
            (
                converStringToNumber(fixedAsset.value.YearDepreciation) *
                (new Date().getFullYear() -
                    new Date(fixedAsset.value.UsingStartedDate).getFullYear())
            ).toFixed(0)
        )

        fixedAsset.value.RemainderCost = Number(
            (
                converStringToNumber(fixedAsset.value.Cost) -
                converStringToNumber(fixedAsset.value.AccumulationDepreciation)
            ).toFixed(0)
        )
    }
)

watch(
    () => fixedAsset.value.Cost,
    () => {
        fixedAsset.value.YearDepreciation = Number(
            (
                (converStringToNumber(fixedAsset.value.Cost) *
                    converStringToNumber(fixedAsset.value.DepreciationRate)) /
                100
            ).toFixed(0)
        )
        fixedAsset.value.AccumulationDepreciation = Number(
            (
                converStringToNumber(fixedAsset.value.YearDepreciation) *
                (new Date().getFullYear() -
                    new Date(fixedAsset.value.UsingStartedDate).getFullYear())
            ).toFixed(0)
        )
        console.log(
            new Date().getFullYear() - new Date(fixedAsset.value.UsingStartedDate).getFullYear()
        )

        fixedAsset.value.RemainderCost = Number(
            (
                converStringToNumber(fixedAsset.value.Cost) -
                converStringToNumber(fixedAsset.value.AccumulationDepreciation)
            ).toFixed(0)
        )
    }
)

const getFixedAssetNewCode = async () => {
    const response = await fixedAssetAPI.getFixedAssetNewCode()
    fixedAsset.value.FixedAssetCode = response.data
}

const createFixedAsset = async () => {
    try {
        await fixedAssetAPI.createFixedAsset(formatFixedAsset(fixedAsset.value))
    } catch (error) {
        console.log(error)
    }
}

const updateFixedAsset = async () => {
    try {
        await fixedAssetAPI.updateFixedAsset(formatFixedAsset(fixedAsset.value))
    } catch (error) {
        console.log(error)
    }
}

const converStringToNumber = (value: string | number): number =>
    Number(value.toString().replace(/\./g, '').replace(/,/g, '.'))

const submit = async () => {
    switch (fixedAssetPopupMode.value) {
        case FixedAssetPopupModeEnum.UPDATE:
            await updateFixedAsset()
            break
        case FixedAssetPopupModeEnum.REPLICATE:
            await createFixedAsset()
            break
        case FixedAssetPopupModeEnum.CREATE:
            await createFixedAsset()
            break
        default:
            break
    }
    emits('close')
    emits('submit', fixedAsset.value, fixedAssetPopupMode.value)
}

// Format trước khi gửi lên server
const formatFixedAsset = (fixedAsset: FixedAsset): FixedAsset => ({
    ...fixedAsset,
    Cost: Number(fixedAsset.Cost.toString().replace(/\./g, '')),
    RemainderCost: Number(fixedAsset.RemainderCost.toString().replace(/\./g, '')),
    AccumulationDepreciation: Number(
        fixedAsset.AccumulationDepreciation.toString().replace(/\./g, '')
    ),
    DepreciationRate: Number(fixedAsset.DepreciationRate.toString().replace(/,/g, '.')),
    YearDepreciation: Number(fixedAsset.YearDepreciation.toString().replace(/\./g, '')),
    LifeTime: Number(fixedAsset.LifeTime.toString().replace(/\./g, '')),
    Quantity: Number(fixedAsset.Quantity.toString().replace(/\./g, ''))
})

// Lấy ra resource
const {
    fixed_asset_popup: {
        cost,
        department_code,
        department_code_choose,
        department_name,
        depreciation_rate,
        fixed_asset_category_code,
        fixed_asset_category_code_choose,
        fixed_asset_category_name,
        fixed_asset_code,
        fixed_asset_code_input,
        fixed_asset_name,
        fixed_asset_name_input,
        heading: { create, replicate, update },
        life_time,
        quantity,
        using_started_date,
        purchase_date,
        tracked_year,
        year_depreciation
    }
} = useResource()
</script>

<style scoped lang="scss">
@import './FixedAssetPopup.scss';
</style>
