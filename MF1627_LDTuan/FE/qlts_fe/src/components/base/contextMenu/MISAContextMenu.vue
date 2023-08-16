<template>
  <div
    class="context-menu-container"
    v-show="isShowContextMenu"
    :style="{ top: posY + 'px', left: posX + 'px' }"
  >
    <div class="context">
      <div
        class="context-menu__item"
        v-for="(item, index) in listItem"
        :key="index"
        @click="btnClick(item)"
      >
        <div class="context-menu__item--icon" :class="[{'icon-black': item.text == 'Thêm mới'}]">
          <MISAIcon :iconContextMenu="item.icon"></MISAIcon>
        </div>
        <div class="context-menu__item--text">{{ item.text }}</div>
      </div>
    </div>
  </div>
</template>
<script>
export default {
  name: "MISAContextMenu",
  props: {
    isShowContextMenu: Boolean,
    posX: {
      type: Number,
      default: 0,
    },
    posY: {
      type: Number,
      default: 0,
    },
    listItem: Object,
  },
  data() {
    return {};
  },
  methods: {
    btnClick(item) {
      this.$emit("getItemContext", item.index);
    },
  },
};
</script>
<style scoped>
.context-menu-container {
  position: absolute;
  width: 160px;
  background-color: #ffffff;
  box-shadow: 1px 1px 4px #bbbaba;
  border-radius: 4px;
  z-index: 10;
  box-sizing: border-box;
}
.context {
  width: 100%;
  margin: 8px 0px;
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  row-gap: 2px;
  box-sizing: border-box;
}
.context-menu__item {
  width: 90%;
  height: 39px;
  display: flex;
  align-items: center;
  border-radius: 4px;
  transition: all ease-in-out 0.2s;
}
.context-menu__item:hover {
  cursor: pointer;
  background-color: #c7e0f5;
}
.context-menu__item--icon {
  margin-right: 12px;
  margin-left: 13px;
}
.icon-black{
    margin-left: 18px;
}
</style>
