<template>
  <section class="absolute tooltip br-4" :class="classTooltip">
    <section class="tooltip__wrapper relative br-4">
      <span class="tooltip__content">{{ content }}</span>
    </section>
  </section>
</template>

<script>
export default {
  name: "MISATooltip",
  props: {
    // Nội dung
    content: {
      type: String,
      default: "",
    },

    // Vị trí
    position: {
      type: String,
      default: "bot",
    },
  },
  computed: {
    /**
     * Chọn class tương ứng với vị trí đã chọn
     * Author: LDTUAN (02/08/2023)
     */
    classTooltip() {
      return `tooltip--${this.position}`;
    },
  },
};
</script>
<style>
.tooltip {
  display: none;
  background-color: rgb(25, 169, 205);
  width: auto;
  box-shadow: 0 2px 6px rgb(0, 0, 0);
  z-index: 999;
}

.tooltip--bot {
  top: calc(100% + 5px);
  left: 50%;
  transform: translate(-50%, 0);
  min-width: 80px;
  box-shadow: 0 2px 6px rgba(0, 0, 0, 0.16);
}

.tooltip--bot-head {
  top: calc(60% + 5px);
  min-width: 80px;
  box-shadow: 0 2px 6px rgba(0, 0, 0, 0.16);
  left: 2%;
}

.tooltip--right {
  min-width: 100px;
  right: 0;
  top: 50%;
  transform: translate(calc(100% + 5px), -50%);
}
.tooltip--top {
  min-width: 150px;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -150%);
}

.tooltip--left{
  justify-content: left;
}

.tooltip--left-300 {
  top: calc(100% + 5px);
  left: -300%;
  top: 300%;
  transform: translate(-50%, 0);
  min-width: 80px;
  box-shadow: 0 2px 6px rgba(0, 0, 0, 0.16);
}

.tooltip--left-10 {
  top: calc(100% + 5px);
  left: -10%;
  transform: translate(-50%, 0);
  min-width: 80px;
  box-shadow: 0 2px 6px rgba(0, 0, 0, 0.16);
}

.tooltip__content {
  font-size: 14px;
  color: rgb(255, 255, 255);
  width: auto;
}

.tooltip__wrapper {
  display: flex;
  align-items: center;
  justify-content: center;
  text-align: center;
  line-height: 20px;
  font-size: 1.6rem;
  height: 20px;
  padding: 0 5px;
  width: auto;
}

.tooltip--bottom .tooltip__wrapper::before {
  content: "";
  position: absolute;
  top: -10px;
  left: 50%;
  transform: translateX(-50%);
  border-color: transparent transparent rgba(26, 164, 200, 0.2) transparent;
  border-style: solid;
  border-width: 5px 5px;
}
.tooltip--right .tooltip__wrapper::before {
  content: "";
  position: absolute;
  left: -10px;
  top: 50%;
  transform: translateY(-50%);
  border-color: transparent rgba(26, 164, 200, 0.2) transparent transparent;
  border-style: solid;
  border-width: 5px 5px;
}
</style>
