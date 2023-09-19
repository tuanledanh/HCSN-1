import { ref } from 'vue'
import { defineStore } from 'pinia'
import { Language } from '@/enum'

export const useLanguage = defineStore('language', () => {
    const language = ref<string>(Language.VN)
    function setLanguage(lang: string) {
        language.value = lang
    }

    return {
        language,
        setLanguage
    }
})
