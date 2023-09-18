import type {
    TransferAssetFilter,
    TransferAssetPaging,
    TransferAssetCreateFull,
    TransferAssetUpdateFull
} from '@/types'
import http from '@/utils/http'

const URL = 'TransferAsset'

const transferAssetAPI = {
    getTransferAssetCodeNew() {
        return http.get<string>(`${URL}/NewCode`)
    },
    getTransferAssetPaging(transferAssetFilter: TransferAssetFilter) {
        return http.get<TransferAssetPaging>(`${URL}/Paging`, { params: transferAssetFilter })
    },
    createTransferAssetFull(transferAssetCreateFull: TransferAssetCreateFull) {
        return http.post(`${URL}`, transferAssetCreateFull)
    },
    updateTransferAssetFull(transferAssetUpdateFull: TransferAssetUpdateFull) {
        return http.put(`${URL}`, transferAssetUpdateFull)
    },
    deleteTransferAsset(transferAssetId: string) {
        return http.delete(`${URL}/${transferAssetId}`)
    },
    deleteManyTransferAsset(transferAssetIds: string[]) {
        return http.delete(`${URL}`, { data: transferAssetIds })
    }
}

export default transferAssetAPI
