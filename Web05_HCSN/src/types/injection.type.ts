import type { Ref } from 'vue'
import type { UpdateRowActionType } from './types'

export interface TableInjection {
    id: string
    data: Ref<Array<Object>>
    loading: Ref<boolean>
    border?: boolean
    hasFooter?: boolean
    scrollTop: Ref<number>
    pageNumber: Ref<number>
    pageLimit: Ref<number>
    checkedAll: Ref<boolean>
    rowIdHover: Ref<string>
    rowIdFocus: Ref<string>
    listRowIdSelected: Ref<Array<string>>

    updateListRowIdSelected: (action: UpdateRowActionType, rowId: string) => void

    clickRow: (event: MouseEvent) => void
    dbclickRow: () => void
}
