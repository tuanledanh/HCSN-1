<template>
  <div
    class="popup--child"
    @keyup.esc="btnCloseForm"
    @keydown="saveShortCut"
    tabindex="0"
  >
    <div class="popup-container border-radius-4">
      <div class="popup__header--child border--top-right border--top-left">
        <span class="header__title font-weight--500"
          >Chọn tài sản điều chuyển
        </span>
        <MISAButton
          button_icon_normal
          exit
          bottom
          ref="exit"
          content="Thoát"
          @click="btnCloseForm"
          :tabindex="18"
          @keydown="checkTabIndex($event, 'islast')"
        ></MISAButton>
      </div>
      <div class="popup__body--child">
        <div class="body__form">
          <div class="search">
            <MISAInput
              search
              normal
              medium
              placeholder="Tìm kiếm theo mã"
              v-model="inputSearch"
              @searchByCode="searchByCode"
              maxlength="50"
              :tabindex="13"
              ref="search"
              @focus="setInputFocus('search')"
            ></MISAInput>
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
            </div>
          </div>
          <!-- ------------------------Table start------------------------ -->
          <div class="table border-radius-4">
            <!-- ------------------------Header------------------------ -->
            <div class="header--row row--child">
              <div
                class="header cell display--center-center border--right border--bottom"
              >
                <input
                  type="checkbox"
                  @click="headCheckboxClick($event)"
                  :checked="
                    assets.every((assetItem) =>
                      selectedRowsByCheckBox.some(
                        (selectedItem) =>
                          selectedItem.FixedAssetId === assetItem.FixedAssetId
                      )
                    ) &&
                    assets.length > 0 &&
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
                Mã tài sản
              </div>
              <div
                class="header cell display--center-left font-weight--700 border--right border--bottom padding--left-10"
              >
                Tên tài sản
              </div>
              <div
                class="header cell display--center-left font-weight--700 border--right border--bottom padding--left-10"
              >
                Bộ phận sử dụng
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
                class="header cell display--center-right font-weight--700 border--right border--bottom padding--right-10"
              >
                Năm theo dõi
              </div>
            </div>
            <!-- ------------------------Body------------------------ -->
            <div class="body-data relative">
              <div
                class="body--row row--child"
                v-for="asset in assets"
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
                  />
                </div>
                <div
                  class="cell display--center-center border--right border--bottom"
                >
                  {{ assets.indexOf(asset) + 1 }}
                </div>
                <div
                  class="cell display--center-left border--right border--bottom padding--left-10"
                >
                  {{ asset.FixedAssetCode }}
                </div>
                <div
                  class="cell display--center-left border--right border--bottom padding--left-10"
                >
                  <el-tooltip
                    v-if="asset.FixedAssetName.length > 20"
                    :visible="visible"
                    placement="right"
                  >
                    <template #content>
                      <span>{{ asset.FixedAssetName }}</span>
                    </template>
                    <span>{{ truncateText(asset.FixedAssetName, 20) }}</span>
                  </el-tooltip>
                  <span v-else>{{
                    truncateText(asset.FixedAssetName, 20)
                  }}</span>
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
                  <span v-else>{{
                    truncateText(asset.DepartmentName, 20)
                  }}</span>
                </div>
                <div
                  class="cell display--center-right border--right border--bottom padding--right-10"
                >
                  {{ formatMoney(asset.Cost) }}
                </div>
                <div
                  class="cell display--center-right border--right border--bottom padding--right-10"
                >
                  {{ AssetDepreciation(asset.Cost, asset.LifeTime) }}
                </div>
                <div
                  id="cell"
                  class="cell display--center-right border--right border--bottom padding--right-10"
                >
                  {{ asset.TrackedYear }}
                </div>
              </div>
            </div>
          </div>
          <!-- ------------------------Table end------------------------ -->
          <MISACalculator
            form="transfer-choose-form"
            :totalPrice="totalPrice"
            :totalResidualValue="totalResidualValue"
            :numberColumnLeft="5"
          ></MISACalculator>
          <MISAPaging
            :totalRecords="totalRecords"
            :totalPages="totalPages"
            :currentPage="currentPage"
            :pageLimitList="pageLimitList"
            @filter="getPageLimit"
            @paging="getPageNumber"
          ></MISAPaging>
        </div>
      </div>
      <div class="hr border--bottom"></div>
      <div class="body__content--child">
        <div class="content--top">
          <div class="content__row--child">
            <div class="content__column">
              <MISACombobox
                :api="this.$_MISAApi.Department.Api"
                propText="DepartmentName"
                propValue="DepartmentId"
                ref="DepartmentId"
                @focus="setInputFocus('DepartmentId')"
                label="Bộ phận sử dụng mới"
                required
                placeholder="Bộ phận sử dụng"
                @filter="getDepartmentFilter"
                @deleteDepartment="deleteDepartment($event)"
                :inputChange="inputChange"
                isDisplay
                self_adjust_size
                medium
                padding
                input_35
                :tabindex="14"
              ></MISACombobox>
            </div>
            <div class="content__column">
              <MISAInput
                normalGrid
                label="Ghi chú"
                v-model="Description"
                medium
                maxlength="255"
                :tabindex="15"
              ></MISAInput>
            </div>
          </div>
        </div>
      </div>
      <div class="popup__footer border--bot-right border--bot-left">
        <MISAButton
          buttonSub
          textButton="Hủy"
          :tabindex="17"
          @click="btnCloseForm"
        ></MISAButton>
        <MISAButton
          buttonMain
          textButton="Đồng ý"
          @click="btnSaveAsset"
          :tabindex="16"
        ></MISAButton>
      </div>
    </div>
  </div>

  <MISALoading v-if="isLoading"></MISALoading>

  <!-- ======================================================= TOAST ======================================================= -->
  <div v-if="isShowToastValidate" class="blur">
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
  <div v-if="isShowToastValidateChange" class="blur">
    <MISAToast typeToast="warning" :content="toast_content_warning"
      ><MISAButton
        buttonSub
        textButton="Đóng"
        @click="btnCloseToastWarning"
        focus
        :tabindex="1"
        ref="button"
      ></MISAButton>
      <MISAButton
        buttonMain
        textButton="Hủy"
        @click="btnAbsCloseForm"
        :tabindex="2"
        @keydown="checkTabIndex($event, 'islast')"
      ></MISAButton
    ></MISAToast>
  </div>
