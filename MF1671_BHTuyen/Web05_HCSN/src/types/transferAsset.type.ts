import type { ActionMode } from './types'
export default interface TransferAsset {
    TransferAssetId: string

    TransferAssetCode: string

    TransferDate: string

    TransactionDate: string

    Cost: number | string

    RemainderCost: number | string

    Note: string
}

export interface TransferAssetPaging {
    TransferAssetTotal: number

    TransferAssets: TransferAsset[]
}

export interface TransferAssetFilter {
    PageLimit: number
    PageNumber: number
}

export interface TransferAssetDetail {
    TransferAssetDetailId?: string
    FixedAssetId: string
    FixedAssetCode: string
    FixedAssetName: string
    Cost: number | string
    RemainderCost: number | string
    DepartmentId: string
    DepartmentName: string
    TransferDepartmentId: string
    TransferDepartmentName: string
    Reason: string

    TrackedYear: number | string

    TransferAssetId?: string
}

export interface TransferAssetDetailFilter {
    PageLimit: number
    PageNumber: number
    TransferAssetId: string
}

export interface TransferAssetDetailPaging {
    TransferAssetDetailTotal: number
    TransferAssetDetails: TransferAssetDetail[]
}

export interface Receiver {
    ReceiverId: string
    ReceiverOrder: number
    Fullname: string
    Delegate: string

    Position: string

    TransferAssetId: string
}

export interface ReceiverFlag {
    Receiver: Receiver

    ActionMode: ActionMode
}

export interface TransferAssetDetailFlag {
    TransferAssetDetail: TransferAssetDetail
    ActionMode: ActionMode
}
export interface TransferAssetCreateFull {
    TransferAsset: TransferAsset

    TransferAssetDetails: TransferAssetDetail[]

    Receivers?: Receiver[]
}

export interface TransferAssetUpdateFull {
    TransferAsset: TransferAsset

    TransferAssetDetails?: TransferAssetDetailFlag[]

    Receivers?: ReceiverFlag[]
}
