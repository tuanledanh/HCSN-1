<template>
  <div class="popup">
    <div class="popup-container">
      <div class="popup-head">
        <span>Sửa tài sản</span>
        <MISAButton
          button_icon_normal
          exit
          content="Thoát"
          position="bot"
          @click="btnCloseForm"
        ></MISAButton>
      </div>
      <div class="popup-body">
        <div class="popup-body__row">
          <div class="popup__column">
            <MISAInput
              normal
              label="Mã tài sản"
              ref="AssetCode"
              @focus="setInputFocus('AssetCode')"
              placeholder="Nhập mã tài sản"
              v-model="asset.FixedAssetCode"
              focus
              required
              maxlength="50"
            ></MISAInput>
          </div>
          <div class="popup__column">
            <MISAInput
              large
              label="Tên tài sản"
              ref="AssetName"
              @focus="setInputFocus('AssetName')"
              placeholder="Nhập tên tài sản"
              v-model="asset.FixedAssetName"
              required
              maxlength="100"
            ></MISAInput>
          </div>
        </div>
        <div class="popup-body__row">
          <div class="popup__column">
            <MISACombobox
              :api="this.$_MISAApi.Department.Api"
              propText="DepartmentCode"
              propValue="DepartmentId"
              placeholder="Chọn mã bộ phận sử dụng"
              label="Mã bộ phận sử dụng"
              ref="DepartmentId"
              @focus="setInputFocus('DepartmentId')"
              :existCode="asset.DepartmentCode"
              required
              @filter="getDepartmentCode"
              :inputChange="inputChange"
              maxlength="100"
            ></MISACombobox>
          </div>
          <div class="popup__column">
            <MISAInput
              large
              label="Tên bộ phận sử dụng"
              disabled
              v-model="this.department.DepartmentName"
              maxlength="100"
            ></MISAInput>
          </div>
        </div>
        <div class="popup-body__row">
          <div class="popup__column">
            <MISACombobox
              :api="this.$_MISAApi.FixedAssetCategory.Api"
              propText="FixedAssetCategoryCode"
              propValue="FixedAssetCategoryId"
              placeholder="Chọn mã loại tài sản"
              label="Mã loại tài sản"
              ref="AssetTypeId"
              @focus="setInputFocus('AssetTypeId')"
              :existCode="asset.FixedAssetCategoryCode"
              required
              @filter="getAssetTypeCode"
              :inputChange="inputChange"
              maxlength="100"
            ></MISACombobox>
          </div>
          <div class="popup__column">
            <MISAInput
              large
              label="Tên loại tài sản"
              disabled
              v-model="this.assetType.FixedAssetCategoryName"
              maxlength="100"
            ></MISAInput>
          </div>
        </div>
        <div class="popup-body__row">
          <div class="popup__column">
            <MISAInput
              normal
              label="Số lượng"
              ref="AssetQuantity"
              @focus="setInputFocus('AssetQuantity')"
              v-model="asset.Quantity"
              typeOfValue="number"
              required
              right
              maxlength="100"
            ></MISAInput>
          </div>
          <div class="popup__column">
            <MISAInput
              normal
              label="Nguyên giá"
              ref="AssetPrice"
              @focus="setInputFocus('AssetPrice')"
              v-model="asset.Cost"
              typeOfValue="number"
              required
              right
              maxlength="100"
            ></MISAInput>
          </div>
          <div class="popup__column">
            <MISAInput
              normal
              label="Số năm sử dụng"
              ref="YearOfUse"
              @focus="setInputFocus('YearOfUse')"
              typeOfValue="number"
              v-model="asset.LifeTime"
              required
              right
              maxlength="100"
            ></MISAInput>
          </div>
        </div>
        <div class="popup-body__row">
          <div class="popup__column">
            <MISAInput
              normal
              label="Tỷ lệ hao mòn (%)"
              ref="assetDepreciation"
              @focus="setInputFocus('assetDepreciation')"
              v-model="assetDepreciation"
              typeOfValue="percent"
              required
              right
              maxlength="100"
            ></MISAInput>
          </div>
          <div class="popup__column">
            <MISAInput
              normal
              label="Giá trị hao mòn năm"
              ref="yearlyDepreciationAmount"
              @focus="setInputFocus('yearlyDepreciationAmount')"
              v-model="yearlyDepreciationAmount"
              typeOfValue="number"
              required
              right
              maxlength="100"
            ></MISAInput>
          </div>
          <div class="popup__column">
            <MISAInput
              normal
              label="Năm theo dõi"
              typeOfValue="number"
              v-model="asset.TrackedYear"
              disabled
              right
              maxlength="100"
            ></MISAInput>
          </div>
        </div>
        <div class="popup-body__row">
          <div class="popup__column">
            <MISAInputDatePicker
              ref="PurchaseDate"
              @focus="setInputFocus('PurchaseDate')"
              v-model="asset.PurchaseDate"
              label="Ngày mua"
              required
            ></MISAInputDatePicker>
          </div>
          <div class="popup__column">
            <MISAInputDatePicker
              ref="StartUsingDate"
              @focus="setInputFocus('StartUsingDate')"
              v-model="asset.StartUsingDate"
              label="Ngày bắt đầu sử dụng"
              required
            ></MISAInputDatePicker>
          </div>
        </div>
      </div>
      <div class="popup-foot">
        <MISAButton
          buttonMain
          textButton="Lưu"
          @click="btnSaveAsset"
        ></MISAButton>
        <MISAButton
          buttonOutline
          textButton="Hủy"
          @click="btnCancelForm"
        ></MISAButton>
      </div>
    </div>
    <MISALoading v-if="isLoading"></MISALoading>
    <div v-if="isShowToastWarningCancel" class="blur">
      <MISAToast typeToast="warning" :content="toast_content_warning"
        ><MISAButton
        buttonOutline
          textButton="Không"
          @click="btnCloseToastWarning"
        ></MISAButton>
        <MISAButton
          buttonMain
          textButton="Hủy bỏ"
          @click="btnNotSaveAsset"
        ></MISAButton
      ></MISAToast>
    </div>
    <div v-if="isShowToastWarningEdit" class="blur">
      <MISAToast typeToast="warning" :content="toast_content_warning"
        ><MISAButton
        buttonOutline
          textButton="Hủy bỏ"
          @click="btnCloseToastWarning"
        ></MISAButton>
        <MISAButton
          buttonSub
          textButton="Không lưu"
          @click="btnNotSaveAsset"
        ></MISAButton>
        <MISAButton
          buttonMain
          textButton="Lưu"
          @click="btnSaveAsset"
        ></MISAButton
      ></MISAToast>
    </div>
    <div v-if="isShowToastValidate" class="blur">
      <MISAToast
        typeToast="warning"
        :content="'Cần phải nhập thông tin ' + toast_content_warning + '.'"
        ><MISAButton
        buttonMain
          textButton="Đóng"
          @click="btnCloseToastWarning"
        ></MISAButton>
      </MISAToast>
    </div>
    <div v-if="isShowToastValidateBE" class="blur">
      <MISAToast typeToast="warning" :content="toast_content_warning + '.'"
        ><MISAButton
          buttonMain
          textButton="Đóng"
          @click="btnCloseToastWarning"
        ></MISAButton>
      </MISAToast>
    </div>
  </div>
