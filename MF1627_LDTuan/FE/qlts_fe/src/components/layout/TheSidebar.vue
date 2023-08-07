<template>
  <div class="sidebar" :class="isNarrow ? 'sidebar--narrow' : ''">
    <MISASidebarItem
      url="#"
      top
      home
      nonPadding
      content="MISA QLTS"
    ></MISASidebarItem>
    <div class="sidebar-body">
      <MISASidebarItem
        url="#"
        item
        overview
        content="Tổng quan"
        expand
      ></MISASidebarItem>
      <MISASidebarItem
        url="assets"
        item
        asset
        content="Tài sản"
        expand
      ></MISASidebarItem>
      <MISASidebarItem
        url="#"
        item
        asset_HTDB
        content="Tài sản HT-ĐB"
        expand
      ></MISASidebarItem>
      <MISASidebarItem
        url="#"
        item
        tool
        content="Công cụ dụng cụ"
        expand
      ></MISASidebarItem>
      <MISASidebarItem
        url="#"
        item
        category
        content="Danh mục"
      ></MISASidebarItem>
      <MISASidebarItem
        url="#"
        item
        search_sidebar
        content="Tra cứu"
        expand
      ></MISASidebarItem>
      <MISASidebarItem
        url="#"
        item
        report
        content="Báo cáo"
        expand
      ></MISASidebarItem>
    </div>
    <div class="sidebar-foot">
      <MISAIcon narrow @Click="sidebarNarrow()"></MISAIcon>
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
  methods: {

    /**
     * Nếu thực hiện thu gọn/mở rộng sidebar thì thông báo cho các component khác mở rộng/thu gọn lại
     * Author: LDTUAN (02/08/2023)
     */
    sidebarNarrow(){
        this.isNarrow = !this.isNarrow;
        this.$emit("changeWidth", this.isNarrow);
    }
  },
};
</script>
