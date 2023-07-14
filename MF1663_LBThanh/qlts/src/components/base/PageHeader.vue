<template>
  <div>
    <div class="page-header">
      <div class="page-header-left">
        <div class="header-left-search border-radius">
          <div class="search-icon-container">
            <div class="search-icon"></div>
          </div>
          <MisaInput type="text" placeholder="Tìm kiếm tài sản" />
        </div>
        <div class="header-left-combo-box border-radius">
          <div class="filter-icon-container">
            <div class="filter-icon"></div>
          </div>
          <MisaCombobox
            placeholder="Loại tài sản"
            propText="assetTypeName"
          ></MisaCombobox>
        </div>
        <div class="header-left-combo-box border-radius">
          <div class="filter-icon-container">
            <div class="filter-icon"></div>
          </div>
          <MisaCombobox
            placeholder="Bộ phận sử dụng"
            propText="departmentName"
          ></MisaCombobox>
        </div>
      </div>
      <div class="page-header-right">
        <div class="btn header-right-button" @click="toggleAssetForm">
          <div class="plus-icon-container">
            <div class="plus-icon"></div>
          </div>
          Thêm tài sản
        </div>
        <div class="header-right-icon-container">
          <div class="cross-icon"></div>
        </div>
        <div class="header-right-icon-container">
          <div class="trash-icon"></div>
        </div>
      </div>
    </div>
    <AssetForm v-if="showAssetForm" @close="hideAssetForm"></AssetForm>
  </div>
</template>

<script>
import AssetForm from "./AssetsForm/AssetForm.vue";
import MisaInput from "./MisaInput/MisaInput.vue";
// import { getCustomer } from "@/assets/js/api";

export default {
  name: "PageContent",
  components: {
    AssetForm,
    MisaInput,
  },
  data() {
    return {
      showAssetForm: false,
    };
  },
  methods: {
    /**
     * Hàm được sử dụng để toggle hiển thị AssetForm
     * Khi được gọi, nó sẽ đảo ngược trạng thái hiển thị của AssetForm
     * Author: L.B.Thành (28/6/2023)
     */
    toggleAssetForm() {
      this.showAssetForm = !this.showAssetForm; // Toggle the asset form visibility
    },
    /**
     * Hàm được gọi để ẩn AssetForm
     * Khi được gọi, nó sẽ đặt showAssetForm về false để ẩn AssetForm
     * Author: L.B.Thành (28/6/2023)
     */
    hideAssetForm() {
      this.showAssetForm = false; // Hide the asset form
    },
  },
};
</script>
