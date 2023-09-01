<template>
  <div class="content" :class="[{ 'content--expand': isChangeWidth }]">
    <div class="content--top" ref="contentTop">
      <div v-if="selectedRowsByCheckBox.length == 0" class="top-left">
        <span class="font-weight--700">Điều Chuyển</span>
        <MISAIcon loading></MISAIcon>
      </div>
      <div v-else class="top-left font-size-default">
        <span class="font-weight--400"
          >Đã chọn:
          <span class="font-weight--500">{{
            selectedRowsByCheckBox.length
          }}</span></span
        >
        <span
          class="font-weight--500 main-color pointer"
          @click="btnUncheckedAll"
          >Bỏ chọn</span
        >
        <MISAButton
          error
          textButton="Xóa"
          @click="callToastDelete"
        ></MISAButton>
      </div>
      <div class="top-right">
        <MISAButton
          combo
          add_box_white
          buttonMain
          textButton="Thêm chứng từ"
          @click="btnAddDocument"
          large
        ></MISAButton>
      </div>
    </div>
    <div
      class="content--body border--left border--right border--bottom"
      ref="contentBody"
    >
      <div class="body--top" ref="bodyTop">
        <!-- ------------------------Table start------------------------ -->
        <div :class="[{ table: !isResize }, { 'table-flex': isResize }]">
          <!-- ------------------------Header------------------------ -->
          <div class="header--row row">
            <div
              class="header cell display--center-center border--top border--right border--bottom"
            >
              <input type="checkbox" />
            </div>
            <div
              class="header cell display--center-center font-weight--700 border--top border--right border--bottom"
            >
              STT
            </div>
            <div
              class="header cell display--center-left font-weight--700 border--top border--right border--bottom padding--left-10"
            >
              Mã chứng từ
            </div>
            <div
              class="header cell display--center-center font-weight--700 border--top border--right border--bottom"
            >
              Ngày điều chuyển
            </div>
            <div
              class="header cell display--center-center font-weight--700 border--top border--right border--bottom"
            >
              Ngày chứng từ
            </div>
            <div
              class="header cell display--center-right font-weight--700 border--top border--right border--bottom padding--right-10"
            >
              Nguyên giá
            </div>
            <div
              class="header cell display--center-right font-weight--700 border--top border--right border--bottom padding--right-10"
            >
              Giá trị còn lại
            </div>
            <div
              class="header cell display--center-left font-weight--700 border--top border--right border--bottom padding--left-10"
            >
              Ghi chú
            </div>
            <div
              class="header cell display--center-center font-weight--700 border--top border--bottom"
            >
              Chức năng
            </div>
          </div>
          <!-- ------------------------Body------------------------ -->
          <div class="body-data relative">
            <div
              class="body--row row"
              v-for="transferAsset in transferAssets"
              :key="transferAsset.TransferAssetId"
              :class="{ 'row--selected': selectedRows.includes(transferAsset) }"
              @click.exact.stop="callRowOnClick(transferAsset)"
              @click.ctrl.stop="callRowOnCtrlClick(transferAsset)"
            >
              <div
                class="cell display--center-center border--right border--bottom"
                @click.stop="callRowOnClickByCheckBox(transferAsset)"
              >
                <input
                  type="checkbox"
                  :checked="selectedRowsByCheckBox.includes(transferAsset)"
                />
              </div>
              <div
                class="cell display--center-center border--right border--bottom"
              >
                {{ transferAssets.indexOf(transferAsset) + 1 }}
              </div>
              <div
                class="cell display--center-left border--right border--bottom padding--left-10"
              >
                {{ transferAsset.TransferAssetCode }}
              </div>
              <div
                class="cell display--center-center border--right border--bottom"
              >
                {{ transferAsset.TransferDate }}
              </div>
              <div
                class="cell display--center-center border--right border--bottom"
              >
                {{ transferAsset.TransactionDate }}
              </div>
              <div
                class="cell display--center-right border--right border--bottom padding--right-10"
              >
                {{ transferAsset.Cost }}
              </div>
              <div
                class="cell display--center-right border--right border--bottom padding--right-10"
              >
                {{ transferAsset.FixedtransferAssetName }}
              </div>
              <div
                class="cell display--center-left border--right border--bottom padding--left-10"
              >
                {{ transferAsset.Description }}
              </div>
              <div class="cell display--center-center border--bottom">
                <div class="icon-function">
                  <MISATooltip bottom content="Chỉnh sửa">
                    <MISAIcon edit background></MISAIcon>
                  </MISATooltip>
                  <MISATooltip bottom content="Xóa">
                    <MISAIcon deleteIcon background></MISAIcon>
                  </MISATooltip>
                </div>
              </div>
            </div>
          </div>
        </div>
        <!-- ------------------------Table end------------------------ -->

        <MISAPaging
          :totalRecords="totalRecords"
          :totalPages="totalPages"
          :currentPage="currentPage"
          :pageLimitList="pageLimitList"
          @filter="getPageLimit"
          @paging="getPageNumber"
        ></MISAPaging>
      </div>

      <!-- ------------------------Resize bar------------------------ -->
      <div class="resize-bar relative" @mousedown="startResize">
        <hr class="hr--left" />
        <div
          class="resize-bar__icon absolute"
          @mouseenter="handleMouseEnter"
          @mouseleave="handleMouseLeave"
          @mousedown="handleMouseDown"
        >
          <MISATooltipVisible
            bottom
            content="Thay đổi kích thước"
            :visible="visible_tool_tip"
          >
            <MISAIcon resize></MISAIcon>
          </MISATooltipVisible>
        </div>
        <hr class="hr--right" />
      </div>

      <!-- ------------------------Table start------------------------ -->
      <div class="resizable-table" ref="resizableTable">
        <div class="content--top padding--top-6" ref="tableTop">
          <div class="top-left">
            <MISAButton
              add
              buttonMain
              short
              textButton="Thông tin chi tiết"
              @click="btnAddAsset"
              large
            ></MISAButton>
          </div>
          <div class="top-right">
            <div class="top-right__icon">
              <MISATooltip
                bottom_end
                :content="!isShowPagingBottom ? 'Mở rộng' : 'Thu gọn'"
              >
                <MISAIcon
                  :reverse="!isShowPagingBottom"
                  drop_down_thin_blue
                  background_expand_narrow
                  @click="btnCollapseTable"
                ></MISAIcon>
              </MISATooltip>
            </div>
          </div>
        </div>
        <div
          class="table"
          :class="[{ 'table-bot': !isResize }, { 'table-bot-flex': isResize }]"
        >
          <!-- ------------------------Header------------------------ -->
          <div class="header--row row">
            <div
              class="header cell display--center-center font-weight--700 border--top border--right border--bottom"
            >
              STT
            </div>
            <div
              class="header cell display--center-left font-weight--700 border--top border--right border--bottom padding--left-10"
            >
              Mã tài sản
            </div>
            <div
              class="header cell display--center-left font-weight--700 border--top border--right border--bottom padding--left-10"
            >
              Tên tài sản
            </div>
            <div
              class="header cell display--center-right font-weight--700 border--top border--right border--bottom padding--right-10"
            >
              Nguyên giá
            </div>
            <div
              class="header cell display--center-right font-weight--700 border--top border--right border--bottom padding--right-10"
            >
              Giá trị còn lại
            </div>
            <div
              class="header cell display--center-left font-weight--700 border--top border--right border--bottom padding--left-10"
            >
              Bộ phần sử dụng
            </div>
            <div
              class="header cell display--center-left font-weight--700 border--top border--right border--bottom padding--left-10"
            >
              Bộ phận điều chuyển đến
            </div>
            <div
              class="header cell display--center-left font-weight--700 border--top border--bottom padding--left-10"
            >
              Lý do
            </div>
          </div>

          <!-- ------------------------Body------------------------ -->
          <div class="body-data">
            <div
              class="body--row row"
              v-for="asset in assets"
              :key="asset.FixedAssetId"
            >
              <div
                class="cell display--center-center border--right border--bottom"
              >
                {{ assets.indexOf(asset) + 1 }}
              </div>
              <div
                class="cell display--center-left border--right border--bottom padding--left-10"
              >
                {{ asset.FixedAssetName }}
              </div>
              <div
                class="cell display--center-left border--right border--bottom padding--left-10"
              >
                {{ asset.FixedAssetName }}
              </div>
              <div
                class="cell display--center-right border--right border--bottom padding--right-10"
              >
                {{ asset.FixedAssetName }}
              </div>
              <div
                class="cell display--center-right border--right border--bottom padding--right-10"
              >
                {{ asset.FixedAssetName }}
              </div>
              <div
                class="cell display--center-left border--right border--bottom padding--left-10"
              >
                {{ asset.FixedAssetName }}
              </div>
              <div
                class="cell display--center-left border--right border--bottom padding--left-10"
              >
                {{ asset.FixedAssetName }}
              </div>
              <div
                class="cell display--center-left border--bottom padding--left-10"
              >
                {{ asset.FixedAssetName }}
              </div>
            </div>
          </div>
        </div>
        <!-- ------------------------Table end------------------------ -->
        <div>
          <MISAPaging
            v-if="isShowPagingBottom"
            :totalRecords="totalRecords"
            :totalPages="totalPages"
            :currentPage="currentPage"
            :pageLimitList="pageLimitList"
            @filter="getPageLimit"
            @paging="getPageNumber"
          ></MISAPaging>
        </div>
      </div>
    </div>
  </div>
  <MISAAssetTransferForm
    v-if="isFormDisplay"
    @onCloseForm="onCloseForm"
  ></MISAAssetTransferForm>
  <div v-if="isShowToastDelete" class="blur">
    <MISAToast typeToast="warning" :content="toast_content_delete"
      ><MISAButton
        buttonOutline
        textButton="Không"
        @click="callCloseToastWarning"
        :tabindex="2"
        @keydown="callCheckTabIndex($event, 'islast')"
      ></MISAButton>
      <MISAButton
        buttonMain
        textButton="Xóa"
        @click="btnDeleteAssets"
        :tabindex="1"
        ref="cancelForm"
        focus
      ></MISAButton>
    </MISAToast>
  </div>
