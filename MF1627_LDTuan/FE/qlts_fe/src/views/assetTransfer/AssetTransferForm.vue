<template>
  <div
    class="popup"
    tabindex="0"
    @keyup.esc="btnCancelTransferAsset"
    @keydown.ctrl.s="btnSaveTransferAsset"
  >
    <div class="popup__header">
      <span
        v-if="formMode === this.$_MISAEnum.FORM_MODE.ADD"
        class="header__title font-weight--500"
        >Thêm chứng từ điều chuyển
      </span>
      <span v-else class="header__title font-weight--500"
        >Sửa chứng từ điều chuyển
      </span>
      <MISAButton
        button_icon_normal
        exit
        bottom
        ref="exit"
        content="Thoát"
        :tabindex="12"
        @keydown="checkTabIndex($event, 'islast')"
        @click="btnCancelTransferAsset"
      ></MISAButton>
    </div>
    <div class="popup__body">
      <div class="body__title font-weight--500">Thông tin chung</div>
      <div class="body__content border-radius-6">
        <div class="content--top">
          <div class="content__row">
            <div class="content__column">
              <MISAInput
                normalGrid
                label="Mã chứng từ"
                ref="TransferAssetCode"
                @focus="setInputFocus('TransferAssetCode')"
                v-model="transferAsset.TransferAssetCode"
                medium
                required
                maxlength="12"
                typeOfValue="code"
                :tabindex="4"
                @keydown="checkTabIndex($event, 'isFirst')"
                :disabled="actionMode == this.$_MISAEnum.FORM_MODE.VIEW"
              ></MISAInput>
            </div>
            <div class="content__column">
              <MISAInputDatePicker
                label="Ngày chứng từ"
                ref="TransactionDate"
                @focus="setInputFocus('TransactionDate')"
                v-model="transferAsset.TransactionDate"
                medium
                required
                :tabindex="5"
                :disabled="actionMode == this.$_MISAEnum.FORM_MODE.VIEW"
              ></MISAInputDatePicker>
            </div>
            <div class="content__column">
              <MISAInputDatePicker
                label="Ngày điều chuyển"
                ref="TransferDate"
                @focus="setInputFocus('TransferDate')"
                v-model="transferAsset.TransferDate"
                medium
                required
                :tabindex="6"
                :disabled="actionMode == this.$_MISAEnum.FORM_MODE.VIEW"
              ></MISAInputDatePicker>
            </div>
            <div class="content__column">
              <MISAInput
                normalGrid
                label="Ghi chú"
                v-model="transferAsset.Description"
                medium
                maxlength="200"
                :tabindex="7"
                :disabled="actionMode == this.$_MISAEnum.FORM_MODE.VIEW"
              ></MISAInput>
            </div>
          </div>
          <div class="content__row--bot">
            <div class="checkbox">
              <div class="checkbox-combo" @click="showFormReceiver">
                <input
                  type="checkbox"
                  :checked="receivers.length > 0"
                  :disabled="actionMode == this.$_MISAEnum.FORM_MODE.VIEW"
                />
                <label class="font-weight--500">Chọn ban giao nhận</label>
              </div>
            </div>
            <div v-if="receivers.length > 0" class="checkbox">
              <div class="checkbox-combo" @click="showNewestReceiver">
                <input
                  type="checkbox"
                  :checked="isGetNewestReceiver"
                  :disabled="actionMode == this.$_MISAEnum.FORM_MODE.VIEW"
                />
                <label class="font-weight--500"
                  >Thêm ban giao nhận từ lần nhập trước</label
                >
              </div>
            </div>
          </div>
        </div>
        <div v-if="isCreateReceiver" class="content--bot">
          <div v-if="receivers[0]" class="content--bot__row">
            <div class="content--bot__header row--bot">
              <div>
                <MISATooltip top content="Số thứ tự">
                  <span>STT</span></MISATooltip
                >
              </div>
              <div>Họ và tên</div>
              <div class="content__column">Đại diện</div>
              <div class="content__column">Chức vụ</div>
            </div>
            <div class="row--form">
              <div
                v-for="(receiver, index) in receivers"
                :key="index"
                class="content--bot__body row--bot"
              >
                <div>
                  <div class="number border border-radius-4">
                    {{ index + 1 }}
                  </div>
                </div>
                <div>
                  <MISAInput
                    normalGrid
                    medium
                    maxlength="12"
                    placeholder="Nhập họ và tên"
                    v-model="receiver.ReceiverFullname"
                    :disabled="actionMode == this.$_MISAEnum.FORM_MODE.VIEW"
                  ></MISAInput>
                </div>
                <div class="content__column">
                  <MISAInput
                    normalGrid
                    medium
                    maxlength="12"
                    placeholder="Nhập đại diện"
                    v-model="receiver.ReceiverDelegate"
                    :disabled="actionMode == this.$_MISAEnum.FORM_MODE.VIEW"
                  ></MISAInput>
                </div>
                <div class="content__column">
                  <MISAInput
                    normalGrid
                    medium
                    maxlength="12"
                    placeholder="Nhập chức vụ"
                    v-model="receiver.ReceiverPosition"
                    :disabled="actionMode == this.$_MISAEnum.FORM_MODE.VIEW"
                  ></MISAInput>
                </div>
                <div class="content__column">
                  <div class="combo-icon">
                    <div
                      v-if="index !== 0"
                      :class="[{ disabled: this.$_MISAEnum.FORM_MODE.VIEW }]"
                    >
                      <MISATooltip right content="Chuyển lên">
                        <MISAIcon
                          @click="moveUp(index)"
                          pull_up_large
                          :disabled="
                            actionMode == this.$_MISAEnum.FORM_MODE.VIEW
                          "
                        ></MISAIcon>
                      </MISATooltip>
                    </div>
                    <div
                      v-if="index < receivers.length - 1"
                      :class="[{ disabled: this.$_MISAEnum.FORM_MODE.VIEW }]"
                    >
                      <MISATooltip right content="Chuyển xuống">
                        <MISAIcon
                          @click="moveDown(index)"
                          drop_down_large
                          :disabled="
                            actionMode == this.$_MISAEnum.FORM_MODE.VIEW
                          "
                        ></MISAIcon>
                      </MISATooltip>
                    </div>
                    <div
                      @click="btnAddReceiver"
                      :class="[{ disabled: this.$_MISAEnum.FORM_MODE.VIEW }]"
                    >
                      <MISATooltip right content="Thêm mới">
                        <MISAIcon
                          add_box_thin
                          :disabled="
                            actionMode == this.$_MISAEnum.FORM_MODE.VIEW
                          "
                        ></MISAIcon>
                      </MISATooltip>
                    </div>
                    <div
                      @click="btnDeleteReceiver(receiver)"
                      :class="[{ disabled: this.$_MISAEnum.FORM_MODE.VIEW }]"
                    >
                      <MISATooltip right content="Xóa">
                        <MISAIcon
                          v-if="index > 0"
                          deleteIcon
                          :disabled="
                            actionMode == this.$_MISAEnum.FORM_MODE.VIEW
                          "
                        ></MISAIcon
                      ></MISATooltip>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
      <div class="body__action border-radius-6">
        <div class="action--left">
          <div class="body__title font-weight--500">
            Thông tin tài sản điều chuyển
          </div>
          <MISAInput
            search
            normal
            medium
            placeholder="Tìm kiếm tài sản"
            maxlength="50"
            v-model="inputSearch"
            @searchByCode="searchByCode"
            :tabindex="8"
          ></MISAInput>
          <MISAButton
            v-if="
              formMode == this.$_MISAEnum.FORM_MODE.UPDATE &&
              (!this.assets || this.assets.length === 0)
            "
            combo
            large
            loading
            textButton="Load dữ liệu cũ"
            @click="loadOldData"
          ></MISAButton>
        </div>
        <div class="action--right">
          <div
            v-if="selectedRowsByCheckBox.length > 0"
            class="left font-size-default"
          >
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
              @click="btnDeleteSelectedAsset(this.selectedRowsByCheckBox)"
            ></MISAButton>
          </div>
          <MISAButton
            combo
            add_box
            textButton="Chọn tài sản"
            buttonSub
            basic
            large
            @click="btnShowFormChooseAsset"
            :disabled="actionMode == this.$_MISAEnum.FORM_MODE.VIEW"
            :tabindex="9"
            ref="ChooseAsset"
          ></MISAButton>
        </div>
      </div>
      <div class="body__form border-radius-4">
        <!-- ------------------------Table start------------------------ -->
        <div class="table border-radius-4">
          <!-- ------------------------Header------------------------ -->
          <div class="header--row row">
            <div
              class="header cell display--center-center border--right border--bottom"
            >
              <input
                type="checkbox"
                @click="headCheckboxClick($event)"
                :checked="
                  pagingAsset.every((assetItem) =>
                    selectedRowsByCheckBox.some(
                      (selectedItem) =>
                        selectedItem.FixedAssetId === assetItem.FixedAssetId
                    )
                  ) &&
                  pagingAsset.length > 0 &&
                  selectedRowsByCheckBox.length > 0
                "
                :disabled="actionMode == this.$_MISAEnum.FORM_MODE.VIEW"
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
              Mã tài sản
            </div>
            <div
              class="header cell display--center-left font-weight--700 border--right border--bottom padding--left-10"
            >
              Tên tài sản
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
              Bộ phận sử dụng
            </div>
            <div
              class="header cell display--center-center font-weight--700 border--right border--bottom"
            >
              Bộ phần điều chuyển đến
            </div>
            <div
              class="header cell display--center-left font-weight--700 border--right border--bottom padding--left-10"
            >
              Lý do
            </div>
            <div
              class="header cell display--center-center font-weight--700 border--bottom"
            >
              Chức năng
            </div>
          </div>
          <!-- ------------------------Body------------------------ -->
          <div class="body-data relative">
            <div
              class="body--row row"
              v-for="asset in pagingAsset"
              :key="asset.FixedAssetId"
              :class="[
                {
                  'row--selected': selectedRowsByCheckBox.some(
                    (item) => item.FixedAssetId === asset.FixedAssetId
                  ),
                },
                {
                  'row--selected': selectedRows.includes(asset),
                },
              ]"
              @click.exact.stop="callRowOnClick(asset)"
              @click.ctrl.stop="callRowOnCtrlClick(asset)"
              @click.shift.stop="rowOnShiftClick(asset)"
            >
              <div
                class="cell display--center-center border--right border--bottom"
                @click.stop="callRowOnClickByCheckBox(asset)"
              >
                <input
                  type="checkbox"
                  :checked="
                    selectedRowsByCheckBox.some(
                      (item) => item.FixedAssetId === asset.FixedAssetId
                    )
                  "
                  :disabled="actionMode == this.$_MISAEnum.FORM_MODE.VIEW"
                />
              </div>
              <div
                class="cell display--center-center border--right border--bottom"
              >
                {{
                  pagingAsset.indexOf(asset) + 1 + pageLimit * (currentPage - 1)
                }}
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
                {{ AssetDepreciation(asset.Cost, asset.LifeTime) }}
              </div>
              <div
                class="cell display--center-left border--right border--bottom padding--left-10"
              >
                <el-tooltip
                  v-if="asset.DepartmentName.length > 20"
                  :visible="visible"
                  placement="right"
                >
                  <template #content>
                    <span>{{ asset.DepartmentName }}</span>
                  </template>
                  <span>{{ truncateText(asset.DepartmentName, 20) }}</span>
                </el-tooltip>
                <span v-else>{{ truncateText(asset.DepartmentName, 20) }}</span>
              </div>
              <div
                id="cell"
                class="cell display--center-center border--right border--bottom"
              >
                <MISACombobox
                  :api="this.$_MISAApi.Department.Api"
                  propText="DepartmentName"
                  propValue="DepartmentId"
                  placeholder="Bộ phận sử dụng"
                  :existCode="asset.NewDepartmentName"
                  :inputChange="inputChange"
                  @filter="getNewDepartment(asset, $event)"
                  @deleteDepartment="deleteDepartment(asset, $event)"
                  :newDepartment="asset.NewDepartmentName"
                  isDisplay
                  self_adjust_size
                  :disabled="actionMode == this.$_MISAEnum.FORM_MODE.VIEW"
                ></MISACombobox>
              </div>
              <div
                class="cell display--center-left border--right border--bottom"
              >
                <MISAInput
                  normalGrid
                  medium
                  padding_2
                  v-model="asset.Description"
                  maxlength="255"
                  :disabled="actionMode == this.$_MISAEnum.FORM_MODE.VIEW"
                ></MISAInput>
              </div>
              <div class="cell display--center-center border--bottom">
                <div
                  class="icon-function"
                  :class="[
                    { disabled: actionMode == this.$_MISAEnum.FORM_MODE.VIEW },
                  ]"
                >
                  <MISAIcon
                    deleteIcon
                    background
                    @click="btnDeleteAsset(asset)"
                    :disabled="actionMode == this.$_MISAEnum.FORM_MODE.VIEW"
                  ></MISAIcon>
                </div>
              </div>
            </div>
          </div>
        </div>
        <!-- ------------------------Table end------------------------ -->
        <MISACalculator
          form="transfer-form"
          :totalPrice="totalPrice"
          :totalResidualValue="totalResidualValue"
          :numberColumnLeft="4"
          :numberColumnRight="3"
        ></MISACalculator>
        <MISAPagingFE
          :totalRecords="totalRecords"
          :totalPages="totalPages"
          :currentPage="currentPage"
          :pageLimitList="pageLimitList"
          @filter="getPageLimit"
          @paging="getPageNumber"
        ></MISAPagingFE>
      </div>
    </div>
    <div class="popup__footer">
      <MISAButton
        buttonOutline
        textButton="Hủy"
        @click="btnCancelTransferAsset"
        :disabled="actionMode == this.$_MISAEnum.FORM_MODE.VIEW"
        :tabindex="11"
      ></MISAButton>
      <MISAButton
        buttonMain
        textButton="Lưu"
        @click="btnSaveTransferAsset"
        :disabled="actionMode == this.$_MISAEnum.FORM_MODE.VIEW"
        :tabindex="10"
      ></MISAButton>
    </div>
  </div>
  <MISAAssetTransferChooseForm
    @onCloseForm="onCloseForm"
    v-if="isShowFormChooseAsset"
    @loadData="loadData"
    :existFixedAsset="existFixedAsset"
    :deletedAssets="deletedAssets"
  ></MISAAssetTransferChooseForm>

  <!-- ======================================================= TOAST ======================================================= -->
  <div v-if="isShowToastValueChange" class="blur">
    <MISAToast typeToast="warning" :content="toast_content_warning"
      ><MISAButton
        buttonOutline
        textButton="Đóng"
        @click="btnCloseToastWarning"
        :tabindex="1"
        ref="button"
      ></MISAButton>
      <MISAButton
        buttonSub
        textButton="Hủy bỏ"
        @click="btnCloseForm"
        :tabindex="2"
      ></MISAButton>
      <MISAButton
        buttonMain
        textButton="Lưu"
        @click="btnSaveTransferAsset"
        :tabindex="3"
        focus
        @keydown="checkTabIndex($event, 'islast')"
      ></MISAButton
    ></MISAToast>
  </div>
  <div v-if="isShowToastValidateBE" class="blur">
    <MISAToast typeToast="warning" :content="toast_content_warning + '.'"
      ><MISAButton
        buttonSub
        textButton="Đóng"
        @click="btnCloseToastWarning"
        focus
        ref="button"
        :tabindex="1"
        @keydown="checkTabIndex($event, 'islast')"
      ></MISAButton>
    </MISAToast>
  </div>
  <MISALoading v-if="isLoading"></MISALoading>
