import { defineStore } from 'pinia'

/**
 * Author: Bùi Huy Tuyền (11/7/2023)
 * Khai báo store yearFollow
 */
export const useYearOfTrack = defineStore('yearOfTrack', {
    state: () => ({ yearOfTrack: new Date().getFullYear() }),
    actions: {
        /**
         * Author: Bùi Huy Tuyền (11/7/2023)
         * Hàm tăng năm theo năm hiện tại
         */
        increase() {
            this.yearOfTrack++
        },
        /**
         * Author: Bùi Huy Tuyền (11/7/2023)
         * Hàm giảm năm theo năm hiện tại
         */
        decrease() {
            this.yearOfTrack--
        }
    }
})
