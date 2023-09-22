<template>
  <div class="content" :class="[{ 'content--expand': isChangeWidth }]">
    <div class="content--top" ref="contentTop">
      <div v-if="selectedRowsByCheckBox.length == 0" class="top-left">
        <span class="font-weight--700">Điều Chuyển</span>
        <MISATooltip top content="Tải lại dữ liệu">
          <MISAIcon loading @click="reLoad"></MISAIcon
        ></MISATooltip>
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
      <div class="top-head-right">
        <MISAButton
          combo
          add_box_white
          buttonMain
          textButton="Thêm chứng từ"
          @click="btnAddDocument"
          large
        ></MISAButton>
        <MISATooltip bottom content="Tư vấn">
          <MISAIcon chat></MISAIcon
        ></MISATooltip>
        <MISATooltip bottom content="Hỏi đáp">
          <MISAIcon question></MISAIcon
        ></MISATooltip>
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
          <div
            class="header--row row border--top"
            :class="[{ 'row-expand': isChangeWidth }]"
          >
            <div
              class="header cell display--center-center border--right border--bottom"
            >
              <input
                type="checkbox"
                @click="headCheckboxClick($event)"
                :checked="
                  transferAssets.every((transferItem) =>
                    selectedRowsByCheckBox.some(
                      (selectedItem) =>
                        selectedItem.TransferAssetId ===
                        transferItem.TransferAssetId
                    )
                  ) &&
                  transferAssets.length > 0 &&
                  selectedRowsByCheckBox.length > 0
                "
              />
            </div>
            <div
              class="header cell display--center-center font-weight--700 border--right border--bottom"
            >
              <MISATooltip top content="Số thứ tự">
                <span>STT</span></MISATooltip
              >
            </div>
            <div
              class="header cell display--center-left font-weight--700 border--right border--bottom padding--left-10"
            >
              Mã chứng từ
            </div>
            <div
              class="header cell display--center-center font-weight--700 border--right border--bottom"
            >
              Ngày điều chuyển
            </div>
            <div
              class="header cell display--center-center font-weight--700 border--right border--bottom"
            >
              Ngày chứng từ
            </div>
            <div
              class="header cell display--center-right font-weight--700 border--right border--bottom padding--right-10"
            >
              Nguyên giá
            </div>
            <div
              class="header cell display--center-right font-weight--700 border--right border--bottom padding--right-10"
            >
              Giá trị còn lại
            </div>
            <div
              class="header cell display--center-left font-weight--700 border--right border--bottom padding--left-10"
            >
              Ghi chú
            </div>
            <div
              id="1"
              class="header cell display--center-center font-weight--700 border--bottom"
            >
              Chức năng
            </div>
          </div>
          <!-- ------------------------Body------------------------ -->
          <div class="body-data relative border--top">
            <div
              class="body--row row"
              v-for="transferAsset in transferAssets"
              :key="transferAsset.TransferAssetId"
              :class="[
                {
                  'row--selected': selectedRowsByCheckBox.some(
                    (item) =>
                      item.TransferAssetId === transferAsset.TransferAssetId
                  ),
                },
                {
                  'row--selected': selectedRows.includes(transferAsset),
                },
                { 'row-expand': isChangeWidth },
              ]"
              @dblclick="btnEditTransferAsset(transferAsset)"
              @click.exact.stop="callRowOnClick(transferAsset)"
              @click.ctrl.stop="callRowOnCtrlClick(transferAsset)"
              @click.shift.stop="rowOnShiftClick(transferAsset)"
              @contextmenu.prevent="btnClickRight($event, transferAsset)"
            >
              <div
                class="cell display--center-center border--right border--bottom"
                @click.stop="callRowOnClickByCheckBox(transferAsset)"
              >
                <input
                  type="checkbox"
                  :checked="
                    selectedRowsByCheckBox.some(
                      (item) =>
                        item.TransferAssetId === transferAsset.TransferAssetId
                    )
                  "
                />
              </div>
              <div
                class="cell display--center-center border--right border--bottom"
              >
                {{
                  transferAssets.indexOf(transferAsset) +
                  1 +
                  pageLimitTransfer * (currentPageTransfer - 1)
                }}
              </div>
              <div
                class="cell display--center-left border--right border--bottom padding--left-10"
              >
                {{ transferAsset.TransferAssetCode }}
              </div>
              <div
                class="cell display--center-center border--right border--bottom"
              >
                {{ formatDate(transferAsset.TransferDate) }}
              </div>
              <div
                class="cell display--center-center border--right border--bottom"
              >
                {{ formatDate(transferAsset.TransactionDate) }}
              </div>
              <div
                class="cell display--center-right border--right border--bottom padding--right-10"
              >
                {{ formatMoney(Math.round(transferAsset.Cost)) }}
              </div>
              <div
                class="cell display--center-right border--right border--bottom padding--right-10"
              >
                {{ formatMoney(Math.round(transferAsset.RemainingCost)) }}
              </div>
              <div
                class="cell display--center-left border--right border--bottom padding--left-10"
              >
                {{ transferAsset.Description }}
              </div>
              <div
                class="cell display--center-center border--bottom row-scroll"
              >
                <div class="icon-function">
                  <MISAIcon
                    view
                    background
                    @click="btnViewTransferAsset(transferAsset)"
                  ></MISAIcon>
                  <MISAIcon
                    edit
                    background
                    @click="btnEditTransferAsset(transferAsset)"
                  ></MISAIcon>
                  <MISAIcon
                    deleteIcon
                    background
                    @click="callToastDeleteSingle(transferAsset)"
                  ></MISAIcon>
                </div>
              </div>
            </div>
          </div>
        </div>
        <!-- ------------------------Table end------------------------ -->

        <MISACalculator
          form="transfer-list"
          :isChangeWidth="isChangeWidth"
          :totalPrice="totalPrice"
          :totalResidualValue="totalResidualValue"
          :numberColumnLeft="5"
        ></MISACalculator>
        <MISAPaging
          :totalRecords="totalRecordsTransfer"
          :totalPages="totalPagesTransfer"
          :currentPage="currentPageTransfer"
          :pageLimitList="pageLimitListTransfer"
          @filter="getPageLimitTransfer"
          @paging="getPageNumberTransfer"
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
              large_6
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
              <MISATooltip top content="Số thứ tự">
                <span>STT</span></MISATooltip
              >
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
                {{ assets.indexOf(asset) + 1 + pageLimit * (currentPage - 1) }}
              </div>
              <div
                class="cell display--center-left border--right border--bottom padding--left-10"
              >
                {{ asset.FixedAssetCode }}
              </div>
              <div
                class="cell display--center-left border--right border--bottom padding--left-10"
              >
                {{ asset.FixedAssetName }}
              </div>
              <div
                class="cell display--center-right border--right border--bottom padding--right-10"
              >
                {{ formatMoney(Math.round(asset.Cost)) }}
              </div>
              <div
                class="cell display--center-right border--right border--bottom padding--right-10"
              >
                {{ formatMoney(Math.round(asset.RemainingCost)) }}
              </div>
              <div
                class="cell display--center-left border--right border--bottom padding--left-10"
              >
                <el-tooltip
                  v-if="asset.OldDepartmentName.length > 20"
                  :visible="visible"
                  placement="right"
                >
                  <template #content>
                    <span>{{ asset.OldDepartmentName }}</span>
                  </template>
                  <span>{{ truncateText(asset.OldDepartmentName, 20) }}</span>
                </el-tooltip>
                <span v-else>{{
                  truncateText(asset.OldDepartmentName, 20)
                }}</span>
              </div>
              <div
                class="cell display--center-left border--right border--bottom padding--left-10"
              >
                <el-tooltip
                  v-if="asset.NewDepartmentName.length > 20"
                  :visible="visible"
                  placement="right"
                >
                  <template #content>
                    <span>{{ asset.NewDepartmentName }}</span>
                  </template>
                  <span>{{ truncateText(asset.NewDepartmentName, 20) }}</span>
                </el-tooltip>
                <span v-else>{{
                  truncateText(asset.NewDepartmentName, 20)
                }}</span>
              </div>
              <div
                class="cell display--center-left border--bottom padding--left-10"
              >
                {{ asset.Description }}
              </div>
            </div>
          </div>
        </div>
        <!-- ------------------------Table end------------------------ -->
        <div>
          <MISAPagingFE
            v-if="isShowPagingBottom"
            :totalRecords="totalRecords"
            :totalPages="totalPages"
            :currentPage="currentPage"
            :pageLimitList="pageLimitList"
            @filter="getPageLimit"
            @paging="getPageNumber"
          ></MISAPagingFE>
        </div>
      </div>
    </div>
  </div>
  <MISALoading v-if="isLoading"></MISALoading>
  <MISAContextMenu
    :listItem="this.$_MISAResource.VN.TransferContext"
    :isShowContextMenu="isShowContextMenu"
    :posX="mouseX"
    :posY="mouseY"
    @click.stop="hideContextMenu"
    @getItemContext="getItemContext"
    v-click-outside="() => (isShowContextMenu = false)"
  ></MISAContextMenu>
  <MISAAssetTransferForm
    v-if="isFormDisplay"
    @onCloseForm="onCloseForm"
    @reLoad="reLoad"
    :transferAssetToUpdate="transferData"
    :actionMode="actionMode"
    @updateSuccess="toast_content_success = $event"
    @addSuccess="toast_content_success = $event"
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
        @click="btnDeleteMultiTransferAsset"
        :tabindex="1"
        ref="button"
        focus
      ></MISAButton>
    </MISAToast>
  </div>
  <div v-if="isShowToastDeleteSingle" class="blur">
    <MISAToast typeToast="warning" :content="toast_content_delete_single"
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
        @click="btnDeleteSingleTransferAsset"
        :tabindex="1"
        ref="button"
        focus
      ></MISAButton>
    </MISAToast>
  </div>
  <div v-if="isShowToastValidateBE" class="blur">
    <MISAToast
      typeToast="warning"
      :content="toast_content_warning + '.'"
      :moreInfo="moreInfo"
      ><MISAButton
        buttonSub
        textButton="Đóng"
        @click="callCloseToastWarning"
        focus
        ref="button"
        :tabindex="1"
        @keydown="callCheckTabIndex($event, 'islast')"
      ></MISAButton>
    </MISAToast>
  </div>
  <MISAToast
    v-if="isShowToastAddSuccess"
    typeToast="success"
    :content="toast_content_success"
  ></MISAToast>
  <MISAToast
    v-if="isShowToastUpdateSuccess"
    typeToast="update"
    :content="toast_content_success"
  ></MISAToast>

  <div v-if="isShowToastValidateAriseTransfer" class="blur">
    <MISAToast
      typeToast="warning"
      :content="toast_content_warning + '.'"
      :moreInfo="moreInfo"
      ><MISAButton
        buttonSub
        textButton="Đóng"
        @click="callCloseToastWarning"
        focus
        ref="button"
        :tabindex="1"
        @keydown="callCheckTabIndex($event, 'islast')"
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