</template>
<script>
import MISAAssetTransferForm from "./AssetTransferForm.vue";
import { rowOnClick } from "../../helpers/table/selectRow";
import { rowOnCtrlClick } from "../../helpers/table/selectRow";
import { rowOnClickByCheckBox } from "../../helpers/table/selectRow";
import { showToastDelete } from "../../helpers/table/toastMessage";
import { closeToastWarning } from "../../helpers/table/toastMessage";
import { checkTabIndex } from "../../helpers/tabIndex/tabIndex";
export default {
  name: "AssetTransferList",
  components: {
    MISAAssetTransferForm,
  },
  props: {
    isChangeWidth: {
      type: Boolean,
      default: false,
    },
  },
  data() {
    return {
      // ----------------------------- Data -----------------------------
      // Danh sách bản ghi
      transferAssets: [],
      // Danh sách các bản ghi đã chọn
      selectedRows: [],
      // Index của bản ghi đầu tiên trong danh sách
      lastIndex: 0,

      // ----------------------------- Form -----------------------------
      isFormDisplay: false,

      // ----------------------------- Paging -----------------------------
      pageLimitList: [],
      // Tổng bản ghi
      totalRecords: 0,
      // Tổng trang
      totalPages: 0,
      // Trang hiện tại
      currentPage: 1,

      // ----------------------------- Resize table -----------------------------
      // Hiển thị tooltip của icon resize
      visible_tool_tip: false,
      // Kiểm tra coi đã nhấn giữ icon resize chưa
      isMouseDown: false,
      // Điểm bắt đầu của chuột khi click vào khối div resize bar
      startY: null,
      // Chiều cao ban đầu của resizable table
      initialHeight: null,
      // Chiều cao ban đầu của resizable table (giá trị không thể đổi)
      initialHeightFix: null,
      // Chiều cao của resizable table khi đang resize
      initialHeightAfterResize: null,
      // Chiều cao ban đầu của table top (giá trị không thể đổi)
      tableTopHeightFix: null,
      // Thu gọn table bằng icon
      isNarrow: false,
      // Hiển thị paging ở dưới
      isShowPagingBottom: true,
      // Table đã thay đổi width do resize
      isResize: false,

      // ----------------------------- Checkbox -----------------------------
      // Tick ô checkbox input
      isCheckboxChecked: false,
      // Danh sách các bản ghi đã chọn
      selectedRowsByCheckBox: [],

      // ----------------------------- Toast -----------------------------
      isShowToastDelete: false,
      toast_content_delete: null,

      // ----------------------------- Tab index -----------------------------
      buttonFocus: null,
    };
  },
  created() {
    this.loadData();
    this.$_emitter.emit("onDisplayContent");
  },
  methods: {
    // load data tạm thời
    loadData() {
      this.$_MISAApi.TransferAsset.Filter(1, 20, null)
        .then((res) => {
          this.transferAssets = res.data.Data;
          console.log(this.transferAssets);
        })
        .catch((res) => {
          console.log(res);
        });
    },

    //------------------------------------------- Click row -------------------------------------------

    /**
     * Thực hiện call hàm rowOnClick từ file js để bôi xanh 1 dòng nếu click vào dòng đó
     * @param {object} asset thông tin tài sản
     * Author: LDTUAN (02/08/2023)
     */
    callRowOnClick(asset) {
      rowOnClick.call(this, asset, 'transferAssets');
    },

    callRowOnClickByCheckBox(asset) {
      rowOnClickByCheckBox.call(this, asset, 'transferAssets');
    },

    callRowOnCtrlClick(asset) {
      rowOnCtrlClick.call(this, asset, 'transferAssets');
    },

    btnUncheckedAll() {
      this.selectedRows = [];
      this.selectedRowsByCheckBox = [];
    },

    callToastDelete() {
      showToastDelete.call(
        this,
        this.$_MISAResource.VN.Form.Warning.Delete.Single,
        this.$_MISAResource.VN.Form.Warning.Delete.Multiple
      );
    },

    callCloseToastWarning() {
      closeToastWarning.call(this);
    },

    //------------------------------------------- Resize -------------------------------------------
    /**
     * Khi click vào resize bar sẽ set cho toàn bộ body event resizing để có thể
     * Kéo thoải mái ở bất cứ đâu mà vẫn điều chỉnh được độ dài của table
     * Nếu thả chuột thì gọi đến event stopResize để thôi chỉnh độ dài
     */
    startResize(event) {
      if (!this.initialHeightFix) {
        this.initialHeightFix = this.$refs.resizableTable.clientHeight;
      }
      console.log(this.initialHeightFix);
      event.preventDefault(); // Ngăn chặn chọn văn bản khi kéo
      document.addEventListener("mousemove", this.resizing);
      document.addEventListener("mouseup", this.stopResize);

      this.startY = event.clientY; // Lưu vị trí chuột xuất phát
      this.initialHeight = this.$refs.resizableTable.clientHeight; // Lưu kích thước ban đầu của resizable-table
      this.isResize = true;
      this.resizing(event);
    },

    /**
     * Lấy độ chênh giữa khoảng cách từ chuột lúc mới bấm so với lúc kéo để cộng vào chiều dài của table
     * Đồng thời thêm bớt độ chênh đó cho body-top
     * @param {*} event
     * Author: LDTUAN (17/08/2023)
     */
    resizing(event) {
      const movementY = this.startY - event.clientY; // Tính toán khoảng cách di chuyển của chuột
      const newHeight = this.initialHeight + movementY; // Tính toán chiều cao mới dựa trên kích thước ban đầu và khoảng cách di chuyển

      const contentBodyHeight = this.$refs.contentBody.clientHeight;
      const minHeight = this.$refs.tableTop.clientHeight; // Độ cao tối thiểu
      const maxHeight = contentBodyHeight; // Độ cao tối đa
      // Áp dụng độ cao mới vào resizable-table và giữ nguyên con trỏ
      this.$refs.resizableTable.style.height = `${Math.min(
        Math.max(newHeight, minHeight),
        maxHeight
      )}px`;
      this.initialHeightAfterResize = this.$refs.resizableTable.clientHeight;
      this.$refs.bodyTop.style.height = `${
        contentBodyHeight - Math.min(Math.max(newHeight, minHeight), maxHeight)
      }px`;
      document.body.style.cursor = "ns-resize";
      if (this.$refs.resizableTable.clientHeight == minHeight) {
        this.isShowPagingBottom = false;
      } else {
        this.isShowPagingBottom = true;
      }
    },

    /**
     * Xóa hết các event liên quan đến resize khi nhả chuột
     * Author: LDTUAN (17/08/2023)
     */
    stopResize() {
      document.removeEventListener("mousemove", this.resizing);
      document.removeEventListener("mouseup", this.stopResize);
      document.body.style.cursor = "auto"; // Trả lại con trỏ mặc định
    },

    btnCollapseTable() {
      if (!this.initialHeightFix) {
        this.initialHeightFix = this.$refs.resizableTable.clientHeight;
      }
      this.isResize = true;
      const tableHeight = this.$refs.resizableTable.clientHeight;
      const tableTop = this.$refs.tableTop.clientHeight;
      const contentBodyHeight = this.$refs.contentBody.clientHeight;
      if (tableHeight <= tableTop) {
        this.$refs.resizableTable.style.height = `${this.initialHeightFix}px`;
        this.$refs.bodyTop.style.height = `${
          contentBodyHeight - this.initialHeightFix
        }px`;
        this.initialHeightAfterResize = this.initialHeightFix;
      } else {
        this.$refs.resizableTable.style.height = `${tableTop}px`;
        this.$refs.bodyTop.style.height = `${contentBodyHeight}px`;
        this.initialHeightAfterResize = tableTop;
      }
      this.isShowPagingBottom = !this.isShowPagingBottom;
    },

    //------------------------------------------- Tool tip resize -------------------------------------------
    /**
     * Sự kiện khi di chuột đến thanh resize thì hiển thị tooltip
     * Author: LDTUAN (17/08/2023)
     */
    handleMouseEnter() {
      this.visible_tool_tip = !this.isMouseDown;
    },

    /**
     * Di chuột khỏi thanh resize thì không hiển thị tooltip
     * Author: LDTUAN (17/08/2023)
     */
    handleMouseLeave() {
      this.visible_tool_tip = false;
    },

    /**
     * Nếu nhấn giữ icon resize thì không hiển thị tooltip, và tắt cả mouseenter
     * đó là lý do visible_tool_tip của mouseenter set dựa theo isMouseDown
     * Author: LDTUAN (17/08/2023)
     */
    handleMouseDown() {
      this.isMouseDown = true;
      this.visible_tool_tip = false;
    },

    /**
     * Đã thoát khỏi mousedown, giờ mouseenter có thể thực hiện bình thường
     * Author: LDTUAN (17/08/2023)
     */
    handleMouseUp() {
      this.isMouseDown = false;
    },

    //------------------------------------------- Tab index -------------------------------------------
    callCheckTabIndex(event, index) {
      this.buttonFocus = "cancelForm";
      checkTabIndex.call(this, event, index);
    },

    //------------------------------------------- Tab index -------------------------------------------
    btnAddDocument() {
      this.isFormDisplay = true;
    },

    onCloseForm() {
      this.isFormDisplay = false;
    },
  },
  mounted() {
    /**
     * Nếu nhả chuột khỏi bất cứ vị trí nào trên web thì thay đổi giá trị của isMouseDown
     * Để mouseenter hoạt động bình thường, để handleMouseUp ở div resize-bar và khi nhả chuột ở icon resize
     * Thì nó không nhận event mouseup nên mới phải để ở khối của toàn bộ trang web
     * Author: LDTUAN (19/08/2023)
     */
    document.addEventListener("mouseup", this.handleMouseUp);

    /**
     * Lấy chiều cao của resizable table để làm điều kiện set hiển thị cho paging của nó
     * Author: LDTUAN (19/08/2023)
     */
    this.initialHeightAfterResize = this.$refs.resizableTable.clientHeight;
    this.tableTopHeightFix = this.$refs.tableTop.clientHeight;
  },
  beforeUnmount() {
    document.removeEventListener("mouseup", this.handleMouseUp);
  },
};
</script>
<style scoped>
.content {
  width: calc(100% - 200px);
  height: calc(100vh - 44px);
  float: right;
  box-sizing: border-box;
  padding: 13px 20px 26px 19px;
  display: flex;
  flex-direction: column;
  transition: all ease-in-out 0.1s;
  background-color: var(--background-color-main-content);
  overflow: hidden;
}

