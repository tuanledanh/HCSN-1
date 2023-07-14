<!-- <template>
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
          :key="asset.CustomerId"
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
          <td class="table__body">{{ asset.FirstName }}</td>
          <td class="table__body">{{ asset.CompanyName }}</td>
          <td class="table__body">{{ asset.CompanyTaxCode }}</td>
          <td class="table__body">{{ asset.LastName }}</td>
          <td class="table__body">{{ asset.CreatedDate }}</td>
          <td class="table__body table__body--right">
            {{ asset.CustomerCode }}
          </td>
          <td class="table__body table__body--right">
            {{ asset.DebitAmount }}
          </td>
          <td class="table__body table__body--right">
            {{ asset.CustomerId }}
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
</template>
<script>
export default {
  name: "MISATable",
  props: ['assets', 'selectedRows'],
  data() {
    return {
        selectedRows: [],
    }
  },
  methods: {
    rowOnClick(asset) {
      const index = this.selectedRows.indexOf(asset);
      if (index !== -1) {
        this.selectedRows.splice(index, 1);
      } else {
        this.selectedRows.push(asset);
      }
    },
  },
};
</script> -->