</template>
<script>
import { rowOnClick } from "../../helpers/table/selectRow";
import { rowOnCtrlClick } from "../../helpers/table/selectRow";
import { rowOnClickByCheckBox } from "../../helpers/table/selectRow";
import MISAAssetTransferChooseForm from "./AssetTransferChooseForm.vue";

import { formatMoneyToInt } from "../../helpers/common/format/format";
import { formatMoney } from "../../helpers/common/format/format";
import { truncateText } from "../../helpers/common/format/format";
import { AssetDepreciation } from "../../helpers/common/format/format";
import { DateFormat } from "../../helpers/common/format/format";
export default {
  name: "MISAAssetTransferForm",
  components: {
    MISAAssetTransferChooseForm,
  },
  props: {
    transferAssetToUpdate: {
      type: Object,
      default: null,
    },
    actionMode: {
      type: Boolean,
      default: false,
    },
  },
  data() {
    return {
      // ----------------------------- Data -----------------------------
      // Toàn bộ dữ liệu của chứng từ điều chuyển
      transferData: {},
      // Chứng từ tài sản
      transferAsset: {},
      originalTransferAsset: {},
      // Danh sách bản ghi tài sản
      assets: [],
      // Danh sách bản ghi trước khi cập nhật (cái này sẽ có 1 vài field thay đổi theo assets để thực hiện cho việc cập nhật)
      oldAssets: [],
      // Danh sách bản ghi từ db, các bản ghi này có giá trị không đổi, nó thể hiện đúng dữ liệu hiện có trong db
      originalAssets: [],
      // Danh sách bản ghi từ db, các bản ghi này có giá trị không đổi, nó thể hiện đúng dữ liệu hiện có trong db
      originalReceivers: [],
      // Tài sản dùng để phân trang
      pagingAsset: [],
      // Danh sách người nhận
      receivers: [],
      oldReceivers: [],
      // Danh sách người nhận mới nhất (từ lần nhập trước đó)
      newestReceivers: [],
      // Danh sách các bản ghi đã chọn
      selectedRows: [],
      // Index của bản ghi đầu tiên trong danh sách
      lastIndex: 0,
      // Hiển thị phần tạo người nhận
      isCreateReceiver: false,
      // Danh sách chứa các phần tử bị xóa, các phần từ này là các chi tiết chứng từ đã tồn tại trong chứng từ, có transferAssetDetailId
      deletedAssets: null,

      // ----------------------------- Form -----------------------------
      isFormDisplay: false,
      // Hiển thị form chọn tài sản
      isShowFormChooseAsset: false,
      // Danh sách bản ghi đã tồn tại trong form này
      existFixedAsset: null,
      isLoading: false,

      // ----------------------------- Paging -----------------------------
      // Số trang hiện tại
      pageNumber: 1,
      // Số lượng bản ghi tối đa mỗi trang
      pageLimit: 20,
      // Số lượng bản ghi tối đa mỗi trang
      pageLimitList: [],
      // Tổng bản ghi
      totalRecords: 0,
      // Tổng trang
      totalPages: 0,
      // Trang hiện tại
      currentPage: 1,
      // Tổng nguyên giá
      totalPrice: "0",
      // Tổng giá trị còn lại
      totalResidualValue: "0",

      // ----------------------------- Checkbox -----------------------------
      // Tick ô checkbox input
      isCheckboxChecked: false,
      // Danh sách các bản ghi đã chọn
      selectedRowsByCheckBox: [],

      // ----------------------------- Toast -----------------------------
      isShowToastDelete: false,
      toast_content_delete: null,

      // ----------------------------- Tab index -----------------------------
      buttonFocus: "",

      // ----------------------------- COMBOBOX -----------------------------
      inputChange: null,

      // ----------------------------- TOAST -----------------------------
      isShowToastValueChange: false,
      toast_content_warning: null,
      isShowToastValidateBE: false,
      moreInfo: null,
      //
      // Focus vào ô nhập liệu, ban đầu là mã chứng từ
      inputFocus: "TransferAssetCode",

      // ----------------------------- SEARCH -----------------------------
      inputSearch: null,
    };
  },
  computed: {
    /**
     * Kiểm trả xem chức năng user đang thực hiện là thêm mới hay cập nhật tài sản
     * Author: LDTUAN (03/09/2023)
     */
    formMode() {
      switch (this.actionMode) {
        case this.$_MISAEnum.FORM_MODE.UPDATE:
          return this.$_MISAEnum.FORM_MODE.UPDATE;
        case this.$_MISAEnum.FORM_MODE.VIEW:
          return this.$_MISAEnum.FORM_MODE.VIEW;
        default:
          return this.$_MISAEnum.FORM_MODE.ADD;
      }
    },

    /**
     * Kiểm tra xem danh sách ban giao nhận có bằng ban giao nhận từ lần nhập trước không
     */
    isGetNewestReceiver() {
      if (this.receivers.length !== this.newestReceivers.length) {
        return false;
      }
      // some để kiểm tra xem có ít nhất một phần tử trong
      // this.receiver không tìm thấy trong newestReceivers hoặc ngược lại
      const result = this.receivers.some(
        (item) =>
          !this.newestReceivers.some(
            (newItem) =>
              newItem.ReceiverFullname === item.ReceiverFullname &&
              newItem.ReceiverDelegate === item.ReceiverDelegate &&
              newItem.ReceiverPosition === item.ReceiverPosition
          )
      );
      // Trả về true nếu danh sách bằng nhau, ngược lại trả về false
      return !result;
    },
  },
  mounted() {
    this.$refs[this.inputFocus].focusInput();
    window.addEventListener("keydown", this.disableCtrlS);
  },
  beforeUnmount() {
    // Loại bỏ sự kiện keydown khi component bị hủy
    window.removeEventListener("keydown", this.disableCtrlS);
  },
  created() {
    // Tải danh sách option giới hạn bản ghi mỗi trang
    this.getPageLimitList();

    /**
     * Kiểm tra thực hiện thêm, sửa, hay sao chép để thực hiện thao tác tương ứng
     * Author: LDTUAN (02/08/2023)
     */
    switch (this.formMode) {
      case this.$_MISAEnum.FORM_MODE.UPDATE:
        this.loadTransferDataUpdate();
        break;
      case this.$_MISAEnum.FORM_MODE.VIEW:
        this.loadTransferDataView();
        break;
      case this.$_MISAEnum.FORM_MODE.ADD:
        this.loadTransferAssetCode();
        break;
    }
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
      this.currentPage = 1;
      this.pagingAssetFE();
    },

    /**
     * Phân trang theo số trang mới
     * @param {int} value số trang
     * Author: LDTUAN (02/08/2023)
     */
    pageNumber(value) {
      this.pageNumber = value;
      this.currentPage = value;
      this.pagingAssetFE();
    },

    currentPage(value) {
      this.pageNumber = value;
      this.currentPage = value;
    },
  },
  methods: {
    disableCtrlS(event) {
      // Kiểm tra nếu Ctrl (hoặc Command trên macOS) và phím S được nhấn cùng lúc
      if ((event.ctrlKey || event.metaKey) && event.key === "s") {
        // Ngăn chặn sự kiện mặc định (lưu trang web)
        event.preventDefault();
      }
    },

    AssetDepreciation,
    formatMoney,
    formatMoneyToInt,
    truncateText,

    //------------------------------------------- ADD-UPDATE CHỨNG TỪ -------------------------------------------
    /**
     * Lưu chứng từ
     * Author: LDTUAN (04/09/2023)
     */
    // TODO: validate
    btnSaveTransferAsset() {
      this.validate();
      if (!this.isShowToastValidateBE) {
        switch (this.formMode) {
          case this.$_MISAEnum.FORM_MODE.UPDATE:
            this.updateTransferAssetHelper(this.$_MISAEnum.FORM_MODE.UPDATE);
            break;
          case this.$_MISAEnum.FORM_MODE.ADD:
            this.AddTransferAsset();
            break;
        }
      }
    },

    /**
     * Cập nhật chứng từ
     * Author: LDTUAN (04/09/2023)
     */
    UpdateTransferAsset(
      transferAssetId,
      newTransferAsset,
      listTransferAssetDetail,
      listReceiverFinal
    ) {
      var listDetail = listTransferAssetDetail.map((item) => ({
        ...item,
        Description: item.Description ?? "",
      }));
      let transfer = {
        TransferAsset: newTransferAsset,
        ListTransferAssetDetail: listDetail,
        ListReceiver: listReceiverFinal,
      };
      this.$_MISAApi.TransferAsset.Update(transferAssetId, transfer)
        .then(() => {
          this.btnCloseForm();
          // Phải để emit này lên trên reLoad, nếu k nó sẽ gọi reload trước, load xong mới gọi đến emit này
          this.$emit(
            "updateSuccess",
            this.$_MISAResource.VN.Form.Warning.Transfer.Success.Update
          );
          this.$emit("reLoad");
        })
        .catch((res) => {
          this.$processErrorResponse(res);
          this.isShowToastValidateBE = true;
          this.toast_content_warning = res.response.data.UserMessage;
          if (this.toast_content_warning.includes("Ngày điều chuyển")) {
            this.inputFocus = "TransferDate";
          }
          if (this.toast_content_warning.includes("Mã chứng từ")) {
            this.inputFocus = "TransferAssetCode";
          }
        });
    },

    /**
     * Chuẩn bị dữ liệu rồi trả về cho hàm UpdateTransferAsset gọi đến để thực hiện gọi đến api
     * Author: LDTUAN (04/09/2023)
     */
    updateTransferAssetHelper(action) {
      this.prepareDataForAsset();
      // 1. Lấy thông tin của chứng từ điều chuyển cũ
      this.transferAsset.TransactionDate = DateFormat(
        this.transferAsset.TransactionDate
      );
      this.transferAsset.TransferDate = DateFormat(
        this.transferAsset.TransferDate
      );
      this.transferAsset.Description = this.transferAsset.Description
        ? this.transferAsset.Description.trim()
        : "";
      let oldTransferAsset = this.transferAsset;

      // 2.Lấy id của chứng từ điều chuyển
      let transferAssetId = oldTransferAsset.TransferAssetId;

      // 3.Lấy thông tin của chứng từ điều chuyển mới
      let newTransferAsset = {
        TransferAssetCode: oldTransferAsset.TransferAssetCode,
        TransferDate: oldTransferAsset.TransferDate,
        TransactionDate: oldTransferAsset.TransactionDate,
        Description:
          oldTransferAsset.Description === null
            ? ""
            : oldTransferAsset.Description,
      };
      // 3.1.Xem thông tin chứng từ có đổi gì không
      const arePropertiesEqual =
        this.originalTransferAsset.TransferAssetCode ===
          newTransferAsset.TransferAssetCode &&
        this.originalTransferAsset.TransferDate ===
          newTransferAsset.TransferDate &&
        this.originalTransferAsset.TransactionDate ===
          newTransferAsset.TransactionDate &&
        (this.originalTransferAsset.Description === null
          ? ""
          : this.originalTransferAsset.Description) ===
          (newTransferAsset.Description === null
            ? ""
            : newTransferAsset.Description);

      let selectedFieldsDetail = [
        "FixedAssetId",
        "OldDepartmentId",
        "NewDepartmentId",
        "Description",
      ];
      let selectedFieldsReceiver = [
        "ReceiverCode",
        "ReceiverFullname",
        "ReceiverDelegate",
        "ReceiverPosition",
      ];

      // 4.Chuyển DepartmentName và DepartmentId thành OldDepartmentName và OldDepartmentId trong this.assets
      let assets = this.assets;
      assets.forEach((asset) => {
        asset.OldDepartmentName = asset.DepartmentName;
        asset.OldDepartmentId = asset.DepartmentId;
      });

      // 5.Tương tự, chuyển DepartmentName và DepartmentId thành OldDepartmentName và OldDepartmentId trong this.oldAssets
      let oldAssets = [...this.oldAssets];
      oldAssets.forEach((oldAsset) => {
        oldAsset.OldDepartmentName = oldAsset.DepartmentName;
        oldAsset.OldDepartmentId = oldAsset.DepartmentId;
      });
      let listTransferAssetDetail = this.createAddUpdateDeleteList(
        assets,
        "TransferAssetDetailId",
        oldAssets,
        selectedFieldsDetail
      );

      // 6.Validate xem trong add và update coi có bản ghi nào có phòng ban giống nhau không, nếu có thì báo lỗi ngay
      // var listAddUpdate = listTransferAssetDetail.filter(
      //   (asset) => asset.Flag == 1 || asset.Flag == 0
      // );
      // 7.tạo enum cho flag
      listTransferAssetDetail.forEach((asset) => {
        if (
          !asset.OldDepartmentId ||
          asset.OldDepartmentId === null ||
          asset.OldDepartmentId == "" ||
          !asset.NewDepartmentId ||
          asset.NewDepartmentId === null ||
          asset.NewDepartmentId == "" ||
          asset.OldDepartmentId === asset.NewDepartmentId
        ) {
          // 7.1ném ra toast lỗi ở đây
          this.isShowToastValidateBE = true;
          this.toast_content_warning =
            this.$_MISAResource.VN.Form.Warning.Department.Duplicate;
        }
      });

      // 8.Lọc các bản ghi không thay đổi dữ liệu (chỉ áp dụng với trường hợp update)
      /******************************/
      // 8.1.Lấy danh sách các bản ghi update (khi update có những description bằng null hoặc rỗng, thì quy hết về rỗng để so sánh)
      var listUpdate = listTransferAssetDetail
        .filter((asset) => asset.Flag == 1)
        .map((item) => ({
          ...item,
          Description: item.Description ?? "",
        }));
      // 8.2.Lấy danh sách bản ghi nguyên thủy từ db
      var originalAssets = this.originalAssets.map((item) => ({
        ...item,
        Description: item.Description ?? "",
      }));

      // 8.3.Tạo một danh sách ID trùng nhau
      const commonIds = listUpdate
        .filter((updateItem) =>
          originalAssets.some(
            (oldItem) =>
              oldItem.TransferAssetDetailId === updateItem.TransferAssetDetailId
          )
        )
        .map((item) => item.TransferAssetDetailId);

      // 8.4.Lọc ra các bản ghi không thay đổi dựa vào newDepartmentId và description vì đây là 2 field duy nhất có thể thay đổi
      const unchangedRecords = commonIds
        .filter((transferAssetDetailId) => {
          const updateItem = listUpdate.find(
            (item) => item.TransferAssetDetailId === transferAssetDetailId
          );
          const oldItem = originalAssets.find(
            (item) => item.TransferAssetDetailId === transferAssetDetailId
          );

          // 8.4.1Kiểm tra điều kiện thay đổi (ví dụ: NewDepartmentId và Description)
          return (
            updateItem.NewDepartmentId === oldItem.NewDepartmentId &&
            updateItem.Description === oldItem.Description
          );
        })
        .map((transferAssetDetailId) => {
          const updateItem = listUpdate.find(
            (item) => item.TransferAssetDetailId === transferAssetDetailId
          );
          return { ...updateItem };
        });

      // 8.5.Sửa các flag thành 3
      const unchangedIds = unchangedRecords.map(
        (record) => record.TransferAssetDetailId
      );
      listTransferAssetDetail.forEach((asset) => {
        if (unchangedIds.includes(asset.TransferAssetDetailId)) {
          asset.Flag = 3;
        }
      });

      // 9.Sử dụng hàm createAddUpdateDeleteList cho listReceiver
      let listReceiverFinal = this.createAddUpdateDeleteList(
        this.receivers,
        "ReceiverId",
        this.oldReceivers,
        selectedFieldsReceiver
      );

      // 9.1.Gắn flag 3 cho những receiver không đổi dữ liệu
      listReceiverFinal
        .filter((item) => item.Flag == 1)
        .forEach((updateItem) => {
          const matchingItem = this.originalReceivers.find(
            (originalItem) => originalItem.ReceiverId === updateItem.ReceiverId
          );
          if (
            matchingItem &&
            matchingItem.ReceiverFullname === updateItem.ReceiverFullname &&
            matchingItem.ReceiverDelegate === updateItem.ReceiverDelegate &&
            matchingItem.ReceiverPosition === updateItem.ReceiverPosition
          ) {
            updateItem.Flag = 3;
          }
        });

      // Nếu không thay đổi dữ liệu gì thì đóng form
      var listAssetIdAdd = listTransferAssetDetail.filter(
        (asset) => asset.Flag == 0
      );
      var listAssetIdUpdate = listTransferAssetDetail.filter(
        (asset) => asset.Flag == 1
      );
      var listAssetIdDelete = listTransferAssetDetail.filter(
        (asset) => asset.Flag == 2
      );
      var listAssetIdUnchange = listTransferAssetDetail.filter(
        (asset) => asset.Flag == 3
      );
      var listReceiverIdAdd = listReceiverFinal.filter(
        (asset) => asset.Flag == 0
      );
      var listReceiverIdUpdate = listReceiverFinal.filter(
        (asset) => asset.Flag == 1
      );
      var listReceiverIdDelete = listReceiverFinal.filter(
        (asset) => asset.Flag == 2
      );

      switch (this.formMode) {
        case this.$_MISAEnum.FORM_MODE.UPDATE:
          if (
            arePropertiesEqual &&
            listAssetIdAdd == 0 &&
            listAssetIdUpdate == 0 &&
            listAssetIdDelete == 0 &&
            listAssetIdUnchange != 0 &&
            listReceiverIdAdd == 0 &&
            listReceiverIdUpdate == 0 &&
            listReceiverIdDelete == 0
          ) {
            if (action == this.$_MISAEnum.FORM_MODE.UPDATE) {
              this.isShowToastValidateBE = true;
              this.toast_content_warning =
                this.$_MISAResource.VN.Form.Warning.Transfer.Save.UnChange;
            }
            if (action == this.$_MISAEnum.FORM_MODE.CANCEL) {
              this.btnCloseForm();
            }
          } else {
            if (action == this.$_MISAEnum.FORM_MODE.CANCEL) {
              this.isShowToastValueChange = true;
              this.toast_content_warning =
                this.$_MISAResource.VN.Form.Warning.Transfer.Edit.ValueChange;
            } else if (action == this.$_MISAEnum.FORM_MODE.UPDATE) {
              if (!this.assets || this.assets.length <= 0) {
                this.buttonFocus = "ChooseAsset";
                this.isShowToastValidateBE = true;
                this.toast_content_warning =
                  this.$_MISAResource.VN.Form.Warning.Transfer.Add.NoAsset;
              } else {
                if (!this.isShowToastValidateBE) {
                  this.UpdateTransferAsset(
                    transferAssetId,
                    newTransferAsset,
                    listTransferAssetDetail,
                    listReceiverFinal
                  );
                }
              }
            }
          }
          break;
        case this.$_MISAEnum.FORM_MODE.ADD:
          if (
            listAssetIdAdd == 0 &&
            listAssetIdUpdate == 0 &&
            listAssetIdDelete == 0 &&
            listReceiverIdAdd == 0 &&
            listReceiverIdUpdate == 0 &&
            listReceiverIdDelete == 0
          ) {
            this.btnCloseForm();
          } else {
            if (action == this.$_MISAEnum.FORM_MODE.CANCEL) {
              this.isShowToastValueChange = true;
              this.toast_content_warning =
                this.$_MISAResource.VN.Form.Warning.Transfer.Edit.ValueChange;
            }
          }
          break;
      }
    },

    prepareDataForAsset() {
      this.assets.forEach((asset) => {
        const match = this.pagingAsset.find(
          (paging) => paging.FixedAssetId === asset.FixedAssetId
        );
        if (match) {
          asset.NewDepartmentId = match.NewDepartmentId;
          asset.NewDepartmentName = match.NewDepartmentName;
          asset.NewDepartmentCode = match.NewDepartmentCode;
          asset.Description = match.Description;
        }
      });
    },

    createAddUpdateDeleteList(sourceList, idField, oldList, selectedFields) {
      // 1.Danh sách add
      let listAdd = sourceList
        .filter((item) => !Object.prototype.hasOwnProperty.call(item, idField))
        .map((item) => {
          let newItem = {
            Flag: 0,
          };

          // Chọn các thuộc tính quan trọng từ selectedFields
          for (const field of selectedFields) {
            newItem[field] = item[field];
          }

          return newItem;
        });

      // 2.Danh sách update-delete
      let listUD = sourceList.filter(
        (item) =>
          item[idField] !== null &&
          item[idField] !== "" &&
          typeof item[idField] !== "undefined" &&
          Object.prototype.hasOwnProperty.call(item, idField)
      );

      let listDelete = oldList
        .filter((oldItem) => {
          return !listUD.find(
            (newItem) => newItem[idField] === oldItem[idField]
          );
        })
        .map((item) => {
          let newItem = {
            Flag: 2,
            [idField]: item[idField],
          };
          for (const field of selectedFields) {
            newItem[field] = item[field];
          }

          return newItem;
        });

      let listUpdate = listUD
        .filter((newItem) => {
          return oldList.find(
            (oldItem) => newItem[idField] === oldItem[idField]
          );
        })
        .map((newItem) => {
          let updatedItem = {
            Flag: 1,
            [idField]: newItem[idField],
          };
          for (const field of selectedFields) {
            updatedItem[field] = newItem[field];
          }

          return updatedItem;
        });

      // 3.Nối các danh sách lại với nhau
      return [...listAdd, ...listDelete, ...listUpdate];
    },

    /**
     * Thêm mới chứng từ
     * Author: LDTUAN (04/09/2023)
     *
     */
    AddTransferAsset() {
      this.prepareDataForAsset();
      this.transferData.ListReceiver = this.receivers;
      let nullReceiver = false;
      this.receivers.forEach((receiver) => {
        if (!receiver.ReceiverFullname.trim()) {
          nullReceiver = true;
        }
      });
      if (nullReceiver == true) {
        this.isShowToastValidateBE = true;
        this.toast_content_warning =
          this.$_MISAResource.VN.Form.Warning.Transfer.Add.NoReceiver;
      } else {
        if (!this.assets || this.assets.length <= 0) {
          this.buttonFocus = "ChooseAsset";
          this.isShowToastValidateBE = true;
          this.toast_content_warning =
            this.$_MISAResource.VN.Form.Warning.Transfer.Add.NoAsset;
        } else {
          const transferAssetDetails = this.assets.map((asset) => ({
            FixedAssetId: asset.FixedAssetId,
            OldDepartmentId: asset.DepartmentId,
            NewDepartmentId: asset.NewDepartmentId,
            Description: asset.Description ? asset.Description.trim() : "",
          }));
          this.transferData.ListTransferAssetDetail = transferAssetDetails;
          this.transferAsset.TransactionDate = DateFormat(
            this.transferAsset.TransactionDate
          );
          this.transferAsset.TransferDate = DateFormat(
            this.transferAsset.TransferDate
          );
          this.transferAsset.Description = this.transferAsset.Description
            ? this.transferAsset.Description.trim()
            : "";
          this.transferData.TransferAsset = this.transferAsset;
          this.$_MISAApi.TransferAsset.Create(this.transferData)
            .then(() => {
              this.btnCloseForm();
              // Phải để emit này lên trên reLoad, nếu k nó sẽ gọi reload trước, load xong mới gọi đến emit này
              this.$emit(
                "addSuccess",
                this.$_MISAResource.VN.Form.Warning.Transfer.Success.Add
              );
              this.$emit("reLoad");
            })
            .catch((res) => {
              this.$processErrorResponse(res);
              this.isShowToastValidateBE = true;
              this.toast_content_warning = res.response.data.UserMessage;
              if (this.toast_content_warning.includes("Ngày điều chuyển")) {
                this.inputFocus = "TransferDate";
              }
              if (this.toast_content_warning.includes("Mã chứng từ")) {
                this.inputFocus = "TransferAssetCode";
              }
            });
        }
      }
    },

    //------------------------------------------- TRANSFER ASSET -------------------------------------------
    /**
     * Lấy mã code mới cho chứng từ
     */
    loadTransferAssetCode() {
      this.isLoading = true;
      this.$_MISAApi.TransferAsset.GetNewCode()
        .then((res) => {
          this.transferAsset.TransferAssetCode = res.data;
          this.transferAsset.TransferDate = DateFormat(this.$getCurrentDate());
          this.transferAsset.TransactionDate = DateFormat(
            this.$getCurrentDate()
          );
          this.isLoading = false;
        })
        .catch((res) => {
          this.$processErrorResponse(res);
          this.isShowToastValidateBE = true;
          this.toast_content_warning = res.response.data.UserMessage;
        });
    },

    async loadTransferDataView() {
      var oldTransferAsset = this.transferAssetToUpdate;
      // Vì mã code có thể chứa 1 số ký tự đặc biệt như # vì thường được sử dụng để đánh dấu một phần của URL được xử lý bởi JavaScript trên trình duyệt và không được gửi lên máy chủ
      // Vì vậy trước khi gửi phải mã hóa
      // BE tự mã hóa rồi nên chỉ cần làm ở FE
      const encodedTransferAssetCode = encodeURIComponent(
        oldTransferAsset.TransferAssetCode
      );
      await this.$_MISAApi.TransferAsset.GetByCode(encodedTransferAssetCode)
        .then((res) => {
          if (res.data.ReceiverTransfers[0]) {
            this.receivers = res.data.ReceiverTransfers;
            this.isCreateReceiver = true;
          }

          this.transferAsset = {
            TransferAssetId: res.data.TransferAssetId,
            TransferAssetCode: res.data.TransferAssetCode,
            TransferDate: DateFormat(res.data.TransferDate),
            TransactionDate: DateFormat(res.data.TransactionDate),
            Description: res.data.Description,
          };

          this.loadData(res.data.FixedAssetTransfers);
          // Duyệt qua mảng assets và chuyển giá trị của olddpepartment sang department
          this.assets.forEach((asset) => {
            asset.DepartmentName = asset.OldDepartmentName;
            asset.DepartmentId = asset.OldDepartmentId;
            // Sau khi chuyển giá trị, bạn có thể xoá thuộc tính olddpepartment nếu cần
            delete asset.OldDepartmentName;
            delete asset.OldDepartmentId;
          });

          this.getNewestReceiver();
          this.pagingAssetFE();
        })
        .catch((res) => {
          this.$processErrorResponse(res);
          this.isShowToastValidateBE = true;
          this.toast_content_warning = res.response.data.UserMessage;
        });
    },

    async loadTransferDataUpdate() {
      this.isLoading = true;
      var oldTransferAsset = this.transferAssetToUpdate;
      // Vì mã code có thể chứa 1 số ký tự đặc biệt như # vì thường được sử dụng để đánh dấu một phần của URL được xử lý bởi JavaScript trên trình duyệt và không được gửi lên máy chủ
      // Vì vậy trước khi gửi phải mã hóa
      // BE tự mã hóa rồi nên chỉ cần làm ở FE
      const encodedTransferAssetCode = encodeURIComponent(
        oldTransferAsset.TransferAssetCode
      );
      await this.$_MISAApi.TransferAsset.GetByCode(encodedTransferAssetCode)
        .then((res) => {
          if (res.data.ReceiverTransfers[0]) {
            this.receivers = res.data.ReceiverTransfers;
            this.isCreateReceiver = true;
          }

          this.transferAsset = {
            TransferAssetId: res.data.TransferAssetId,
            TransferAssetCode: res.data.TransferAssetCode,
            TransferDate: DateFormat(res.data.TransferDate),
            TransactionDate: DateFormat(res.data.TransactionDate),
            Description: res.data.Description,
          };
          this.originalTransferAsset = JSON.parse(
            JSON.stringify(this.transferAsset)
          );

          this.loadData(res.data.FixedAssetTransfers);
          // Duyệt qua mảng assets và chuyển giá trị của olddpepartment sang department
          this.assets.forEach((asset) => {
            asset.DepartmentName = asset.OldDepartmentName;
            asset.DepartmentId = asset.OldDepartmentId;
            // Sau khi chuyển giá trị, bạn có thể xoá thuộc tính olddpepartment nếu cần
            delete asset.OldDepartmentName;
            delete asset.OldDepartmentId;
          });

          // Lưu lại oldAssets và oldReceivers này để xài cho việc update-delete
          // Cách viết ... là để sao chép dữ liệu, nếu viết this.a = this.b thì sẽ làm tham chiếu
          // Chúng sẽ cùng trỏ đến 1 địa chỉ nên 1 cái thay đổi dữ liệu thì cái kia đổi theo
          this.oldAssets = [...this.assets];
          this.originalAssets = JSON.parse(JSON.stringify(this.assets));
          this.oldReceivers = [...this.receivers];
          this.originalReceivers = JSON.parse(JSON.stringify(this.receivers));

          this.getNewestReceiver();
          this.pagingAssetFE();
          this.isLoading = false;
        })
        .catch((res) => {
          this.$processErrorResponse(res);
          this.isShowToastValidateBE = true;
          this.toast_content_warning = res.response.data.UserMessage;
        });
    },

    /**
     * Danh sách số lượng bản ghi mỗi trang
     * Author: LDTUAN (02/08/2023)
     */
    getPageLimitList() {
      this.pageLimitList = [20, 10, 5];
    },

    //------------------------------------------- RECEIVER -------------------------------------------
    moveUp(index) {
      if (index > 0) {
        // Đổi chỗ item tại index với item phía trên nó
        const temp = this.receivers[index];
        this.receivers[index] = this.receivers[index - 1];
        this.receivers[index - 1] = temp;
      }
    },
    moveDown(index) {
      if (index < this.receivers.length - 1) {
        // Đổi chỗ item tại index với item phía dưới nó
        const temp = this.receivers[index];
        this.receivers[index] = this.receivers[index + 1];
        this.receivers[index + 1] = temp;
      }
    },

    btnAddReceiver() {
      if (this.formMode != this.$_MISAEnum.FORM_MODE.VIEW) {
        const newReceiver = {
          ReceiverCode: "",
          ReceiverFullname: "",
          ReceiverDelegate: "",
          ReceiverPosition: "",
        };
        this.receivers.push(newReceiver);
      }
    },

    btnDeleteReceiver(receiver) {
      if (this.formMode != this.$_MISAEnum.FORM_MODE.VIEW) {
        const index = this.receivers.indexOf(receiver);
        if (index !== -1) {
          this.receivers.splice(index, 1);
        }
      }
    },

    showFormReceiver() {
      this.isCreateReceiver = !this.isCreateReceiver;
      if (this.isCreateReceiver) {
        this.btnAddReceiver();
      } else {
        this.receivers = [];
      }
    },

    showNewestReceiver() {
      if (this.isGetNewestReceiver) {
        this.newestReceivers = [];
        this.receivers = [];
        this.btnAddReceiver();
      } else {
        this.$_MISAApi.Receiver.GetNewest()
          .then((res) => {
            this.newestReceivers = res.data;
            if (
              this.newestReceivers !== null &&
              this.newestReceivers.length > 0 &&
              this.newestReceivers[0] !== null
            ) {
              this.receivers = JSON.parse(JSON.stringify(this.newestReceivers)); // Tạo bản sao của this.newestReceivers
              for (let i = 0; i < this.receivers.length; i++) {
                delete this.receivers[i].ReceiverId; // Loại bỏ thuộc tính "id"
              }
            }
          })
          .catch((res) => {
            this.$processErrorResponse(res);
            this.isShowToastValidateBE = true;
            this.toast_content_warning = res.response.data.UserMessage;
          });
      }
    },

    getNewestReceiver() {
      this.$_MISAApi.Receiver.GetNewest()
        .then((res) => {
          this.newestReceivers = res.data;
        })
        .catch((res) => {
          this.$processErrorResponse(res);
          this.isShowToastValidateBE = true;
          this.toast_content_warning = res.response.data.UserMessage;
        });
    },

    //------------------------------------------- ASSET -------------------------------------------
    // load data từ form chọn tài sản chứng từ
    loadData(items) {
      // Tạo một đối tượng để ánh xạ FixedAssetId với TransferAssetDetailId trong deletedAssets
      this.isLoading = false;
      if (
        items &&
        items.length > 0 &&
        this.deletedAssets &&
        this.deletedAssets.length > 0
      ) {
        // Tạo một đối tượng để ánh xạ FixedAssetId với TransferAssetDetailId trong deletedAssets
        const deletedAssetMap = {};
        this.deletedAssets.forEach((asset) => {
          deletedAssetMap[asset.FixedAssetId] = asset.TransferAssetDetailId;
        });

        // Duyệt qua mảng items và thêm TransferAssetDetailId nếu có
        items.forEach((item) => {
          const transferDetailId = deletedAssetMap[item.FixedAssetId];
          if (transferDetailId !== undefined) {
            item.TransferAssetDetailId = transferDetailId;
            // item.Description = item.Description ? item.Description : "";
            // item.NewDepartmentCode = item.NewDepartmentCode ? item.NewDepartmentCode : "raw";
          }
        });
        // Duyệt qua mảng oldAssets và items để tìm và gán dữ liệu
        this.oldAssets.forEach((oldItem) => {
          items.forEach((item) => {
            if (oldItem.TransferAssetDetailId === item.TransferAssetDetailId) {
              oldItem.NewDepartmentId = item.NewDepartmentId;
              oldItem.NewDepartmentName = item.NewDepartmentName;
              oldItem.Description = item.Description;
            }
          });
        });
      }

      if (!this.assets || this.assets.length <= 0) {
        this.assets = items;
      } else {
        this.assets.unshift(...items);
      }
      this.pageNumber = 1;
      this.currentPage = 1;
      this.pagingAssetFE();
      this.isLoading = false;
    },

    loadOldData() {
      this.assets = JSON.parse(JSON.stringify(this.originalAssets));
      this.pagingAssetFE();
    },

    pagingAssetFE() {
      let pagingAsset = JSON.parse(JSON.stringify(this.assets));

      if (
        this.inputSearch &&
        this.inputSearch !== null &&
        this.inputSearch !== ""
      ) {
        pagingAsset = pagingAsset.filter((asset) => {
          return asset.FixedAssetCode.toLowerCase().includes(
            this.inputSearch.toLowerCase()
          );
        });
      }

      this.totalRecords = pagingAsset.length;
      this.totalPages = Math.ceil(this.totalRecords / this.pageLimit);
      const start = (this.currentPage - 1) * this.pageLimit;
      const end = start + this.pageLimit;
      this.pagingAsset = pagingAsset.slice(start, end);

      var totalPrice = this.pagingAsset.reduce((total, asset) => {
        return total + Math.round(asset.Cost);
      }, 0);
      var totalDepreciation = this.pagingAsset.reduce((total, asset) => {
        return (
          total +
          this.formatMoneyToInt((asset.Cost * (1 / asset.LifeTime)).toFixed(0))
        );
      }, 0);
      var totalResidualValue = totalPrice - totalDepreciation;

      this.totalPrice = formatMoney(totalPrice);
      this.totalResidualValue = formatMoney(totalResidualValue);
    },

    /**
     * Thực hiện load lại dữ khi thay đổi số trang
     * @param {int} pageNumber số trang
     * Author: LDTUAN (02/08/2023)
     */
    getPageNumber(pageNumber) {
      this.pageNumber = pageNumber;
      this.currentPage = pageNumber;
    },

    /**
     * Thực hiện load lại dữ khi thay đổi giới hạn bản ghi
     * @param {int} pageLimit giới hạn bản ghi mỗi trang
     * Author: LDTUAN (02/08/2023)
     */
    getPageLimit(pageLimit) {
      this.pageLimit = pageLimit;
    },

    btnDeleteAsset(asset) {
      const index = this.pagingAsset.indexOf(asset);
      if (index !== -1) {
        this.pagingAsset.splice(index, 1);
      }
      const indexAssets = this.assets.findIndex(
        (item) => item.TransferAssetDetailId === asset.TransferAssetDetailId
      );
      if (indexAssets !== -1) {
        this.assets.splice(indexAssets, 1);
      }

      if (this.pagingAsset.length === 0) {
        this.currentPage = 1;
        if (this.pageNumber !== 1) {
          this.pageNumber = 1;
        }
      }
      this.pagingAssetFE();
    },

    btnDeleteSelectedAsset(assetsToDelete) {
      assetsToDelete.forEach((asset) => {
        const index = this.pagingAsset.findIndex(
          (item) => item.TransferAssetDetailId === asset.TransferAssetDetailId
        );

        if (index !== -1) {
          this.pagingAsset.splice(index, 1);
        }

        const indexAssets = this.assets.findIndex(
          (item) => item.TransferAssetDetailId === asset.TransferAssetDetailId
        );

        if (indexAssets !== -1) {
          this.assets.splice(indexAssets, 1);
        }
      });

      this.selectedRowsByCheckBox = [];

      if (this.pagingAsset.length === 0) {
        this.currentPage = 1;
        this.pagingAssetFE();
        if (this.pageNumber !== 1) {
          this.pageNumber = 1;
        }
      }
    },

    //------------------------------------------- COMBOBOX -------------------------------------------
    /**
     * Cập nhật thông tin phòng ban cho tài sản
     * @param {object} asset tài sản
     * @param {string} item phòng ban
     * Author: LDTUAN (06/09/2023)
     */
    getNewDepartment(asset, item) {
      if (item) {
        asset.NewDepartmentId = item.DepartmentId;
        asset.NewDepartmentName = item.DepartmentName;
      }
    },

    /**
     * Khi xóa text thì cập nhật lại id = null
     * @param {object} asset tài sản
     * @param {string} item phòng ban
     * Author: LDTUAN (06/09/2023)
     */
    deleteDepartment(asset, item) {
      if (!item) {
        asset.NewDepartmentId = null;
      }
    },

    //------------------------------------------- Click row -------------------------------------------

    /**
     * Thực hiện call hàm rowOnClick từ file js để bôi xanh 1 dòng nếu click vào dòng đó
     * @param {object} asset thông tin tài sản
     * Author: LDTUAN (02/08/2023)
     */
    callRowOnClick(asset) {
      rowOnClick.call(this, asset);
    },

    callRowOnClickByCheckBox(asset) {
      rowOnClickByCheckBox.call(this, asset, "assets");
    },

    callRowOnCtrlClick(asset) {
      if (this.formMode != this.$_MISAEnum.FORM_MODE.VIEW) {
        rowOnCtrlClick.call(this, asset, "assets");
      }
    },

    rowOnShiftClick(asset) {
      if (event.shiftKey) {
        const index = this.pagingAsset.indexOf(asset);
        let list = [];
        let newestIndex = index + 1;
        if (newestIndex <= this.lastIndex) {
          list = this.pagingAsset.slice(newestIndex - 1, this.lastIndex + 1);
        } else {
          list = this.pagingAsset.slice(this.lastIndex, newestIndex);
        }
        this.selectedRows = [];
        this.selectedRowsByCheckBox = list;
      }
    },

    btnUncheckedAll() {
      this.selectedRows = [];
      this.selectedRowsByCheckBox = [];
    },

    /**
     * Thực hiện tick/bỏ tick tất cả bản ghi trong danh sách khi tick/bỏ tích checkbox
     * @param {checkbox} event input checkbox
     * Author: LDTUAN (02/08/2023)
     */
    headCheckboxClick(event) {
      if (event.target.checked) {
        for (const asset of this.pagingAsset) {
          const index = this.selectedRowsByCheckBox.findIndex(
            (selectedItem) => selectedItem.FixedAssetId === asset.FixedAssetId
          );
          if (index === -1) {
            this.selectedRowsByCheckBox.push(asset);
          }
        }
      } else {
        for (const asset of this.pagingAsset) {
          const index = this.selectedRowsByCheckBox.findIndex(
            (selectedItem) => selectedItem.FixedAssetId === asset.FixedAssetId
          );
          if (index !== -1) {
            this.selectedRowsByCheckBox.splice(index, 1);
          }
        }
        this.selectedRows = [];
      }
    },

    //------------------------------------------- Form-------------------------------------------
    /**
     * Đóng form, gửi thông tin về cho component cha để đóng nó
     * Author: LDTUAN (21/08/2023)
     */
    btnCloseForm() {
      this.$emit("onCloseForm");
    },

    btnCancelTransferAsset() {
      if (this.formMode == this.$_MISAEnum.FORM_MODE.VIEW) {
        this.btnCloseForm();
      } else {
        this.updateTransferAssetHelper(this.$_MISAEnum.FORM_MODE.CANCEL);
      }
    },

    btnShowFormChooseAsset() {
      this.pagingAsset.forEach((asset) => {
        if (!Object.prototype.hasOwnProperty.call(asset, "DepartmentCode")) {
          asset.DepartmentCode = "raw";
          asset.FixedAssetCategoryCode = "raw";
          asset.FixedAssetCategoryName = "raw";
        }
      });
      this.assets.forEach((asset) => {
        if (!Object.prototype.hasOwnProperty.call(asset, "DepartmentCode")) {
          asset.DepartmentCode = "raw";
          asset.FixedAssetCategoryCode = "raw";
          asset.FixedAssetCategoryName = "raw";
        }
      });
      this.existFixedAsset = this.assets;
      this.deletedAssets = this.originalAssets.filter((original) => {
        return (
          this.existFixedAsset.findIndex(
            (exist) =>
              exist.TransferAssetDetailId === original.TransferAssetDetailId
          ) === -1
        );
      });
      this.isShowFormChooseAsset = true;
    },

    onCloseForm() {
      this.isShowFormChooseAsset = false;
      this.buttonFocus = "ChooseAsset";
      this.$refs[this.buttonFocus].focusButton();
      this.buttonFocus = null;
    },

    //------------------------------------------- TOAST -------------------------------------------
    btnCloseToastWarning() {
      this.isShowToastValueChange = false;
      this.isShowToastValidateBE = false;
      if (this.inputFocus) {
        this.$refs[this.inputFocus].focusInput();
        this.inputFocus = null;
      }
      if (this.buttonFocus) {
        this.$refs[this.buttonFocus].focusButton();
        this.buttonFocus = null;
      }
    },

    //------------------------------------------- TAB INDEX -------------------------------------------
    /**
     * Nhấn tab để di chuyển giữa các component
     * @param {*} event
     * @param {int} index số index của các component
     * Author: LDTUAN (10/09/2023)
     */
    checkTabIndex(event, index) {
      var charCode = event.which ? event.which : event.keyCode;
      if (
        index == "islast" &&
        charCode == this.$_MISAEnum.KEYCODE.TAB &&
        this.toast_content_warning
      ) {
        event.preventDefault();
        this.buttonFocus = "button";
        this.$refs[this.buttonFocus].focusButton();
      } else if (
        index == "islast" &&
        charCode == this.$_MISAEnum.KEYCODE.TAB &&
        !this.toast_content_warning
      ) {
        event.preventDefault();
        this.inputFocus = "TransferAssetCode";
        this.$refs[this.inputFocus].focusInput();
      }
    },

    //------------------------------------------- VALIDATE -------------------------------------------
    setInputFocus(value) {
      this.inputFocus = value;
    },

    validate() {
      if (this.receivers !== null) {
        var nullReceiver = null;
        this.receivers.forEach((receiver) => {
          if (!receiver.ReceiverFullname.trim()) {
            nullReceiver = true;
          }
        });
        if (nullReceiver == true) {
          //this.inputFocus = "ReceiverFullname";
          this.isShowToastValidateBE = true;
          this.toast_content_warning =
            this.$_MISAResource.VN.Form.Warning.Transfer.Add.NoReceiver;
        }
      }

      if (
        DateFormat(this.transferAsset.TransferDate) <
        DateFormat(this.transferAsset.TransactionDate)
      ) {
        this.inputFocus = "TransferDate";
        this.isShowToastValidateBE = true;
        this.toast_content_warning =
          this.$_MISAResource.VN.Form.Warning.Transfer.SmallTransactionDate;
      }

      switch ("") {
        case this.transferAsset.TransferAssetCode ?? "":
          this.inputFocus = "TransferAssetCode";
          this.isShowToastValidateBE = true;
          this.toast_content_warning =
            this.$_MISAResource.VN.Form.Warning.Transfer.TransferAssetCode;
          break;
        case this.transferAsset.TransactionDate ?? "":
          this.inputFocus = "TransactionDate";
          this.isShowToastValidateBE = true;
          this.toast_content_warning =
            this.$_MISAResource.VN.Form.Warning.Transfer.TransactionDate;
          break;
        case this.transferAsset.TransferDate ?? "":
          this.inputFocus = "TransferDate";
          this.isShowToastValidateBE = true;
          this.toast_content_warning =
            this.$_MISAResource.VN.Form.Warning.Transfer.TransferDate;
          break;
      }
    },

    //------------------------------------------- VALIDATE -------------------------------------------
    /**
     * Thực hiện tìm kiếm khi nhấn enter
     * @param {int} keyCode mã code của phím
     * Author: LDTUAN (19/09/2023)
     */
    searchByCode(keyCode) {
      if (keyCode == this.$_MISAEnum.KEYCODE.ENTER) {
        this.pageNumber = 1;
        this.currentPage = 1;
        this.pagingAssetFE();
      }
    },
  },
};
</script>
<style scoped>
.popup {
  position: fixed;
  top: 0;
  right: 0;
  bottom: 0;
  left: 0;
  background-color: #dff2ff;
  z-index: 2;
  display: flex;
  flex-direction: column;
}