.content--expand {
  width: calc(100% - 66px);
}

.content--top {
  display: flex;
  flex-direction: row;
  align-items: center;
  justify-content: space-between;
  background-color: var(--background-color-default);
  padding: 8px 8px;
  z-index: 1;
}

.top-left {
  display: flex;
  align-items: center;
  column-gap: 8px;
  font-size: 20px;
}

.content--body {
  width: 100%;
  height: calc(100% - 36px);
  max-height: 702px;
  display: flex;
  flex-direction: column;
  box-sizing: border-box;
  border-color: var(--table-border-color);
}

.body--top {
  height: 60%;
  display: flex;
  flex-direction: column;
  justify-content: space-between;
  background-color: var(--background-color-default);
}

/* ------------------------------------------- Table ------------------------------------------- */
.table {
  max-height: 363px;
  display: flex;
  flex-direction: column;
  background-color: var(--background-color-default);
  border-spacing: unset;
  overflow-y: auto;
}

.table-flex {
  max-height: unset;
  flex: 1;
  display: flex;
  flex-direction: column;
  background-color: var(--background-color-default);
  border-spacing: unset;
  overflow-y: auto;
}

.table-bot {
  max-height: 214px;
}

.table-bot-flex {
  flex: 1;
  max-height: unset;
}

