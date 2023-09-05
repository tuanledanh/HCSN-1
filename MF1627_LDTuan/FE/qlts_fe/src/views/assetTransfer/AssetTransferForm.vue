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
                v-model="transferAsset.TransferAssetCode"
                medium
                required
                maxlength="12"
              ></MISAInput>
            </div>
            <div class="content__column">
              <MISAInputDatePicker
                label="Ngày chứng từ"
                v-model="transferAsset.TransactionDate"
                medium
                required
              ></MISAInputDatePicker>
            </div>
            <div class="content__column">
              <MISAInputDatePicker
                label="Ngày điều chuyển"
                v-model="transferAsset.TransferDate"
                medium
                required
              ></MISAInputDatePicker>
            </div>
            <div class="content__column">
              <MISAInput
                normalGrid
                label="Ghi chú"
                v-model="transferAsset.Description"
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
          <div v-if="receivers[0]" class="content--bot__row">
            <div class="content--bot__header row--bot">
              <div>STT</div>
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
                  ></MISAInput>
                </div>
                <div class="content__column">
                  <MISAInput
                    normalGrid
                    medium
                    maxlength="12"
                    placeholder="Nhập đại diện"
                    v-model="receiver.ReceiverDelegate"
                  ></MISAInput>
                </div>
                <div class="content__column">
                  <MISAInput
                    normalGrid
                    medium
                    maxlength="12"
                    placeholder="Nhập chức vụ"
                    v-model="receiver.ReceiverPosition"
                  ></MISAInput>
                </div>
                <div class="content__column">
                  <div class="combo-icon">
                    <MISAIcon pull_up_large></MISAIcon>
                    <MISAIcon drop_down_large></MISAIcon>
                    <div @click="btnAddReceiver">
                      <MISAIcon add_box_thin></MISAIcon>
                    </div>
                    <div @click="btnDeleteReceiver(receiver)">
                      <MISAIcon v-if="index > 0" deleteIcon></MISAIcon>
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
              v-for="(asset, index) in assets"
              :key="index"
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
                  :existCode="asset.NewDepartmentName"
                  :inputChange="inputChange"
                  @filter="getNewDepartment(asset, $event)"
                  :newDepartment="asset.NewDepartmentName"
                  isDisplay
                  self_adjust_size
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
                ></MISAInput>
              </div>
              <div class="cell display--center-center border--bottom">
                <div class="icon-function">
                  <MISAIcon
                    deleteIcon
                    background
                    @click="btnDeleteAsset(asset)"
                  ></MISAIcon>
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
        @click="btnSaveTransferAsset"
        :tabindex="16"
      ></MISAButton>
    </div>
  </div>
  <MISAAssetTransferChooseForm
    @onCloseForm="onCloseForm"
    v-if="isShowFormChooseAsset"
    @loadData="loadData"
    :existFixedAsset="existFixedAsset"
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
    ty: {},
  },
  data() {
    return {
      // ----------------------------- Data -----------------------------
      // Toàn bộ dữ liệu của chứng từ điều chuyển
      transferData: {},
      // Chứng từ tài sản
      transferAsset: {},
      // Danh sách bản ghi tài sản
      assets: [],
      oldAssets: [],
      // Danh sách người nhận
      receivers: [],
      oldReceivers: [],
      // Danh sách các bản ghi đã chọn
      selectedRows: [],
      // Index của bản ghi đầu tiên trong danh sách
      lastIndex: 0,
      // Hiển thị phần tạo người nhận
      isCreateReceiver: false,

      // ----------------------------- Form -----------------------------
      isFormDisplay: false,
      // Hiển thị form chọn tài sản
      isShowFormChooseAsset: false,
      // Danh sách bản ghi đã tồn tại trong form này
      existFixedAsset: null,

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
      inputChange: null,
    };
  },
  computed: {
    /**
     * Kiểm trả xem chức năng user đang thực hiện là thêm mới hay cập nhật tài sản
     * Author: LDTUAN (03/09/2023)
     */
    formMode() {
      if (this.transferAssetToUpdate != null) {
        return this.$_MISAEnum.FORM_MODE.UPDATE;
      } else {
        return this.$_MISAEnum.FORM_MODE.ADD;
      }
    },
  },
  created() {
    /**
     * Kiểm tra thực hiện thêm, sửa, hay sao chép để thực hiện thao tác tương ứng
     * Author: LDTUAN (02/08/2023)
     */
    switch (this.formMode) {
      case this.$_MISAEnum.FORM_MODE.UPDATE:
        this.loadTransferDataUpdate();
        break;
      case this.$_MISAEnum.FORM_MODE.ADD:
        this.loadTransferAssetCode();
        break;
    }
  },
  mounted() {},
  methods: {
    AssetDepreciation,
    formatMoney,
    truncateText,

    //------------------------------------------- ADD-UPDATE CHỨNG TỪ -------------------------------------------
    /**
     * Lưu chứng từ
     * Author: LDTUAN (04/09/2023)
     */
    // TODO: validate
    btnSaveTransferAsset() {
      switch (this.formMode) {
        case this.$_MISAEnum.FORM_MODE.UPDATE:
          this.updateTransferAssetHelper();
          break;
        case this.$_MISAEnum.FORM_MODE.ADD:
          this.AddTransferAsset();
          break;
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
      let transfer = {
        TransferAsset: newTransferAsset,
        ListTransferAssetDetail: listTransferAssetDetail,
        ListReceiver: listReceiverFinal,
      };
      this.$_MISAApi.TransferAsset.Update(transferAssetId, transfer)
        .then(() => {
          this.btnCloseForm();
          this.$emit("reLoad");
        })
        .catch((res) => {
          // this.$processErrorResponse(res);
          // this.isShowToastValidateBE = true;
          // this.toast_content_warning = res.response.data.UserMessage;
          console.log(res);
        });
    },

    /**
     * Chuẩn bị dữ liệu rồi trả về cho hàm UpdateTransferAsset gọi đến để thực hiện gọi đến api
     * Author: LDTUAN (04/09/2023)
     */
    updateTransferAssetHelper() {
      // 1. Lấy thông tin của chứng từ điều chuyển cũ
      let oldTransferAsset = this.transferAsset;

      // 2.Lấy id của chứng từ điều chuyển
      let transferAssetId = oldTransferAsset.TransferAssetId;

      // 3.Lấy thông tin của chứng từ điều chuyển mới
      let newTransferAsset = {
        TransferAssetCode: oldTransferAsset.TransferAssetCode,
        TransferDate: oldTransferAsset.TransferDate,
        TransactionDate: oldTransferAsset.TransactionDate,
        Description: oldTransferAsset.Description,
      };
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

      // Chuyển DepartmentName và DepartmentId thành OldDepartmentName và OldDepartmentId trong this.assets
      let assets = this.assets;
      assets.forEach((asset) => {
        asset.OldDepartmentName = asset.DepartmentName;
        asset.OldDepartmentId = asset.DepartmentId;
      });

      // Tương tự, chuyển DepartmentName và DepartmentId thành OldDepartmentName và OldDepartmentId trong this.oldAssets
      let oldAssets = this.oldAssets;
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

      // Sử dụng hàm createAddUpdateDeleteList cho listReceiver
      let listReceiverFinal = this.createAddUpdateDeleteList(
        this.receivers,
        "ReceiverId",
        this.oldReceivers,
        selectedFieldsReceiver
      );
      this.UpdateTransferAsset(
        transferAssetId,
        newTransferAsset,
        listTransferAssetDetail,
        listReceiverFinal
      );
    },

    createAddUpdateDeleteList(sourceList, idField, oldList, selectedFields) {
      // 1. Danh sách add
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

      // 2. Danh sách update-delete
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

      let listUpdate = oldList
        .filter((oldItem) => {
          return listUD.find(
            (newItem) => newItem[idField] === oldItem[idField]
          );
        })
        .map((item) => {
          let newItem = {
            Flag: 1,
            [idField]: item[idField],
          };
          for (const field of selectedFields) {
            newItem[field] = item[field];
          }

          return newItem;
        });

      // 3. Nối các danh sách lại với nhau
      return [...listAdd, ...listDelete, ...listUpdate];
    },

    /**
     * Thêm mới chứng từ
     * Author: LDTUAN (04/09/2023)
     *
     */
    AddTransferAsset() {
      this.transferData.ListReceiver = this.receivers;
      const transferAssetDetails = this.assets.map((asset) => ({
        FixedAssetId: asset.FixedAssetId,
        OldDepartmentId: asset.DepartmentId,
        NewDepartmentId: asset.NewDepartmentId,
        Description: asset.description,
      }));
      this.transferData.ListTransferAssetDetail = transferAssetDetails;
      this.transferData.TransferAsset = this.transferAsset;
      this.$_MISAApi.TransferAsset.Create(this.transferData)
        .then(() => {
          this.btnCloseForm();
          this.$emit("reLoad");
        })
        .catch((res) => {
          // this.$processErrorResponse(res);
          // this.isShowToastValidateBE = true;
          // this.toast_content_warning = res.response.data.UserMessage;
          console.log(res);
        });
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
          // this.$processErrorResponse(res);
          // this.isShowToastValidateBE = true;
          // this.toast_content_warning = res.response.data.UserMessage;
          console.log(res);
        });
    },

    async loadTransferDataUpdate() {
      var oldTransferAsset = this.transferAssetToUpdate;
      await this.$_MISAApi.TransferAsset.GetByCode(
        oldTransferAsset.TransferAssetCode
      )
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

          // Lưu lại oldAssets và oldReceivers này để xài cho việc update-delete
          // Cách viết ... là để sao chép dữ liệu, nếu viết this.a = this.b thì sẽ làm tham chiếu
          // Chúng sẽ cùng trỏ đến 1 địa chỉ nên 1 cái thay đổi dữ liệu thì cái kia đổi theo
          this.oldAssets = [...this.assets];
          this.oldReceivers = [...this.receivers];
        })
        .catch((res) => {
          console.log(res);
        });
    },

    //------------------------------------------- RECEIVER -------------------------------------------
    btnAddReceiver() {
      const newReceiver = {
        ReceiverCode: "",
        ReceiverFullname: "",
        ReceiverDelegate: "",
        ReceiverPosition: "",
      };
      this.receivers.push(newReceiver);
    },

    btnDeleteReceiver(receiver) {
      const index = this.receivers.indexOf(receiver);
      if (index !== -1) {
        this.receivers.splice(index, 1);
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

    //------------------------------------------- ASSET -------------------------------------------
    // load data từ form chọn tài sản chứng từ
    // TODO: làm thêm api lấy danh sách tài sản không chứa những tài sản đã chọn
    loadData(items) {
      if (!this.assets || this.assets.length <= 0) {
        this.assets = items;
      } else {
        this.assets = this.assets.concat(items);
      }
    },

    btnDeleteAsset(asset) {
      const index = this.assets.indexOf(asset);
      if (index !== -1) {
        this.assets.splice(index, 1);
      }
    },

    //------------------------------------------- COMBOBOX -------------------------------------------
    getNewDepartment(asset, item) {
      if (item) {
        asset.NewDepartmentId = item.DepartmentId;
        asset.NewDepartmentName = item.DepartmentName;
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
      this.assets.forEach((asset) => {
        if (!Object.prototype.hasOwnProperty.call(asset, "DepartmentCode")) {
          asset.DepartmentCode = "raw";
          asset.FixedAssetCategoryCode = "raw";
          asset.FixedAssetCategoryName = "raw";
        }
      });
      this.existFixedAsset = this.assets;
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
