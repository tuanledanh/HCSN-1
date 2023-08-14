<template>
  <el-tooltip :visible="display" placement="right">
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
    display: {
      type: Boolean,
      default: false,
    },
  },
  data() {
    return {
    };
  },
};
</script>
