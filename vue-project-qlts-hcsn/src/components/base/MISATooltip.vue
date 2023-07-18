<template>
    <section class="absolute t-tooltip br-4" :class="classTooltip">
        <section class="t-tooltip__wrapper relative br-4">
            <h1 class="t-tooltip__content">{{ content }}</h1>
        </section>
    </section>
</template>

<script>
export default {
    name: 'MISATooltip',
    /**
     * Author: Bùi Huy Tuyền (04/07/2023)
     * Định nghĩa props cho component
     */
    props: {
        content: {
            type: String,
            default: 'hehhhf'
        },
        type: {
            type: String,
            default: 'bottom'
        }
    },
    computed: {
        /**
         * Author: Bùi Huy Tuyền (011/07/2023)
         * Định nghĩa class cho component
         */
        classTooltip() {
            return `t-tooltip--${this.type}`
        }
    }
}
</script>
