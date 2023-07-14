<template>
  <div class="label">
    <label for="">{{ label }}</label>
    <div :class="[{'icon--require': required}]"></div>
  </div>
</template>
<script>
export default {
  name: "MISALabel",
  props: {
    label: {
      type: String,
      default: "",
    },
    required: {
      type: Boolean,
      default: false,
    },
  },
};
</script>
