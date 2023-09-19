import http from '../utils/http'

const URL = 'FixedAssets'

const FixedAssetAPI = {
    /**
     * @description Lấy tất cả tài sản cố định
     * @author @LB.Thành (06/08/2023)
     */
    getAllFixedAsset() {
        return http.get(URL)
    },
    /**
     * @description Lấy tài sản cố định theo id
     * @param {*} fixedAssetId
     * @returns
     * @author @LB.Thành (06/08/2023)
     */
    getFixedAssetById(fixedAssetId) {
        return http.get(`${URL}/${fixedAssetId}`)
    },
    /**
     * @description Lấy mã tài sản cố định
     * @returns Lấy mã tài sản cố định
     * @author @LB.Thành (06/08/2023)
     */
    getFixedAssetCode() {
        return http.get(`${URL}/Code`)
    },
    /**
     * @description Lấy số lượng tài sản cố định theo điều kiện lọc
     * @param {*} fixedAssetFilter
     * @returns Lấy số lượng tài sản cố định theo điều kiện lọc
     * @author @LB.Thành (06/08/2023)
     */
    getFixedAssetPaging(fixedAssetFilter) {
        return http.get(`${URL}/Filter`, { params: fixedAssetFilter })
    },
    /**
     * @description Tạo mới một tài sản cố định
     * @param {*} createFixedAsset
     * @returns
     * @author @LB.Thành (06/08/2023)
     */
    createFixedAsset(createFixedAsset) {
        return http.post(URL, createFixedAsset)
    },
    /**
     * @description Cập nhật một tài sản cố định
     * @param {*} fixedAssetId ID tài sản cố định
     * @param {*} updateCreate Tài sản cố định cần cập nhật
     * @author @LB.Thành (06/08/2023)
     * @returns
     */
    updateFixedAsset(fixedAssetId, updateCreate) {
        return http.put(`${URL}/${fixedAssetId}`, updateCreate)
    },
    /**
     * @description Xóa một tài sản cố định
     * @param {*} fixedAssetId ID tài sản cố định
     * @author @LB.Thành (06/08/2023)
     * @returns
     */
    deleteFixedAssetById(fixedAssetId) {
        return http.delete(`${URL}/${fixedAssetId}`)
    },
    /**
     * @description Xóa nhiều tài sản cố định
     * @param {*} listFixedAssetId Danh sách Id cần xóa
     * @author @LB.Thành (06/08/2023)
     * @returns
     */
    deleteManyFixedAsset(listFixedAssetId) {
        return http.delete(`${URL}`, { data: listFixedAssetId })
    },
    /**
     * @description Export dữ liệu tài sản cố định
     * @param {*} listFixedAssetId Danh sách Id cần export
     * @returns
     * @author @LB.Thành (06/08/2023)
     */
    exportExcelFixedAsset(listFixedAssetId) {
        return http.post(`${URL}/Export`, listFixedAssetId, {
            responseType: 'blob'
        })
    },

    getFixedAssetForTransfer(data) {
        return http.post(`${URL}/FilterForTransfer`, data)
    }
}

export default FixedAssetAPI
