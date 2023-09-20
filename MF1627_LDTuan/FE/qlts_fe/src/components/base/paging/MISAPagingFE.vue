<template>
  <div class="table__pagingFE">
    <div class="pagingFE">
      <div class="pagingFE--left">
        <span
          >Tổng số: <strong>{{ totalRecords }}</strong> bản ghi</span
        >
        <MISADropdownList
          :pageLimitList="pageLimitList"
          @filter="getPageLimit"
        ></MISADropdownList>
        <div class="page--itemFE">
          <MISAIcon
            prev
            :totalPages="totalPages"
            :currentPage="currentPage"
            @paging="getPageNumber"
            :disabled="pageNumber == 1"
          ></MISAIcon>
          <div
            v-for="(page, index) in pages"
            :key="index"
            class="pagingFE__number"
            :class="{ 'pagingFE__number--active': page == pageNumber }"
            @click.prevent="getPageNumber(page)"
          >
            {{ page }}
          </div>
          <MISAIcon
            next
            :totalPages="totalPages"
            :currentPage="currentPage"
            @paging="getPageNumber"
            :disabled="
              pageNumber === 0 || totalPages === 0 || pageNumber == totalPages
            "
          ></MISAIcon>
        </div>
      </div>
      <div class="pagingFE--right">
        <div class="calculatorFE">
          <div v-if="quantity > 0" class="quantity">{{ quantity }}</div>
          <div v-if="price > 0" class="priceFE">{{ price }}</div>
          <div v-if="depreciation > 0" class="depreciationFE">
            {{ depreciation }}
          </div>
          <div v-if="residualValue > 0" class="residualValue">
            {{ residualValue }}
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "MISAPagingFE",
  props: {
    // Tổng số bản ghi
    totalRecords: {
      type: Int32Array,
      default: 0,
    },

    // Danh sách giới hạn bản ghi mỗi trang
    pageLimitList: {
      type: Array,
      default: null,
    },

    // Tổng số trang
    totalPages: {
      type: Int32Array,
      default: null,
    },

    // Trang hiện tại
    currentPage: {
      type: Int32Array,
      default: 1,
    },
  },
  data() {
    return {
      // Giới hạn số bản ghi ban đầu
      pageLimit: 20,

      // Số trang ban đầu
      pageNumber: 1,
    };
  },
  computed: {
    /**
     * Tính toán có bao nhiêu số trang
     * Author: LDTUAN (02/08/2023)
     */
    totalPage() {
      return Math.ceil(this.totalRecords / this.pageLimit);
    },

    /**
     * Tính toán các số trang có thể hiển thị
     * Author: LDTUAN (02/08/2023)
     */
    pages() {
      let pages = [];
      for (let i = 1; i <= this.totalPage; i++) {
        if (
          i == 1 ||
          i == this.pageNumber ||
          i == this.totalPage ||
          i == this.pageNumber + 1
        ) {
          if (
            this.pageNumber == this.totalPage &&
            i == this.pageNumber &&
            this.totalPage > 3
          ) {
            pages.push(this.pageNumber - 1);
          }
          pages.push(i);
        } else if (i == this.pageNumber + 2 || i == this.pageNumber - 1) {
          pages.push("...");
        }
      }
      return pages;
    },
  },
  watch: {
    /**
     * Khi giá trị thay đổi thì báo cho component cha để thực hiện lấy dữ liệu tương ứng
     * @param {*} value giá trị mới của pageLimit
     * Author: LDTUAN (02/08/2023)
     */
    pageLimit(value) {
      this.$emit("filter", value);
    },

    /**
     * Khi giá trị thay đổi thì báo cho component cha để thực hiện lấy dữ liệu tương ứng
     * @param {*} value giá trị mới của pageNumber
     * Author: LDTUAN (02/08/2023)
     */
    pageNumber(newValue, oldValue) {
      if (newValue === "...") {
        this.pageNumber = oldValue;
      } else {
        this.$emit("paging", newValue);
      }
    },

    currentPage(value) {
      this.pageNumber = value;
    },
  },
  methods: {
    /**
     * Lấy giá trị từ dropdown trả về
     * @param {int} value lấy giới hạn bản ghi mỗi trang
     * Author: LDTUAN (02/08/2023)
     */
    getPageLimit(value) {
      this.pageLimit = value;
      this.pageNumber = 1;
    },

    /**
     * Giá trị trả về từ dropdown
     * @param {int} value số trang
     * Author: LDTUAN (02/08/2023)
     */
    getPageNumber(value) {
      this.pageNumber = value;
    },
  },
};
</script>

<style scoped>
.pagingFE {
  /* border-top: 1px solid var(--background-color-table-expand-narrow); */
  background-color: #ffffff;
  height: 39px;
  width: 100%;
  padding-left: 16px;
  display: flex;
  flex-direction: row;
  align-items: center;
  justify-content: space-between;
  border-bottom-left-radius: 3.5px;
  border-bottom-right-radius: 3.5px;
  box-sizing: border-box;
}

.pagingFE--left {
  display: flex;
  flex-direction: row;
  align-items: center;
  column-gap: 25px;
}

.pagingFE--left .input--option {
  width: 59px;
  height: 25px;
  box-sizing: border-box;
  margin-left: 38px;
  margin-right: 20px;
}

.pagingFE--left .input--option input {
  padding-left: 14px;
}

.page--itemFE {
  display: flex;
  flex-direction: row;
  align-items: center;
  column-gap: 10px;
}

.page--itemFE .icon {
  border-radius: 4px;
  padding: 4px 6px;
}

.page--itemFE .icon:hover {
  background-color: rgb(239, 239, 239);
}

.pagingFE__number {
  padding: 2px 6px;
  cursor: pointer;
}

.pagingFE__number:hover {
  border-radius: 4px;
  background-color: rgb(239, 239, 239);
}

.pagingFE__number--active {
  border-radius: 4px;
  background-color: rgb(234, 234, 234);
  font-weight: 700;
}

.calculatorFE {
  display: flex;
  flex-direction: row;
  padding-right: 120px;
  font-weight: 700;
}

.residualValueFE {
  width: 122px;
  text-align: right;
}

.depreciationFE {
  width: 122px;
  text-align: right;
}

.priceFE {
  width: 122px;
  text-align: right;
}
</style>
