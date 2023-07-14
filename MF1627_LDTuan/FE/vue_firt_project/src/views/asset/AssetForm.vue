<template>
  <div class="popup">
    <div class="popup-container">
      <!-- Popup head start-->
      <div class="popup-head">
        <span>Sửa tài sản</span>
        <button
          class="button button--sub button--sub-non-border button--empty icon-content-top"
          @click="btnCloseOnClick()"
        >
          <div class="icon--exit"></div>
        </button>
      </div>
      <!-- Popup head end-->

      <!-- Popup body start-->
      <div class="popup-body">
        <div class="popup-body__row">
          <div class="popup__column">
            <MISAInput
              inputClass="input--popup-width"
              label="Mã tài sản"
              placeholder="Nhập mã tài sản"
              v-model="asset.assetCode"
              ref="txtAssetCode"
              :isSubmittedForm="isSubmittedForm"
              required
            ></MISAInput>
          </div>
          <div class="popup__column">
            <MISAInput
              inputClass="input--popup-high-width"
              label="Tên tài sản"
              placeholder="Nhập tên tài sản"
              v-model="asset.assetName"
              :isSubmittedForm="isSubmittedForm"
              required
            ></MISAInput>
          </div>
        </div>
        <div class="popup-body__row">
          <div class="popup__column">
            <div class="label">
              <label for="">Mã bộ phận sử dụng</label>
              <div class="icon--require"></div>
            </div>
            <div class="input input--popup input--popup-width">
              <input
                class="input__font--italic"
                type="text"
                placeholder="Chọn mã bộ phận sử dụng"
              />
              <div class="icon--input icon--input-right">
                <div class="icon--drop-down"></div>
              </div>
            </div>
          </div>
          <div class="popup__column">
            <MISAInput
              inputClass="input--popup-high-width"
              label="Tên bộ phận sử dụng"
              disabled
            ></MISAInput>
          </div>
        </div>
        <div class="popup-body__row">
          <div class="popup__column">
            <div class="label">
              <label for="">Mã loại tài sản</label>
              <div class="icon--require"></div>
            </div>
            <div class="input input--popup input--popup-width">
              <input
                class="input__font--italic"
                type="text"
                placeholder="Chọn mã loại tài sản"
              />
              <div class="icon--input icon--input-right">
                <div class="icon--drop-down"></div>
              </div>
            </div>
          </div>
          <div class="popup__column">
            <MISAInput
              inputClass="input--popup-high-width"
              label="Tên loại tài sản"
              disabled
            ></MISAInput>
          </div>
        </div>
        <div class="popup-body__row">
          <div class="popup__column">
            <MISAInputQuantity
              label="Số lượng"
              v-model="asset.assetQuantity"
              required
            ></MISAInputQuantity>
          </div>
          <div class="popup__column">
            <MISAInput
              inputClass="input--popup-width input__text--right"
              label="Nguyên giá"
              v-model="asset.assetPrice"
              required
            ></MISAInput>
          </div>
          <div class="popup__column">
            <MISAInput
              inputClass="input--popup-width input__text--right"
              label="Số năm sử dụng"
              v-model="asset.yearOfUse"
              required
            ></MISAInput>
          </div>
        </div>
        <div class="popup-body__row">
          <div class="popup__column">
            <MISAInputQuantity
              label="Tỷ lệ hao mòn (%)"
              v-model="asset.AssetDepreciation"
              required
            ></MISAInputQuantity>
          </div>
          <div class="popup__column">
            <MISAInput
              inputClass="input--popup-width input__text--right"
              label="Giá trị hao mòn năm"
              v-model="isZero"
              required
            ></MISAInput>
          </div>
          <div class="popup__column">
            <MISAInput
              inputClass="input--popup-width input__text--right"
              label="Năm theo dõi"
              v-model="asset.yearOfTracking"
              disabled
            ></MISAInput>
          </div>
        </div>
        <div class="popup-body__row">
          <div class="popup__column">
            <MISAInputDate
              label="Ngày mua"
              v-model="dateNow"
              required
            ></MISAInputDate>
          </div>
          <div class="popup__column">
            <MISAInputDate
              label="Ngày bắt đầu sử dụng"
              v-model="dateNow"
              required
            ></MISAInputDate>
          </div>
        </div>
      </div>
      <!-- Popup body end-->

      <!-- Popup foot start-->
      <div class="popup-foot">
        <button class="button button--main" @click="btnSaveOnClick()">
          Lưu
        </button>
        <button class="button button--sub">Hủy</button>
      </div>
      <!-- Popup foot end-->
    </div>
    <MISALoading v-if="isLoading"></MISALoading>
    <MISADialogNotice
      v-show="showNotice"
      :errors="errors"
      @onCloseDialog="showNotice = false"
    ></MISADialogNotice>
  </div>
