<template>
  <router-view :isChangeWidth="isChangeWidth"></router-view>
</template>
<script>
export default {
  name: "TheMain",
  props: {
    // Thay đổi độ rộng
    isChangeWidth: {
      type: Boolean,
      default: false,
    },
  },
};
</script>
