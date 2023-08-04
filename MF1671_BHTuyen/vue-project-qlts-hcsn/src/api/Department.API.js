import http from '../utils/http'

const URL = 'Department'
const DepartmentAPI = {
    /**
     * @description Lấy tất cả phòng ban
     * @returns
     * @author @buihuytuyen
     */
    getAllDepartment() {
        return http.get(URL)
    },
    /**
     * @description Lấy phòng ban theo id
     * @param {*} id ID phòng ban
     * @returns Thông tin của phòng ban
     * @author @buihuytuyen
     */
    getDepartmentById(id) {
        return http.get(`${URL}/${id}`)
    }
}

export default DepartmentAPI
