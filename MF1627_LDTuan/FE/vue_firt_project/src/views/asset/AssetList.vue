<template>
  <div class="main-content">
    <div class="content-top">
      <div class="content-top--left">
        <div class="input">
          <div class="icon--input">
            <div class="icon--search"></div>
          </div>
          <input
            class="input__font--italic"
            type="text"
            placeholder="Tìm kiếm tài sản"
          />
        </div>
        <div class="input input--option">
          <div class="icon--input">
            <div class="icon--filter"></div>
          </div>
          <input type="text" value="Loại tài sản" />
          <div class="icon--input icon--input-right">
            <div class="icon--drop-down"></div>
          </div>
        </div>
        <div class="input input--option">
          <div class="icon--input">
            <div class="icon--filter"></div>
          </div>
          <input type="text" value="Bộ phận sử dụng" />
          <div class="icon--input icon--input-right">
            <div class="icon--drop-down"></div>
          </div>
        </div>
      </div>
      <div class="content-top--right">
        <div class="button-combo" @click="btnAddOnClick">
          <div class="icon--button">
            <div class="icon--add"></div>
          </div>
          <button class="button button--main">Thêm tài sản</button>
        </div>
        <button
          class="button button--sub button--sub-non-border icon-content-top"
          @click="reloadData()"
        >
          <div class="icon--export"></div>
        </button>
        <button
          class="button button--sub button--sub-non-border icon-content-top"
          @click="btnDeleteOnClick()"
        >
          <div class="icon--delete"></div>
        </button>
      </div>
    </div>
    <div class="content-body">
      <div class="table-container">
        <table class="table" id="tbAsset">
          <thead>
            <tr>
              <th class="table__head table__head--padding">
                <input
                  type="checkbox"
                  @click="headCheckboxClick($event)"
                  :checked="
                    selectedRows.length === assets.length && assets.length !== 0
                  "
                />
              </th>
              <th class="table__head">STT</th>
              <th class="table__head">Mã tài sản</th>
              <th class="table__head">Tên tài sản</th>
              <th class="table__head">Loại tài sản</th>
              <th class="table__head">Bộ phận sử dụng</th>
              <th class="table__head table__head--right">Số lượng</th>
              <th class="table__head table__head--right">Nguyên giá</th>
              <th class="table__head table__head--right">HM/KH lũy kế</th>
              <th class="table__head table__head--right">Giá trị còn lại</th>
              <th class="table__head table__head--center">Chức năng</th>
            </tr>
          </thead>
          <tbody id="tbodyAsset">
            <tr
              class="tr--body"
              v-for="asset in assets"
              :key="asset.assetId"
              @click="rowOnClick(asset)"
              :class="{ 'tr--body-selected': selectedRows.includes(asset) }"
            >
              <td class="table__body">
                <input
                  type="checkbox"
                  :value="asset"
                  :checked="selectedRows.includes(asset)"
                />
              </td>
              <td class="table__body">{{ assets.indexOf(asset) }}</td>
              <td class="table__body">{{ asset.assetCode }}</td>
              <td class="table__body">{{ asset.assetName }}</td>
              <td class="table__body">{{ asset.assetTypeName }}</td>
              <td class="table__body">{{ asset.departmentName }}</td>
              <td class="table__body table__body--right">
                {{ asset.assetQuantity }}
              </td>
              <td class="table__body table__body--right">
                {{ asset.assetPrice }}
              </td>
              <td class="table__body table__body--right">
                {{ asset.assetDepreciation }}
              </td>
              <td class="table__body table__body--right">
                {{ asset.DateOfBirth }}
              </td>
              <td class="table__body table__body--center">
                <div class="icon-function">
                  <button class="button--empty" @click="btnEditOnCLick(asset)">
                    <div class="icon--edit"></div>
                  </button>
                  <div class="icon--copy"></div>
                </div>
              </td>
            </tr>
          </tbody>
        </table>
      </div>
      
      <div class="table__paging">
        <div class="paging">
          <div class="paging--left">
            <span>Tổng số: <strong>200</strong> bản ghi</span>
            <div class="input input--option">
              <input type="text" value="20" />
              <div class="icon--input icon--input-right">
                <div class="icon--drop-down"></div>
              </div>
            </div>
            <div class="page--number">
              <div class="icon--prev"></div>
              <span>1</span>
              <span>2</span>
              <span>...</span>
              <span>10</span>
              <div class="icon--next"></div>
            </div>
          </div>
          <div class="paging--right"></div>
        </div>
      </div>
    </div>
  </div>
  <AssetForm v-if="showAssetForm" :EditAsset="editAsset"></AssetForm>
  <MISALoading v-if="isLoading"></MISALoading>
  <MISADialogNotice
    v-show="showNotice"
    :errors="errors"
    @onCloseDialog="showNotice = false"
  ></MISADialogNotice>