.row {
  display: grid;
  grid-template-columns:
    44px 50px 120px 150px 150px 140px 200px calc(100% - 964px)
    110px;
  height: 35px;
  cursor: pointer;
}

.table-bot .row {
  grid-template-columns: 50px 120px 200px 140px 150px 180px 200px calc(
      100% - 1040px
    );
}

.table-bot-flex .row {
  grid-template-columns: 50px 120px 200px 140px 150px 180px 200px calc(
      100% - 1040px
    );
}

.cell {
  min-height: 35px;
  background-color: var(--background-color-default);
  transition: var(--transition-02s);
  border-color: var(--table-border-color);
}

.icon-function {
  display: flex;
  column-gap: 8px;
}
.header {
  background-color: var(--background-color-table-head);
}

.header input[type="text"] {
  width: 100%;
  height: 100%;
}

.body--row:hover .cell {
  background-color: var(--table-body-hover);
}

.row--selected .cell {
  background-color: var(--table-body-hover);
}

.body {
  color: var(--table-body-text-color);
}
.body-data {
  overflow-y: auto;
}

/* ------------------------------------------- Resize-bar ------------------------------------------- */
.resize-bar {
  width: 100%;
  border-color: var(--resize-bar-color);
  background-color: var(--background-color-default);
  cursor: ns-resize;
  padding: 2px 0px;
  display: flex;
  align-items: center;
  column-gap: 3px;
}

.resize-bar hr {
  margin: unset;
}

.hr--left {
  width: 50%;
}

.hr--right {
  width: 50%;
}

.resize-bar__icon {
  left: 50%;
  transform: translateX(-50%);
  z-index: 2;
}

.resizable-table {
  flex: 1;
  display: flex;
  flex-direction: column;
}
</style>