</template>
<script>
import { rowOnClick } from "../../helpers/table/selectRow";
import { rowOnCtrlClick } from "../../helpers/table/selectRow";
import { rowOnClickByCheckBox } from "../../helpers/table/selectRow";

import { truncateText } from "../../helpers/common/format/format";
import { formatMoney } from "../../helpers/common/format/format";
import { formatMoneyToInt } from "../../helpers/common/format/format";
import { AssetDepreciation } from "../../helpers/common/format/format";

export default {
  name: "AssetTransferChooseForm",
  props: {
    existFixedAsset: {
      type: Object,
      default: null,
    },
    deletedAssets: {
      type: Object,
      default: null,
    },
  },
  data() {
    return {
      // ----------------------------- DATA -----------------------------
      // Danh sách bản ghi
      assets: [],
      // Danh sách các bản ghi đã chọn
      selectedRows: [],
      // Index của bản ghi đầu tiên trong danh sách
      lastIndex: 0,
      // Danh sách người nhận
      listReceivers: 1,
      // Hiển thị phần tạo người nhận
      isCreateReceiver: false,
      // Lý do điều chuyển
      description: null,
      isLoading: false,

      // ----------------------------- FORM -----------------------------
      isFormDisplay: false,

      // ----------------------------- PAGING -----------------------------
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

      // ----------------------------- CHECKBOX -----------------------------
      // Tick ô checkbox input
      isCheckboxChecked: false,
      // Danh sách các bản ghi đã chọn
      selectedRowsByCheckBox: [],

      // ----------------------------- TOAST -----------------------------
      isShowToastValidate: false,
      isShowToastValidateChange: false,
      toast_content_warning: null,

      // ----------------------------- TAB INDEX -----------------------------
      buttonFocus: null,
      inputFocus: "search",

      // ----------------------------- COMBOBOX -----------------------------
      departmentFilter: null,
      inputChange: null,

      // ----------------------------- SEARCH -----------------------------
      // Nội dung tìm kiếm
      inputSearch: "",
    };
  },
  watch: {
    /**
     * Phân trang lại với giới hạn trang mới
     * @param {int} value giới hạn bản ghi
     * Author: LDTUAN (02/08/2023)
     */
    pageLimit(value) {
      this.loadData(1, value);
    },

    /**
     * Phân trang theo số trang mới
     * @param {int} value số trang
     * Author: LDTUAN (02/08/2023)
     */
    pageNumber(value) {
      this.loadData(value, this.pageLimit);
      this.currentPage = value;
    },
  },
  mounted() {
    this.$refs[this.inputFocus].focusInput();
  },
  created() {
    // Tải danh sách option giới hạn bản ghi mỗi trang
    this.getPageLimitList();

    this.loadData();
  },
  methods: {
    AssetDepreciation,
    formatMoney,
    formatMoneyToInt,
    truncateText,

    loadData(pageNumber = this.pageNumber, pageLimit = this.pageLimit) {
      this.isLoading = true;
      let detailIds = null;
      if (this.deletedAssets) {
        detailIds = this.deletedAssets.map(
          (item) => item.TransferAssetDetailId
        );
      }
      let dataFilter = {
        PageNumber: pageNumber,
        PageLimit: pageLimit,
        FilterName: this.inputSearch,
        FixedAssetDtos: this.existFixedAsset,
        TransferAssetDetailIds: detailIds,
      };
      this.$_MISAApi.FixedAsset.FilterForTransfer(dataFilter, {
        headers: { "Content-Type": "application/json" },
      })
        .then((res) => {
          this.assets = res.data.Data;
          this.totalPages = res.data.TotalPages;
          this.totalRecords = res.data.TotalRecords;

          var totalPrice = this.assets.reduce((total, asset) => {
            return total + Math.round(asset.Cost);
          }, 0);
          var totalDepreciation = this.assets.reduce((total, asset) => {
            return (
              total +
              this.formatMoneyToInt(
                (asset.Cost * (1 / asset.LifeTime)).toFixed(0)
              )
            );
          }, 0);
          var totalResidualValue = totalPrice - totalDepreciation;

          this.totalPrice = formatMoney(totalPrice);
          this.totalResidualValue = formatMoney(totalResidualValue);
          this.isLoading = false;
        })
        .catch((res) => {
          this.isShowToastValidate = true;
          this.toast_content_warning = res.response.data.UserMessage;
          this.isLoading = false;
        });
    },

    /**
     * Danh sách số lượng bản ghi mỗi trang
     * Author: LDTUAN (02/08/2023)
     */
    getPageLimitList() {
      this.pageLimitList = [20, 10, 5];
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
     * Thực hiện load lại dữ khi thay đổi giới hạn bản ghi
     * @param {int} pageLimit giới hạn bản ghi mỗi trang
     * Author: LDTUAN (02/08/2023)
     */
    getPageLimit(pageLimit) {
      this.pageLimit = pageLimit;
    },

    //------------------------------------------- CLICK ROW -------------------------------------------

    /**
     * Thực hiện call hàm rowOnClick từ file js để bôi xanh 1 dòng nếu click vào dòng đó
     * @param {object} asset thông tin tài sản
     * Author: LDTUAN (02/08/2023)
     */
    callRowOnClick(asset) {
      rowOnClick.call(this, asset, "assets");
    },

    callRowOnClickByCheckBox(asset) {
      rowOnClickByCheckBox.call(this, asset, "assets");
    },

    callRowOnCtrlClick(asset) {
      rowOnCtrlClick.call(this, asset, "assets");
    },

    rowOnShiftClick(asset){
      if (event.shiftKey) {
        const index = this.assets.indexOf(asset);
        let list = [];
        let newestIndex = index + 1;
        if (newestIndex <= this.lastIndex) {
          list = this.assets.slice(newestIndex - 1, this.lastIndex + 1);
        } else {
          list = this.assets.slice(this.lastIndex, newestIndex);
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
        for (const asset of this.assets) {
          const index = this.selectedRowsByCheckBox.findIndex(
            (selectedItem) => selectedItem.FixedAssetId === asset.FixedAssetId
          );
          if (index === -1) {
            this.selectedRowsByCheckBox.push(asset);
          }
        }
      } else {
        for (const asset of this.assets) {
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

    //------------------------------------------- SAVE DATA -------------------------------------------
    btnSaveAsset() {
      if (this.selectedRowsByCheckBox.length <= 0) {
        this.toast_content_warning =
          this.$_MISAResource.VN.Form.Warning.SelectData.TransferAsset;
        this.isShowToastValidate = true;
        this.inputFocus = "search";
      } else if (!this.departmentFilter) {
        this.inputFocus = "DepartmentId";
        this.toast_content_warning =
          this.$_MISAResource.VN.Form.Warning.SelectData.Department.Null;
        this.isShowToastValidate = true;
      } else {
        let departmentName = this.departmentFilter.DepartmentName;
        let departmentId = this.departmentFilter.DepartmentId;
        let description = this.Description;
        const containsDepartment = this.selectedRowsByCheckBox.some(
          (asset) => asset.DepartmentName === departmentName
        );
        if (containsDepartment) {
          this.toast_content_warning =
            this.$_MISAResource.VN.Form.Warning.SelectData.Department.Duplicate;
          this.isShowToastValidate = true;
        } else {
          const assetsWithNewDepartment = this.selectedRowsByCheckBox.map(
            (asset) => ({
              ...asset,
              NewDepartmentName: departmentName,
              NewDepartmentId: departmentId,
              Description: description,
            })
          );
          this.$emit("loadData", assetsWithNewDepartment);
          this.btnAbsCloseForm();
        }
      }
    },

    /**
     * Bấm tổ hợp phím ctrl + s để lưu
     * @param {*} event sự kiện khi khi nhấn ctrl
     * Author: LDTUAN (18/09/2023)
     */
    saveShortCut(event) {
      var charCode = event.which ? event.which : event.keyCode;
      if (charCode == this.$_MISAEnum.KEYCODE.S && event.ctrlKey == true) {
        event.preventDefault();
        this.btnSaveAsset();
      }
    },

    //------------------------------------------- COMBOBOX -------------------------------------------
    /**
     * Thực hiện filter theo phòng ban
     * @param {object} item phòng ban
     * Author: LDTUAN (02/08/2023)
     */
    getDepartmentFilter(item) {
      this.departmentFilter = item;
    },

    /**
     * Khi xóa text thì cập nhật lại id = null
     * @param {object} asset tài sản
     * @param {string} item phòng ban
     * Author: LDTUAN (19/09/2023)
     */
    deleteDepartment(item) {
      if (!item) {
        this.departmentFilter = null;
      }
    },

    //------------------------------------------- TOAST -------------------------------------------
    btnCloseToastWarning() {
      this.isShowToastValidate = false;
      this.isShowToastValidateChange = false;
      this.toast_content_warning = null;
      this.$refs[this.inputFocus].focusInput();
    },

    btnCloseForm() {
      if(this.selectedRowsByCheckBox !== null && this.selectedRowsByCheckBox.length > 0){
      this.isShowToastValidateChange = true;
      this.toast_content_warning = this.$_MISAResource.VN.Form.Warning.DeleteChooseAsset.UnChange;
        return;
      }
      this.$emit("onCloseForm");
    },

    btnAbsCloseForm(){
      this.isShowToastValidateChange = false;
      this.$emit("onCloseForm");
    },

    //------------------------------------------- TAB INDEX -------------------------------------------
    setInputFocus(value) {
      this.inputFocus = value;
    },

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
        this.inputFocus = "search";
        this.$refs[this.inputFocus].focusInput();
      }
    },

    //------------------------------------------- SEARCH -------------------------------------------
    /**
     * Thực hiện tìm kiếm khi nhấn enter
     * @param {int} keyCode mã code của phím
     * Author: LDTUAN (19/09/2023)
     */
    searchByCode(keyCode) {
      if (keyCode == this.$_MISAEnum.KEYCODE.ENTER) {
        this.loadData();
        this.currentPage = 1;
      }
    },
  },
};
</script>
<style scoped>
.popup--child {
  position: fixed;
  top: 0;
  right: 0;
  bottom: 0;
  left: 0;
  background-color: #0000005d;
  z-index: 2;
}

.popup-container {
  width: 1100px;
  height: 730px;
  position: absolute;
  display: flex;
  flex-direction: column;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
}

.popup__header--child {
  border-bottom: 1px solid rgba(0, 0, 0, 0.16);
  height: 70px;
  padding: 0 22px;
  display: flex;
  justify-content: space-between;
  align-items: center;
  background-color: var(--background-color-asset-transfer-form);
}

.popup__body--child {
  flex: 1;
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

.body__content--child {
  display: flex;
  flex-direction: column;
  background-color: var(--background-color-asset-transfer-form);
  box-sizing: border-box;
  padding: 0px 28px 0px 28px;
}

.content--top {
  display: flex;
  flex-direction: column;
  justify-content: center;
  min-height: 100px;
}

.content__row--child {
  display: grid;
  grid-template-columns: 1.5fr 3fr;
  column-gap: 20px;
}

.content__row--bot {
  flex: 1;
  display: flex;
  flex-direction: column-reverse;
}

.checkbox {
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

.search {
  height: 50px;
  display: flex;
  align-items: center;
  padding: 0 12px;
  column-gap: 20px;
}

/* ------------------------------------------- Body Form ------------------------------------------- */
.body__form {
  flex: 1;
  display: flex;
  flex-direction: column;
  justify-content: space-between;
  background-color: var(--background-color-default);
}

/* ------------------------------------------- Table ------------------------------------------- */
.table {
  flex: 1;
  max-height: 390px;
  display: flex;
  flex-direction: column;
  border-spacing: unset;
  overflow-y: auto;
}

.table-bot {
  flex: 1;
}

.row--child {
  display: grid;
  grid-template-columns: 44px 50px 160px 172px 176px 156px 200px calc(
      100% - 958px
    );
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
}

/* ------------------------------------------- Hr ------------------------------------------- */
.hr {
  height: 8px;
  border-color: var(--background-color-table-expand-narrow);
  background-color: var(--background-color-default);
}
</style>
