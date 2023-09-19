<template>
    <section class="t-paging center-y" :class="classPaging">
        <section
            class="wrapper-icon changePageNumber decreasePageNumber"
            :class="{ disable: pageNumber == 1 }"
            @click="decreasePageNumber"
            data-title="Trang trước"
            title="Trang trước"
        >
            <section class="icon footer__left__icon-prev"></section>
        </section>

        <section class="number__page center-y">
            <section
                class="wrapper-icon"
                :class="{ 'number__page--active': pageNumber === pageNumberStart }"
                @click="
                    start !== '...'
                        ? pageNumber == pageNumberEnd || pageNumber == 2
                            ? ((pageNumber = 1), (pageNumberStart = 1), (pageNumberMiddle = 2))
                            : start > 1
                            ? ((pageNumber = start),
                              (pageNumberMiddle = pageNumber),
                              (pageNumberStart = pageNumber - 1))
                            : ((pageNumber = 1), (pageNumberStart = 1), (pageNumberMiddle = 2))
                        : null
                "
            >
                {{ start }}
            </section>
            <section
                v-if="pageNumberEnd != 2 && pageNumberEnd != 1"
                class="wrapper-icon"
                :class="{ 'number__page--active': pageNumber === pageNumberMiddle }"
                @click="middle !== '...' ? (pageNumber = middle) : null"
            >
                {{ middle }}
            </section>
            <section
                v-if="pageNumberEnd != 3 && pageNumberEnd != 2 && pageNumberEnd != 1"
                class="wrapper-icon"
                :class="{ 'number__page--active': pageNumber === pageNumberHide }"
                @click="pageNumberHide !== '...' ? (pageNumber = pageNumberHide) : null"
            >
                {{ pageNumberHide }}
            </section>
            <section
                v-if="pageNumberEnd != 1"
                class="wrapper-icon"
                :class="{ 'number__page--active': pageNumber === pageNumberEnd }"
                @click="
                    start !== '...'
                        ? ((pageNumber = pageNumberEnd),
                          (pageNumberMiddle = pageNumber - 2),
                          (pageNumberStart = pageNumberMiddle - 1))
                        : null
                "
            >
                {{ pageNumberEnd }}
            </section>
        </section>

        <section
            class="wrapper-icon changePageNumber increasePageNumber"
            :class="{ disable: pageNumber == pageNumberEnd || pageNumberEnd == 1 }"
            @click="increasePageNumber"
            data-title="Trang sau"
            title="Trang sau"
        >
            <section class="icon footer__left__icon-next"></section>
        </section>
    </section>
</template>

<script>
export default {
    name: 'MISAPaging',
    /**
     * Author: LB.Thành (04/07/2023)
     * ĐỊnh nghĩa props cho component
     */
    props: {
        // Class Paging
        classPaging: {
            type: String,
            default: ''
        },
        // Số trang hiện tại
        modelValue: {
            type: Number,
            default: 0
        },
        // Số trang cuối
        numberEnd: {
            type: Number,
            default: 0
        }
    },
    data: function () {
        return {
            pageNumber: 1,
            pageNumberStart: 1,
            pageNumberMiddle: 2,
            pageNumberEnd: 0
        }
    },
    methods: {
        // Tăng số trang lên 1
        increasePageNumber() {
            if (this.pageNumberEnd == this.pageNumber) return
            if (this.pageNumber > 1 && this.pageNumber < this.pageNumberEnd - 2) {
                this.pageNumberStart++
                this.pageNumberMiddle++
                this.pageNumber++
            } else if (
                this.pageNumber === 1 ||
                (this.pageNumber >= this.pageNumberEnd - 2 && this.pageNumber < this.pageNumberEnd)
            ) {
                this.pageNumber++
            }
        },
        // Giảm số trang xuống 1
        decreasePageNumber() {
            if (this.pageNumber == 1) return
            if (this.pageNumber > 0) {
                if (this.pageNumber >= this.pageNumberStart) {
                    if (this.pageNumber > 2 && this.pageNumber < this.pageNumberEnd - 1) {
                        this.pageNumberStart--
                        this.pageNumberMiddle--
                        this.pageNumber--
                    } else if (
                        this.pageNumber === 2 ||
                        (this.pageNumber >= this.pageNumberEnd - 1 &&
                            this.pageNumber <= this.pageNumberEnd)
                    ) {
                        this.pageNumber--
                    }
                }
            }
        }
    },
    mounted() {
        this.pageNumberEnd = this.numberEnd
    },
    updated() {
    },
    watch: {
        pageNumber(value) {
            if (this.pageNumber == 1) {
                this.pageNumberStart = 1
                this.pageNumberMiddle = 2
            }
            this.$emit('update:modelValue', value)
        },
        modelValue(value) {
            this.pageNumber = value
        },
        numberEnd(value) {
            this.pageNumberEnd = value
        }
    },
    computed: {
        start() {
            return this.pageNumberEnd == 4 || this.pageNumberEnd == 3 || this.pageNumberEnd == 2
                ? 1
                : this.pageNumber === this.pageNumberEnd - 2
                ? '...'
                : this.pageNumber === this.pageNumberEnd - 1 ||
                  this.pageNumber === this.pageNumberEnd
                ? 1
                : this.pageNumberStart
        },
        middle() {
            return this.pageNumberEnd == 4 || this.pageNumberEnd == 3
                ? 2
                : this.pageNumber === this.pageNumberEnd - 1 ||
                  this.pageNumber === this.pageNumberEnd
                ? '...'
                : this.pageNumberMiddle
        },
        pageNumberHide() {
            if (this.pageNumberEnd == 4) return 3
            if (this.pageNumberEnd == 3) return 3

            return this.pageNumber === this.pageNumberEnd - 1 ||
                this.pageNumber === this.pageNumberEnd - 2 ||
                this.pageNumber === this.pageNumberEnd ||
                this.pageNumberEnd === 4
                ? this.pageNumberEnd - 1
                : '...'
        }
    }
}
</script>
