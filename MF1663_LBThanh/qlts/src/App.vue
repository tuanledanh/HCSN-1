<template>
  <div>
    <TheSidebar
      :collapsed="this.collapsed"
      @collapsed="handleCollapsed"
    ></TheSidebar>
    <TheHeader :class="{ 'collapsed-width': this.collapsed }"></TheHeader>
    <TheMain :class="{ 'collapsed-width': this.collapsed }"></TheMain>
  </div>
</template>

<script>
import TheSidebar from "./components/layout/TheSidebar.vue";
import TheHeader from "./components/layout/TheHeader.vue";
import TheMain from "./components/layout/TheMain.vue";

export default {
  name: "App",
  components: {
    TheSidebar,
    TheHeader,
    TheMain,
  },
  data() {
    return {
      collapsed: false,
    };
  },
  methods: {
    handleCollapsed(data) {
      this.collapsed = data;
    },
  },
};
</script>

<style>
@import url(./assets/css/main.css);
</style>
