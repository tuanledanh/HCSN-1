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
        <MISAButton
          combo
          large
          loading
          textButton="Cập nhật dữ liệu"
          @click="loadDataAsset"
        ></MISAButton>
      </div>
      <div class="content-top--right">
        <MISAButton
          combo
          buttonMain
          add
          textButton="Thêm tài sản"
          @click="btnAddAsset"
        ></MISAButton>
        <MISAButton
          buttonIcon
          exportIcon
          bottom
          content="Xuất excel"
          @click="btnShowToastExport"
        ></MISAButton>
        <MISAButton
          buttonIcon
          deleteIcon
          content="Xóa"
          bottom_end
          @click="btnShowToastDelete"
        ></MISAButton>
      </div>
    </div>
    <div class="content-body">
      <div
        class="table-container"
        :class="{ 'table-container--noData': this.assets.length == 0 }"
      >
        <table class="table" id="tbAsset">
          <thead>
            <tr>
              <th class="table__head table__head--padding width-16px">
                <input
                  type="checkbox"
                  @click="headCheckboxClick($event)"
                  :checked="
                    assets.every((assetItem) =>
                      selectedRows.some(
                        (selectedItem) =>
                          selectedItem.FixedAssetId === assetItem.FixedAssetId
                      )
                    ) &&
                    assets.length > 0 &&
                    selectedRows.length > 0
                  "
                />
              </th>
              <th class="table__head table__head--center width-64px">
                <MISATooltip top content="Số thứ tự">
                  <span>STT</span></MISATooltip
                >
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
                <MISATooltip top content="Hao mòn/khấu hao lũy kế">
                  <span>HM/KH lũy kế</span></MISATooltip
                >
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
              @click.exact.stop="callRowOnClick(asset)"
              @click.ctrl.stop="rowOnCtrlClick(asset)"
              @click.shift.stop="rowOnShiftClick(asset)"
              @dblclick="btnEditAsset(asset)"
              @contextmenu.prevent="btnClickRight($event, asset)"
              :class="{
                'tr--body-selected': selectedRows.some(
                  (item) => item.FixedAssetId === asset.FixedAssetId
                ),
              }"
            >
              <td class="table__body">
                <input
                  type="checkbox"
                  :value="asset"
                  :checked="
                    selectedRows.some(
                      (item) => item.FixedAssetId === asset.FixedAssetId
                    )
                  "
                />
              </td>
              <td class="table__body table__body--center">
                {{ assets.indexOf(asset) + 1 + pageLimit * (currentPage - 1) }}
              </td>
              <td class="table__body">{{ asset.FixedAssetCode }}</td>
              <td class="table__body">
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
                <span v-else>{{ truncateText(asset.FixedAssetName, 20) }}</span>
              </td>
              <td class="table__body">
                <el-tooltip
                  v-if="asset.FixedAssetCategoryName.length > 20"
                  :visible="visible"
                  placement="right"
                >
                  <template #content>
                    <span>{{ asset.FixedAssetCategoryName }}</span>
                  </template>
                  <span>{{
                    truncateText(asset.FixedAssetCategoryName, 20)
                  }}</span>
                </el-tooltip>
                <span v-else>{{
                  truncateText(asset.FixedAssetCategoryName, 20)
                }}</span>
              </td>
              <td class="table__body">
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
                  <MISATooltip bottom content="Chỉnh sửa">
                    <MISAIcon edit @click="btnEditAsset(asset)"></MISAIcon>
                  </MISATooltip>
                  <MISATooltip bottom content="Sao chép">
                    <MISAIcon copy @click="btnCloneAsset(asset)"></MISAIcon>
                  </MISATooltip>
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
  <MISAContextMenu
    :listItem="this.$_MISAResource.VN.ContextMenu"
    :isShowContextMenu="isShowContextMenu"
    :posX="mouseX"
    :posY="mouseY"
    @click.stop="hideContextMenu"
    @getItemContext="getItemContext"
    v-click-outside="() => (isShowContextMenu = false)"
  ></MISAContextMenu>
  <MISAToast
    v-if="isShowToastSuccess"
    typeToast="success"
    :content="toast_content_success"
  ></MISAToast>
  <div v-if="isShowToastDelete" class="blur">
    <MISAToast
      v-if="selectedRows.length > 0"
      typeToast="warning"
      :content="toast_content_delete"
      ><MISAButton
        buttonOutline
        textButton="Không"
        @click="btnCloseToastWarning"
        :tabindex="2"
        @keydown="checkTabIndex($event, 'islast')"
      ></MISAButton>
      <MISAButton
        buttonMain
        textButton="Xóa"
        @click="btnDeleteAssets"
        focus
        ref="button"
        :tabindex="1"
      ></MISAButton>
    </MISAToast>
    <MISAToast v-else typeToast="warning" :content="toast_content_delete"
      ><MISAButton
        buttonMain
        textButton="Đóng"
        @click="btnCloseToastWarning"
        focus
        ref="button"
        :tabindex="1"
        @keydown="checkTabIndex($event, 'islast')"
      ></MISAButton>
    </MISAToast>
  </div>
  <div v-if="isShowToastExport" class="blur">
    <MISAToast typeToast="export" :content="toast_content_export"
      ><MISAButton
        buttonOutline
        textButton="Không"
        @click="btnCloseToastWarning"
        :tabindex="2"
        @keydown="checkTabIndex($event, 'islast')"
      ></MISAButton>
      <MISAButton
        buttonMain
        textButton="Tạo"
        @click="btnExportData"
        focus
        ref="button"
        :tabindex="1"
      ></MISAButton>
    </MISAToast>
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
  <div v-if="isShowToastValidateAriseTransfer" class="blur">
    <MISAToast
      typeToast="warning"
      :content="toast_content_warning + '.'"
      :moreInfo="moreInfo"
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
</template>
<script>
import { formatMoney } from "../../helpers/common/format/format";
import { formatMoneyToInt } from "../../helpers/common/format/format";
import { AssetDepreciation } from "../../helpers/common/format/format";
import fileDownload from "js-file-download";
import { truncateText } from "../../helpers/common/format/format";
import MISAAssetForm from "./AssetForm.vue";

