import { readonly, ref } from 'vue'
import { defineStore } from 'pinia'
import type { Department } from '@/types'
import { departmentAPI } from '@/api'

export const useDepartment = defineStore('department', () => {
    const departments = ref<Array<Department>>([])

    const getAllDepartment = async () => {
        const response = await departmentAPI.getAllDepartment()
        departments.value = JSON.parse(JSON.stringify(response.data))
    }

    const getDepartmentByName = (departmentName: string) =>
        departments.value.find((department) => department.DepartmentName === departmentName) || {
            DepartmentId: '',
            DepartmentName: '',
            DepartmentCode: ''
        }

    return {
        departments: readonly(departments),
        getAllDepartment,
        getDepartmentByName
    }
})
