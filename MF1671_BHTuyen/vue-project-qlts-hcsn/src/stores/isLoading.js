import { defineStore } from 'pinia'

export const useIsLoading = defineStore('isLoading', {
    state: () => ({ isLoading: false }),
    actions: {
        setIsLoading(isLoading) {
            this.isLoading = isLoading
        }
    }
})
