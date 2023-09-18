import { onMounted, onBeforeUnmount, type Ref } from 'vue'

export default function useOnClickOutside(
    componentRef: Ref<HTMLElement | null>,
    callback: () => any
) {
    if (!componentRef) return

    const listen = (event: Event): void => {
        if (
            (event.target as HTMLElement) === componentRef.value ||
            componentRef.value?.contains(event.target as HTMLElement)
        ) {
            return
        }
        callback()
    }

    onMounted(() => {
        document.addEventListener('mouseup', listen)
    })
    onBeforeUnmount(() => {
        document.removeEventListener('mouseup', listen)
    })
}
