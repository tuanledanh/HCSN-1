<template>
    <section class="t-pagination h-40 flex item-center w-100 px-10 col-gap-20">
        <!-- Tổng số trang -->
        <section class="t-pagination__total">
            {{ `${pagination.total}:` }}
            <span class="fw-700">{{ total }}</span>
            {{ pagination.item }}
        </section>

        <!-- Số bản ghi / trang -->
        <section class="t-pagination__limit flex item-center col-gap-10">
            {{ pagination.limit }}
            <MISADropdown
                width="48px"
                height="24px"
                v-model="pageLimit"
                :list="[1, 3, 5, 10, 20, 30, 50, 80, 90]"
                :position="positionDropdown"
            />
        </section>

        <!-- Trang hiện tại -->
        <section class="t-pagination__page-number flex item-center col-gap-6">
            <!-- Đến trang đầu tiên -->
            <MISAButton
                type-button="icon"
                :disable="pageNumber == 1"
                circle
                height="20px"
                width="20px"
                @click="pageNumber = 1"
                tooltip
                :tooltip-content="pagination.page_first"
                :tooltip-type="TooltipType.TOP"
            >
                <template #icon-left>
                    <section class="icon chevron-left"></section>
                    <section class="icon chevron-left"></section>
                </template>
            </MISAButton>
            <!-- Giảm số xuống 1 đơn vị -->
            <MISAButton
                type-button="icon"
                circle
                height="20px"
                width="20px"
                :disable="pageNumber == 1"
                @click="pageNumber--"
                tooltip
                :tooltip-content="pagination.page_prev"
                :tooltip-type="TooltipType.TOP"
            >
                <template #icon>
                    <section class="icon chevron-left"></section>
                </template>
            </MISAButton>

            <!-- Số trang hiện tại -->
            <h1 class="flex col-gap-6">
                <span>{{ pagination.page }}</span>
                <span class="fw-700">{{ pageNumber }}</span>
            </h1>

            <!-- Tăng số trang lên 1 đơn vị -->
            <MISAButton
                type-button="icon"
                :disable="pageNumber == pageSize"
                circle
                height="20px"
                width="20px"
                tooltip
                :tooltip-content="pagination.page_next"
                :tooltip-type="TooltipType.TOP"
                @click="pageNumber++"
            >
                <template #icon>
                    <section class="icon chevron-right"></section>
                </template>
            </MISAButton>
            <MISAButton
                type-button="icon"
                :disable="pageNumber == pageSize"
                circle
                height="20px"
                width="20px"
                @click="pageNumber = pageSize"
                tooltip
                :tooltip-content="`${pagination.page_last} ${pageSize}`"
                :tooltip-type="TooltipType.TOP"
            >
                <template #icon-left>
                    <section class="icon chevron-right"></section>
                    <section class="icon chevron-right"></section>
                </template>
            </MISAButton>
        </section>
    </section>
</template>

<script setup lang="ts">
import { useResource } from '@/hook'
import type { PaginationProps } from '@/types'
import { computed } from 'vue'
import { TooltipType } from '@/enum'

const props = withDefaults(defineProps<PaginationProps>(), {
    total: 0,
    positionDropdown: 'bottom'
})

const pageLimit = defineModel<number>('pageLimit', { local: true, default: 10 })
const pageNumber = defineModel<number>('pageNumber', {
    local: true,
    default: 1
})

const total = computed(() => props.total)

const pageSize = computed(() => Math.ceil(total.value / pageLimit.value) || 1)

const { pagination } = useResource()
</script>

<style scoped lang="scss">
@import './MISAPagination.scss';
</style>
