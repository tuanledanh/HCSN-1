<template>
  <div
    :class="[
      'icon',
        { 'icon-background': background },
      { 'icon--disabled': disabled },
      { 'icon--input': input },
      { 'icon--button': button },
      { 'icon--sidebar': sidebar },
      { 'icon--narrow': narrow },
      { 'icon--tick-true': tick_true },
      { 'icon--combobox': combobox },
      { 'icon--combobox-delete': combobox_delete },
      { 'icon--filter': filter },
      { 'icon--navbar': navbar },
    ]"
    @click="onPagingClick"
  >
    <!-- icon tick true này k có trong svg -->
    <svg
      v-if="tick_true"
      xmlns="http://www.w3.org/2000/svg"
      width="24"
      height="24"
      viewBox="0 0 24 24"
    >
      <path
        fill="currentColor"
        d="M21 7L9 19l-5.5-5.5l1.41-1.41L9 16.17L19.59 5.59L21 7Z"
      />
    </svg>
    <div
      v-else
      :class="[
        iconContextMenu,
        { 'icon-search': iconSearch },
        { 'icon-required': required },
        { 'icon-add': add },
        { 'icon-add-box': add_box },
        { 'icon-export': exportIcon },
        { 'icon-delete': deleteIcon },
        { 'icon-notification': notification },
        { 'icon-utilities': utilities },
        { 'icon-question': question },
        { 'icon-user': user },
        { 'icon-drop-down': drop_down },
        { 'icon-pull-up': pull_up },
        { 'icon-home': home },
        { 'icon-overview': overview },
        { 'icon-asset': asset },
        { 'icon-asset-HTDB': asset_HTDB },
        { 'icon-tool': tool },
        { 'icon-category': category },
        { 'icon-search-sidebar': search_sidebar },
        { 'icon-report': report },
        { 'icon-drop-down-thin': drop_down_thin },
        { 'icon-prev-white': narrow },
        { 'icon-filter': filter },
        { 'icon-prev': prev },
        { 'icon-next': next },
        { 'icon-edit': edit },
        { 'icon-copy': copy },
        { 'icon-exit': exit },
        { 'icon-exit--small': exit_small },
        { 'icon-warning': warning },
        { 'icon-success': success },
        { 'icon-export-toast': export_toast },
        { 'icon-date-picker': date_picker },
        { 'icon-loading': loading },
      ]"
    ></div>
  </div>
</template>
<script>
export default {
  name: "MISAIcon",
  props: {
    // Tổng số trang
    totalPages: {
      type: Int32Array,
      default: 0,
    },

    // Trang hiện tại
    currentPage: {
      type: Int32Array,
      default: 1,
    },
    // Disable icon
    disabled: {
      type: Boolean,
      default: false,
    },

    // Xác định xem xài class icon--combobox không
    combobox: {
      type: Boolean,
      default: false,
    },
    // Xác định xem xài class icon--combobox-delete không
    combobox_delete: {
      type: Boolean,
      default: false,
    },

    // Xác định xem xài class icon--input không
    input: {
      type: Boolean,
      default: false,
    },

    // Xác định xem xài class icon--button không
    button: {
      type: Boolean,
      default: false,
    },

    // Xác định xem xài class icon--sidebar không
    sidebar: {
      type: Boolean,
      default: false,
    },

    // Icon tìm kiếm
    iconSearch: {
      type: Boolean,
      default: false,
    },

    // Icon yêu cầu
    required: {
      type: Boolean,
      default: false,
    },

    // Icon thêm mới
    add: {
      type: Boolean,
      default: false,
    },
    // Icon thêm mới có border xung quanh
    add_box: {
      type: Boolean,
      default: false,
    },

    // Icon xuất excel
    exportIcon: {
      type: Boolean,
      default: false,
    },

    // Icon xuất excel trên toast message
    export_toast: {
      type: Boolean,
      default: false,
    },

    // Icon xóa
    deleteIcon: {
      type: Boolean,
      default: false,
    },

    // Icon thông báo
    notification: {
      type: Boolean,
      default: false,
    },

    // Icon tiện ích
    utilities: {
      type: Boolean,
      default: false,
    },

    // Icon câu hỏi
    question: {
      type: Boolean,
      default: false,
    },

    // Icon Người dùng
    user: {
      type: Boolean,
      default: false,
    },

    // Icon xổ xuống của combobox hoặc dropdown
    drop_down: {
      type: Boolean,
      default: false,
    },

    // Icon tăng lên
    pull_up: {
      type: Boolean,
      default: false,
    },

    // Icon quản lý tài sản
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

    // Icon xổ xuống nhưng mỏng hơn, xài bên cạnh các mục của sidebar
    drop_down_thin: {
      type: Boolean,
      default: false,
    },

    // Icon thu gọn
    narrow: {
      type: Boolean,
      default: false,
    },

    // Icon dấu tích
    tick_true: {
      type: Boolean,
      default: false,
    },

    // Icon lọc
    filter: {
      type: Boolean,
      default: false,
    },

    // Icon của navbar
    navbar: {
      type: Boolean,
      default: false,
    },

    // Icon lùi
    prev: {
      type: Boolean,
      default: false,
    },

    // Icon tiến
    next: {
      type: Boolean,
      default: false,
    },

    // Icon sửa
    edit: {
      type: Boolean,
      default: false,
    },

    // Icon sao chép
    copy: {
      type: Boolean,
      default: false,
    },

    // Icon thoát
    exit: {
      type: Boolean,
      default: false,
    },
    // Icon thoát nhỏ hơn, xài cho xóa text của combobox
    exit_small: {
      type: Boolean,
      default: false,
    },

    // Icon cảnh báo
    warning: {
      type: Boolean,
      default: false,
    },

    // Icon thành công
    success: {
      type: Boolean,
      default: false,
    },
    // Icon chọn thời gian
    date_picker: {
      type: Boolean,
      default: false,
    },
    // Icon của context menu
    iconContextMenu: {
      type: String,
      default: null,
    },
    // Icon của tải lại dữ liệu
    loading: {
      type: Boolean,
      default: false,
    },
    // Background của icon
    background: {
      type: Boolean,
      default: false,
    },
  },
  data() {
    return {
      // Trang hiện tại đang ở
      page: 1,
    };
  },
  created() {},
  methods: {
    /**
     * Click nút trước hay sau để chuyển trang tương ứng
     * Author: LDTUAN (02/08/2023)
     */
    onPagingClick() {
      this.page = this.currentPage;
      if (this.prev) {
        if (this.page > 1) {
          this.page--;
        } else {
          this.page;
        }
      } else if (this.next) {
        if (this.page < this.totalPages) {
          this.page++;
        } else {
          this.page;
        }
      } else {
        this.page;
      }
      this.$emit("paging", this.page);
    },
  },
};
</script>
<style scoped>
/* Icon image */
/*==================== Sidebar ====================*/
.icon-home {
  background: url("../../../assets/icon/qlts-icon.svg") no-repeat -20px -680px;
  width: 36px;
  height: 36px;
}