</template>
<script>
export default {
  name: "AssetForm",
  props: ["EditAsset"],
  data() {
    return {
      asset: {},
      isLoading: false,
      errors: [],
      showNotice: false,
      isZero: 0,
      isYear: 2023,
      dateNow: "",
      submittedForm: false,
    };
  },
  created() {
    if (this.formMode == this.$_MISAEnum.FORM_MODE.UPDATE) {
      // Nếu user thực hiện chức năng update thì sẽ tải thông tin cần update lên popup
      this.loadUpdateAssest();
    } 
    // else {
    //   // Nếu user thực hiện chức năng add thì sẽ tải mã tài sản mới lên popup
    //   this.loadAssetCode();
    // }

    this.$_emitter.on("onSubmittedForm", () => {
      this.submittedForm = false;
    });
  },
  mounted() {
    // Focus vào ô nhập liệu có ref là txtAssetCode
    this.$refs["txtAssetCode"].focus();
    // Thực hiện hàm getCurrenDate() để lấy thời gian hiện tại
    this.$getCurrentDate();

    this.$_emitter.off("onSubmittedForm");
  },
  computed: {
    /**
     * Kiểm trả xem chức năng user đang thực hiện là thêm mới hay cập nhật tài sản
     * Author: LDTUAN (03/07/2023)
     */
    formMode() {
      return this.EditAsset?.assetCode
        ? this.$_MISAEnum.FORM_MODE.UPDATE
        : this.$_MISAEnum.FORM_MODE.ADD;
    },
  },
  methods: {
    /**
     * Thực hiện load data của tài sản để cập nhật
     * Author: LDTUAN (03/07/2023)
     */
    loadUpdateAssest() {
      try {
        this.isLoading = true;
        this.$_MSIAApi.Customer.GetById(this.EditAsset.assetCode)
          .then((res) => {
            setTimeout(() => {
            this.asset = res.data;
            this.isLoading = false;
            console.log(res.data);
            }, 1000);
          })
          .catch((res) => {
            this.$processErrorResponse(res);
          });
      } catch (error) {
        this.errors.push(error);
        this.showNotice = true;
      }
    },
    /**
     * Lấy mã tài sản mới đế thêm mới tài sản
     * Author: LDTUAN (03/07/2023)
     */
    // loadAssetCode() {
    //   try {
    //     this.isLoading = true;
    //     this.$_MSIAApi.Customer.NewCustomerCode()
    //       .then((res) => {
    //         this.asset.CustomerCode = res.data;
    //         this.isLoading = false;
    //       })
    //       .catch((res) => {
    //         this.$processErrorResponse(res);
    //       });
    //   } catch (error) {
    //     this.errors.push(error);
    //     this.showNotice = true;
    //   }
    // },
    /**
     * Thực hiện close form thêm tài sản khi click
     * Author: LDTUAN (27/06/2023)
     */
    btnCloseOnClick() {
      this.$_emitter.emit("onCloseEmitter");
    },
    /**
     * Thực hiện lưu lại tài sản vào database
     * Author: LDTUAN (03/07/2023)
     */
    btnSaveOnClick() {
      try {
        this.isSubmittedForm = true;
        // Validate dữ liệu
        this.$onValidateData();
        // Dữ liệu đã hợp lệ thì gọi api thực hiện cất dữ liệu
        if (this.errors.length == 0) {
          if (this.formMode == this.$_MISAEnum.FORM_MODE.UPDATE) {
            alert("edit");
          } else {
            this.$_MSIAApi.Customer.Create(this.asset)
              .then((res) => {
                console.log(res);
              })
              .catch((res) => {
                this.$processErrorResponse(res);
              });
          }
        } else {
          this.showNotice = true;
        }
      } catch (error) {
        this.errors.push(error);
        this.showNotice = true;
      }
    },
  },
};
</script>