.popup__header {
  border-bottom: 1px solid rgba(0, 0, 0, 0.16);
  height: 44px;
  padding: 0 22px;
  display: flex;
  justify-content: space-between;
  align-items: center;
  background-color: var(--background-color-asset-transfer-form);
}

.popup__body {
  flex: 1;
  padding: 16px 16px 16px 22px;
  display: flex;
  flex-direction: column;
}

.popup__footer {
  border-top: 1px solid rgba(0, 0, 0, 0.16);
  height: 52px;
  padding-right: 12px;
  display: flex;
  justify-content: end;
  align-items: center;
  column-gap: 12px;
  background-color: var(--background-color-asset-transfer-form);
}

.header__title {
  font-size: 18px;
}

.body__title {
  font-size: 16px;
}

/* ------------------------------------------- Body Content ------------------------------------------- */

.body__content {
  display: flex;
  flex-direction: column;
  background-color: var(--background-color-asset-transfer-form);
  padding: 20px 20px 20px 28px;
  margin-top: 20px;
  box-sizing: border-box;
}

.content--top {
  display: flex;
  flex-direction: column;
  min-height: 100px;
}

.content__row {
  display: grid;
  grid-template-columns: repeat(3, 1fr) 3fr;
  column-gap: 20px;
}

.content__row--bot {
  flex: 1;
  display: flex;
  flex-direction: row;
  column-gap: 50px;
}