.icon-overview {
  background: url("../../../assets/icon/qlts-icon.svg") no-repeat -21px -153px;
  width: 22px;
  height: 22px;
}

.icon-asset {
  background: url("../../../assets/icon/qlts-icon.svg") no-repeat -65px -197px;
  width: 22px;
  height: 23px;
}

.icon-asset-HTDB {
  background: url("../../../assets/icon/qlts-icon.svg") no-repeat -110px -153px;
  width: 21px;
  height: 23px;
}

.icon-tool {
  background: url("../../../assets/icon/qlts-icon.svg") no-repeat -153px -153px;
  width: 22px;
  height: 22px;
}

.icon-category {
  background: url("../../../assets/icon/qlts-icon.svg") no-repeat -197px -155px;
  width: 22px;
  height: 18px;
}

.icon-search-sidebar {
  background: url("../../../assets/icon/qlts-icon.svg") no-repeat -241px -153px;
  width: 22px;
  height: 22px;
}

.icon-report {
  background: url("../../../assets/icon/qlts-icon.svg") no-repeat -329px -153px;
  width: 22px;
  height: 22px;
}

/*==================== Navbar ====================*/
.icon-notification {
  background: url("../../../assets/icon/qlts-icon.svg") no-repeat -332px -109px;
  width: 16px;
  height: 20px;
}

.icon-utilities {
  background: url("../../../assets/icon/qlts-icon.svg") no-repeat -199px -23px;
  width: 18px;
  height: 18px;
}

.icon-question {
  background: url("../../../assets/icon/qlts-icon.svg") no-repeat -67px -67px;
  width: 18px;
  height: 18px;
}

.icon-user {
  background: url("../../../assets/icon/qlts-icon.svg") no-repeat -23px -23px;
  width: 18px;
  height: 18px;
}

.icon-drop-down {
  background: url("../../../assets/icon/qlts-icon.svg") no-repeat -72px -338px;
  width: 7px;
  height: 5px;
  transform: scale(1.2);
}

.icon-drop-down-thin {
  background: url("../../../assets/icon/qlts-icon.svg") no-repeat -158px -293px;
  width: 11px;
  height: 7px;
}

.icon-pull-up {
  background: url("../../../assets/icon/qlts-icon.svg") no-repeat -28px -338px;
  width: 7px;
  height: 5px;
  transform: scale(1.2);
}

/*==================== Main content ====================*/
.icon-search {
  background: url("../../../assets/icon/qlts-icon.svg") no-repeat -243px -23px;
  width: 18px;
  height: 18px;
}

.icon-filter {
  background: url("../../../assets/icon/qlts-icon.svg") no-repeat -243px -69px;
  width: 17px;
  height: 16px;
}

.icon-add {
  background: url("../../../assets/icon/qlts-icon.svg") no-repeat -204px -424px;
  width: 8px;
  height: 8px;
  transform: scale(1.2);
}

