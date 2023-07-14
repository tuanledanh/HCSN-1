<template>
  <div class="sidebar" :class="isNarrow ? 'sidebar-narrow' : ''">
    <router-link to="/" class="sidebar-top">
      <div class="icon-top">
        <div class="icon--home"></div>
      </div>
      <span class="sidebar-text sidebar-text-non-padding"> MISA QLTS </span>
    </router-link>
    <div class="sidebar-body">
      <router-link to="/assets" class="sidebar-item">
        <div class="icon-body tooltip">
          <div class="icon--overview"></div>
          <span class="tooltipText">Tooltip text</span>
        </div>
        <div class="sidebar-text">
          <span>Tổng quan</span>
          <div class="icon--drop-down-thin"></div>
        </div>
      </router-link>
      <router-link to="/#" class="sidebar-item">
        <div class="icon-body tooltip">
          <div class="icon--asset"></div>
        </div>
        <div class="sidebar-text">
          <span>Tài sản</span>
          <div class="icon--drop-down-thin"></div>
        </div>
      </router-link>
      <router-link to="/#" class="sidebar-item">
        <div class="icon-body tooltip">
          <div class="icon--asset-HTDB"></div>
        </div>
        <div class="sidebar-text">
          <span>Tài sản HT-ĐB</span>
          <div class="icon--drop-down-thin"></div>
        </div>
      </router-link>
      <router-link to="/#" class="sidebar-item">
        <div class="icon-body tooltip">
          <div class="icon--tool"></div>
        </div>
        <div class="sidebar-text">
          <span>Công cụ dụng cụ</span>
          <div class="icon--drop-down-thin"></div>
        </div>
      </router-link>
      <router-link to="/#" class="sidebar-item">
        <div class="icon-body tooltip">
          <div class="icon--category"></div>
        </div>
        <div class="sidebar-text">
          <span>Danh mục</span>
        </div>
      </router-link>
      <router-link to="/#" class="sidebar-item">
        <div class="icon-body tooltip">
          <div class="icon--search-sidebar"></div>
        </div>
        <div class="sidebar-text">
          <span>Tra cứu</span>
          <div class="icon--drop-down-thin"></div>
        </div>
      </router-link>
      <router-link to="/#" class="sidebar-item">
        <div class="icon-body tooltip">
          <div class="icon--report"></div>
        </div>
        <div class="sidebar-text">
          <span>Báo cáo</span>
          <div class="icon--drop-down-thin"></div>
        </div>
      </router-link>
    </div>
    <div class="sidebar-foot">
      <div class="icon-foot" @click="sidebarNarrow()">
        <div class="icon--prev-white"></div>
      </div>
    </div>
  </div>
</template>
<script>
export default {
  name: "TheSidebar",
  data() {
    return {
      isNarrow: false,
    }
  },
  methods: {
    /**
     * Thu gọn thanh sidebar, đồng thời thông báo để navbar và main-content thay đổi độ rộng phù hợp
     * Author: LDTUAN (03/07/2023)
     */
    sidebarNarrow(){
      this.isNarrow = !this.isNarrow;
      this.$emit('changeWidth', this.isNarrow);
    }
  },
}
</script>