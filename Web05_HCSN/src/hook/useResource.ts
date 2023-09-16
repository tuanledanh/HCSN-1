import { type ResourceData } from '@/types'
import { useLanguage } from '@/stores'
import resource from '@/resource'

const useResource = (): ResourceData => {
    const { language } = useLanguage()
    return resource[language as keyof typeof resource]
}

export default useResource
