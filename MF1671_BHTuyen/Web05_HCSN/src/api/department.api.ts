import type { Department } from '@/types'
import http from '@/utils/http'

const URL = 'Department'

const departmentAPI = {
    // Lấy danh sách phòng ban
    getAllDepartment() {
        return http.get<Department[]>(URL)
    }
}

export default departmentAPI
