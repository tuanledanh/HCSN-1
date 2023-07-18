<template>
    <section class="main w-100 h-100 flex-column">
        <TheHeader />
        <section class="main__layout w-100 px-20">
            <router-view />
        </section>
    </section>
</template>

<script>
import TheHeader from './TheHeader.vue'

export default {
    name: 'TheMain',
    components: {
        TheHeader
    }
}
</script>
