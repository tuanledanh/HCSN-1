import http from '../utils/http'

const URL = 'TransferAssets'

const TransferAssetAPI = {
    /**
     * @description Lấy tất cả chứng từ và phân trang
     * @author @LB.Thành (06/09/2023)
     */
    getAllTransferAssetPaging(pageLimit, pageNumber) {
        return http.get(URL, {
            params: {
                PageLimit: pageLimit,
                PageNumber: pageNumber
            }
        });
    },
    /**
     * @description Tạo chứng từ mới
     * @author @LB.Thành (06/09/2023)
     */
    createTransferAsset(createTransferAssetObj) {
        return http.post(URL, createTransferAssetObj)
    },

    /**
     * @description Lấy mã chứng từ mới
     * @returns mã chứng từ mới
     * @author @LB.Thành (10/09/2023)
     */
    getTransferAssetCode() {
        return http.get(`${URL}/Code`)
    },

    /**
     * @description Cập nhật chứng từ
     * @author @LB.Thành (06/09/2023)
     */
    updateTransferAsset(id, updateTransferAssetObj) {
        return http.put(`${URL}/${id}`, updateTransferAssetObj)
    },

    /**
     * 
     * @description Xóa nhiều chứng từ
     * @author @LB.Thành (06/09/2023)
     */
    deleteManyTransferAssets(ids){
        return http.delete(URL, {data: ids})
    }

}

export default TransferAssetAPI