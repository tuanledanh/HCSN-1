<template>
  <el-tooltip :visible="visible" placement="right">
    <template #content>
      <span>{{ content }}</span>
    </template>
    <router-link
      :to="'/' + url"
      :class="[{ 'sidebar-item': item }, { 'sidebar-top': top }]"
      @mouseenter="visible = narrow"
      @mouseleave="visible = false"
    >
      <MISAIcon
        sidebar
        :home="home"
        :overview="overview"
        :asset="asset"
        :asset_HTDB="asset_HTDB"
        :tool="tool"
        :category="category"
        :search_sidebar="search_sidebar"
        :report="report"
      ></MISAIcon>
      <div
        :class="['sidebar-text', { 'sidebar-text-non-padding': nonPadding }]"
      >
        <span>{{ content }}</span>
        <MISATooltip bottom content="Mở rộng">
          <MISAIcon v-if="expand" drop_down_thin></MISAIcon>
        </MISATooltip>
      </div>
    </router-link>
  </el-tooltip>
</template>
<script>
export default {
  name: "MISASidebarItem",
  props: {
    // Đường dẫn
    url: {
      type: String,
      default: "",
    },
    // Sidebar thu gọn
    narrow: {
      type: Boolean,
      default: false,
    },

    // Vị trí
    top: {
      type: Boolean,
      default: false,
    },

    // Xem có phải là các danh mục hay không
    item: {
      type: Boolean,
      default: false,
    },

    // Nội dung
    content: {
      type: String,
      default: "",
    },

    // Icon trang chủ
    home: {
      type: Boolean,
      default: false,
    },

    // Icon tổng quan
    overview: {
      type: Boolean,
      default: false,
    },

    // Icon tài sản
    asset: {
      type: Boolean,
      default: false,
    },

    // Icon tài sản HT-ĐB
    asset_HTDB: {
      type: Boolean,
      default: false,
    },

    // Icon công cụ dụng cụ
    tool: {
      type: Boolean,
      default: false,
    },

    // Icon danh mục
    category: {
      type: Boolean,
      default: false,
    },

    // Icon tra cứu
    search_sidebar: {
      type: Boolean,
      default: false,
    },

    // Icon báo cáo
    report: {
      type: Boolean,
      default: false,
    },

    // Icon mở rộng
    expand: {
      type: Boolean,
      default: false,
    },

    // Không cần padding
    nonPadding: {
      type: Boolean,
      default: false,
    },
  },
  data() {
    return {
      // Hiển thị tooltip hay không
      visible: false,
    };
  },
};
</script>
