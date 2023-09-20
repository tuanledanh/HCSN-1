<template>
  <div
    :class="[
      'icon',
      { 'icon-reverse': reverse },
      'icon--resize',
      { 'icon--box-thin': add_box_thin },
      { 'icon--background': background },
      { 'icon--background-expand-narrow': background_expand_narrow },
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
      { 'icon--arrow': arrow },
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
    <svg
      v-else-if="add_box"
      xmlns="http://www.w3.org/2000/svg"
      width="24"
      height="24"
      viewBox="0 0 48 48"
    >
      <g
        fill="none"
        stroke="currentColor"
        stroke-linejoin="round"
        stroke-width="4"
      >
        <rect width="36" height="36" x="6" y="6" rx="6" />
        <path stroke-linecap="round" d="M24 16v16m-8-8h16" />
      </g>
    </svg>
    <svg
      v-else-if="add_box_white"
      xmlns="http://www.w3.org/2000/svg"
      width="24"
      height="24"
      viewBox="0 0 48 48"
    >
      <g fill="none" stroke="white" stroke-linejoin="round" stroke-width="4">
        <rect width="36" height="36" x="6" y="6" rx="6" />
        <path stroke-linecap="round" d="M24 16v16m-8-8h16" />
      </g>
    </svg>
    <svg
      v-else-if="resize"
      xmlns="http://www.w3.org/2000/svg"
      width="24"
      height="24"
      viewBox="0 0 512 512"
    >
      <path
        fill="currentColor"
        d="M256.236 504L120 367.764v-38.156h72v-144h-72V146.98L255.766 11.216L392 147.452v38.156h-72v144h72v38.627ZM159.1 361.608l97.137 97.137l97.137-97.137H288v-208h64.9l-97.134-97.137l-97.138 97.137H224v208Z"
      />
    </svg>
    <svg
      v-else-if="view"
      xmlns="http://www.w3.org/2000/svg"
      width="24"
      height="24"
      viewBox="0 0 256 256"
    >
      <path
        fill="currentColor"
        d="M245.48 125.57c-.34-.78-8.66-19.23-27.24-37.81C201 70.54 171.38 50 128 50S55 70.54 37.76 87.76c-18.58 18.58-26.9 37-27.24 37.81a6 6 0 0 0 0 4.88c.34.77 8.66 19.22 27.24 37.8C55 185.47 84.62 206 128 206s73-20.53 90.24-37.75c18.58-18.58 26.9-37 27.24-37.8a6 6 0 0 0 0-4.88ZM128 194c-31.38 0-58.78-11.42-81.45-33.93A134.77 134.77 0 0 1 22.69 128a134.56 134.56 0 0 1 23.86-32.06C69.22 73.42 96.62 62 128 62s58.78 11.42 81.45 33.94A134.56 134.56 0 0 1 233.31 128C226.94 140.21 195 194 128 194Zm0-112a46 46 0 1 0 46 46a46.06 46.06 0 0 0-46-46Zm0 80a34 34 0 1 1 34-34a34 34 0 0 1-34 34Z"
      />
    </svg>
    <div
      v-else
      :class="[
        iconContextMenu,
        { 'icon-search': iconSearch },
        { 'icon-required': required },
        { 'icon-add': add },
        { 'icon-add-box-black-thin': add_box_thin },
        { 'icon-export': exportIcon },
        { 'icon-delete': deleteIcon },
        { 'icon-notification': notification },
        { 'icon-utilities': utilities },
        { 'icon-question': question },
        { 'icon-chat': chat },
        { 'icon-user': user },
        { 'icon-drop-down': drop_down },
        { 'icon-drop-down-large': drop_down_large },
        { 'icon-pull-up': pull_up },
        { 'icon-pull-up-large': pull_up_large },
        { 'icon-home': home },
        { 'icon-overview': overview },
        { 'icon-asset': asset },
        { 'icon-asset-HTDB': asset_HTDB },
        { 'icon-tool': tool },
        { 'icon-category': category },
        { 'icon-search-sidebar': search_sidebar },
        { 'icon-report': report },
        { 'icon-drop-down-thin': drop_down_thin },
        { 'icon-drop-down-thin-blue': drop_down_thin_blue },
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
        { 'icon-arrow': arrow },
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
    // Mũi tên icon
    arrow: {
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
    // Icon thêm mới nhưng có box và viền mỏng
    add_box_thin: {
      type: Boolean,
      default: false,
    },
    // Icon thêm mới có border xung quanh
    add_box: {
      type: Boolean,
      default: false,
    },
    // Icon thêm mới có border xung quanh, nhưng màu trắng
    add_box_white: {
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

    // Icon chat
    chat: {
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
    // Icon xổ xuống nhưng icon này bự hơn
    drop_down_large: {
      type: Boolean,
      default: false,
    },

    // Icon tăng lên
    pull_up: {
      type: Boolean,
      default: false,
    },
    // Icon tăng lên nhưng icon này bự hơn
    pull_up_large: {
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
    // Icon xổ xuống nhưng mỏng hơn và màu xanh dương
    drop_down_thin_blue: {
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
    // Icon điều chỉnh độ rộng theo chiều dọc
    resize: {
      type: Boolean,
      default: false,
    },
    // Icon xem
    view: {
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
    // Quay ngược icon 180 độ
    reverse: {
      type: Boolean,
      default: false,
    },
    // Background của icon
    background: {
      type: Boolean,
      default: false,
    },
    // Background của icon xài cho mở rộng thu hẹp
    background_expand_narrow: {
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

.icon-chat {
  background: url("../../../assets/icon/qlts-icon.svg") no-repeat -23px -68px;
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

.icon-arrow {
  background: url("../../../assets/icon/qlts-icon.svg") no-repeat -375px -246px;
	width: 18px;
	height: 12px;
  filter: invert(100%);
  transform: rotate(180deg);
}

.icon-drop-down-large {
  background: url("../../../assets/icon/qlts-icon.svg") no-repeat -420px -330px;
  width: 16px;
  height: 19px;
}

.icon-drop-down-thin {
  background: url("../../../assets/icon/qlts-icon.svg") no-repeat -158px -293px;
  width: 11px;
  height: 7px;
}

.icon-drop-down-thin-blue {
  background: url("../../../assets/icon/qlts-icon.svg") no-repeat -334px -293px;
  width: 11px;
  height: 7px;
}

.icon-pull-up {
  background: url("../../../assets/icon/qlts-icon.svg") no-repeat -28px -338px;
  width: 7px;
  height: 5px;
  transform: scale(1.2);
}

.icon-pull-up-large {
  background: url("../../../assets/icon/qlts-icon.svg") no-repeat -332px -331px;
  width: 17px;
  height: 18px;
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

.icon-add-box-black-thin {
  background: url("../../../assets/icon/qlts-icon.svg") no-repeat -204px -424px;
  width: 8px;
  height: 8px;
  filter: invert(100%);
  transform: scale(1.3);
}

.icon-add-black {
  background: url("../../../assets/icon/qlts-icon.svg") no-repeat -204px -424px;
  width: 8px;
  height: 8px;
  filter: invert(100%);
  transform: scale(2);
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

.icon-reverse {
  transform: rotate(180deg);
}

.icon--navbar {
  height: 18px;
}

.icon--input {
  position: absolute;
  left: 6px;
  width: 24px;
}

.input--search > div > .icon--input{
  top: 0;
}

.icon--button {
  position: absolute;
  left: 12px;
  top: 0;
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
  cursor: not-allowed;
  pointer-events: none;
  opacity: 0.6;
}

.icon--background {
  background-color: var(--background-color-default);
  width: 28px;
  height: 28px;
  border-radius: 50%;
}

.icon--background-expand-narrow {
  background-color: var(--background-color-table-expand-narrow);
  width: 20px;
  height: 20px;
  border-radius: 50%;
}

.icon--box-thin {
  width: 18px;
  height: 18px;
  border: 1px solid black;
  border-radius: 4px;
}

.icon--arrow{
  width: 40px;
}
</style>
