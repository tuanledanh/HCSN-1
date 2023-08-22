<template>
  <el-tooltip :visible="visible" placement="bottom">
    <template #content>
      <span>{{ content }}</span>
    </template>
    <slot></slot>
  </el-tooltip>
</template>
<script>
export default {
  name: "MISATooltipVisible",
  props: {
    // Nội dung của tooltip
    content: {
      type: String,
      default: "",
    },
    // Nhìn thấy tooltip hoặc không
    visible: {
      type: Boolean,
      default: false,
    },
  },
};
</script>
