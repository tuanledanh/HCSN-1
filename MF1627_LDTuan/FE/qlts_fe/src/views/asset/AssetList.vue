<template>
  <div
    v-if="this.assets"
    class="main-content"
    :class="[{ 'main-content--expand': isChangeWidth }]"
  >
    <div class="content-top">
      <div class="content-top--left">
        <MISAInput
          search
          placeholder="Tìm kiếm theo mã"
          v-model="inputSearch"
          @searchByCode="searchByCode"
          maxlength="50"
        ></MISAInput>
        <MISACombobox
          :api="this.$_MISAApi.FixedAssetCategory.Api"
          propText="FixedAssetCategoryName"
          propValue="FixedAssetCategoryId"
          placeholder="Loại tài sản"
          @filter="getAssetTypeFilter"
          :inputChange="inputAssetCategoryNameChange"
        ></MISACombobox>
        <MISACombobox
          :api="this.$_MISAApi.Department.Api"
          propText="DepartmentName"
          propValue="DepartmentId"
          placeholder="Bộ phận sử dụng"
          @filter="getDepartmentFilter"
          :inputChange="inputDepartmentNameChange"
        ></MISACombobox>
      </div>
      <div class="content-top--right">
        <MISAButton
          combo
          buttonMain
          textButton="Thêm tài sản"
          @click="btnAddAsset"
        ></MISAButton>
        <MISAButton
          buttonIcon
          exportIcon
          content="Xuất excel"
          position="bot"
          @click="btnShowToastExport"
        ></MISAButton>
        <MISAButton
          buttonIcon
          deleteIcon
          content="Xóa"
          position="left-10"
          @click="btnShowToastDelete"
          :disabled="selectedRows.length == 0"
        ></MISAButton>
      </div>
    </div>
    <div class="content-body">
      <div
        class="table-container"
        :class="{ 'table-container--noData': this.assets.length == 0 }"
      >
        <table class="table relative" id="tbAsset">
          <thead>
            <tr>
              <th class="table__head table__head--padding width-16px">
                <input
                  type="checkbox"
                  @click="headCheckboxClick($event)"
                  :checked="
                    selectedRows.length === assets.length &&
                    assets.length > 0 &&
                    selectedRows.length > 0
                  "
                />
              </th>
              <th class="table__head table__head--center width-64px">
                <section class="relative" title="Số thứ tự">STT</section>
              </th>
              <th class="table__head width-170px">Mã tài sản</th>
              <th class="table__head width-220px">Tên tài sản</th>
              <th class="table__head width-220px">Loại tài sản</th>
              <th class="table__head width-220px">Bộ phận sử dụng</th>
              <th class="table__head table__head--right width-120px">
                Số lượng
              </th>
              <th class="table__head table__head--right width-120px">
                Nguyên giá
              </th>
              <th class="table__head table__head--right width-120px">
                <section class="relative" title="Hao mòn/khấu hao lỹ kế">
                  HM/KH lũy kế
                </section>
              </th>
              <th class="table__head table__head--right width-120px">
                Giá trị còn lại
              </th>
              <th class="table__head table__head--center width-100px">
                Chức năng
              </th>
            </tr>
          </thead>
          <div v-if="assets.length == 0" class="noData absolute">
            <h1>Không tìm thấy dữ liệu</h1>
          </div>
          <tbody v-else id="tbodyAsset">
            <tr
              class="tr--body"
              v-for="asset in assets"
              :key="asset.FixedAssetId"
              @click="rowOnClick(asset)"
              @dblclick="btnEditAsset(asset)"
              :class="{ 'tr--body-selected': selectedRows.includes(asset) }"
            >
              <td class="table__body">
                <input
                  type="checkbox"
                  :value="asset"
                  :checked="selectedRows.includes(asset)"
                />
              </td>
              <td class="table__body table__body--center">
                {{ assets.indexOf(asset) + 1 }}
              </td>
              <td class="table__body">{{ asset.FixedAssetCode }}</td>
              <td class="table__body">
                <section
                  :title="
                    asset.FixedAssetName.length > 20 ? asset.FixedAssetName : ''
                  "
                >
                  {{ truncateText(asset.FixedAssetName, 20) }}
                </section>
              </td>
              <td class="table__body">
                <section
                  :title="
                    asset.FixedAssetCategoryName.length > 20
                      ? asset.FixedAssetCategoryName
                      : ''
                  "
                >
                  {{ truncateText(asset.FixedAssetCategoryName, 20) }}
                </section>
              </td>
              <td class="table__body">
                <section
                  :title="
                    asset.DepartmentName.length > 20 ? asset.DepartmentName : ''
                  "
                >
                  {{ truncateText(asset.DepartmentName, 20) }}
                </section>
              </td>
              <td class="table__body table__body--right">
                {{ formatMoney(asset.Quantity) }}
              </td>
              <td class="table__body table__body--right">
                {{ formatMoney(asset.Cost) }}
              </td>
              <td class="table__body table__body--right">
                {{
                  formatMoney((asset.Cost * (1 / asset.LifeTime)).toFixed(0))
                }}
              </td>
              <td class="table__body table__body--right">
                {{ AssetDepreciation(asset.Cost, asset.LifeTime) }}
              </td>
              <td class="table__body">
                <div class="icon-function">
                  <div @click="btnEditAsset(asset)">
                    <MISAIcon edit></MISAIcon>
                  </div>
                  <div @click="btnCloneAsset(asset)">
                    <MISAIcon copy></MISAIcon>
                  </div>
                </div>
              </td>
            </tr>
          </tbody>
        </table>
      </div>
      <MISAPaging
        :totalRecords="totalRecords"
        :totalPages="totalPages"
        :currentPage="currentPage"
        :pageLimitList="pageLimitList"
        @filter="getPageLimit"
        @paging="getPageNumber"
        :quantity="totalQuantity"
        :price="totalPrice"
        :depreciation="totalDepreciation"
        :residualValue="totalResidualValue"
      ></MISAPaging>
    </div>
  </div>
  <MISALoading v-if="isLoading"></MISALoading>
  <MISAAssetForm
    v-if="onOpenForm"
    :editAsset="editAsset"
    :cloneAsset="cloneAsset"
    @onCloseForm="onCloseForm"
    @reLoad="reLoad"
  ></MISAAssetForm>
  <MISAToast
    v-if="isShowToastSuccess"
    typeToast="success"
    :content="toast_content_success"
  ></MISAToast>
  <div v-if="isShowToastDelete" class="blur">
    <MISAToast typeToast="warning" :content="toast_content_delete"
      ><MISAButton
        buttonSub
        textButton="Không"
        @click="btnCloseToastWarning"
      ></MISAButton>
      <MISAButton
        buttonMain
        textButton="Xóa"
        @click="btnDeleteAssets"
      ></MISAButton>
    </MISAToast>
  </div>
  <div v-if="isShowToastExport" class="blur">
    <MISAToast typeToast="export" :content="toast_content_export"
      ><MISAButton
        buttonSub
        textButton="Không"
        @click="btnCloseToastWarning"
      ></MISAButton>
      <MISAButton
        buttonMain
        textButton="Tạo"
        @click="btnExportData"
      ></MISAButton>
    </MISAToast>
  </div>
  <div v-if="isShowToastValidateBE" class="blur">
    <MISAToast typeToast="warning" :content="toast_content_warning + '.'"
      ><MISAButton
        buttonSub
        textButton="Đóng"
        @click="btnCloseToastWarning"
      ></MISAButton>
    </MISAToast>
  </div>