.icon-add-box {
  background: url("../../../assets/icon/Sprites.64af8f61.svg") no-repeat -426px -146px;
  width: 20px;
  height: 20px;
}

.icon-add-black {
  background: url("../../../assets/icon/qlts-icon.svg") no-repeat -467px -423px;
  width: 10px;
  height: 10px;
  transform: scale(1.8);
}

.icon-export {
  background: url("../../../assets/icon/qlts-icon.svg") no-repeat -287px -111px;
  width: 18px;
  height: 17px;
}

.icon-delete {
  background: url("../../../assets/icon/qlts-icon.svg") no-repeat -463px -111px;
  width: 18px;
  height: 18px;
}

.icon-delete-black {
  background: url("../../../assets/icon/qlts-icon.svg") no-repeat -419px -111px;
  width: 18px;
  height: 18px;
}

.icon-prev {
  background: url("../../../assets/icon/qlts-icon.svg") no-repeat -29px -246px;
  width: 7px;
  height: 12px;
}

.icon-prev-white {
  background: url("../../../assets/icon/qlts-icon.svg") no-repeat -29px -290px;
  width: 7px;
  height: 12px;
}

.icon-next {
  background: url("../../../assets/icon/qlts-icon.svg") no-repeat -73px -246px;
  width: 7px;
  height: 11px;
}

.icon-edit {
  background: url("../../../assets/icon/qlts-icon.svg") no-repeat -156px -68px;
  width: 16px;
  height: 16px;
}

.icon-copy {
  background: url("../../../assets/icon/qlts-icon.svg") no-repeat -244px -112px;
  width: 16px;
  height: 16px;
}

.icon-success {
  background: url("../../../assets/icon/qlts-icon.svg") no-repeat -71px -512px;
  width: 11px;
  height: 8px;
  transform: scale(1.5);
}

/*==================== General icon ====================*/
.icon-exit {
  background: url("../../../assets/icon/qlts-icon.svg") no-repeat -375px -287px;
  width: 18px;
  height: 18px;
  cursor: pointer;
}

.icon-exit--small {
  background: url("../../../assets/icon/qlts-icon.svg") no-repeat -336px -248px;
  width: 8px;
  height: 8px;
  transform: scale(1.4);
  cursor: pointer;
}

.icon-required {
  background: url("../../../assets/icon/Sprites.64af8f61.svg") no-repeat -1612px -474px;
  width: 13px;
  height: 13px;
  transform: scale(0.5);
  color: red;
}

.icon-date-picker {
  background: url("../../../assets/icon/qlts-icon.svg") no-repeat -287px -67px;
  width: 18px;
  height: 18px;
}

.icon-loading {
  background: url("../../../assets/icon/qlts-icon.svg") no-repeat -110px -68px;
  width: 20px;
  height: 16px;
}

.icon-warning {
  background: url("../../../assets/icon/Sprites.64af8f61.svg") no-repeat -1136px -467px;
  width: 24px;
  height: 23px;
  transform: scale(2);
  margin-left: 10px;
  margin-right: 20px;
}

.icon-export-toast {
  background: url("../../../assets/icon/Sprites.64af8f61.svg") no-repeat -228px -1140px;
  width: 56px;
  height: 64px;
}

/*==================================================================================================== Icon setting ================================================================================*/
.icon {
  display: flex;
  height: 100%;
  align-items: center;
  justify-content: center;
  cursor: pointer;
}

.icon--navbar {
  height: 18px;
}

.icon--input {
  position: absolute;
  left: 6px;
  width: 24px;
}

.icon--button {
  position: absolute;
  left: 12px;
}

.icon--sidebar {
  margin: 0 10px;
}

.sidebar--narrow .sidebar-item .icon--sidebar {
  width: 100%;
  margin: unset;
  border-radius: 6px;
}

.sidebar--narrow .sidebar-item .icon--sidebar:hover {
  background-color: #1aa4c8;
}

.sidebar--narrow .router-link-active .icon--sidebar {
  background-color: #1aa4c8;
}

.icon--narrow {
  width: 24px;
  height: 24px;
  display: flex;
  align-items: center;
  justify-content: center;
  border: 1px solid #ffffff7d;
  border-radius: 4px;
}

.icon--narrow:hover {
  background-color: #ffffff46;
}

.icon--tick-true {
  visibility: hidden;
  display: unset;
  margin: 0px 6px;
}

.icon--combobox {
  width: 32px;
  position: absolute;
  right: 0;
  cursor: pointer;
}

.icon--combobox-delete {
  width: 32px;
  position: absolute;
  right: 0;
  cursor: pointer;
}

.icon--filter {
  width: 36px;
  position: absolute;
  left: 0;
}

.icon--disabled {
  pointer-events: none;
  opacity: 0.6;
  cursor: not-allowed;
}

.icon-background{
  background-color: var(--background-color-default);
  width: 28px;
  height: 28px;
  border-radius: 50%;
}
</style>
