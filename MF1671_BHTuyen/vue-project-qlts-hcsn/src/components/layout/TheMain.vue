<template>
    <section class="main w-100 flex-column center">
        <!-- Header -->
        <TheHeader />
        <!-- Main layout -->
        <section class="main__layout w-100 px-20">
            <router-view />
            <section class="blur center" v-if="isLoading">
                <section class="loader"></section>
            </section>
        </section>
    </section>
</template>

<script>
import TheHeader from './TheHeader.vue'
import { useIsLoading } from '/src/stores/isLoading.js'
export default {
    name: 'TheMain',
    components: {
        TheHeader
    },
    computed: {
        // Lấy dữ liệu từ trong store
        // Kiểm tra xem có đang loading hay không
        // Author: Bùi Huy Tuyền (11/07/2023)
        isLoading() {
            return useIsLoading().isLoading
        }
    },
    updated() {
        console.log('Update Main')
    }
}
</script>
