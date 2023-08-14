<template>
  <el-tooltip v-if="bottom_start"
    class="box-item"
    effect="dark"
    :content="content"
    placement="bottom-start"
  >
    <slot></slot>
  </el-tooltip>
  <el-tooltip v-if="bottom"
    class="box-item"
    effect="dark"
    :content="content"
    placement="bottom"
  >
    <slot></slot>
  </el-tooltip>
  <el-tooltip v-if="bottom_end"
    class="box-item"
    effect="dark"
    :content="content"
    placement="bottom-end"
  >
    <slot></slot>
  </el-tooltip>
  <el-tooltip v-if="top"
    class="box-item"
    effect="dark"
    :content="content"
    placement="top"
  >
    <slot></slot>
  </el-tooltip>
  <el-tooltip v-if="right"
    class="box-item"
    effect="dark"
    :content="content"
    placement="right"
  >
    <slot></slot>
  </el-tooltip>
</template>
<script>
export default {
  name: "MISATooltip",
  props: {
    // Nội dung của tooltip
    content: {
      type: String,
      default: "",
    },
    // Vị trí hiển thị, góc trái dưới
    bottom_start: {
      type: Boolean,
      default: false,
    },
    // Vị trí hiển thị, ở dưới
    bottom: {
      type: Boolean,
      default: false,
    },
    // Vị trí hiển thị, góc phải dưới
    bottom_end: {
      type: Boolean,
      default: false,
    },
    // Vị trí hiển thị, bên trên
    top: {
      type: Boolean,
      default: false,
    },
    // Vị trí hiển thị, bên phải
    right: {
      type: Boolean,
      default: false,
    },
  },
};
</script>
