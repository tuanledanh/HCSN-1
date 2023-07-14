<template>
  <div
    class="sidebar"
    :class="[{ 'collapse-menu-container': isCollapsed }, {sidebarClass}]"
  >
    <div class="sidebar-top" :class="{ 'sidebar-top-mr0': isCollapsed }">
      <div class="home-container">
        <div class="home-icon" @click="collapseMenuToggle"></div>
        <div
          class="app-title"
          :class="{ 'collapse-menu-feature-text': isCollapsed }"
        >
          MISA QLTS
        </div>
      </div>

      <div class="sidebar-icon-container">
        <div class="sidebar-router border-radius" :class="{ w44: isCollapsed }">
          <div class="sidebar-icon">
            <div class="sidebar-icon-inside1"></div>
          </div>
          <div
            class="router-name"
            :class="{ 'collapse-menu-feature-text': isCollapsed }"
          >
            Tổng quan
          </div>
        </div>

        <div
          class="sidebar-router sidebar-router-active border-radius"
          :class="{ w44: isCollapsed }"
        >
          <div class="sidebar-icon">
            <div class="sidebar-icon-inside2"></div>
          </div>
          <div
            class="router-name"
            :class="{ 'collapse-menu-feature-text': isCollapsed }"
          >
            Tài sản
          </div>
        </div>

        <div class="sidebar-router border-radius" :class="{ w44: isCollapsed }">
          <div class="sidebar-icon">
            <div class="sidebar-icon-inside3"></div>
          </div>
          <div
            class="router-name"
            :class="{ 'collapse-menu-feature-text': isCollapsed }"
          >
            Tài sản HT-ĐB
          </div>
        </div>

        <div class="sidebar-router border-radius" :class="{ w44: isCollapsed }">
          <div class="sidebar-icon">
            <div class="sidebar-icon-inside4"></div>
          </div>
          <div
            class="router-name"
            :class="{ 'collapse-menu-feature-text': isCollapsed }"
          >
            Công cụ dụng cụ
          </div>
        </div>

        <div class="sidebar-router border-radius" :class="{ w44: isCollapsed }">
          <div class="sidebar-icon">
            <div class="sidebar-icon-inside5"></div>
          </div>
          <div
            class="router-name"
            :class="{ 'collapse-menu-feature-text': isCollapsed }"
          >
            Danh mục
          </div>
        </div>

        <div class="sidebar-router border-radius" :class="{ w44: isCollapsed }">
          <div class="sidebar-icon">
            <div class="sidebar-icon-inside6"></div>
          </div>
          <div
            class="router-name"
            :class="{ 'collapse-menu-feature-text': isCollapsed }"
          >
            Tra cứu
          </div>
        </div>

        <div class="sidebar-router border-radius" :class="{ w44: isCollapsed }">
          <div class="sidebar-icon">
            <div class="sidebar-icon-inside7"></div>
          </div>
          <div
            class="router-name"
            :class="{ 'collapse-menu-feature-text': isCollapsed }"
          >
            Báo cáo
          </div>
        </div>
      </div>
    </div>

    <div
      class="sidebar-bottom"
      :class="{ 'collapse-menu-feature-text': isCollapsed }"
    >
      <!-- 
      .stop modifier trong @click.stop được sử dụng để ngăn chặn sự lan truyền của sự kiện khi nhấp vào phần tử 
      và chỉ kích hoạt phương thức xử lý sự kiện tương ứng trong phần tử đó.
    -->
      <div
        class="sidebar-bottom-icon-container"
        @click.stop="collapseMenuToggle"
      >
        <div class="left-arrow-block-icon"></div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  props: ["collapsed"],
  data() {
    return {
      isCollapsed: false,
    };
  },
  computed: {
    sidebarClass() {
      return this.collapsed ? "collapsed-width" : "";
    },
  },
  methods: {
    /**
     * Toggle trạng thái của menu
     * Khi được gọi, phương thức này sẽ đảo ngược giá trị của isCollapsed
     * Author: L.B.Thành (1/7/2023)
     */
    collapseMenuToggle() {
      this.isCollapsed = !this.isCollapsed;
      this.$emit("collapsed", this.isCollapsed);
    },
  },
};
</script>
