<template>
    <header class="main__header h-44 center-y bg-white justify-between px-20 w-100">
        <!-- Tiêu đề -->
        <section class="main__header__left fw-700 center-y">Danh sách tài sản</section>
        <section class="main__header__right center-y">
            <!-- Organization -->
            <section class="header__right__title">Sở tài chính</section>

            <!-- Năm hiện tại -->
            <section class="header__right__year center-y br-4">
                <span class="year__title">Năm</span>
                <span class="year fw-700 center-y">{{ yearFollow }}</span>

                <section class="wrapper-icon flex-column row-gap-3">
                    <section class="choose-year-up icon" @click="increase"></section>
                    <section class="choose-year-down icon" @click="decrease"></section>
                </section>
            </section>

            <!--  -->
            <section class="header__right__icon center-y col-gap-20">
                <section class="wrapper-icon relative">
                    <section class="notification icon"></section>
                    <m-tooltip content="Thông báo" />
                </section>
                <section class="wrapper-icon relative">
                    <section class="option icon"></section>
                    <m-tooltip content="Tùy chọn" />
                </section>
                <section class="wrapper-icon relative">
                    <section class="help icon"></section>
                    <m-tooltip content="Trợ giúp" />
                </section>
                <section class="avatar-section center-y col-gap-8">
                    <section class="wrapper-icon relative">
                        <section class="avatar icon"></section>
                        <m-tooltip content="Avatar" />
                    </section>
                    <section class="down icon"></section>
                </section>
            </section>
        </section>
    </header>
</template>

<script>
import { useYearOfTrack } from '/src/stores/yearOfTrack.js'

export default {
    name: 'TheHeader',

    methods: {},
    computed: {
        /**
         * Author: LB.Thành (11/07/2023)
         * Lấy dữ liệu từ trong store
         */
        yearOfTrackStore() {
            return useYearOfTrack()
        },
        /**
         * Lấy năm hiện tại
         */
        yearFollow() {
            return this.yearOfTrackStore.yearOfTrack
        },
        /**
         * Hàm tăng năm
         */
        increase() {
            return this.yearOfTrackStore.increase
        },
        /**
         * Hàm giảm năm
         */
        decrease() {
            return this.yearOfTrackStore.decrease
        }
    },
    updated() {
        console.log('Update Header')
    }
}
</script>
