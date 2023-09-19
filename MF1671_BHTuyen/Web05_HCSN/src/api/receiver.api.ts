import type { Receiver } from '@/types'
import http from '@/utils/http'

const URL = 'Receivers'

const receiverAPI = {
    // Lấy danh sách phòng ban
    getReceivers(transferAssetId: string) {
        return http.get<Receiver[]>(`${URL}/${transferAssetId}`)
    }
}

export default receiverAPI