</template>
<script>
import { formatMoneyToInt } from "../../helpers/common/format/format";
import { formatMoney } from "../../helpers/common/format/format";
import { DateFormat } from "../../helpers/common/format/format";
export default {
  name: "AssetForm",
  props: {
    // Tài sản cần cập nhật
    editAsset: {
      type: Object,
      default: null,
    },

    // Tài sản cần sao chép
    cloneAsset: {
      type: Object,
      default: null,
    },
  },
  data() {
    return {
      // Tài sản
      asset: {},
      // Giá trị input thay đổi
      inputChange: null,
      // Loading
      isLoading: false,
      // Danh sách lỗi
      errors: [],
      // Hiển thi thông báo
      showNotice: false,
      // Gửi yêu cầu đóng form cho component cha
      requestCloseForm: false,
      // Danh sách phòng ban
      department: {},
      // Danh sách loại tài sản
      assetType: {},
      // Tỉ lệ hao mòn
      assetDepreciation: 0,
      // Giá trị hao mòn năm
      yearlyDepreciationAmount: 0,
      // Ngày tháng
      date: null,
      // Focus
      // Focus vào ô nhập liệu, ban đầu là mã tài sản
      inputFocus: "AssetCode",

      // =================================Toast===================================================
      // Hiển thị thông báo khi click vào icon thoát hoặc hủy
      isShowToastWarningCancel: false,
      // Hiển thị thông báo khi hủy cập nhật
      isShowToastWarningEdit: false,
      // Hiển thị thông báo lỗi liên quan đến kiểm tra giá trị các ô nhập liệu
      isShowToastValidate: false,
      // Hiển thị thông báo lỗi liên quan đến backend
      isShowToastValidateBE: false,
      // Nội dung thông báo lỗi
      toast_content_warning: null,

      // =================================Clone asset===================================================
      // Mã code mới cho việc sao chép tài sản
      cloneAssetCode: null,
    };
  },
  watch: {
    /**
     * Tự tính lại tỉ lệ hao mòn và giá trị hao mòn
     * @param {int} value số năm sử dụng
     * Author: LDTUAN (02/08/2023)
     */
    "asset.LifeTime": function (value) {
      if (value > 0) {
        this.assetDepreciation = (1 / value).toFixed(4);
        //this.asset.AssetPrice= formatMoney(this.asset.AssetPrice);
        var assetCost = formatMoneyToInt(this.asset.Cost);
        this.yearlyDepreciationAmount = formatMoney(
          (assetCost * this.assetDepreciation).toFixed(0)
        );
        this.assetDepreciation = (this.assetDepreciation * 100).toFixed(2);
      } else {
        this.assetDepreciation = 0;
      }
    },

    /**
     * Tự tính lại giá trị hao mòn
     * @param {int} value nguyên giá
     * Author: LDTUAN (02/08/2023)
     */
    "asset.Cost": function (value) {
      if (formatMoneyToInt(value) > 0) {
        this.yearlyDepreciationAmount = formatMoney(
          ((formatMoneyToInt(value) * this.assetDepreciation) / 100).toFixed(0)
        );
      } else {
        this.assetDepreciation = 0;
      }
    },

    /**
     * Tự tính lại số năm sử dụng dựa vào tỉ lệ và giá trị hao mòn
     * @param {int} value giá trị hao mòn
     * Author: LDTUAN (02/08/2023)
     */
    yearlyDepreciationAmount(value) {
      const valueInt = formatMoneyToInt(value);
      const assetCostInt = formatMoneyToInt(this.asset.Cost);

      if (valueInt > 0 && assetCostInt > 0) {
        this.asset.LifeTime = (assetCostInt / valueInt).toFixed(0);
      }
    },

    "asset.PurchaseDate": function (value) {
      this.asset.PurchaseDate = DateFormat(value);
    },

    "asset.StartUsingDate": function (value) {
      this.asset.StartUsingDate = DateFormat(value);
    },
  },
  created() {
    /**
     * Lấy code mới trong trường hợp sao chép dữ liệu, để gán code mới
     * Author: LDTUAN (02/08/2023)
     */
    this.loadAssetCode();

    /**
     * Kiểm tra thực hiện thêm, sửa, hay sao chép để thực hiện thao tác tương ứng
     * Author: LDTUAN (02/08/2023)
     */
    switch (this.formMode) {
      case this.$_MISAEnum.FORM_MODE.UPDATE:
        this.loadUpdateAsset();
        break;
      case this.$_MISAEnum.FORM_MODE.CLONE:
        this.loadUpdateAsset();
        break;
      case this.$_MISAEnum.FORM_MODE.ADD:
        this.loadAssetCode();
        break;
    }
  },
  beforeMount() {},
  mounted() {},
  computed: {
    /**
     * Kiểm trả xem chức năng user đang thực hiện là thêm mới hay cập nhật tài sản
     * Author: LDTUAN (03/07/2023)
     */
    formMode() {
      if (this.cloneAsset != null) {
        return this.$_MISAEnum.FORM_MODE.CLONE;
      } else if (this.editAsset != null) {
        return this.$_MISAEnum.FORM_MODE.UPDATE;
      } else {
        return this.$_MISAEnum.FORM_MODE.ADD;
      }
    },
  },
  methods: {
    /** --------------------Load data---------------------- */
    /**
     * Thực hiện load data của tài sản để cập nhật
     * Author: LDTUAN (03/07/2023)
     */
    async loadUpdateAsset() {
      try {
        this.isLoading = true;
        let assetOld = this.editAsset;
        var clone = false;
        if (this.formMode == this.$_MISAEnum.FORM_MODE.CLONE) {
          assetOld = this.cloneAsset;
          clone = true;
        }
        await this.$_MISAApi.FixedAsset.GetByCode(assetOld.FixedAssetCode)
          .then((res) => {
            setTimeout(() => {
              this.asset = res.data;
              // Lý do format vì ở watch có theo dõi giá trị của assetPrice
              // nhưng là giá trị kiểu string, nếu là int khi truyền từ editAsset
              // thì sẽ có lỗi khi formatMoneyToInt, nên phải chuyển đổi ngay ở đây
              this.asset.Cost = formatMoney(this.asset.Cost);
              this.asset.PurchaseDate = DateFormat(this.asset.PurchaseDate);
              this.asset.StartUsingDate = DateFormat(this.asset.StartUsingDate);
              if (clone) {
                this.asset.FixedAssetCode = this.cloneAssetCode;
              }
            }, 0);

            this.isLoading = false;
          })
          .catch((res) => {
            this.$processErrorResponse(res);
            this.isShowToastValidateBE = true;
            this.toast_content_warning = res.response.data.UserMessage;
          });
      } catch (error) {
        // this.errors.push(error);
        // this.showNotice = true;
        console.log(error);
      }
    },
    /**
     * Lấy mã tài sản mới đế thêm mới tài sản
     * Author: LDTUAN (03/07/2023)
     */
    async loadAssetCode() {
      try {
        this.isLoading = true;
        await this.$_MISAApi.FixedAsset.GetNewCode()
          .then((res) => {
            this.asset.FixedAssetCode = res.data;
            this.cloneAssetCode = res.data;
            const date = new Date();
            this.asset.TrackedYear = date.getFullYear();
            this.asset.PurchaseDate = DateFormat(this.$getCurrentDate());
            this.asset.StartUsingDate = DateFormat(this.$getCurrentDate());
            if (this.formMode == this.$_MISAEnum.FORM_MODE.ADD) {
              this.asset.Quantity = 0;
              this.asset.Cost = 0;
              this.asset.LifeTime = 0;
            }
            this.isLoading = false;
          })
          .catch((res) => {
            this.$processErrorResponse(res);
            this.isShowToastValidateBE = true;
            this.toast_content_warning = res.response.data.UserMessage;
          });
      } catch (error) {
        // this.errors.push(error);
        // this.showNotice = true;
        console.log(error);
      }
    },

    /**
     * Gán lại id của phòng ban cho tài sản
     * @param {object} item phòng ban
     */
    async getDepartmentCode(item) {
      this.department = item;
      this.asset.DepartmentId = this.department.DepartmentId;
    },

    /**
     * Gán lại id và số năm sử dụng của loại tài sản cho tài sản
     * @param {object} item loại tài sản
     * Author: LDTUAN (02/08/2023)
     */
    async getAssetTypeCode(item) {
      this.assetType = item;
      this.asset.FixedAssetCategoryId = this.assetType.FixedAssetCategoryId;
      if (!this.isUpdate()) {
        this.asset.LifeTime;
      } else {
        this.asset.LifeTime = this.assetType.LifeTime;
      }
    },

    /** --------------------Form---------------------- */
    /**
     * Thông báo khi click icon X để đóng form, message tương ứng với từng hành động khác nhau
     * Author: LDTUAN (02/08/2023)
     */
    btnCloseForm() {
      if (!this.isUpdate()) {
        this.requestCloseForm = true;
        this.$emit("onCloseForm", this.requestCloseForm);
      } else {
        switch (this.formMode) {
          case this.$_MISAEnum.FORM_MODE.ADD:
            this.toast_content_warning =
              this.$_MISAResource.VN.Form.Warning.Cancel.Add;
            this.isShowToastWarningCancel = true;
            break;
          case this.$_MISAEnum.FORM_MODE.CLONE:
            this.toast_content_warning =
              this.$_MISAResource.VN.Form.Warning.Cancel.Add;
            this.isShowToastWarningCancel = true;
            break;
          case this.$_MISAEnum.FORM_MODE.UPDATE:
            this.toast_content_warning =
              this.$_MISAResource.VN.Form.Warning.Edit;
            this.isShowToastWarningEdit = true;
            break;
        }
      }
    },

    /**
     * Thông báo khi click nút hủy, hiển thị thông báo tương ứng với từng hành động
     * Author: LDTUAN (02/08/2023)
     */
    btnCancelForm() {
      if (!this.isUpdate()) {
        switch (this.formMode) {
          case this.$_MISAEnum.FORM_MODE.UPDATE:
            this.toast_content_warning =
              this.$_MISAResource.VN.Form.Warning.Cancel.Update;
            this.isShowToastWarningCancel = true;
            break;
          case this.$_MISAEnum.FORM_MODE.CLONE:
            this.toast_content_warning =
              this.$_MISAResource.VN.Form.Warning.Cancel.Add;
            this.isShowToastWarningCancel = true;
            break;
        }
      } else {
        switch (this.formMode) {
          case this.$_MISAEnum.FORM_MODE.ADD:
            this.toast_content_warning =
              this.$_MISAResource.VN.Form.Warning.Cancel.Add;
            this.isShowToastWarningCancel = true;
            break;
          case this.$_MISAEnum.FORM_MODE.CLONE:
            this.toast_content_warning =
              this.$_MISAResource.VN.Form.Warning.Cancel.Add;
            this.isShowToastWarningCancel = true;
            break;
          case this.$_MISAEnum.FORM_MODE.UPDATE:
            this.toast_content_warning =
              this.$_MISAResource.VN.Form.Warning.Edit;
            this.isShowToastWarningEdit = true;
            break;
        }
      }
    },

    /** --------------------Update data---------------------- */

    /**
     * Lưu tài sản nếu validate xong
     * Author: LDTUAN (02/08/2023)
     */
    btnSaveAsset() {
      this.validate();
      // if toast show validate thì cần if nếu k có nó thì mới cho add hoặc update
      if (!this.isShowToastValidate && !this.isShowToastValidateBE) {
        if (this.isUpdate()) {
          const asset = {
            FixedAssetCode: this.asset.FixedAssetCode.trim(),
            FixedAssetName: this.asset.FixedAssetName.trim(),
            DepartmentId: this.department.DepartmentId,
            DepartmentCode: this.department.DepartmentCode,
            DepartmentName: this.department.DepartmentName,
            FixedAssetCategoryId: this.assetType.FixedAssetCategoryId,
            FixedAssetCategoryCode: this.assetType.FixedAssetCategoryCode,
            FixedAssetCategoryName: this.assetType.FixedAssetCategoryName,
            PurchaseDate: this.asset.PurchaseDate,
            StartUsingDate: this.asset.StartUsingDate,
            Cost: formatMoneyToInt(this.asset.Cost),
            Quantity: formatMoneyToInt(this.asset.Quantity),
            TrackedYear: this.asset.TrackedYear,
            LifeTime: this.asset.LifeTime,
          };
          if (this.formMode == this.$_MISAEnum.FORM_MODE.UPDATE) {
            try {
              this.$_MISAApi.FixedAsset.Update(this.asset.FixedAssetId, asset)
                .then(() => {
                  this.btnCloseForm();
                  const ob = {
                    Asset: this.asset,
                    FixedAssetCategoryName:
                      this.assetType.FixedAssetCategoryName,
                    DepartmentName: this.department.DepartmentName,
                  };
                  this.$emit("reLoad", ob);
                  this.btnCloseToastWarning();
                  this.btnNotSaveAsset();
                })
                .catch((res) => {
                  this.$processErrorResponse(res);
                  this.isShowToastValidateBE = true;
                  this.toast_content_warning = res.response.data.UserMessage;
                });
            } catch (error) {
              console.log(error);
            }
          } else if (
            this.formMode == this.$_MISAEnum.FORM_MODE.ADD ||
            this.formMode == this.$_MISAEnum.FORM_MODE.CLONE
          ) {
            try {
              this.$_MISAApi.FixedAsset.Create(asset)
                .then(() => {
                  this.btnCloseForm();
                  const ob = {
                    Asset: this.asset,
                    FixedAssetCategoryName:
                      this.assetType.FixedAssetCategoryName,
                    DepartmentName: this.department.DepartmentName,
                  };
                  this.$emit("reLoad", ob);
                  this.btnCloseToastWarning();
                  this.btnNotSaveAsset();
                })
                .catch((res) => {
                  this.$processErrorResponse(res);
                  this.isShowToastValidateBE = true;
                  this.toast_content_warning = res.response.data.UserMessage;
                });
            } catch (error) {
              console.log(error);
            }
          }
        } else {
          this.btnNotSaveAsset();
        }
      }
    },

    /** --------------------Focus---------------------- */
    /**
     * Focus vào ô nhập liệu
     * @param {input} value ô input
     * Author: LDTUAN (02/08/2023)
     */
    setInputFocus(value) {
      this.inputFocus = value;
    },

    /** --------------------Toast---------------------- */
    /**
     * Đóng các thông báo khi click nút hủy
     * Author: LDTUAN (02/08/2023)
     */
    btnCloseToastWarning() {
      this.isShowToastWarningCancel = false;
      this.isShowToastWarningEdit = false;
      this.isShowToastValidate = false;
      this.isShowToastValidateBE = false;
    },

    /**
     * Đóng form khi click không lưu
     * Author: LDTUAN (02/08/2023)
     */
    btnNotSaveAsset() {
      this.requestCloseForm = true;
      this.$emit("onCloseForm", this.requestCloseForm);
    },

    /** --------------------Validate---------------------- */
    /**
     * Kiểm trả dữ liệu trên ô nhập liệu có rỗng hay không, có message tương ứng nếu rỗng
     * Author: LDTUAN (02/08/2023)
     */
    validate() {
      if (this.asset.PurchaseDate > this.asset.StartUsingDate) {
        this.isShowToastValidateBE = true;
        this.toast_content_warning =
          this.$_MISAResource.VN.Form.Validate.PurchaseDate;
      }
      switch ("") {
        case this.asset.FixedAssetCode ?? "":
          this.inputFocus = "AssetCode";
          this.isShowToastValidate = true;
          this.toast_content_warning =
            this.$_MISAResource.VN.Form.Validate.FixedAssetCode;
          break;
        case this.asset.FixedAssetName ?? "":
          this.inputFocus = "AssetName";
          this.isShowToastValidate = true;
          this.toast_content_warning =
            this.$_MISAResource.VN.Form.Validate.FixedAssetName;
          break;
        case this.asset.DepartmentId ?? "":
          this.inputFocus = "DepartmentId";
          this.isShowToastValidate = true;
          this.toast_content_warning =
            this.$_MISAResource.VN.Form.Validate.DepartmentId;
          break;
        case this.asset.FixedAssetCategoryId ?? "":
          this.inputFocus = "AssetTypeId";
          this.isShowToastValidate = true;
          this.toast_content_warning =
            this.$_MISAResource.VN.Form.Validate.FixedAssetCategoryId;
          break;
      }
      switch (0) {
        case this.asset.Quantity:
          this.inputFocus = "AssetQuantity";
          this.isShowToastValidate = true;
          this.toast_content_warning =
            this.$_MISAResource.VN.Form.Validate.Quantity;
          break;
        case this.asset.Cost:
          this.inputFocus = "AssetPrice";
          this.isShowToastValidate = true;
          this.toast_content_warning =
            this.$_MISAResource.VN.Form.Validate.Cost;
          break;
        case formatMoneyToInt(this.asset.LifeTime):
          this.inputFocus = "YearOfUse";
          this.isShowToastValidate = true;
          this.toast_content_warning =
            this.$_MISAResource.VN.Form.Validate.LifeTime;
          break;
        case this.assetDepreciation:
          this.inputFocus = "assetDepreciation";
          this.isShowToastValidate = true;
          this.toast_content_warning =
            this.$_MISAResource.VN.Form.Validate.AssetDepreciation;
          break;
        case formatMoneyToInt(this.yearlyDepreciationAmount):
          this.inputFocus = "yearlyDepreciationAmount";
          this.isShowToastValidate = true;
          this.toast_content_warning =
            this.$_MISAResource.VN.Form.Validate.YearlyDepreciationAmount;
          break;
      }
    },

    /**
     * Kiểm tra xem dữ liệu có thay đổi không, thể hiện message tương ứng khi thoát, hủy
     * Author: LDTUAN (02/08/2023)
     */
    isUpdate() {
      if (this.formMode == this.$_MISAEnum.FORM_MODE.UPDATE) {
        let isChange =
          this.editAsset.FixedAssetCode !== this.asset.FixedAssetCode.trim();
        isChange =
          isChange ||
          this.editAsset.FixedAssetName !== this.asset.FixedAssetName.trim();
        isChange = isChange || this.editAsset.Quantity !== this.asset.Quantity;
        isChange =
          isChange || this.editAsset.Cost !== formatMoneyToInt(this.asset.Cost);
        isChange =
          isChange ||
          this.editAsset.LifeTime !== formatMoneyToInt(this.asset.LifeTime);
        isChange =
          isChange || this.editAsset.TrackedYear !== this.asset.TrackedYear;
        isChange =
          isChange ||
          DateFormat(this.editAsset.PurchaseDate) !==
            DateFormat(this.asset.PurchaseDate);
        isChange =
          isChange ||
          DateFormat(this.editAsset.StartUsingDate) !==
            DateFormat(this.asset.StartUsingDate);
        isChange =
          isChange ||
          this.editAsset.FixedAssetCategoryId !==
            this.asset.FixedAssetCategoryId;
        isChange =
          isChange || this.editAsset.DepartmentId !== this.asset.DepartmentId;
        return isChange;
      } else if (this.formMode == this.$_MISAEnum.FORM_MODE.CLONE) {
        let isChange =
          this.cloneAsset.FixedAssetCode !== this.asset.FixedAssetCode.trim();
        isChange =
          isChange ||
          this.cloneAsset.FixedAssetName !== this.asset.FixedAssetName.trim();
        isChange = isChange || this.cloneAsset.Quantity !== this.asset.Quantity;
        isChange =
          isChange ||
          this.cloneAsset.Cost !== formatMoneyToInt(this.asset.Cost);
        isChange =
          isChange ||
          this.cloneAsset.LifeTime !== formatMoneyToInt(this.asset.LifeTime);
        isChange =
          isChange || this.cloneAsset.TrackedYear !== this.asset.TrackedYear;
        isChange =
          isChange ||
          DateFormat(this.cloneAsset.PurchaseDate) !==
            DateFormat(this.asset.PurchaseDate);
        isChange =
          isChange ||
          DateFormat(this.cloneAsset.StartUsingDate) !==
            DateFormat(this.asset.StartUsingDate);
        isChange =
          isChange ||
          this.cloneAsset.FixedAssetCategoryId !==
            this.asset.FixedAssetCategoryId;
        isChange =
          isChange || this.cloneAsset.DepartmentId !== this.asset.DepartmentId;
        return isChange;
      } else if (this.formMode == this.$_MISAEnum.FORM_MODE.ADD) {
        return true;
      }
      return false;
    },
  },
  updated() {
    // Focus vào ô nhập liệu
    this.$refs[this.inputFocus].focusInput();
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
  background-color: #0000005d;
  z-index: 2;
}

.popup-container {
  width: 860px;
  height: 580px;
  position: absolute;
  display: flex;
  flex-direction: column;
  box-sizing: border-box;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
  background-color: #ffffff;
  border-radius: 4px;
}

/*=================================== Popup head ===================================*/
.popup-head {
  padding: 20px 16px;
  font-family: Roboto, sans-serif;
  font-weight: 700;
  font-size: 20px;
  box-sizing: border-box;
  display: flex;
  flex-direction: row;
  align-items: center;
  justify-content: space-between;
}

/*=================================== Popup body ===================================*/
.popup-body {
  flex: 1;
  display: flex;
  flex-direction: column;
  row-gap: 16px;
  padding-left: 16px;
  box-sizing: border-box;
}

.popup-body__row {
  box-sizing: border-box;
  display: flex;
  column-gap: 16px;
}

.popup__column {
  font-family: Roboto, sans-serif;
  font-weight: 400;
  display: flex;
  flex-direction: column;
}

/*=================================== Popup foot ===================================*/
.popup-foot {
  background-color: #f5f5f5;
  height: 52px;
  box-sizing: border-box;
  display: flex;
  flex-direction: row-reverse;
  column-gap: 10px;
  align-items: center;
  padding-right: 16px;
  border-bottom-left-radius: 4px;
  border-bottom-right-radius: 4px;
}
</style>
