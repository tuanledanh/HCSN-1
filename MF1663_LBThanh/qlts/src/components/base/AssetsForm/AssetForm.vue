<template>
  <div class="asset-form" v-if="isVisible" :class="{ visible: isVisible }">
    <div class="asset-form-container border-radius">
      <div class="asset-form-content">
        <div class="asset-form-top">
          <div class="asset-form-name">Thêm tài sản</div>
          <div class="close-icon-container">
            <div class="close-icon" @click="onCloseClick"></div>
          </div>
        </div>
        <div class="asset-form-row">
          <div class="asset-row-left">
            <div class="input-lable">
              Mã tài sản <span class="required-star">*</span>
            </div>
            <MisaInput
              type="text"
              placeholder=""
              v-model="assetCode"
            ></MisaInput>
          </div>
          <div class="asset-row-right">
            <div class="input-lable">
              Tên tài sản <span class="required-star">*</span>
            </div>
            <MisaInput type="text" placeholder="Nhập tên tài sản"></MisaInput>
          </div>
        </div>
        <div class="asset-form-row">
          <div class="asset-row-left">
            <div class="input-lable">
              Mã bộ phận sử dụng <span class="required-star">*</span>
            </div>
            <div class="asset-form-combobox-container">
              <MisaInput
                type="text"
                placeholder="Chọn mã bộ phận sử dụng"
                id="asset-form-combobox"
              ></MisaInput>
              <div class="combobox-chevd-icon-container">
                <div class="chevd-icon"></div>
              </div>
            </div>
          </div>
          <div class="asset-row-right">
            <div class="input-lable">
              Tên bộ phận sử dụng <span class="required-star">*</span>
            </div>
            <MisaInput type="text" disabled></MisaInput>
          </div>
        </div>
        <div class="asset-form-row">
          <div class="asset-row-left">
            <div class="input-lable">
              Mã loại tài sản <span class="required-star">*</span>
            </div>
            <div class="asset-form-combobox-container">
              <MisaInput
                type="text"
                placeholder="Chọn mã loại tài sản"
                id="asset-form-combobox"
              ></MisaInput>
              <div class="combobox-chevd-icon-container">
                <div class="chevd-icon"></div>
              </div>
            </div>
          </div>

          <div class="asset-row-right">
            <div class="input-lable">
              Tên loại tài sản <span class="required-star">*</span>
            </div>
            <MisaInput type="text" disabled></MisaInput>
          </div>
        </div>
        <div class="asset-form-row">
          <div class="asset-row-left">
            <div class="input-lable">
              Số lượng <span class="required-star">*</span>
            </div>

            <div class="asset-form-number-input">
              <MisaInput
                type="text"
                placeholder="01"
                id="asset-form-combobox"
              ></MisaInput>
              <div class="quantity-icon-container">
                <div class="year-option-icon"></div>
              </div>
            </div>
          </div>
          <div class="asset-row-left">
            <div class="input-lable">
              Nguyên giá <span class="required-star">*</span>
            </div>
            <div class="asset-form-number-input">
              <MisaInput
                type="text"
                placeholder="0"
                id="asset-form-combobox"
              ></MisaInput>
            </div>
          </div>
          <div class="asset-row-left">
            <div class="input-lable">
              Số năm sử dụng <span class="required-star">*</span>
            </div>
            <div class="asset-form-number-input">
              <MisaInput
                type="text"
                placeholder="0"
                id="asset-form-combobox"
              ></MisaInput>
            </div>
          </div>
        </div>
        <div class="asset-form-row">
          <div class="asset-row-left">
            <div class="input-lable">
              Tỷ lệ hao mòn (%) <span class="required-star">*</span>
            </div>
            <div class="asset-form-number-input">
              <MisaInput
                type="text"
                placeholder="0"
                id="asset-form-combobox"
              ></MisaInput>
              <div class="quantity-icon-container">
                <div class="year-option-icon"></div>
              </div>
            </div>
          </div>
          <div class="asset-row-left">
            <div class="input-lable">
              Giá trị hao mòn năm <span class="required-star">*</span>
            </div>
            <div class="asset-form-number-input">
              <MisaInput
                type="text"
                placeholder="0"
                id="asset-form-combobox"
              ></MisaInput>
            </div>
          </div>
          <div class="asset-row-left">
            <div class="input-lable">Năm theo dõi</div>
            <div class="asset-form-number-input">
              <MisaInput
                type="text"
                placeholder="2023"
                id="asset-form-combobox"
              ></MisaInput>
            </div>
          </div>
        </div>
        <div class="asset-form-row">
          <div class="asset-row-left">
            <div class="input-lable">
              Ngày mua <span class="required-star">*</span>
            </div>
            <div class="asset-form-year-input">
              <MisaInput
                type="text"
                placeholder=""
                id="asset-form-combobox"
              ></MisaInput>
              <div class="quantity-icon-container">
                <div class="calendar-icon"></div>
              </div>
            </div>
          </div>
          <div class="asset-row-left">
            <div class="input-lable">
              Ngày bắt đầu sử dụng <span class="required-star">*</span>
            </div>
            <div class="asset-form-year-input">
              <MisaInput
                type="text"
                placeholder=""
                id="asset-form-combobox"
              ></MisaInput>
              <div class="quantity-icon-container">
                <div class="calendar-icon"></div>
              </div>
            </div>
          </div>
        </div>
      </div>
      <div class="asset-form-button">
        <button class="btn" @click="onCloseClick">Hủy</button>
        <button class="btn btn-primary" @click="submitForm">Lưu</button>
      </div>
    </div>
    <MisaDialog v-if="isShowDialog" :content="dialogContent"></MisaDialog>
  </div>
</template>

<script>
import MisaDialog from "../MisaDialog/MisaDialog.vue";
import MisaInput from "../MisaInput/MisaInput.vue";

export default {
  name: "AssetForm",
  components: {
    MisaInput,
    MisaDialog,
  },
  data() {
    return {
      isVisible: true, // Trạng thái hiển thị của form, mặc định là true
      assetCode: "", // Data property to store the input value
      isAssetCodeValid: true, // Flag to track the validity of the input
      isShowDialog: false,
      dialogContent: "",
    };
  },
  watch() {},
  methods: {
    /**
     * Hàm được gọi khi click vào biểu tượng đóng form
     * Khi được gọi, nó sẽ đặt isVisible về false để ẩn form
     * và sử dụng $emit để gửi sự kiện "close" để thông báo cho component cha ẩn form
     * Author: L.B.Thành (28/6/2023)
     */
    onCloseClick() {
      this.isVisible = false; // Đặt trạng thái hiển thị của form về false khi click vào biểu tượng X
      this.$emit("close"); // Gửi sự kiện "close" để thông báo cho component cha ẩn form
    },
    // onValidateData() {
      
    // },
    // submitForm() {
    //   // Validate dữ liệu
    //   try {

    //   } catch (err) {
    //     alert('Lỗi');
    //   }
    // },
  },
};
</script>