</template>
<script>
import AssetForm from "./AssetForm.vue";
export default {
  name: "AssetList",
  components: {
    AssetForm,
  },
  created() {
    // Tải dữ liệu từ api về và hiển thị
    this.loadData();
    // Tạo 1 emitter mới, khi sự kiện "onCloseEmitter" được triggered từ bất kỳ nơi nào trong mã,
    // hàm callback sẽ được gọi để thực hiện đóng form asset
    this.$_emitter.on("onCloseEmitter", () => {
      this.showAssetForm = false;
    });
  },
  unmounted() {
    // Tắt emitter để tránh việc xử lý sự kiện không cần thiết hoặc gây ra lỗi trong tương lai
    this.$_emitter.off("onCloseEmitter");
  },
  data() {
    return {
      showAssetForm: false,
      editAsset: {},
      assets: [],
      selectedRows: [],
      isLoading: false,
      errors: [],
      showNotice: false,
    };
  },
  methods: {
    /**
     * Thực hiện tải data từ api
     * Author: LDTUAN (27/06/2023)
     */
    loadData() {
      try {
        this.isLoading = true;
        this.$_MSIAApi.Customer.GetAll()
          .then((res) => {
            this.assets = res.data;
            this.selectedRows = [];
            console.log(this.assets.length + "/////" + this.selectedRows.length);
            this.isLoading = false;
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
     * Phương thức thực hiện reload data
     * Author: LDTUAN (27/06/2023)
     */
    reloadData() {
      this.loadData();
    },
    /**
     *
     * @param {checkbox} event đây là biến dùng để truy cập vào đối tượng sự kiện, ở đây là checkox
     */
    headCheckboxClick(event) {
      if (event.target.checked) {
        for (const asset of this.assets) {
          this.selectedRows.push(asset);
        }
      } else {
        for (const asset of this.assets) {
          this.selectedRows.splice(asset);
        }
      }
    },
    /**
     * Hiển thị form thêm tài sản khi click
     * Author: LDTUAN (27/06/2023)
     */
    btnAddOnClick() {
      this.showAssetForm = true;
      this.editAsset = "";
    },
    /**
     * Hiện thị form thêm tài sản để thực hiện edit khi click
     * Author: LDTUAN (27/06/2023)
     */
    btnEditOnCLick(asset) {
      this.editAsset = asset;
      this.showAssetForm = true;
    },
    /**
     * Thêm - bỏ những row khỏi selectedRows array khi click vào row
     * Author: LDTUAN (27/06/2023)
     */
    rowOnClick(asset) {
      const index = this.selectedRows.indexOf(asset);
      if (index !== -1) {
        this.selectedRows.splice(index, 1);
      } else {
        this.selectedRows.push(asset);
      }
    },
    /**
     * Thực hiện splice những hàng đã được chọn
     * Author: LDTUAN (25/06/2023)
     */
    btnDeleteOnClick() {
      this.assets = this.assets.filter(
        // Loại bỏ các asset có tồn tại trong selectedRows
        (asset) => !this.selectedRows.includes(asset)
      );
      this.selectedRows = [];
    },
  },
};
</script>
