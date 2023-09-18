<template>
    <section
        class="t-table-column br-4 flex flex-col"
        :class="`${border ? 'border' : ''} ${fixed ? 'fixed-' + sideFixed : ''} ${
            !hasFooter ? 'without-footer' : ''
        }`"
        :style="`width: ${
            type === TableColumnType.SELECTION || type === TableColumnType.INDEX ? '40px' : width
        };flex: ${
            type === TableColumnType.SELECTION || type === TableColumnType.INDEX
                ? '0 0 40px'
                : width
        };`"
        ref="tableColumn"
    >
        <!-- Header -->
        <header
            class="t-table-column__header flex item-center h-40 px-10"
            :class="`justify-${textAlign} ${type === TableColumnType.SELECTION ? 'pointer' : ''}`"
            @mouseover="showTooltip"
            @mouseout="hideTooltip"
        >
            <!-- Khi là cột Checkbox -->
            <MISAChecbox
                v-if="type === TableColumnType.SELECTION"
                @on-checked="checkAll"
                :checked="checkedAll"
            />

            <!-- Khi là cột số thứ tự -->
            <section v-else-if="type === TableColumnType.INDEX" class="fw-500">
                {{ TableColumnResource.index }}
            </section>

            <!-- Khi là cột bình thường -->
            <section v-else class="fw-500 text-over">
                {{ label }}
            </section>
        </header>

        <!-- Body -->
        <main class="t-table-column__body">
            <!-- Khi là cột checkbox -->
            <section
                class="wrapper"
                ref="columnScroll"
                @scroll="onScroll"
                v-if="type === TableColumnType.SELECTION"
            >
                <section
                    class="t-table-column__cell flex item-center justify-center h-40 pointer px-10"
                    :class="`${checkRowHover(item) ? 'hover' : ''} 
                            ${checkRowActive(item) ? 'active' : ''}`"
                    v-for="item in columnIds"
                    :key="item"
                    @mouseover="rowIdHover = item"
                    @mouseout="rowIdHover = ''"
                >
                    <section class="skeleton" v-if="loading"></section>
                    <MISAChecbox
                        @on-checked="
                            (checked) =>
                                checked
                                    ? updateListRowIdSelected(UpdateRowAction.ADD, item)
                                    : updateListRowIdSelected(UpdateRowAction.DELETE, item)
                        "
                        :checked="listRowIdSelected.includes(item)"
                        v-else
                    />
                </section>
            </section>

            <!-- Khi là cột số thứ tự -->
            <section
                class="wrapper"
                ref="columnScroll"
                @scroll="onScroll"
                v-else-if="type === TableColumnType.INDEX"
            >
                <section
                    class="t-table-column__cell flex item-center justify-center h-40 pointer px-10"
                    :class="`${checkRowHover(item) ? 'hover' : ''} 
                            ${checkRowActive(item) ? 'active' : ''}`"
                    v-for="(item, index) in columnIds"
                    :key="item"
                    @mouseover="rowIdHover = item"
                    @mouseout="rowIdHover = ''"
                    @click.prevent.stop="clickRow"
                    @dblclick.prevent.stop="dbclickRow"
                >
                    <section class="skeleton" v-if="loading"></section>
                    <section v-else>{{ pageLimit * (pageNumber - 1) + index + 1 }}</section>
                </section>
            </section>

            <!-- Khi là cột mở rộng -->
            <section
                class="wrapper"
                :class="`${scroll ? 'scroll' : ''}`"
                ref="columnScroll"
                @scroll="onScroll"
                v-else-if="type === TableColumnType.EXPAND"
            >
                <section
                    class="t-table-column__cell flex item-center justify-center h-40 pointer col-gap-10 px-10"
                    :class="`${checkRowHover(item) ? 'hover' : ''} 
                            ${checkRowActive(item) ? 'active' : ''}`"
                    v-for="(item, index) in columnIds"
                    :key="item"
                    @mouseover="rowIdHover = item"
                    @mouseout="rowIdHover = ''"
                    @click.prevent.stop="clickRow"
                    @dblclick.prevent.stop="dbclickRow"
                    ref="row"
                >
                    <section class="skeleton" v-if="loading"></section>
                    <slot :name="item" :checkTooltip="checkTooltip(index)" v-else></slot>
                </section>
            </section>

            <!-- Khi là cột bình thường -->
            <section
                class="wrapper"
                :class="`${scroll ? 'scroll' : ''}`"
                ref="columnScroll"
                @scroll="onScroll"
                v-else
            >
                <section
                    class="t-table-column__cell flex item-center h-40 px-10 pointer"
                    :class="`justify-${textAlign} 
                            ${checkRowHover(item[id as keyof object]) ? 'hover' : ''} 
                            ${checkRowActive(item[id as keyof object]) ? 'active' : ''}`"
                    v-for="item in columnData"
                    :key="item[id as keyof object]"
                    @mouseover="rowIdHover = item[id as keyof object]"
                    @mouseout="rowIdHover = ''"
                    @click.prevent.stop="clickRow"
                    @dblclick.prevent.stop="dbclickRow"
                >
                    <section class="skeleton" v-if="loading"></section>
                    <section v-else class="text-over">
                        {{
                            hasFormatNumber
                                ? formatNumber(item[columnKey as keyof object])
                                : hasFormatDate
                                ? formatDateTime(item[columnKey as keyof object])
                                : item[columnKey as keyof object]
                        }}
                    </section>
                </section>
            </section>
        </main>

        <!-- Footer -->
        <footer
            class="t-table-column__footer h-40 flex item-center px-10"
            :class="`justify-${textAlign}`"
            v-if="hasFooter"
        >
            <section class="skeleton" v-if="loading && hasTotal"></section>
            <section v-else class="fw-500">
                {{ hasTotal ? (hasFormatNumber ? formatNumber(total) : total) : '' }}
            </section>
        </footer>

        <Teleport to="body">
            <MISATooltip
                :content="tooltip || 'Số thứ tự'"
                :type="TooltipTypeEnum.TOP"
                :top="coordinatesTooltip.top"
                :left="coordinatesTooltip.left"
                :show="show"
                v-if="tooltip || type === TableColumnType.INDEX"
            />
        </Teleport>
    </section>
</template>

<script setup lang="ts">
import { UpdateRowAction, TableColumnType, TooltipType as TooltipTypeEnum } from '@/enum'
import { useGetArrayWithKey, useResource } from '@/hook'
import type { TableColumnProps, TableInjection, TooltipType } from '@/types'
import { computed, ref, watch, inject } from 'vue'

// Định nghĩa props
const props = withDefaults(defineProps<TableColumnProps>(), {
    type: 'normal',
    label: '',
    columnKey: 'code',
    textAlign: 'center',
    width: 'auto',
    fixed: false,
    sideFixed: 'left',
    hasTotal: false,
    scroll: false,
    hasFormatNumber: false,
    hasFormatDate: false
})

const columnData = ref<Array<object>>([])
const columnIds = ref<Array<string>>([])
const show = ref<boolean>(false)

const tableColumn = ref<HTMLElement | null>(null)
const columnScroll = ref<HTMLElement | null>(null)
const row = ref<HTMLElement[]>([])

const { table_column: TableColumnResource } = useResource()
const {
    id,
    data,
    border,
    scrollTop,
    rowIdHover,
    rowIdFocus,
    pageLimit,
    pageNumber,
    hasFooter,
    checkedAll,
    listRowIdSelected,
    loading,
    updateListRowIdSelected,
    clickRow,
    dbclickRow
} = inject('table') as TableInjection

const total = computed((): number => {
    if (props.hasTotal) {
        const array = columnData.value.map<number>((item) => item[props.columnKey as keyof object])
        return array.reduce((acc: number, cur: number) => acc + cur, 0)
    }
    return 0
})

const coordinatesTooltip = ref<{ top: number; left: number }>({ top: 0, left: 0 })

const showTooltip = () => {
    if (props.tooltip || props.type === TableColumnType.INDEX) {
        const rect = tableColumn.value?.getBoundingClientRect()
        if (rect) {
            coordinatesTooltip.value = {
                top: rect.top,
                left: rect.left + rect.width / 2
            }
        }
    }
    show.value = true
}

const hideTooltip = () => {
    show.value = false
}

const checkAll = (checked: boolean) => {
    checkedAll.value = checked
    if (checkedAll.value) {
        columnIds.value.forEach((item) => {
            if (!listRowIdSelected.value.includes(item)) {
                listRowIdSelected.value.push(item)
            }
        })
    } else {
        listRowIdSelected.value = listRowIdSelected.value.filter((item) => {
            return !columnIds.value.includes(item)
        })
    }
}

const checkRowHover = (rowId: string): boolean =>
    (rowId === rowIdHover.value || listRowIdSelected.value.includes(rowId)) && !loading.value

const checkRowActive = (rowId: string): boolean => rowId === rowIdFocus.value && !loading.value

const formatNumber = (value: number): string => {
    return value.toString().replace(/\B(?=(\d{3})+(?!\d))/g, '.')
}

const formatDateTime = (value: string): string => {
    return new Date(value).toLocaleDateString('vi-VN', {
        day: '2-digit',
        month: '2-digit',
        year: 'numeric'
    })
}

const onScroll = (event: Event) => {
    scrollTop.value = (event.target as HTMLElement).scrollTop
}

const checkTooltip = (index: number): TooltipType => {
    if (row.value[index] != undefined && columnScroll.value != undefined) {
        const reactRow = row.value[index].getBoundingClientRect()
        const reactColumn = columnScroll.value.getBoundingClientRect()
        return reactRow.bottom + 35 >= reactColumn.bottom
            ? TooltipTypeEnum.TOP
            : TooltipTypeEnum.BOTTOM
    } else return TooltipTypeEnum.BOTTOM
}

watch(scrollTop, (value) => {
    columnScroll.value?.scroll({ top: value, behavior: 'instant' })
})

watch(
    () => data.value,
    () => {
        columnData.value = useGetArrayWithKey(data.value, props.columnKey, id)
        columnIds.value = columnData.value.map((item) => item[id as keyof object])
    },
    { immediate: true }
)
</script>

<style scoped lang="scss">
@import './MISATableColumn.scss';
</style>
