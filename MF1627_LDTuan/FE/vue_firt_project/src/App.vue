<template>
  <!-- Layout start -->
  <TheNavbar :class="isChangeWidth ? 'navbar-expand' : ''"></TheNavbar>
  <TheSidebar @changeWidth="changeWidthNavMain($event)"></TheSidebar>
  <TheMain></TheMain>
  <!-- Layout end -->

  <!-- Base start -->
  <MISAToast></MISAToast>
  <!-- Base end -->
</template>
<script>
// Base start
import MISAToast from '@/components/base/toast/MISAToast.vue';
// Base end

// Layout start
import TheMain from '@/components/layout/TheMain.vue'
import TheNavbar from '@/components/layout/TheNavbar.vue';
import TheSidebar from '@/components/layout/TheSidebar.vue';
// Layout end 
export default {
  name: "App",
  data() {
    return {
      isChangeWidth: null,
    }
  },
  components: {
    // Base start
    MISAToast,
    // Base end

    // Layout start
    TheMain,
    TheNavbar,
    TheSidebar
    // Layout end
  },
  methods: {
    changeWidthNavMain(isChangeWidth){
      this.isChangeWidth = isChangeWidth;
    }
  },
};
</script>

<style>
  @import url(./css/main.css);
</style>
