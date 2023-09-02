<template>
  <div class="popup">
    <div class="popup__header">
      <span class="header__title font-weight--500"
        >Thêm chứng từ điều chuyển
      </span>
      <MISAButton
        button_icon_normal
        exit
        bottom
        ref="exit"
        content="Thoát"
        @click="btnCloseForm"
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
                medium
                required
                maxlength="12"
              ></MISAInput>
            </div>
            <div class="content__column">
              <MISAInputDatePicker
                label="Ngày chứng từ"
                medium
                required
              ></MISAInputDatePicker>
            </div>
            <div class="content__column">
              <MISAInputDatePicker
                label="Ngày điều chuyển"
                medium
                required
              ></MISAInputDatePicker>
            </div>
            <div class="content__column">
              <MISAInput
                normalGrid
                label="Ghi chú"
                medium
                required
                maxlength="4"
              ></MISAInput>
            </div>
          </div>
          <div class="content__row--bot">
            <div class="checkbox" @click="showFormReceiver">
              <input type="checkbox" />
              <label class="font-weight--500">Chọn ban giao nhận</label>
            </div>
          </div>
        </div>
        <div v-if="isCreateReceiver" class="content--bot">
          <div class="content--bot__row">
            <div class="content--bot__header row--bot">
              <div>STT</div>
              <div>Họ và tên</div>
              <div class="content__column">Đại diện</div>
              <div class="content__column">Chức vụ</div>
            </div>
            <div class="row--form">
              <div
                v-for="index in listReceivers"
                :key="index"
                class="content--bot__body row--bot"
              >
                <div>
                  <div class="number border border-radius-4">{{ index }}</div>
                </div>
                <div>
                  <MISAInput
                    normalGrid
                    medium
                    maxlength="12"
                    placeholder="Nhập họ và tên"
                  ></MISAInput>
                </div>
                <div class="content__column">
                  <MISAInput
                    normalGrid
                    medium
                    maxlength="12"
                    placeholder="Nhập đại diện"
                  ></MISAInput>
                </div>
                <div class="content__column">
                  <MISAInput
                    normalGrid
                    medium
                    maxlength="12"
                    placeholder="Nhập chức vụ"
                  ></MISAInput>
                </div>
                <div class="content__column">
                  <div class="combo-icon">
                    <MISAIcon pull_up_large></MISAIcon>
                    <MISAIcon drop_down_large></MISAIcon>
                    <div @click="btnAddReceiver">
                      <MISAIcon add_box_thin></MISAIcon>
                    </div>
                    <div @click="btnDeleteReceiver">
                      <MISAIcon v-if="index > 1" deleteIcon></MISAIcon>
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
          ></MISAInput>
        </div>
        <div class="action--right">
          <MISAButton
            combo
            add_box
            textButton="Chọn tài sản"
            buttonSub
            basic
            large
            @click="btnShowFormChooseAsset"
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
              <input type="checkbox" />
            </div>
            <div
              class="header cell display--center-center font-weight--700 border--right border--bottom"
            >
              STT
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
              v-for="asset in assets"
              :key="asset.FixedAssetId"
              :class="{ 'row--selected': selectedRows.includes(asset) }"
              @click.exact.stop="callRowOnClick(asset)"
              @click.ctrl.stop="callRowOnCtrlClick(asset)"
            >
              <div
                class="cell display--center-center border--right border--bottom"
                @click.stop="callRowOnClickByCheckBox(asset)"
              >
                <input
                  type="checkbox"
                  :checked="selectedRowsByCheckBox.includes(asset)"
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
                  @filter="getDepartmentFilter"
                  :newDepartment="asset.newDepartmentName"
                  isDisplay
                  self_adjust_size
                ></MISACombobox>
              </div>
              <div
                class="cell display--center-left border--right border--bottom"
              >
                <MISAInput normalGrid medium padding_2></MISAInput>
              </div>
              <div class="cell display--center-center border--bottom">
                <div class="icon-function">
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
    </div>
    <div class="popup__footer">
      <MISAButton
        buttonOutline
        textButton="Hủy"
        @click="btnCancelForm"
        :tabindex="15"
      ></MISAButton>
      <MISAButton
        buttonMain
        textButton="Lưu"
        @click="btnSaveAsset"
        :tabindex="16"
      ></MISAButton>
    </div>
  </div>
  <MISAAssetTransferChooseForm
    @onCloseForm="onCloseForm"
    v-if="isShowFormChooseAsset"
    @loadData="loadData"
  ></MISAAssetTransferChooseForm>
</template>
<script>
import { rowOnClick } from "../../helpers/table/selectRow";
import { rowOnCtrlClick } from "../../helpers/table/selectRow";
import { rowOnClickByCheckBox } from "../../helpers/table/selectRow";
import MISAAssetTransferChooseForm from "./AssetTransferChooseForm.vue";

import { formatMoney } from "../../helpers/common/format/format";
import { truncateText } from "../../helpers/common/format/format";
import { AssetDepreciation } from "../../helpers/common/format/format";
export default {
  name: "MISAAssetTransferForm",
  components: {
    MISAAssetTransferChooseForm,
  },
  data() {
    return {
      // ----------------------------- Data -----------------------------
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

      // ----------------------------- Form -----------------------------
      isFormDisplay: false,
      // Hiển thị form chọn tài sản
      isShowFormChooseAsset: false,

      // ----------------------------- Paging -----------------------------
      pageLimitList: [],
      // Tổng bản ghi
      totalRecords: 0,
      // Tổng trang
      totalPages: 0,
      // Trang hiện tại
      currentPage: 1,

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

      // ----------------------------- COMBOBOX -----------------------------
    };
  },
  created() {
    this.loadData();
  },
  methods: {
    AssetDepreciation,
    formatMoney,
    truncateText,

    // load data tạm thời
    loadData(items) {
      if (!this.assets || this.assets.length <= 0) {
        this.assets = items;
      } else {
        this.assets = this.assets.concat(items);
      }
      console.log(this.assets);
    },

    //------------------------------------------- COMBOBOX -------------------------------------------
    

    //------------------------------------------- RECEIVER -------------------------------------------
    btnAddReceiver() {
      this.listReceivers++;
    },

    btnDeleteReceiver() {
      this.listReceivers--;
    },

    showFormReceiver() {
      this.isCreateReceiver = !this.isCreateReceiver;
      this.listReceivers = 1;
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
      rowOnClickByCheckBox.call(this, asset);
    },

    callRowOnCtrlClick(asset) {
      rowOnCtrlClick.call(this, asset);
    },

    btnUncheckedAll() {
      this.selectedRows = [];
      this.selectedRowsByCheckBox = [];
    },

    //------------------------------------------- Form-------------------------------------------
    /**
     * Đóng form, gửi thông tin về cho component cha để đóng nó
     * Author: LDTUAN (21/08/2023)
     */
    btnCloseForm() {
      this.$emit("onCloseForm");
    },

    btnShowFormChooseAsset() {
      this.isShowFormChooseAsset = true;
    },

    onCloseForm() {
      this.isShowFormChooseAsset = false;
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
  max-height: 435px;
  display: flex;
  flex-direction: column;
  border-spacing: unset;
  overflow-y: auto;
}

.table-bot {
  flex: 1;
}

.row {
  display: grid;
  grid-template-columns:
    44px 50px 160px 180px 200px 200px 200px 200px calc(100% - 1344px)
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
}
</style>