//import { rowOnClick } from "../../helpers/table/selectRow";

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
      // Danh sách toàn bộ tài sản, chỉ có filter theo mã code (nếu tìm kiếm), mã phòng ban và mã loại tài sản
      assetsTotal: [],
      // Danh sách loại tài sản
      assetTypes: [],
      // Danh sách phòng ban
      departments: [],
      // Danh sách các bản ghi đã chọn
      selectedRows: [],
      // Loading
      isLoading: false,
      // Index của bản ghi đầu tiên trong danh sách
      lastIndex: 0,
      // Focus vào ô nhập liệu, ban đầu là mã tài sản
      buttonFocus: "",

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
      isShowToastValidateAriseTransfer: false,
      moreInfo: null,

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

      // =================================Context menu=================================
      // Hiển thị context menu
      isShowContextMenu: false,
      // Tọa độ x của menu
      mouseX: 0,
      // Tọa độ y của menu
      mouseY: 0,
      //
      rowIndex: -1,
      // Thông tin của bản ghi
      assetContext: null,
    };
  },
  mounted() {
    //document.addEventListener("selectstart", this.handleKeyShift);
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
    currentPage(value) {
      this.pageNumber = value;
      this.currentPage = value;
    },
  },
  created() {
    // Tải danh sách bản ghi
    this.loadDataAsset();
    // Tải danh sách option giới hạn bản ghi mỗi trang
    this.getPageLimitList();
    // Hiển thị chữ danh sách tài sản
    this.$_emitter.emit("onDisplayContent", { message: "Danh sách tài sản" });
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

    async getTotalRecordBaseFilter() {
      await this.$_MISAApi.FixedAsset.Filter(
        1,
        this.$_MISAEnum.INT.MAX_VALUE,
        this.filterName,
        this.departmentFilter,
        this.assetTypeFilter
      )
        .then((res) => {
          this.assetsTotal = res.data.Data;
        })
        .catch((res) => {
          this.$processErrorResponse(res);
          this.isShowToastValidateBE = true;
          this.toast_content_warning = res.response.data.UserMessage;
        });
    },

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
      this.currentPage = 1;
      this.pageNumber = 1;
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
        // Lấy danh sách các cặp [FixedAssetId, modifiedDate] từ selectedRows
        const idDatePairs = this.selectedRows.map((asset) => [
          asset.FixedAssetId,
          asset.ModifiedDate,
        ]);
        // Sắp xếp theo trường "modified date"
        idDatePairs.sort((a, b) => a[1] - b[1]);
        // Lấy danh sách chỉ chứa FixedAssetId đã sắp xếp theo modified date
        const listIds = idDatePairs.map((pair) => pair[0]);
        const requestData = listIds;
        this.$_MISAApi.FixedAsset.CheckTransfer(
          listIds,
          this.$_MISAEnum.ACTION.DELETE
        )
          .then(() => {
            this.$_MISAApi.FixedAsset.DeleteMany(requestData, {
              headers: { "Content-Type": "application/json" },
            })
              .then(() => {
                this.loadDataAsset();
                this.toast_content_success =
                  this.$_MISAResource.VN.Form.DeleteSuccess;
                this.isShowToastSuccess = true;
                setTimeout(() => {
                  this.isShowToastSuccess = false;
                }, 3000);
              })
              .then((this.isLoading = false))
              .catch((res) => {
                this.$processErrorResponse(res);
                this.isShowToastValidateBE = true;
                this.toast_content_warning = res.response.data.UserMessage;
              });
          })
          .catch((res) => {
            this.$processErrorResponse(res);
            this.isShowToastValidateAriseTransfer = true;
            this.toast_content_warning = res.response.data.UserMessage;
            this.moreInfo = res.response.data.MoreInfo;
            this.isLoading = false;
          });
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
          `<strong>${
            selectedRows[0].FixedAssetCode +
            " - " +
            selectedRows[0].FixedAssetName
          }</strong>` +
          "?";
      } else if (numberOfRecords > 1) {
        this.toast_content_delete =
          `<strong>${formattedNumberOfRecords}</strong>` +
          this.$_MISAResource.VN.Form.Warning.Delete.Multiple;
      } else {
        this.toast_content_delete =
          this.$_MISAResource.VN.Form.Warning.Delete.Zero;
      }
      this.isShowToastDelete = true;
    },

    /**
     * Show thông báo khi thực hiện xóa
     * Author: LDTUAN (02/08/2023)
     */
    async btnShowToastExport() {
      let selectedRows = null;
      if (this.selectedRows.length == 0) {
        await this.getTotalRecordBaseFilter();
        selectedRows = this.assetsTotal;
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
      this.isShowToastValidateAriseTransfer = false;
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

    /** ------------------------Tick checkbox / short cut-------------------- */
    /**
     * Ngăn event liên quan các phím tắt mặc định
     * Author: LDTUAN (09/08/2023)
     */
    // handleKeyShift() {
    //   document.addEventListener("selectstart", function(event){
    //     event.preventDefault();
    //   });
    // },
    /**
     * Thực hiện tick/bỏ tick tất cả bản ghi trong danh sách khi tick/bỏ tích checkbox
     * @param {checkbox} event input checkbox
     * Author: LDTUAN (02/08/2023)
     */
    headCheckboxClick(event) {
      this.selectedRows = [];
      if (event.target.checked) {
        for (const asset of this.assets) {
          const index = this.selectedRows.findIndex(
            (selectedItem) => selectedItem.FixedAssetId === asset.FixedAssetId
          );
          if (index === -1) {
            this.selectedRows.push(asset);
          }
        }
      } else {
        for (const asset of this.assets) {
          const index = this.selectedRows.findIndex(
            (selectedItem) => selectedItem.FixedAssetId === asset.FixedAssetId
          );
          if (index !== -1) {
            this.selectedRows.splice(index, 1);
          }
        }
      }
    },

    /**
     * Thực hiện call hàm rowOnClick từ file js để bôi xanh 1 dòng nếu click vào dòng đó
     * @param {object} asset thông tin tài sản
     * Author: LDTUAN (02/08/2023)
     */
    callRowOnClick(asset) {
      const index = this.selectedRows.findIndex(
        (selectedItem) => selectedItem.FixedAssetId === asset.FixedAssetId
      );
      if (index !== -1) {
        this.selectedRows = [];
      } else {
        this.selectedRows = [];
        this.selectedRows.push(asset);
      }
      if (this.selectedRows.length == 0) {
        this.lastIndex = 0;
      }
    },

    /**
     * Chọn bản ghi bằng cách nhấn ctrl + click
     * @param {object} asset bản ghi
     * Author: LDTUAN (09/08/2023)
     */
    rowOnCtrlClick(asset) {
      const index = this.selectedRows.indexOf(asset);
      if (index !== -1) {
        this.selectedRows.splice(index, 1);
      } else {
        this.selectedRows.push(asset);
      }
      if (this.selectedRows.length == 0) {
        this.lastIndex = 0;
      } else {
        this.lastIndex = this.assets.indexOf(asset);
      }
    },

    /**
     * Chọn bản ghi bằng cách nhấn shift + click
     * @param {object} asset bản ghi
     * Author: LDTUAN (09/08/2023)
     */
    rowOnShiftClick(asset) {
      if (event.shiftKey) {
        const index = this.assets.indexOf(asset);
        let list = [];
        let newestIndex = index + 1;
        if (newestIndex <= this.lastIndex) {
          list = this.assets.slice(newestIndex - 1, this.lastIndex + 1);
        } else {
          list = this.assets.slice(this.lastIndex, newestIndex);
        }
        this.selectedRows = list;
      }
    },

    /**
     * Chuột phải để mở context mennu
     * @param {*} event
     * @param {object} asset bản ghi
     * Author: LDTUAN (09/08/2023)
     */
    btnClickRight(event, asset) {
      //event.preventDefault();
      this.isShowContextMenu = true;
      this.mouseX = event.clientX;
      this.mouseY = event.clientY;
      this.rowIndex = this.assets.indexOf(asset);
      this.assetContext = asset;
      this.selectedRows = [];
      this.selectedRows.push(this.assetContext);
    },

    /**
     * Thực hiện hành động tương ứng với option đã chọn từ context menu
     * @param {int} index số index nhận từ option của context menu
     * Author: LDTUAN (09/08/2023)
     */
    getItemContext(index) {
      switch (index) {
        case 1:
          this.btnAddAsset();
          break;
        case 2:
          this.btnEditAsset(this.assetContext);
          break;
        case 3:
          this.btnCloneAsset(this.assetContext);
          break;
        case 4:
          this.btnShowToastDelete();
          break;
      }
    },

    /**
     * Tắt context menu
     * Author: LDTUAN (09/08/2023)
     */
    hideContextMenu() {
      this.isShowContextMenu = false;
      this.rowIndex = -1;
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
      this.onOpenForm = false;
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
      this.isShowToastExport = false;
      this.isLoading = true;
      let listIds = null;
      if (this.selectedRows != null && this.selectedRows.length > 0) {
        listIds = this.selectedRows.map((asset) => asset.FixedAssetId);
      } else {
        listIds = this.assetsTotal.map((asset) => asset.FixedAssetId);
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
          fileDownload(res.data, "Asset.xlsx");
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

    /**
     * Nhấn tab để di chuyển giữa các component
     * @param {*} event
     * @param {int} index số index của các component
     * Author: LDTUAN (09/08/2023)
     */
    checkTabIndex(event, index) {
      var charCode = event.which ? event.which : event.keyCode;
      if (index == "islast" && charCode == this.$_MISAEnum.KEYCODE.TAB) {
        event.preventDefault();
        this.buttonFocus = "button";
        this.$refs[this.buttonFocus].focusButton();
      }
    },
  },
};
</script>
<style scoped>
.table-container {
  height: calc(698px - 39px);
  width: 100%;
  overflow-y: auto;
  border-radius: 3.5px;
}

.table {
  flex: 1;
  width: 100%;
  background-color: #ffffff;
  border-spacing: unset;
  border: unset;
  border-radius: 3.5px;
  box-shadow: 0 3px 10px rgba(0, 0, 0, 0.16);
}

.table-container--noData {
  overflow-y: hidden;
}

.noData {
  display: flex;
  align-items: center;
  justify-content: center;
  /* background-color: aqua; */
  width: 100%;
  height: calc(698px - 70px);
}

.width-16px {
  width: 16px;
  max-width: 16px;
}

.width-64px {
  width: 64px;
  min-width: 64px;
}

.width-100px {
  width: 100px;
  min-width: 100px;
}

.width-120px {
  width: 120px;
  min-width: 120px;
}

.width-170px {
  width: 170px;
  min-width: 170px;
}

.width-220px {
  width: 220px;
}

.table__head:first-child,
.table__body:first-child {
  padding-left: 16px;
}

/*==================== Table head ====================*/

thead {
  position: sticky;
  top: 0;
  z-index: 1;
  background-color: #ffffff;
}

.table__head {
  font-family: Roboto, sans-serif;
  font-weight: 700;
  text-align: left;
  height: 38px;
  border-bottom: 1px solid #e0e0e0;
  font-size: 15px;
  cursor: context-menu;
}

.table__head input {
  margin: unset;
  width: 14px;
  height: 14px;
}

.table__head--right {
  text-align: right;
}

.table__head--center {
  text-align: center;
}

/*==================== Table body ====================*/

#tbodyAsset {
  height: calc(100% - 38px);
}

.table__body {
  font-family: Roboto, sans-serif;
  font-weight: 400;
  height: 39px;
  border-bottom: 1px solid #e0e0e0;
  font-size: 15px;
  position: relative;
}

.table__body input {
  margin: unset;
  width: 14px;
  height: 14px;
}

.tr--body-selected {
  background-color: rgba(26, 164, 200, 0.2);
}

.tr--body {
  cursor: pointer;
}

.tr--body:hover {
  background-color: rgba(26, 164, 200, 0.2);
}

/* Ẩn icon edit và copy, chỉ hiện khi hover row */
.tr--body > .table__body > .icon-function {
  display: none;
}

.tr--body:hover > .table__body > .icon-function {
  display: flex;
  column-gap: 16px;
}

.table__body--right {
  text-align: right;
}

.table__body--center {
  text-align: center;
}

.table__body--center-button {
  display: flex;
  align-items: center;
  justify-content: center;
}

.icon-function {
  align-items: center;
  justify-content: center;
  column-gap: 16px;
}
</style>
