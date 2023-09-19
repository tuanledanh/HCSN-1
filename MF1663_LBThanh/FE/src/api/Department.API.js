import http from '../utils/http'

const URL = 'Departments'
const DepartmentAPI = {
    /**
     * @description Lấy tất cả phòng ban
     * @returns
     * @author @LB.Thành
     */
    getAllDepartment() {
        return http.get(URL)
    },
    /**
     * @description Lấy phòng ban theo id
     * @param {*} id ID phòng ban
     * @returns Thông tin của phòng ban
     * @author @LB.Thành
     */
    getDepartmentById(id) {
        return http.get(`${URL}/${id}`)
    }
}

export default DepartmentAPI
