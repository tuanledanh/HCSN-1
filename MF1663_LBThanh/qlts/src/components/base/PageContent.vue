<template>
  <div class="page-content">
    <div class="page-table-body">
      <table class="table">
        <thead>
          <tr>
            <th><input type="checkbox" v-model="selectAll" /></th>
            <th>STT</th>
            <th>Mã tài sản</th>
            <th>Tên tài sản</th>
            <th>Loại tài sản</th>
            <th>Bộ phận sử dụng</th>
            <th>Số lượng</th>
            <th>Nguyên giá</th>
            <th class="tooltip-container">
              HM/KH lũy kế
              <span class="tooltips-text">Hao mòn / Khấu hao</span>
            </th>
            <th>Giá trị còn lại</th>
            <th>Chức năng</th>
          </tr>
        </thead>
        <tbody>
          <tr
            v-for="(asset, index) in assets"
            :key="asset.CustomerId"
            :class="{ 'background-color-hover': selectedRow.includes(asset) }"
            @click="toggleRowSelection(asset)"
          >
            <td>
              <input type="checkbox" v-model="selectedRow" :value="asset" />
            </td>
            <td>{{ index + 1 }}</td>
            <td>{{ asset.assetCode }}</td>
            <td>{{ asset.assetName }}</td>
            <td>{{ asset.assetTypeName }}</td>
            <td>{{ asset.departmentName }}</td>
            <td>{{ asset.assetQuantity }}</td>
            <td>{{ asset.assetPrice }}</td>
            <td>{{ (asset.assetPrice * asset.assetDepreciation)/100  }}</td>
            <td>{{ asset.assetPrice - (asset.assetPrice * asset.assetDepreciation)/100}}</td>
            <td>
              <div class="feature-icons">
                <div class="edit-icon-container">
                  <div class="edit-icon"></div>
                </div>
                <div class="copy-icon-container">
                  <div class="copy-icon"></div>
                </div>
              </div>
            </td>
          </tr>
        </tbody>
      </table>
    </div>
    <LoadingAnimation v-if="isLoading"></LoadingAnimation>
    <TablePaging/>
  </div>
</template>

<script>
import LoadingAnimation from "../animation/LoadingAnimation.vue";
import TablePaging from './TablePaging.vue';
import {getAllAssets} from '../../assets/js/api'
export default {
  name: "AssetList",
  components: {
    LoadingAnimation,
    TablePaging,
  },
  created() {
    //load dữ liệu
    this.loadData();
  },
  methods: {
    /**
     * Gọi API, chuyển qua JSON và nạp vào assets để tiến hành thao tác với dữ liệu
     * Author: L.B.Thành (24/6/2023)
     */
    loadData() {
      this.isLoading = true;
      fetch(getAllAssets)
        .then((res) => res.json())
        .then((data) => {
          this.assets = data;
          console.log(data)
          this.isLoading = false;
        });
    },
    /**
     * Hàm được gọi khi checkbox được thay đổi hoặc row được click để thêm hoặc xóa asset khỏi mảng selectedRow:
     * Author: L.B.Thành (27/6/2023)
     */
    toggleRowSelection(asset) {
      const index = this.selectedRow.indexOf(asset);
      if (index === -1) {
        this.selectedRow.push(asset);
      } else {
        this.selectedRow.splice(index, 1);
      }
      // Set selectAll = true nếu tất cả các row đều được chọn và ngược lại
      this.selectAll = this.selectedRow.length === this.assets.length;
    },
  },
  watch: {
    /**
     * Hàm được sử dụng để check số row được lựa chọn có phải toàn bộ hay không
     * Nếu tất cả các row đều được chọn thì nạp chúng vào selectedRow
     * Nếu không thì set selectAll thành false để unchecked selectAll
     * Author: L.B.Thành (27/6/2023)
     */
    selectAll(value) {
      if (value) {
        // Chọn tất cả các row
        this.selectedRow = [...this.assets];
      } else {
        // Bỏ chọn tất cả các row
        this.selectAll = false;
      }
    },
  },
  data() {
    return {
      assets: [],
      selectedRow: [],
      selectAll: false,
      isLoading: false
    };
  },
};
</script>