import { formatMoney } from "../../helpers/common/format/format";
import { formatDate } from "../../helpers/common/format/format";
import { truncateText } from "../../helpers/common/format/format";
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
      // Dữ liệu của 1 chứng từ, để chuyển sang cho AssetTransferForm và update
      transferData: null,
      // Danh sách chi tiết bản ghi
      assets: [],
      // Danh sách các bản ghi đã chọn
      selectedRows: [],
      // Index của bản ghi đầu tiên trong danh sách
      lastIndex: 0,
      // Thông tin bản ghi chứng từ, dùng để xóa duy nhất 1 chứng từ
      transferAssetSingle: null,
      // Loading
      isLoading: false,
      transferAssetCode: null,

      // ----------------------------- Form -----------------------------
      isFormDisplay: false,
      actionMode: false,
      isSuccessAddOrUpdate: false,

      // ----------------------------- Paging -----------------------------
      // Số trang hiện tại
      pageNumber: 1,
      pageNumberTransfer: 1,
      // Số lượng bản ghi tối đa mỗi trang
      pageLimit: 20,
      pageLimitTransfer: 20,

      pageLimitList: [],
      pageLimitListTransfer: [],
      // Tổng bản ghi
      totalRecords: 0,
      totalRecordsTransfer: 0,
      // Tổng trang
      totalPages: 0,
      totalPagesTransfer: 0,
      // Trang hiện tại
      currentPage: 1,
      currentPageTransfer: 1,
      // Tổng nguyên giá
      totalPrice: "0",
      // Tổng giá trị còn lại
      totalResidualValue: "0",

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
      isShowToastDeleteSingle: false,
      toast_content_delete_single: null,
      isShowToastValidateBE: false,
      toast_content_warning: null,
      moreInfo: null,
      isShowToastAddSuccess: false,
      isShowToastUpdateSuccess: false,
      toast_content_success: null,
      isShowToastValidateAriseTransfer: false,

      // ----------------------------- Tab index -----------------------------
      buttonFocus: null,

      // =================================Context menu=================================
      // Hiển thị context menu
      isShowContextMenu: false,
      // Tọa độ x của menu
      mouseX: 0,
      // Tọa độ y của menu
      mouseY: 0,
      //
      rowIndex: -1,
      // Thông tin của bản ghi
      transferAssetContext: null,
    };
  },
  watch: {
    /**
     * Phân trang lại với giới hạn trang mới
     * @param {int} value giới hạn bản ghi
     * Author: LDTUAN (02/08/2023)
     */
    pageLimit(value) {
      this.pageLimit = value;
      this.pageNumber = 1;
      this.loadDataDetail();
    },

    /**
     * Phân trang theo số trang mới
     * @param {int} value số trang
     * Author: LDTUAN (02/08/2023)
     */
    pageNumber(value) {
      this.pageNumber = value;
      this.currentPage = value;
      this.loadDataDetail();
    },

    currentPageTransfer(value) {
      this.pageNumberTransfer = value;
      this.currentPageTransfer = value;
    },

    /**
     * Phân trang lại với giới hạn trang mới
     * @param {int} value giới hạn bản ghi
     * Author: LDTUAN (02/08/2023)
     */
    pageLimitTransfer(value) {
      this.loadData(1, value);
    },

    /**
     * Phân trang theo số trang mới
     * @param {int} value số trang
     * Author: LDTUAN (02/08/2023)
     */
    pageNumberTransfer(value) {
      this.loadData(value, this.pageLimitTransfer);
      this.currentPageTransfer = value;
    },
  },
  created() {
    // Tải danh sách option giới hạn bản ghi mỗi trang
    this.getPageLimitList();
    this.getPageLimitListTransfer();
    this.loadData();
    this.$_emitter.emit("onDisplayContent");
  },
  methods: {
    formatDate,
    formatMoney,
    truncateText,

    /**
     * Lấy dữ liệu của chứng từ
     * Author: LDTUAN (02/09/2023)
     */
    loadData(
      pageNumber = this.pageNumberTransfer,
      pageLimit = this.pageLimitTransfer
    ) {
      const originalTransfer = JSON.parse(
        JSON.stringify(this.selectedRowsByCheckBox)
      );
      this.isLoading = true;
      this.$_MISAApi.TransferAsset.Filter(pageNumber, pageLimit, null)
        .then((res) => {
          this.transferAssets = res.data.Data;
          this.totalPagesTransfer = res.data.TotalPages;
          this.totalRecordsTransfer = res.data.TotalRecords;
          this.assets = [];
          this.isLoading = false;

          if (this.transferAssets.length > 0)
            this.callRowOnClick(this.transferAssets[0]);

          var totalPrice = this.transferAssets.reduce(
            (total, transferAsset) => {
              return total + Math.round(transferAsset.Cost);
            },
            0
          );
          var totalResidualValue = this.transferAssets.reduce(
            (total, transferAsset) => {
              return total + Math.round(transferAsset.RemainingCost);
            },
            0
          );

          this.totalPrice = formatMoney(totalPrice);
          this.totalResidualValue = formatMoney(totalResidualValue);

          if (this.isSuccessAddOrUpdate) {
            setTimeout(() => {
              this.isShowToastAddSuccess = false;
              this.isShowToastUpdateSuccess = false;
              this.toast_content_success = null;
              this.isSuccessAddOrUpdate = false;
            }, 3000);
          }

          if (originalTransfer && originalTransfer.length > 0) {
            this.selectedRowsByCheckBox = originalTransfer;
          }
        })
        .catch((res) => {
          this.$processErrorResponse(res);
          this.isShowToastValidateBE = true;
          this.toast_content_warning = res.response.data.UserMessage;
          this.isLoading = false;
        });
    },

    reLoad() {
      this.isSuccessAddOrUpdate = true;
      switch (this.toast_content_success) {
        case this.$_MISAResource.VN.Form.Warning.Transfer.Success.Update:
          this.isShowToastUpdateSuccess = true;
          break;
        case this.$_MISAResource.VN.Form.Warning.Transfer.Success.Add:
          this.isShowToastAddSuccess = true;
          break;
      }
      this.pageNumberTransfer = 1;
      this.currentPageTransfer = 1;
      this.loadData();
    },

    /**
     * Lấy dữ liệu của chi tiết chứng từ
     * Author: LDTUAN (02/09/2023)
     */
    loadDataDetail() {
      // Vì mã code có thể chứa 1 số ký tự đặc biệt như # vì thường được sử dụng để đánh dấu một phần của URL được xử lý bởi JavaScript trên trình duyệt và không được gửi lên máy chủ
      // Vì vậy trước khi gửi phải mã hóa
      // BE tự mã hóa rồi nên chỉ cần làm ở FE
      const encodedTransferAssetCode = encodeURIComponent(
        this.transferAssetCode
      );
      this.$_MISAApi.TransferAsset.GetByCode(encodedTransferAssetCode)
        .then((res) => {
          const assets = res.data.FixedAssetTransfers;
          this.totalRecords = assets.length;
          this.totalPages = Math.ceil(this.totalRecords / this.pageLimit);
          const start = (this.currentPage - 1) * this.pageLimit;
          const end = start + this.pageLimit;
          this.assets = assets.slice(start, end);
          if (this.selectedRows.length === 0) {
            this.assets = [];
          }
        })
        .catch((res) => {
          this.$processErrorResponse(res);
          this.isShowToastValidateBE = true;
          this.toast_content_warning = res.response.data.UserMessage;
        });
    },

    /**
     * Xóa 1 hoặc nhiều bản ghi khi click vào button xóa
     * Author: LDTUAN (10/09/2023)
     */
    deleteTransferAssets(transferAssetIds) {
      this.isLoading = true;
      if (transferAssetIds && transferAssetIds.length > 0) {
        const requestData = transferAssetIds;
        this.$_MISAApi.TransferAsset.DeleteMany(requestData, {
          headers: { "Content-Type": "application/json" },
        })
          .then(() => {
            this.selectedRow = [];
            this.selectedRowsByCheckBox = [];
            this.loadData();
          })
          .then(() => {
            this.isLoading = false;
            this.toast_content_success =
              this.$_MISAResource.VN.Form.Warning.Transfer.Success.Delete;
            this.isShowToastUpdateSuccess = true;
            setTimeout(() => {
              this.isShowToastUpdateSuccess = false;
            }, 3000);
          })
          .catch((res) => {
            this.$processErrorResponse(res);
            this.isShowToastValidateBE = true;
            this.toast_content_warning = res.response.data.UserMessage;
            this.moreInfo = res.response.data.MoreInfo;
            this.isLoading = false;
          });
      }
      this.isShowToastDelete = false;
      this.isShowToastDeleteSingle = false;
    },

    /**
     * Danh sách số lượng bản ghi mỗi trang
     * Author: LDTUAN (02/08/2023)
     */
    getPageLimitList() {
      this.pageLimitList = [20, 10, 5];
    },

    /**
     * Danh sách số lượng bản ghi mỗi trang
     * Author: LDTUAN (02/08/2023)
     */
    getPageLimitListTransfer() {
      this.pageLimitListTransfer = [20, 10, 5];
    },

    /**
     * Thực hiện load lại dữ khi thay đổi số trang
     * @param {int} pageNumber số trang
     * Author: LDTUAN (02/08/2023)
     */
    getPageNumber(pageNumber) {
      this.pageNumber = pageNumber;
    },

    /**
     * Thực hiện load lại dữ khi thay đổi số trang
     * @param {int} pageNumber số trang
     * Author: LDTUAN (02/08/2023)
     */
    getPageNumberTransfer(pageNumber) {
      this.pageNumberTransfer = pageNumber;
    },

    /**
     * Thực hiện load lại dữ khi thay đổi giới hạn bản ghi
     * @param {int} pageLimit giới hạn bản ghi mỗi trang
     * Author: LDTUAN (02/08/2023)
     */
    getPageLimit(pageLimit) {
      this.pageLimit = pageLimit;
    },

    /**
     * Thực hiện load lại dữ khi thay đổi giới hạn bản ghi
     * @param {int} pageLimit giới hạn bản ghi mỗi trang
     * Author: LDTUAN (02/08/2023)
     */
    getPageLimitTransfer(pageLimit) {
      this.pageLimitTransfer = pageLimit;
    },

    //------------------------------------------- Click row -------------------------------------------

    /**
     * Thực hiện call hàm rowOnClick từ file js để bôi xanh 1 dòng nếu click vào dòng đó
     * @param {object} asset thông tin tài sản
     * Author: LDTUAN (02/08/2023)
     */
    callRowOnClick(transferAsset) {
      this.isShowContextMenu = false;
      rowOnClick.call(this, transferAsset, "transferAssets");
      this.transferAssetCode = transferAsset.TransferAssetCode;
      this.loadDataDetail(this.transferAssetCode);
    },

    callRowOnClickByCheckBox(transferAsset) {
      this.isShowContextMenu = false;
      rowOnClickByCheckBox.call(this, transferAsset, "transferAssets");
    },

    callRowOnCtrlClick(transferAsset) {
      this.isShowContextMenu = false;
      rowOnCtrlClick.call(this, transferAsset, "transferAssets");
    },

    rowOnShiftClick(asset) {
      if (event.shiftKey) {
        const index = this.transferAssets.indexOf(asset);
        let list = [];
        let newestIndex = index + 1;
        if (newestIndex <= this.lastIndex) {
          list = this.transferAssets.slice(newestIndex - 1, this.lastIndex + 1);
        } else {
          list = this.transferAssets.slice(this.lastIndex, newestIndex);
        }
        this.selectedRows = [];
        this.selectedRowsByCheckBox = list;
      }
    },

    /**
     * Thực hiện tick/bỏ tick tất cả bản ghi trong danh sách khi tick/bỏ tích checkbox
     * @param {checkbox} event input checkbox
     * Author: LDTUAN (02/08/2023)
     */
    headCheckboxClick(event) {
      if (event.target.checked) {
        for (const transfer of this.transferAssets) {
          const index = this.selectedRowsByCheckBox.findIndex(
            (selectedItem) =>
              selectedItem.TransferAssetId === transfer.TransferAssetId
          );
          if (index === -1) {
            this.selectedRowsByCheckBox.push(transfer);
          }
        }
      } else {
        for (const transfer of this.transferAssets) {
          const index = this.selectedRowsByCheckBox.findIndex(
            (selectedItem) =>
              selectedItem.TransferAssetId === transfer.TransferAssetId
          );
          if (index !== -1) {
            this.selectedRowsByCheckBox.splice(index, 1);
          }
        }
        this.selectedRows = [];
      }
    },

    btnUncheckedAll() {
      this.selectedRows = [];
      this.selectedRowsByCheckBox = [];
    },

    callToastDelete() {
      showToastDelete.call(
        this,
        this.$_MISAResource.VN.Form.Warning.DeleteTransfer.Single,
        this.$_MISAResource.VN.Form.Warning.DeleteTransfer.Multiple
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
      this.buttonFocus = "button";
      checkTabIndex.call(this, event, index);
    },

    //------------------------------------------- FORM -------------------------------------------
    /**
     * Mở form thêm chứng từ
     */
    btnAddDocument() {
      this.isFormDisplay = true;
      this.actionMode = this.$_MISAEnum.FORM_MODE.ADD;
    },

    /**
     * Đóng form thêm chứng từ
     */
    onCloseForm() {
      this.isFormDisplay = false;
      this.transferData = null;
      this.actionMode = null;
    },

    /**
     * Truyền dữ liệu và mở form thêm chứng từ để sửa đổi
     */
    // btnEditTransferAsset(transferAsset) {
    //   this.checkTransferAssetArise(transferAsset.TransferAssetId);
    //   if (this.isShowToastValidateAriseTransfer == false) {
    //     this.btnAddDocument();
    //     this.transferData = transferAsset;
    //     this.actionMode = this.$_MISAEnum.FORM_MODE.UPDATE;
    //   }
    // },

    btnEditTransferAsset(transferAsset) {
      this.$_MISAApi.TransferAsset.GetNewest(transferAsset.TransferAssetId)
        .then(() => {
          this.btnAddDocument();
          this.transferData = transferAsset;
          this.actionMode = this.$_MISAEnum.FORM_MODE.UPDATE;
        })
        .catch((res) => {
          this.$processErrorResponse(res);
          this.isShowToastValidateAriseTransfer = true;
          this.toast_content_warning = res.response.data.UserMessage;
          this.moreInfo = res.response.data.MoreInfo;
        });
    },

    /**
     * Mở form view thông tin chứng từ
     * @param {object} transferAsset Thông tin chứng từ
     * Author: LDTUAN (13/09/2023)
     */
    btnViewTransferAsset(transferAsset) {
      this.btnAddDocument();
      this.transferData = transferAsset;
      this.actionMode = this.$_MISAEnum.FORM_MODE.VIEW;
    },

    callToastDeleteSingle(transferAsset) {
      var listIds = [];
      listIds.push(transferAsset.TransferAssetId);
      console.log(listIds);
      this.transferAssetSingle = listIds;
      this.toast_content_delete_single =
        this.$_MISAResource.VN.Form.Warning.DeleteTransfer.Single +
        `<strong>${transferAsset?.TransferAssetCode}</strong>` +
        " không?";
      this.isShowToastDeleteSingle = true;
    },

    btnDeleteMultiTransferAsset() {
      const transferAssetIds = this.selectedRowsByCheckBox.map(
        (transfer) => transfer.TransferAssetId
      );
      this.deleteTransferAssets(transferAssetIds);
    },

    btnDeleteSingleTransferAsset() {
      this.deleteTransferAssets(this.transferAssetSingle);
    },

    //------------------------------------------- CONTEXT MENU -------------------------------------------
    /**
     * Chuột phải để mở context mennu
     * @param {*} event
     * @param {object} asset bản ghi
     * Author: LDTUAN (09/08/2023)
     */
    btnClickRight(event, transferAsset) {
      //event.preventDefault();
      this.isShowContextMenu = true;
      this.mouseX = event.clientX;
      this.mouseY = event.clientY;
      this.rowIndex = this.transferAssets.indexOf(transferAsset);
      this.transferAssetContext = transferAsset;
      this.selectedRows = [];
      this.selectedRows.push(this.transferAssetContext);
    },

    /**
     * Thực hiện hành động tương ứng với option đã chọn từ context menu
     * @param {int} index số index nhận từ option của context menu
     * Author: LDTUAN (09/08/2023)
     */
    getItemContext(index) {
      switch (index) {
        case 1:
          this.btnEditTransferAsset(this.transferAssetContext);
          break;
        case 2:
          this.callToastDeleteSingle(this.transferAssetContext);
          break;
      }
    },

    /**
     * Tắt context menu
     * Author: LDTUAN (09/08/2023)
     */
    hideContextMenu() {
      this.isShowContextMenu = false;
      this.rowIndex = -1;
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
    // Gọi hàm btnCollapseTable() hai lần để lấy đúng độ rộng của table khi lần đầu hiển thị
    for (let i = 0; i < 2; i++) {
      this.btnCollapseTable();
    }
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
    44px 50px 120px 150px 150px 140px 200px 461px
    120px;
  height: 35px;
  cursor: pointer;
}

.row-expand {
  display: grid;
  grid-template-columns:
    44px 50px 120px 150px 150px 140px 200px 597px
    120px;
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

.body--row:hover > .cell > .icon-function {
  display: flex;
}

.body--row > .cell > .icon-function {
  display: none;
  column-gap: 8px;
}

.row--selected > .cell > .icon-function {
  display: flex;
}

.header--row {
  background-color: var(--background-color-table-head);
  border-color: var(--table-border-color);
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
  overflow-x: hidden;
  border-color: var(--table-border-color);
}

.top-head-right {
  display: flex;
  align-items: center;
  column-gap: 20px;
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
