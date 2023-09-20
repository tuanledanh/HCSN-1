<template>
  <el-tooltip :visible="visible" placement="right">
    <template #content>
      <span>{{ content }}</span>
    </template>
    <router-link
      :to="'/' + url"
      :class="[
        { 'sidebar-item': item },
        { 'sidebar-top': top },
        { 'link-active': subItems?.some((item) => item.url === route) },
      ]"
      @mouseenter="visible = narrow"
      @mouseleave="visible = false"
      @click="btnShowExpand"
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
          <div
            class="sidebar-expand"
            :class="{ reverse: this.openItem == true }"
          >
            <MISAIcon v-if="expand" drop_down_thin></MISAIcon>
          </div>
        </MISATooltip>
      </div>
    </router-link>
  </el-tooltip>
  <div v-if="!narrow && openItem" class="item-expand border-4">
    <router-link
      :to="'/' + subItem.url"
      v-for="subItem in subItems"
      :key="subItem.url"
      class="sidebar-item sub-item"
    >
      <div :class="{ visible: subItem.url === route }">
        <MISAIcon arrow></MISAIcon>
      </div>

      <div class="sidebar-text">
        <span>{{ subItem.content }}</span>
      </div>
    </router-link>
  </div>
</template>
<script>
import { useRoute } from "vue-router";
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
    // Danh sách sub item
    subItems: {
      type: Array,
      default: null,
    },
  },
  data() {
    return {
      // Hiển thị tooltip hay không
      visible: false,
      // Hiển thị danh sách link con của sidebar item
      openItem: false,
      // lưu giá trị cũ của openItem khi thu gọn hay mở rộng sidebar
      openItemResume: false,
    };
  },
  watch: {
    narrow(value) {
      if (value == true) {
        this.openItem = false;
      } else {
        this.openItem = this.openItemResume;
      }
    },
  },
  computed: {
    route() {
      const inputString = useRoute().path;
      return inputString.substring(1);
    },
  },
  methods: {
    btnShowExpand() {
      this.openItem = !this.openItem;
      this.openItemResume = this.openItem;
    },
  },
};
</script>
