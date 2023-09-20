<template>
  <div class="sidebar" :class="isNarrow ? 'sidebar--narrow' : ''">
    <MISASidebarItem
      url="#"
      top
      home
      nonPadding
      :narrow="isNarrow"
      content="MISA QLTS"
    ></MISASidebarItem>
    <div class="sidebar-body">
      <MISASidebarItem
        url="/a"
        item
        overview
        :narrow="isNarrow"
        content="Tổng quan"
        expand
      ></MISASidebarItem>
      <MISASidebarItem
        url="assets"
        item
        asset
        :narrow="isNarrow"
        content="Tài sản"
        expand
        :subItems="this.$_MISAResource.VN.Asset"
      ></MISASidebarItem>
      <MISASidebarItem
        url="#"
        item
        asset_HTDB
        :narrow="isNarrow"
        content="Tài sản HT-ĐB"
        expand
      ></MISASidebarItem>
      <MISASidebarItem
        url="#"
        item
        tool
        :narrow="isNarrow"
        content="Công cụ dụng cụ"
        expand
      ></MISASidebarItem>
      <MISASidebarItem
        url="#"
        item
        category
        :narrow="isNarrow"
        content="Danh mục"
      ></MISASidebarItem>
      <MISASidebarItem
        url="#"
        item
        search_sidebar
        :narrow="isNarrow"
        content="Tra cứu"
        expand
      ></MISASidebarItem>
      <MISASidebarItem
        url="#"
        item
        report
        :narrow="isNarrow"
        content="Báo cáo"
        expand
      ></MISASidebarItem>
    </div>
    <div class="sidebar-foot">
      <MISATooltip right :content="content">
        <div class="sidebar__icon">
          <MISAIcon narrow @Click="sidebarNarrow()"></MISAIcon>
        </div>
      </MISATooltip>
    </div>
  </div>
</template>
<script>
export default {
  name: "TheSidebar",
  data() {
    return {
      // Thu gọn hay không
      isNarrow: false,
    };
  },
  computed: {
    // Thay đổi nội dung tooltip tương ứng với trường hợp thu hẹp hay mở rộng
    content() {
      if (this.isNarrow) {
        return "Mở rộng";
      } else {
        return "Thu gọn";
      }
    },
  },
  methods: {
    /**
     * Nếu thực hiện thu gọn/mở rộng sidebar thì thông báo cho các component khác mở rộng/thu gọn lại
     * Author: LDTUAN (02/08/2023)
     */
    sidebarNarrow() {
      this.isNarrow = !this.isNarrow;
      this.$emit("changeWidth", this.isNarrow);
    },
  },
};
</script>
