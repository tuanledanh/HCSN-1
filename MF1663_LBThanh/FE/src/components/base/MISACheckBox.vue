<template>
    <input
        type="checkbox"
        class="t-checkbox"
        :checked="checked"
        @dblclick.stop
        @change="$emit('change', !checked)"
        :title="title"
    />
</template>

<script>
export default {
    name: 'MISACheckBox',
    /**
     * @typedef {Object} Props
     */
    props: {
        /**
         * @type {Boolean}
         * @default false
         * @description Trạng thái của checkbox
         */
        checked: {
            type: Boolean,
            default: false
        },
        title: {
            type: String,
            default: ''
        }
    },
    emits: ['change']
}
</script>
