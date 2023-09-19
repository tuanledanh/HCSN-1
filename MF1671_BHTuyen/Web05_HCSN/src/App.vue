<script setup lang="ts">
import TheSidebar from '@/layout/sidebar'
import TheMain from '@/layout/main'
import vi from 'element-plus/dist/locale/vi.mjs'
import 'dayjs/locale/vi'
import { ElConfigProvider } from 'element-plus'
import { useDepartment } from './stores/department'
import { onMounted } from 'vue'

const { getAllDepartment } = useDepartment()

onMounted(async () => {
    await getAllDepartment()
})
</script>

<template>
    <ElConfigProvider :locale="vi">
        <TheSidebar />
        <TheMain />
    </ElConfigProvider>
</template>
