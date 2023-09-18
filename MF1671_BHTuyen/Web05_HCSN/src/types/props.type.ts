import type Department from './department.type'
import type FixedAssetCategory from './fixedAssetCategory.type'
import type FixedAsset from './fixedAsset.type'

import type {
    TableColumnType,
    SideFixedTyed,
    TextAlignType,
    PositonType,
    ButtonType,
    FixedAssetPopupMode,
    InputType,
    PopupDetailMode,
    TooltipType,
    ToastMessageType
} from './types'
import type { TransferAsset } from '.'
export interface ButtonProps {
    title?: string
    typeButton?: ButtonType
    tabindex?: string
    height?: string
    width?: string
    padding?: string
    circle?: boolean
    border?: boolean
    disable?: boolean
    tooltip?: boolean
    tooltipContent?: string
    tooltipType?: TooltipType
}

export interface DatePickerProps {
    label?: string
    width?: string

    invalid?: boolean
}

export interface Dropdown {
    width?: string
    height?: string
    list?: Array<any>
    position?: PositonType
}

export interface InputProps {
    type?: InputType
    label?: string
    placeholder?: string
    readonly?: boolean
    textRight?: boolean
    tabindex?: string
    placeholderItalic?: boolean
    width?: string
    padding?: string
    required?: boolean
    isFocus?: boolean

    hasIconChangeNumber?: boolean
}

export interface ComboBoxProps {
    label?: string
    placeholder?: string
    readonly?: boolean
    textRight?: boolean
    tabindex?: string
    placeholderItalic?: boolean
    width?: string
    padding?: string
    required?: boolean
    list?: Array<string>
}

export interface PaginationProps {
    total?: number
    positionDropdown?: PositonType
}

export interface PopupProps {
    heading: string
    disableSubmit?: boolean
}

export interface SidebarItemProps {
    contentTooltip?: string
    hasChevronDown?: boolean
    order: string
    title: string
    to: string
    isGrow?: boolean
    hasMenu?: boolean
    listItemMenu?: Array<[string, string]>
}

export interface TableProps {
    id: string
    width?: string
    style?: string
    border?: boolean
    hasFooter?: boolean
    data: Array<object>
    total: number
    loading?: boolean
    emptyDataTitle?: string
}

export interface TableColumnProps {
    tooltip?: string
    label?: string
    textAlign?: TextAlignType
    type?: TableColumnType
    fixed?: boolean
    sideFixed?: SideFixedTyed
    columnKey?: string
    scroll?: boolean
    width?: string
    hasTotal?: boolean
    hasFormatNumber?: boolean
    hasFormatDate?: boolean
}

export interface FixedAssetPopupProps {
    fixedAssetPopupMode?: FixedAssetPopupMode

    departmentList?: Array<Department>

    fixedAssetCategoryList?: Array<FixedAssetCategory>

    fixedAssetUpdate?: FixedAsset
    fixedAssetReplicate?: FixedAsset
}

export interface PopupDetailProps {
    popupDetailMode?: PopupDetailMode
    transferAssetUpdate?: TransferAsset
}

export interface PopupChooseProps {
    fixedAssetIdIgnores: string[]

    transferAssetDetailIdIgnores: string[]

    transferAssetId: string
}

export interface CheckboxProps {
    checked?: boolean
}

export interface SelectInfoProps {
    selectedTotal: number

    buttonDelete?: boolean
}

export interface DepartmentReceiverProps {
    popupDetailMode?: PopupDetailMode

    transferAssetId?: string
}

export interface LoadingProps {
    loading: boolean
}

export interface TooltipProps {
    content: string

    top?: number

    left?: number

    type: TooltipType

    show?: boolean
}

export interface ToastMessageProps {
    type?: ToastMessageType
    message?: string
    mainButton?: string
    subButton?: string
    outlineButton?: string
}

export interface TransferAssetInfoProps {
    message: string
    moreInfo: Array<string>
}
