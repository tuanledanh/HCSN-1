import http from '../utils/http'

const URL = 'TransferAssetDetail'

const TransferAssetDetailAPI = {
    /**
     * @description Lấy tất cả chi tiết chứng từ và phân trang theo id
     * @author @LB.Thành (06/09/2023)
     */
    getAllTransferAssetDetailPaging(id) {
        return http.get(URL, {
            params: {
                id: id
            }
        });
    }

}

export default TransferAssetDetailAPI