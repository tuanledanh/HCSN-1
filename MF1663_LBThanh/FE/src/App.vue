<script>
import TheSidebar from '@/components/layout/TheSidebar.vue'
import TheMain from '@/components/layout/TheMain.vue'

export default {
    name: 'App',
    components: {
        TheSidebar,
        TheMain
    }
}
</script>
<template>
    <main class="app grid relative">
        <TheSidebar />
        <TheMain />
    </main>
</template>

<style scoped></style>
