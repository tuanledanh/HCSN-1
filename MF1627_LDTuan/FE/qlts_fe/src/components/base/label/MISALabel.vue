<template>
  <div class="label">
    <label for="">{{ label }}</label>
    <MISAIcon v-if="required" required></MISAIcon>
  </div>
</template>
<script>
export default {
  name: "MISALabel",
  props: {
    // Nội dung của nhãn dán
    label: {
      type: String,
      default: "",
    },

    // Xem có cần icon bắt buộc không
    required: {
      type: Boolean,
      default: false,
    },
  },
};
</script>
<style scoped>
.label {
  padding-bottom: 8px;
  display: flex;
  flex-direction: row;
  align-items: center;
}

label{
  cursor: pointer !important;
}
</style>
