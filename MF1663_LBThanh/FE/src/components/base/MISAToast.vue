import { ref } from 'vue';
<template>
    <section
        class="t-toast br-4 flex-column"
        :class="{ 't-toast_success': typeToast === 'success' }"
        @keydown.stop="(event) => $emit('keydown', event)"
        ref="toast"
        tabindex="3"
    >
        <main
            class="t-toast__content center-y"
            :class="{ 't-toast__content_success': typeToast === 'success' }"
        >
            <section class="t-toast--icon-warning center" v-if="typeToast === 'warning'">
                <section class="icon warning"></section>
            </section>
            <section class="t-toast--icon-success center" v-if="typeToast === 'success'">
                <section class="icon success"></section>
            </section>
            <section class="t-toast--icon-error center" v-if="typeToast === 'error'">
                <section class="icon error"></section>
            </section>
            <p class="t-toast--title">
                <span class="bold">{{ messageLeft }}</span> {{ content }}
                <span class="bold">{{ messageRight }}</span>
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
     * Author: LB.Thành (04/07/2023)
     * ĐỊnh nghĩa props cho component
     */
    props: {
        // Loại toast
        typeToast: {
            type: String,
            default: 'warning',
            validator: function (value) {
                return ['success', 'warning', 'error'].indexOf(value) !== -1
            }
        },
        // Nội dung toast
        content: {
            type: String,
            default: ''
        },
        // Nội dung bên trái
        messageLeft: {
            type: String
        },
        // Nội dung bên phải
        messageRight: {
            type: String
        }
    }
}
</script>
