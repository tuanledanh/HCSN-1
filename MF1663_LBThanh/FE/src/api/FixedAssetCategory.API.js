import http from '../utils/http'

const URL = 'FixedAssetCategory'

const FixedAssetCategoryAPI = {
    /**
     * @description Lấy tất cả danh mục tài sản cố định
     * @returns Lấy tất cả danh mục tài sản cố định
     * @LB.Thành
     */
    getAllFixedAssetCategory() {
        return http.get(URL)
    },
    /**
     * @description Lấy danh mục tài sản cố định theo id
     * @param {*} id ID danh mục tài sản cố định
     * @returns
     * @LB.Thành
     */
    getFixedAssetCategoryById(id) {
        return http.get(`${URL}/${id}`)
    }
}

export default FixedAssetCategoryAPI
