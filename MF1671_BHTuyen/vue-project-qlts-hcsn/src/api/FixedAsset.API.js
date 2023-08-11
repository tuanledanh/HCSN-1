import http from '../utils/http'

const URL = 'FixedAsset'

const FixedAssetAPI = {
    /**
     * @description Lấy tất cả tài sản cố định
     * @author @buihuytuyen
     */
    getAllFixedAsset() {
        return http.get(URL)
    },
    /**
     * @description Lấy tài sản cố định theo id
     * @param {*} fixedAssetId
     * @returns
     * @author @buihuytuyen
     */
    getFixedAssetById(fixedAssetId) {
        return http.get(`${URL}/${fixedAssetId}`)
    },
    /**
     * @description Lấy mã tài sản cố định
     * @returns Lấy mã tài sản cố định
     * @author @buihuytuyen
     */
    getFixedAssetCode() {
        return http.get(`${URL}/Code`)
    },
    /**
     * @description Lấy số lượng tài sản cố định theo điều kiện lọc
     * @param {*} fixedAssetFilter
     * @returns Lấy số lượng tài sản cố định theo điều kiện lọc
     * @author @buihuytuyen
     */
    getFixedAssetPaging(fixedAssetFilter) {
        return http.get(`${URL}/Paging`, { params: fixedAssetFilter })
    },
    /**
     * @description Lấy số lượng tài sản cố định theo điều kiện lọc
     * @param {*} fixedAssetFilter
     * @returns Lấy số lượng tài sản cố định theo điều kiện lọc
     * @author @buihuytuyen
     */
    getFixedAssetFilter(fixedAssetFilter) {
        return http.get(`${URL}/Filter`, { params: fixedAssetFilter })
    },
    /**
     * @description Tạo mới một tài sản cố định
     * @param {*} createFixedAsset
     * @returns
     * @author @buihuytuyen
     */
    createFixedAsset(createFixedAsset) {
        return http.post(URL, createFixedAsset)
    },
    /**
     * @description Cập nhật một tài sản cố định
     * @param {*} fixedAssetId ID tài sản cố định
     * @param {*} updateCreate Tài sản cố định cần cập nhật
     * @author @buihuytuyen
     * @returns
     */
    updateFixedAsset(fixedAssetId, updateCreate) {
        return http.put(`${URL}/${fixedAssetId}`, updateCreate)
    },
    /**
     * @description Xóa một tài sản cố định
     * @param {*} fixedAssetId ID tài sản cố định
     * @author @buihuytuyen
     * @returns
     */
    deleteFixedAssetById(fixedAssetId) {
        return http.delete(`${URL}/${fixedAssetId}`)
    },
    /**
     * @description Xóa nhiều tài sản cố định
     * @param {*} listFixedAssetId Danh sách Id cần xóa
     * @author @buihuytuyen
     * @returns
     */
    deleteManyFixedAsset(listFixedAssetId) {
        return http.delete(`${URL}`, { data: listFixedAssetId })
    },
    /**
     * @description Export dữ liệu tài sản cố định
     * @param {*} listFixedAssetId Danh sách Id cần export
     * @returns
     * @author @buihuytuyen
     */
    exportExcelFixedAsset(listFixedAssetId) {
        return http.post(`${URL}/Export`, listFixedAssetId, {
            responseType: 'blob'
        })
    }
}

export default FixedAssetAPI
