<template>
    <section
        class="t-table flex flex-col"
        :class="`${border ? 'border' : ''}`"
        :style="`width: ${width}; ${style};`"
        ref="table"
    >
        <section class="t-table__column br-4 flex">
            <slot></slot>
            <!-- Hiển thị khi Table không có dữ liệu -->
            <section class="empty-data flex flex-col item-center" v-if="data.length == 0">
                <section class="empty-data__icon"></section>
                <section class="empty-data__title" v-if="!!emptyDataTitle">
                    {{ emptyDataTitle }}
                </section>
            </section>
        </section>
        <MISAPagination
            :position-dropdown="'top'"
            :total="total"
            v-model:pageLimit.lazy="pageLimit"
            v-model:pageNumber="pageNumber"
        />
    </section>
</template>

<script setup lang="ts">
import { UpdateRowAction } from '@/enum'
import type { TableProps, TableInjection, UpdateRowActionType } from '@/types'
import { computed, watch } from 'vue'
import { provide, ref } from 'vue'

const props = withDefaults(defineProps<TableProps>(), {
    id: '',
    width: '100%',
    style: '',
    border: false,
    hasFooter: true,
    data: () => [],
    total: 0,
    loading: false
})

const { border, hasFooter, id } = props

const emits = defineEmits<{
    clickRow: [id: string]
    dbclickRow: [id: string]
}>()

// State cuộn nội dung bảng
const scrollTop = ref<number>(0)

// State chỉ số hàng đang hover
const rowIdHover = ref<string>('')
const rowIdFocus = defineModel<string>('rowIdFocus', { local: true, default: '' })

const listRowIdSelected = defineModel<Array<string>>('listRowIdSelected', {
    local: true,
    default: () => []
})

const pageLimit = defineModel<number>('pageLimit', { local: true, default: 20 })
const pageNumber = defineModel<number>('pageNumber', {
    local: true,
    default: 1
})

const data = computed(() => {
    return props.data
})

const loading = computed(() => props.loading)

// State danh sách các hàng được chọn
// const listRowIdSelected = ref<string[]>([])

const checkedAll = ref<boolean>(false)

const updateListRowIdSelected = (action: UpdateRowActionType, rowId: string) => {
    console.log(action, rowId)
    console.log('update checked')
    switch (action) {
        case UpdateRowAction.ADD:
            if (!listRowIdSelected.value.includes(rowId)) {
                listRowIdSelected.value.push(rowId)
                console.log('add: ', listRowIdSelected.value)
            }
            break
        case UpdateRowAction.DELETE:
            if (listRowIdSelected.value.includes(rowId)) {
                listRowIdSelected.value.splice(
                    listRowIdSelected.value.findIndex((item) => item === rowId),
                    1
                )
                console.log('delete: ', listRowIdSelected.value)
            }
            break
        default:
            break
    }
}

const clickRow = (event: MouseEvent) => {
    if (event.detail > 1) {
        event.stopPropagation()
        return
    }
    rowIdFocus.value = rowIdHover.value
    emits('clickRow', rowIdFocus.value)
}

const dbclickRow = () => {
    rowIdFocus.value = rowIdHover.value
    emits('dbclickRow', rowIdFocus.value)
}

watch(pageLimit, () => {
    if (pageNumber.value > 1) pageNumber.value = 1
})

watch(
    [pageNumber, data],
    () => {
        scrollTop.value = 0
        if (data.value.length > 0) {
            checkedAll.value = data.value
                .map<string>((item) => item[id as keyof object])
                .every((item) => {
                    return listRowIdSelected.value.includes(item)
                })
        }
    },
    { immediate: true }
)

watch([listRowIdSelected, listRowIdSelected.value, () => listRowIdSelected.value.length], () => {
    if (listRowIdSelected.value.length === 0) checkedAll.value = false

    checkedAll.value = data.value
        .map((item) => item[id as keyof object])
        .every((item) => {
            return listRowIdSelected.value.includes(item as any)
        })
})

provide<TableInjection>('table', {
    id,
    data,
    loading,
    border,
    hasFooter,
    scrollTop,
    pageLimit,
    pageNumber,
    checkedAll,
    rowIdHover,
    rowIdFocus,
    listRowIdSelected,
    updateListRowIdSelected,
    clickRow,
    dbclickRow
})
</script>

<style scoped lang="scss">
@import './MISATable.scss';
</style>
