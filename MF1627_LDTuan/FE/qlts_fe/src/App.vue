<template>
  <TheNavbar
    :class="isChangeWidth ? 'navbar--expand' : ''"
    :contentDisplay="contentDisplay"
    :contentDisplayText="contentDisplayText"
  ></TheNavbar>
  <TheSidebar @changeWidth="changeWidthNavMain($event)"></TheSidebar>
  <TheMain :isChangeWidth="isChangeWidth"></TheMain>
</template>

<script>
import TheMain from "@/components/layout/TheMain.vue";
import TheNavbar from "@/components/layout/TheNavbar.vue";
import TheSidebar from "@/components/layout/TheSidebar.vue";
export default {
  name: "App",
  components: {
    TheMain,
    TheNavbar,
    TheSidebar,
  },
  data() {
    return {
      // Thay đổi độ rộng
      isChangeWidth: false,
      contentDisplay: false,
      contentDisplayText: null,
    };
  },
  created() {
  },
  methods: {
    /**
     * Thực hiện thay đổi độ rộng của các thành phần
     * @param {bool} isChangeWidth
     * Author: LDTUAN (02/08/2023)
     */
    changeWidthNavMain(isChangeWidth) {
      this.isChangeWidth = isChangeWidth;
    },
    handleDisplayContent(data) {
      if (data) {
        this.contentDisplay = true;
        this.contentDisplayText = data.message;
      } else {
        this.contentDisplay = true;
        this.contentDisplayText = "";
      }
    },
  },
  beforeMount() {
    
    this.$_emitter.on("onDisplayContent", this.handleDisplayContent);
  },
  beforeUnmount() {
    
    this.$_emitter.off("onDisplayContent");
  },
};
</script>

<style>
@import url(./css/main.css);
</style>
