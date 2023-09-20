<template>
  <div @mouseenter="showTooltip" @mouseleave="hideTooltip">
    <el-tooltip
      v-if="bottom_start"
      class="box-item"
      effect="dark"
      :content="content"
      placement="bottom-start"
    >
      <slot></slot>
    </el-tooltip>
    <el-tooltip
      v-if="bottom"
      class="box-item"
      effect="dark"
      :content="content"
      placement="bottom"
      :visible="visible"
    >
      <slot></slot>
    </el-tooltip>
    <el-tooltip
      v-if="bottom_end"
      class="box-item"
      effect="dark"
      :content="content"
      placement="bottom-end"
      :visible="visible"
    >
      <slot></slot>
    </el-tooltip>
    <el-tooltip
      v-if="top"
      class="box-item"
      effect="dark"
      :content="content"
      placement="top"
      :visible="visible"
    >
      <slot></slot>
    </el-tooltip>
    <el-tooltip
      v-if="right"
      class="box-item"
      effect="dark"
      :content="content"
      placement="right"
      :visible="visible"
    >
      <slot></slot>
    </el-tooltip>
  </div>
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
  data() {
    return {
      visible: false,
    };
  },
  methods: {
    showTooltip() {
      this.visible = true;
    },
    hideTooltip() {
      this.visible = false;
    },
  },
};
</script>