</template>
<script>
import { formatMoney } from "../../helpers/common/format/format";
import { formatMoneyToInt } from "../../helpers/common/format/format";
import { AssetDepreciation } from "../../helpers/common/format/format";
import fileDownload from "js-file-download";
import { truncateText } from "../../helpers/common/format/format";
import MISAAssetForm from "./AssetForm.vue";
//import { saveAs } from "file-saver";
export default {
  name: "AssetList",
  components: {
    MISAAssetForm,
  },
  props: {
    // Thay đổi độ rộng
    isChangeWidth: {
      type: Boolean,
      default: false,
    },
  },
  data() {
    return {
      // Danh sách tài sản
      assets: [],
      // Danh sách loại tài sản
      assetTypes: [],
      // Danh sách phòng ban
      departments: [],
      // Danh sách các bản ghi đã chọn
      selectedRows: [],
      // Loading
      isLoading: false,

      // =================================Filter=================================
      // Số trang hiện tại
      pageNumber: null,
      // Số lượng bản ghi tối đa mỗi trang
      pageLimit: 20,
      // Mã code để tìm kiếm
      filterName: null,
      // Lọc theo phòng ban
      departmentFilter: null,
      // Lọc theo loại tài sản
      assetTypeFilter: null,
      // Nội dung tìm kiếm
      inputSearch: "",
      // Danh sách option giới hạn bản ghi mỗi trang
      pageLimitList: [],
      // Tổng bản ghi
      totalRecords: 0,
      // Tổng trang
      totalPages: 0,
      // Trang hiện tại
      currentPage: 1,
      // Thay đổi giá trị của loại tài sản khi cập nhật hoặc thêm mới, sẽ lọc theo tên loại tài sản đó
      inputAssetCategoryNameChange: null,
      // Thay đổi giá trị của phòng ban khi cập nhật hoặc thêm mới, sẽ lọc theo tên phòng ban đó
      inputDepartmentNameChange: null,
      // 
      // =================================Toast=================================
      // Hiển thị thông báo khi xóa
      isShowToastDelete: false,
      // Hiển thị thông báo khi xuất excel
      isShowToastExport: false,
      // Nội dung thông báo xóa
      toast_content_delete: null,
      // Nội dung xuất excel
      toast_content_export: null,
      // Hiển thị thông báo liên quan đến lỗi từ BE
      isShowToastValidateBE: false,
      // Nội dung thông báo
      toast_content_warning: null,

      // =================================form=================================
      // Mở form
      onOpenForm: false,
      // Tài sản cần cập nhật
      editAsset: null,
      // Tài sản cần sao chép
      cloneAsset: null,
      // Thông báo thành công khi tạo, cập nhật
      isShowToastSuccess: false,
      // Nội dung thông báo thành công
      toast_content_success: null,
      // Kiểm tra tạo, cập nhật thành công chưa để hiển thị thông báo
      isSuccessAddOrUpdate: false,

      // =================================Paging=================================
      // Tổng số lượng
      totalQuantity: "0",
      // Tổng nguyên giá
      totalPrice: "0",
      // Tổng giá trị hao mòn
      totalDepreciation: "0",
      // Tổng giá trị còn lại
      totalResidualValue: "0",
    };
  },
  watch: {
    /**
     * Thực hiện lọc theo phòng bàn
     * @param {id} value id của phòng ban
     * Author: LDTUAN (02/08/2023)
     */
    departmentFilter(value) {
      this.baseLoadData(
        1,
        this.pageLimit,
        this.filterName,
        value,
        this.assetTypeFilter
      );
    },

    /**
     * Thực hiện lọc theo loại tài sản
     * @param {id} value id của loại tài sản
     * Author: LDTUAN (02/08/2023)
     */
    assetTypeFilter(value) {
      this.baseLoadData(
        1,
        this.pageLimit,
        this.filterName,
        this.departmentFilter,
        value
      );
    },

    /**
     * Gán lại text cho filterName để thực hiện tìm kiếm
     * @param {string} value
     * Author: LDTUAN (02/08/2023)
     */
    inputSearch(value) {
      this.filterName = value;
    },

    /**
     * Phân trang lại với giới hạn trang mới
     * @param {int} value giới hạn bản ghi
     * Author: LDTUAN (02/08/2023)
     */
    pageLimit(value) {
      this.baseLoadData(
        1,
        value,
        this.filterName,
        this.departmentFilter,
        this.assetTypeFilter
      );
    },

    /**
     * Phân trang theo số trang mới
     * @param {int} value số trang
     * Author: LDTUAN (02/08/2023)
     */
    pageNumber(value) {
      this.baseLoadData(
        value,
        this.pageLimit,
        this.filterName,
        this.departmentFilter,
        this.assetTypeFilter
      );
      this.currentPage = value;
    },
  },
  created() {
    // Tải danh sách bản ghi
    this.loadDataAsset();
    // Tải danh sách option giới hạn bản ghi mỗi trang
    this.getPageLimitList();
  },
  methods: {
    truncateText,
    /**
     * Format số sang chuỗi, mỗi 3 số có 1 dấu chấm
     * Author: LDTUAN (02/08/2023)
     */
    formatMoney,

    /**
     * Format chuỗi sang số để lưu lại trong database
     * Author: LDTUAN (02/08/2023)
     */
    formatMoneyToInt,

    /**
     * Tính toán giá trị hao mòn
     * Author: LDTUAN (02/08/2023)
     */
    AssetDepreciation,
    /** --------------------Load data---------------------- */

    // getTotalRecord() {
    //   try {
    //     this.$_MISAApi.FixedAsset.GetCount()
    //       .then((res) => {
    //         this.totalRecords = res.data;
    //       })
    //       .catch((res) => {
    //         this.$processErrorResponse(res);
    //       });
    //   } catch (error) {
    //     console.log(error);
    //   }
    // },

    /**
     * Hàm chung load dữ liệu tương ứng với các tham số truyền vào
     * @param {int} pageNumber số trang
     * @param {int} pageLimit số lượng tối đa bản ghi mỗi trang
     * @param {string} filterName mã code để thực hiện search
     * @param {int} departmentFilter id của phòng ban
     * @param {int} assetTypeFilter id của loại tài sản
     * Author: LDTUAN (02/08/2023)
     */
    baseLoadData(
      pageNumber,
      pageLimit,
      filterName,
      departmentFilter,
      assetTypeFilter
    ) {
      try {
        this.isLoading = true;
        this.$_MISAApi.FixedAsset.Filter(
          pageNumber,
          pageLimit,
          filterName,
          departmentFilter,
          assetTypeFilter
        )
          .then((res) => {
            this.assets = res.data.Data;
            this.selectedRows = [];
            this.isLoading = false;
            this.totalPages = res.data.TotalPages;
            this.totalRecords = res.data.TotalRecords;

            // Array.reduce(callback, initialValue) initialvalue là giá trị ban đầu
            var totalQuantity = this.assets.reduce((total, asset) => {
              return total + asset.Quantity;
            }, 0);
            var totalPrice = this.assets.reduce((total, asset) => {
              return total + asset.Cost;
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

            this.totalQuantity = formatMoney(totalQuantity);
            this.totalPrice = formatMoney(totalPrice);
            this.totalDepreciation = formatMoney(totalDepreciation);
            this.totalResidualValue = formatMoney(totalResidualValue);

            if (this.isSuccessAddOrUpdate) {
              this.toast_content_success = this.$_MISAResource.VN.Form.Success;
              this.isShowToastSuccess = true;
            }
            setTimeout(() => {
              this.isShowToastSuccess = false;
              this.isSuccessAddOrUpdate = false;
            }, 3000);
          })
          .catch((res) => {
            this.$processErrorResponse(res);
            this.isShowToastValidateBE = true;
            this.toast_content_warning = res.response.data.UserMessage;
          });
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Phương thức thực hiện tải lại dữ liệu tương ứng với phòng ban, loại tài sản
     * Author: LDTUAN (02/08/2023)
     */
    reLoad(item) {
      this.isSuccessAddOrUpdate = true;
      this.editAsset = null;
      this.cloneAsset = null;
      if (this.departmentFilter != null) {
        this.departmentFilter = item.Asset.DepartmentId;
        this.inputDepartmentNameChange = item.DepartmentName;
      }
      if (this.assetTypeFilter != null) {
        this.assetTypeFilter = item.Asset.FixedAssetCategoryId;
        this.inputAssetCategoryNameChange = item.FixedAssetCategoryName;
      }
      this.loadDataAsset();
    },

    /**
     * Phương thức thực hiện lấy dữ liệu tài sản để hiển thị lên table
     * Author: LDTUAN (27/06/2023)
     */
    loadDataAsset() {
      this.baseLoadData(
        this.pageNumber,
        this.pageLimit,
        this.filterName,
        this.departmentFilter,
        this.assetTypeFilter
      );
    },

    /**
     * Danh sách số lượng bản ghi mỗi trang
     * Author: LDTUAN (02/08/2023)
     */
    getPageLimitList() {
      this.pageLimitList = [20, 10, 5];
    },
    /** ------------------------Delete data-------------------- */

    /**
     * Xóa 1 hoặc nhiều bản ghi khi click vào button xóa
     * Author: LDTUAN (02/08/2023)
     */
    btnDeleteAssets() {
      this.isLoading = true;
      if (this.selectedRows.length > 0) {
        const listIds = this.selectedRows.map((asset) => asset.FixedAssetId);
        const requestData = listIds;

        try {
          this.$_MISAApi.FixedAsset.DeleteMany(requestData, {
            headers: { "Content-Type": "application/json" },
          })
            .then(() => this.loadDataAsset())
            .then((this.isLoading = false))
            .catch((res) => {
              this.$processErrorResponse(res);
              this.isShowToastValidateBE = true;
              this.toast_content_warning = res.response.data.UserMessage;
            });
        } catch (error) {
          console.log(error);
        }
      }
      this.isShowToastDelete = false;
    },

    /** ------------------------Toast-------------------- */

    /**
     * Show thông báo khi thực hiện xóa
     * Author: LDTUAN (02/08/2023)
     */
    btnShowToastDelete() {
      const selectedRows = this.selectedRows;
      const numberOfRecords = selectedRows.length;
      // Thêm số 0 vào trước số bản ghi, nếu số bản ghi nhỏ hơn 10
      // padStart là thêm 1 ký tự vào trước chuỗi ban đầu, nếu sau khi thêm mà độ dài chuổi bằng độ dài cung cấp
      // Ở đây ví dụ numberOfRecords = 1, thì nếu thêm số 0, độ dài chuỗi = 2, đúng
      // Nhưng nếu numberOfRecords = 10, thì nếu thêm số 0, độ dài chuỗi = 3, sai
      const formattedNumberOfRecords =
        numberOfRecords < 10
          ? numberOfRecords.toString().padStart(2, "0")
          : numberOfRecords;
      if (numberOfRecords == 1) {
        this.toast_content_delete =
          this.$_MISAResource.VN.Form.Warning.Delete.Single +
          selectedRows[0].FixedAssetCode +
          " - " +
          selectedRows[0].FixedAssetName +
          "?";
      } else if (numberOfRecords > 1) {
        this.toast_content_delete =
          formattedNumberOfRecords +
          this.$_MISAResource.VN.Form.Warning.Delete.Multiple;
      } else {
        return;
      }
      this.isShowToastDelete = true;
    },

    /**
     * Show thông báo khi thực hiện xóa
     * Author: LDTUAN (02/08/2023)
     */
    btnShowToastExport() {
      let selectedRows = null;
      if (this.selectedRows.length == 0) {
        selectedRows = this.assets;
      } else {
        selectedRows = this.selectedRows;
      }
      const numberOfRecords = selectedRows.length;
      // Thêm số 0 vào trước số bản ghi, nếu số bản ghi nhỏ hơn 10
      // padStart là thêm 1 ký tự vào trước chuỗi ban đầu, nếu sau khi thêm mà độ dài chuổi bằng độ dài cung cấp
      // Ở đây ví dụ numberOfRecords = 1, thì nếu thêm số 0, độ dài chuỗi = 2, đúng
      // Nhưng nếu numberOfRecords = 10, thì nếu thêm số 0, độ dài chuỗi = 3, sai
      const formattedNumberOfRecords =
        numberOfRecords < 10
          ? numberOfRecords.toString().padStart(2, "0")
          : numberOfRecords;
      if (numberOfRecords == 1) {
        this.toast_content_export =
          this.$_MISAResource.VN.Form.Warning.Export.Single +
          selectedRows[0].FixedAssetCode +
          " - " +
          selectedRows[0].FixedAssetName +
          "?";
      } else if (numberOfRecords > 1) {
        this.toast_content_export =
          formattedNumberOfRecords +
          this.$_MISAResource.VN.Form.Warning.Export.Multiple;
      } else {
        return;
      }
      this.isShowToastExport = true;
    },

    /**
     * Đóng thông báo xóa
     * Author: LDTUAN (02/08/2023)
     */
    btnCloseToastWarning() {
      this.isShowToastDelete = false;
      this.isShowToastExport = false;
      this.isShowToastValidateBE = false;
    },

    /** ------------------------Filter-------------------- */

    /**
     * Thực hiện filter theo loại tài sản
     * @param {object} item loại tài sản
     * Author: LDTUAN (02/08/2023)
     */
    getAssetTypeFilter(item) {
      this.assetTypeFilter = item.FixedAssetCategoryId;
    },

    /**
     * Thực hiện filter theo phòng ban
     * @param {objet} item phòng ban
     * Author: LDTUAN (02/08/2023)
     */
    getDepartmentFilter(item) {
      this.departmentFilter = item.DepartmentId;
    },

    /**
     * Thực hiện tìm kiếm khi nhấn enter
     * @param {int} keyCode mã code của phím
     * Author: LDTUAN (02/08/2023)
     */
    searchByCode(keyCode) {
      if (keyCode == this.$_MISAEnum.KEYCODE.ENTER) {
        this.baseLoadData(
          this.pageNumber,
          this.pageLimit,
          this.filterName,
          this.departmentFilter,
          this.assetTypeFilter
        );
        this.currentPage = 1;
      }
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
     * Thực hiện load lại dữ khi thay đổi số trang
     * @param {int} pageLimit số trang
     * Author: LDTUAN (02/08/2023)
     */
    getPageNumber(pageNumber) {
      this.pageNumber = pageNumber;
    },

    /** ------------------------Tick checkbox-------------------- */

    /**
     * Thực hiện tick/bỏ tick tất cả bản ghi trong danh sách khi tick/bỏ tích checkbox
     * @param {checkbox} event input checkbox
     * Author: LDTUAN (02/08/2023)
     */
    headCheckboxClick(event) {
      if (event.target.checked) {
        for (const asset of this.assets) {
          if (!this.selectedRows.includes(asset)) {
            this.selectedRows.push(asset);
          }
        }
      } else {
        for (const asset of this.assets) {
          this.selectedRows.splice(asset);
        }
      }
    },

    /**
     * Thực hiện bôi xanh 1 dòng nếu click vào dòng đó
     * @param {object} asset thông tin tài sản
     * Author: LDTUAN (02/08/2023)
     */
    rowOnClick(asset) {
      const index = this.selectedRows.indexOf(asset);
      if (index !== -1) {
        this.selectedRows.splice(index, 1);
      } else {
        this.selectedRows.push(asset);
      }
    },

    /** ------------------------Form-------------------- */

    /**
     * Mở form thêm mới tài sản
     * Author: LDTUAN (02/08/2023)
     */
    btnAddAsset() {
      this.onOpenForm = !this.onOpenForm;
      this.editAsset = null;
      this.cloneAsset = null;
    },

    /**
     * Đóng form
     * Author: LDTUAN (02/08/2023)
     */
    onCloseForm() {
      this.onOpenForm = !this.onOpenForm;
    },

    /**
     * Mở form và gửi dữ liệu vào form để cập nhật
     * @param {object} asset tài sản
     * Author: LDTUAN (02/08/2023)
     */
    btnEditAsset(asset) {
      this.onOpenForm = !this.onOpenForm;
      this.editAsset = asset;
      this.cloneAsset = null;
    },

    /**
     * Mở form và gửi dữ liệu vào form để sao chép
     * @param {object} asset tài sản
     * Author: LDTUAN (02/08/2023)
     */
    btnCloneAsset(asset) {
      this.onOpenForm = !this.onOpenForm;
      this.cloneAsset = asset;
      this.editAsset = null;
    },

    /** ------------------------Form-------------------- */
    btnExportData() {
      this.isLoading = true;
      let listIds = null;
      if (this.selectedRows != null && this.selectedRows.length > 0) {
        listIds = this.selectedRows.map((asset) => asset.FixedAssetId);
      } else {
        listIds = this.assets.map((asset) => asset.FixedAssetId);
      }
      let idsQuery = "";
      listIds.forEach((id) => {
        idsQuery = idsQuery + "," + id;
      });
      idsQuery = idsQuery.substring(1);
      this.$_MISAApi.FixedAsset.Export(
        {
          headers: {
            "Content-Type":
              "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", // trả về là một tệp Excel
          },
          responseType: "blob",
        },
        idsQuery
      )
        .then((res) => {
          // console.log(res);
          // saveAs(res.data, "Asset.xlsx", {
          //   type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
          // });
          fileDownload(res.data, "Asset.xlsx");
          this.isShowToastExport = false;
          this.loadDataAsset();
          this.isLoading = false;
        })
        .catch((res) => {
          this.isLoading = false;
          this.$processErrorResponse(res);
          this.isShowToastValidateBE = true;
          this.toast_content_warning = res.response.data.UserMessage;
        });
    },
  },
};
</script>
