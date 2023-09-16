import type { FixedAsset, FixedAssetPaging, FixedAssetFilter } from '@/types'
import http from '@/utils/http'

const URL = 'FixedAsset'

const fixedAssetAPI = {
    // Lấy danh sách tài sản phân trang
    getFixedAssetPaging(fixedAssetFilter: FixedAssetFilter) {
        return http.get<FixedAssetPaging>(`${URL}/Paging`, { params: fixedAssetFilter })
    },
    getTransferAssetPaging(fixedAssetFilter: FixedAssetFilter) {
        return http.post<FixedAssetPaging>(`${URL}/TransferPaging`, fixedAssetFilter)
    },
    // Lấy mã tài sản mới gợi ý
    getFixedAssetNewCode() {
        return http.get<string>(`${URL}/NewCode`)
    },
    // Tạo mới một tài sản
    createFixedAsset(fixedAsset: FixedAsset) {
        return http.post(URL, fixedAsset)
    },
    // Cập nhật một tài sản
    updateFixedAsset(fixedAsset: FixedAsset) {
        return http.put(`${URL}/${fixedAsset.FixedAssetId}`, fixedAsset)
    },
    // Xóa một tài sản
    deleteFixedAsset(fixedAssetId: string) {
        return http.delete(`${URL}/${fixedAssetId}`)
    },
    // Xóa nhiều tài sản
    deleteManyFixedAsset(fixedAssetIds: string[]) {
        return http.delete(URL, { data: fixedAssetIds })
    },
    // Xuất danh sách tài sản ra file excel
    exportFixedAsset(fixedAssetIds: string[]) {
        return http.post(`${URL}/Export`, fixedAssetIds, { responseType: 'blob' })
    }
}
export default fixedAssetAPI
