<template>
    <section class="t-toast br-4 flex-column">
        <main class="t-toast__content center-y">
            <section class="t-toast--icon-warning center" v-if="typeToast === 'warning'">
                <section class="icon warning"></section>
            </section>
            <section class="t-toast--icon-success center" v-if="typeToast === 'success'">
                <section class="icon success"></section>
            </section>
            <p class="t-toast--title">
                <span class="bold">{{ messageLeft }}</span
                >{{ content }} <span class="bold">{{ messageRight }}</span>
            </p>
        </main>
        <footer class="t-toast__footer center-y" v-if="typeToast === 'warning'">
            <slot> </slot>
        </footer>
    </section>
</template>

<script>
export default {
    name: 'MISAToast',
    /**
     * Author: Bùi Huy Tuyền (04/07/2023)
     * ĐỊnh nghĩa props cho component
     */
    props: {
        typeToast: {
            type: String,
            default: 'success',
            validator: function (value) {
                return ['success', 'warning', 'error'].indexOf(value) !== -1
            }
        },
        content: {
            type: String,
            default: ''
        },
        messageLeft: {
            type: String
        },
        messageRight: {
            type: String
        }
    }
}
</script>
