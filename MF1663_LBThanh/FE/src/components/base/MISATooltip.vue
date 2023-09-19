<template>
    <section class="absolute t-tooltip br-4" :class="classTooltip">
        <section class="t-tooltip__wrapper relative br-4 center">
            <h1 class="t-tooltip__content">{{ content }}</h1>
        </section>
    </section>
</template>

<script>
export default {
    name: 'MISATooltip',
    /**
     * Author: LB.Thành (04/07/2023)
     * Định nghĩa props cho component
     */
    props: {
        // Nội dung tooltip
        content: {
            type: String,
            default: 'hehhhf'
        },
        // Loại tooltip
        type: {
            type: String,
            default: 'bottom'
        }
    },
    computed: {
        /**
         * Author: LB.Thành (011/07/2023)
         * Định nghĩa class cho component
         */
        classTooltip() {
            return `t-tooltip--${this.type}`
        }
    }
}
</script>
