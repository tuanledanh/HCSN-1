export type TableColumnType = 'selection' | 'index' | 'expand' | 'normal'
export type TextAlignType = 'left' | 'center' | 'right'
export type SideFixedTyed = 'left' | 'right'
export type PositonType = 'top' | 'bottom'
export type ButtonType = 'main' | 'sub' | 'outline' | 'icon'
export type UpdateRowActionType = 'ADD' | 'DELETE'
export type FixedAssetPopupMode = 'CREATE' | 'UPDATE' | 'REPLICATE'
export type PopupDetailMode = 'CREATE' | 'UPDATE'
export type InputType = 'text' | 'number' | 'percent' | 'code'
export type ActionMode = 0 | 1 | 2 // 0: update, 1: create, 2: delete 3: noChange
export type ToastMessageType = 'success' | 'error' | 'warning' | 'info'
export type TooltipType = 'top' | 'bottom' | 'left' | 'right'

export interface MyResponse {
    UserMessage?: string
    MoreInfo?: Array<string>
}
