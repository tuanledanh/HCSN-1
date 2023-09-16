export default interface FixedAsset {
    FixedAssetId: string
    FixedAssetCode: string
    FixedAssetName: string
    FixedAssetCategoryCode: string
    FixedAssetCategoryId: string
    FixedAssetCategoryName: string
    DepartmentId: string
    DepartmentName: string
    DepartmentCode: string
    PurchaseDate: string
    UsingStartedDate: string
    Cost: number | string
    AccumulationDepreciation: number | string
    RemainderCost: number | string
    Quantity: number | string
    DepreciationRate: number | string
    YearDepreciation: number | string
    TrackedYear: number | string
    LifeTime: number | string
}

export interface FixedAssetFilter {
    FixedAssetCodeOrName?: string
    FixedAssetCategoryName?: string
    FixedAssetIdIgnores?: string[]

    TransferAssetDetailIdIgnores?: string[]
    DepartmentName?: string
    PageLimit: number
    PageNumber: number
}

export interface FixedAssetPaging {
    FixedAssetTotal: number
    FixedAssets: FixedAsset[]
}
