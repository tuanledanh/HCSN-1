<template>
  <div :class="['tooltip', 'tooltip--center', { 'icon-nav': nav }]">
    <div :class="iconName"></div>
    <span class="tooltipText">{{ tooltipText }}</span>
  </div>
</template>
<script>
export default {
  name: "MISAIcon",
  props: {
    tooltipText: {
      type: String,
      default: "",
    },
    nav: {
      type: Boolean,
      default: false,
    },
    iconName: {
      type: String,
      default: "",
    },
  },
};
</script>
