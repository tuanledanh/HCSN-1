import type {
    TransferAssetDetailPaging,
    TransferAssetDetailFilter,
    TransferAssetDetail
} from '@/types'
import http from '@/utils/http'

const URL = 'TransferAssetDetail'

const transferAssetDetailAPI = {
    getManyByTransferAssetId(transferAssetId: string) {
        return http.get<TransferAssetDetail[]>(`${URL}/${transferAssetId}`)
    },
    // Lấy danh sách tài sản điều chuyển theo phân trang
    getTransferAssetDetailPaging(transferAssetDetailFilter: TransferAssetDetailFilter) {
        return http.get<TransferAssetDetailPaging>(`${URL}/Paging`, {
            params: transferAssetDetailFilter
        })
    },
    // Xóa một tài sản điều chuyển
    deleteTransferAssetDetail(TransferAssetDetailId: string) {
        return http.delete(`${URL}/${TransferAssetDetailId}`)
    }
}

export default transferAssetDetailAPI