.checkbox {
  display: flex;
  align-items: center;
}
.checkbox-combo {
  display: flex;
  align-items: center;
  column-gap: 10px;
}

/* ------------------------------------------- Body content bot ------------------------------------------- */
.content--bot__row {
  display: flex;
  flex-direction: column;
  row-gap: 13px;
}

.row--form {
  display: flex;
  flex-direction: column;
  row-gap: 13px;
  max-height: 83px;
  overflow-y: auto;
}

.content--bot__header {
  margin-top: 20px;
}

.content--bot__header .content__column {
  margin-left: 10px;
}

.row--bot {
  display: grid;
  grid-template-columns: 38px repeat(3, 1.5fr) 1fr;
  column-gap: 15px;
}

.number {
  display: flex;
  align-items: center;
  justify-content: center;
  width: 100%;
  height: 100%;
  border-color: #afafaf;
}

.combo-icon {
  display: flex;
  align-items: center;
  height: 100%;
  margin-left: 20px;
  column-gap: 20px;
}

.content--bot__body .content__column {
  margin-left: 10px;
}

/* ------------------------------------------- Body Action ------------------------------------------- */
.body__action {
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 8px 0px;
}

.action--left {
  display: flex;
  align-items: center;
  column-gap: 14px;
}

.action--right {
  display: flex;
  align-items: center;
  column-gap: 14px;
}

/* ------------------------------------------- Body Form ------------------------------------------- */
.body__form {
  flex: 1;
  display: flex;
  flex-direction: column;
  justify-content: space-between;
  background-color: var(--background-color-default);
  max-height: 474px;
}

/* ------------------------------------------- Table ------------------------------------------- */
.table {
  flex: 1;
  max-height: 435px;
  display: flex;
  flex-direction: column;
  border-spacing: unset;
  overflow-y: auto;
  overflow-x: hidden;
}

.table-bot {
  flex: 1;
}

.row {
  display: grid;
  grid-template-columns:
    44px 50px 160px 180px 200px 200px 200px 200px 295px
    110px;
  height: 35px;
  cursor: pointer;
}

.cell {
  width: 100%;
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
  overflow-x: hidden;
}

.disabled {
  cursor: not-allowed;
}
</style>
