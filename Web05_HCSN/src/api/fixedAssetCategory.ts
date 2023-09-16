import type { FixedAssetCategory } from '@/types'
import http from '@/utils/http'

const URL = 'FixedAssetCategory'

const fixedAssetCategoryAPI = {
    // Lấy danh sách loại tài sản
    getAllFixedAssetCategory() {
        return http.get<FixedAssetCategory[]>(URL)
    }
}

export default fixedAssetCategoryAPI
