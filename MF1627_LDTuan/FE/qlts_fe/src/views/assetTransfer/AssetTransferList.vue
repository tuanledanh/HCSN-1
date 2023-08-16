<template>
  <div class="content" :class="[{ 'content--expand': isChangeWidth }]">
    <div class="content--top">
      <div class="top-left">
        <span class="font-weight--700">Điều Chuyển</span>
        <MISAIcon loading></MISAIcon>
      </div>
      <div class="top-right">
        <MISAButton
          combo
          add
          buttonMain
          textButton="Thêm chứng từ"
          @click="btnAddAsset"
          large
        ></MISAButton>
      </div>
    </div>
    <div class="content--body border--left border--right border--bottom">
      <!-- ------------------------Table start------------------------ -->
      <div class="table" ref="contentBody">
        <!-- ------------------------Header------------------------ -->
        <div class="header--row row">
          <div
            class="header cell display--center-center border--top border--right border--bottom"
          >
            <input type="checkbox" />
          </div>
          <div
            class="header cell display--center-center font-weight--700 border--top border--right border--bottom"
          >
            STT
          </div>
          <div
            class="header cell display--center-left font-weight--700 border--top border--right border--bottom padding--left-10"
          >
            Số chứng từ
          </div>
          <div
            class="header cell display--center-center font-weight--700 border--top border--right border--bottom"
          >
            Ngày chứng từ
          </div>
          <div
            class="header cell display--center-center font-weight--700 border--top border--right border--bottom"
          >
            Ngày điều chuyển
          </div>
          <div
            class="header cell display--center-left font-weight--700 border--top border--right border--bottom padding--left-10"
          >
            Ghi chú
          </div>
          <div
            class="header cell display--center-center font-weight--700 border--top border--bottom"
          >
            Chức năng
          </div>
        </div>

        <!-- ------------------------Filter------------------------ -->

        <div class="header--row row">
          <div
            class="header cell display--center-center border--right border--bottom"
          ></div>
          <div
            class="header cell display--center-center border--right border--bottom"
          ></div>
          <div
            class="header cell display--center-left border--right border--bottom padding-4"
          >
            <input type="text" />
          </div>
          <div
            class="header cell display--center-center border--right border--bottom padding-4"
          >
            <input type="text" />
          </div>
          <div
            class="header cell display--center-center border--right border--bottom padding-4"
          >
            <input type="text" />
          </div>
          <div
            class="header cell display--center-left border--right border--bottom padding-4"
          >
            <input type="text" />
          </div>
          <div class="header cell display--center-center border--bottom"></div>
        </div>
        <!-- ------------------------Body------------------------ -->
        <div class="body-data">
          <div
            class="body--row row"
            v-for="asset in assets"
            :key="asset.FixedAssetId"
          >
            <div
              class="cell display--center-center border--right border--bottom"
            >
              <input type="checkbox" />
            </div>
            <div
              class="cell display--center-center border--right border--bottom"
            >
              {{ assets.indexOf(asset) + 1 }}
            </div>
            <div
              class="body cell display--center-left border--right border--bottom padding--left-10"
            >
              {{ asset.FixedAssetName }}
            </div>
            <div
              class="cell display--center-center border--right border--bottom"
            >
              {{ asset.FixedAssetName }}
            </div>
            <div
              class="cell display--center-center border--right border--bottom"
            >
              {{ asset.FixedAssetName }}
            </div>
            <div
              class="cell display--center-left border--right border--bottom padding--left-10"
            >
              {{ asset.FixedAssetName }}
            </div>
            <div class="cell display--center-center border--bottom">
              <div class="icon-function">
                <MISATooltip bottom content="Chỉnh sửa">
                  <MISAIcon edit background></MISAIcon>
                </MISATooltip>
                <MISATooltip bottom content="Sao chép">
                  <MISAIcon copy background></MISAIcon>
                </MISATooltip>
                <MISATooltip bottom content="Xóa">
                  <MISAIcon deleteIcon background></MISAIcon>
                </MISATooltip>
              </div>
            </div>
          </div>
        </div>
      </div>
      <!-- ------------------------Table end------------------------ -->

      <MISAPaging
        :totalRecords="totalRecords"
        :totalPages="totalPages"
        :currentPage="currentPage"
        :pageLimitList="pageLimitList"
        @filter="getPageLimit"
        @paging="getPageNumber"
        :quantity="12"
        :price="12"
        :depreciation="12"
        :residualValue="12"
      ></MISAPaging>

      <!-- ------------------------Resize bar------------------------ -->
      <div
        class="resize-bar border--top border--bottom"
        @mousedown="startResize"
      ></div>

      <!-- ------------------------Table start------------------------ -->
      <div class="resizable-table">
        <div class="content--top">
          <div class="top-left">
            <MISAButton
              add
              buttonMain
              short
              textButton="Thông tin chi tiết"
              @click="btnAddAsset"
              large
            ></MISAButton>
          </div>
          <div class="top-right">
            <MISAIcon drop_down></MISAIcon>
          </div>
        </div>
        <div class="table table-bot">
          <!-- ------------------------Header------------------------ -->
          <div class="header--row row">
            <div
              class="header cell display--center-center border--top border--right border--bottom"
            >
              <input type="checkbox" />
            </div>
            <div
              class="header cell display--center-center font-weight--700 border--top border--right border--bottom"
            >
              STT
            </div>
            <div
              class="header cell display--center-left font-weight--700 border--top border--right border--bottom padding--left-10"
            >
              Số chứng từ
            </div>
            <div
              class="header cell display--center-center font-weight--700 border--top border--right border--bottom"
            >
              Ngày chứng từ
            </div>
            <div
              class="header cell display--center-center font-weight--700 border--top border--right border--bottom"
            >
              Ngày điều chuyển
            </div>
            <div
              class="header cell display--center-left font-weight--700 border--top border--right border--bottom padding--left-10"
            >
              Ghi chú
            </div>
            <div
              class="header cell display--center-center font-weight--700 border--top border--bottom"
            >
              Chức năng
            </div>
          </div>

          <!-- ------------------------Body------------------------ -->
          <div class="body-data">
            <div
              class="body--row row"
              v-for="asset in assets"
              :key="asset.FixedAssetId"
            >
              <div
                class="cell display--center-center border--right border--bottom"
              >
                <input type="checkbox" />
              </div>
              <div
                class="cell display--center-center border--right border--bottom"
              >
                {{ assets.indexOf(asset) + 1 }}
              </div>
              <div
                class="body cell display--center-left border--right border--bottom padding--left-10"
              >
                {{ asset.FixedAssetName }}
              </div>
              <div
                class="cell display--center-center border--right border--bottom"
              >
                {{ asset.FixedAssetName }}
              </div>
              <div
                class="cell display--center-center border--right border--bottom"
              >
                {{ asset.FixedAssetName }}
              </div>
              <div
                class="cell display--center-left border--right border--bottom padding--left-10"
              >
                {{ asset.FixedAssetName }}
              </div>
              <div class="cell display--center-center border--bottom">
                <div class="icon-function">
                  <MISATooltip bottom content="Chỉnh sửa">
                    <MISAIcon edit background></MISAIcon>
                  </MISATooltip>
                  <MISATooltip bottom content="Sao chép">
                    <MISAIcon copy background></MISAIcon>
                  </MISATooltip>
                  <MISATooltip bottom content="Xóa">
                    <MISAIcon deleteIcon background></MISAIcon>
                  </MISATooltip>
                </div>
              </div>
            </div>
          </div>
        </div>
        <!-- ------------------------Table end------------------------ -->

        <MISAPaging
          :totalRecords="totalRecords"
          :totalPages="totalPages"
          :currentPage="currentPage"
          :pageLimitList="pageLimitList"
          @filter="getPageLimit"
          @paging="getPageNumber"
          :quantity="12"
          :price="12"
          :depreciation="12"
          :residualValue="12"
        ></MISAPaging>
      </div>
    </div>
  </div>
</template>
<script>
export default {
  name: "AssetTransferList",
  props: {
    isChangeWidth: {
      type: Boolean,
      default: false,
    },
  },
  data() {
    return {
      // ----------------------------- Data -----------------------------
      assets: [],

      // ----------------------------- Paging -----------------------------
      pageLimitList: [],
      // Tổng bản ghi
      totalRecords: 0,
      // Tổng trang
      totalPages: 0,
      // Trang hiện tại
      currentPage: 1,
    };
  },
  created() {
    this.loadData();
  },
  methods: {
    // load data tạm thời
    loadData() {
      this.$_MISAApi.FixedAsset.Filter(1, 5, null, null, null)
        .then((res) => {
          this.assets = res.data.Data;
        })
        .catch((res) => {
          console.log(res);
        });
    },
    startResize(event) {
      event.preventDefault(); // Ngăn chặn chọn văn bản khi kéo
      document.addEventListener("mousemove", this.resizing);
      document.addEventListener("mouseup", this.stopResize);

      this.startY = event.clientY; // Lưu vị trí chuột xuất phát
      this.initialHeight = parseFloat(
        getComputedStyle(this.$el.querySelector(".resizable-table")).height
      ); // Lưu kích thước ban đầu của resizable-table
    },
    resizing(event) {
      const movementY = this.startY - event.clientY; // Tính toán khoảng cách di chuyển của chuột
      const newHeight = this.initialHeight + movementY; // Tính toán chiều cao mới dựa trên kích thước ban đầu và khoảng cách di chuyển

      const minHeight = 0; // Độ cao tối thiểu
      const maxHeight = 8000; // Độ cao tối đa
      console.log(this.$refs.contentBody.clientHeight);

      // Áp dụng độ cao mới vào resizable-table và giữ nguyên con trỏ
      this.$el.querySelector(".resizable-table").style.height = `${Math.min(
        Math.max(newHeight, minHeight),
        maxHeight
      )}px`;
      document.body.style.cursor = "ns-resize";
    },
    stopResize() {
      document.removeEventListener("mousemove", this.resizing);
      document.removeEventListener("mouseup", this.stopResize);
      document.body.style.cursor = "auto"; // Trả lại con trỏ mặc định
    },
  },
};
</script>
<style scoped>
.content {
  width: calc(100% - 200px);
  height: calc(100vh - 44px);
  float: right;
  box-sizing: border-box;
  padding: 13px 20px 26px 19px;
  display: flex;
  flex-direction: column;
  transition: all ease-in-out 0.1s;
  background-color: var(--background-color-main-content);
}

.content--expand {
  width: calc(100% - 66px);
}

.content--top {
  display: flex;
  flex-direction: row;
  align-items: center;
  justify-content: space-between;
  background-color: var(--background-color-default);
  padding: 8px 8px;
}

.top-left {
  display: flex;
  align-items: center;
  column-gap: 8px;
}

.top-left span {
  font-size: 20px;
}

.content--body {
  width: 100%;
  height: calc(100% - 36px);
  display: flex;
  flex-direction: column;
  box-sizing: border-box;
  border-color: var(--table-border-color);
}

/* ------------------------------------------- Table ------------------------------------------- */
.table {
  display: flex;
  flex-direction: column;
  width: 100%;
  height: 65%;
  background-color: var(--background-color-default);
  border-spacing: unset;
  overflow-y: auto;
}

.row {
  display: grid;
  grid-template-columns: 44px 50px 120px 140px 140px calc(100% - 604px) 110px;
  height: 35px;
}

.cell {
  min-height: 35px;
  background-color: var(--background-color-default);
  transition: var(--transition-02s);
  border-color: var(--table-border-color);
}

.icon-function {
  display: flex;
  column-gap: 8px;
}

.header {
  background-color: var(--background-color-table-head);
}

.header input[type="text"] {
  width: 100%;
  height: 100%;
}

.body--row:hover .cell {
  background-color: var(--table-body-hover);
}

.body {
  color: var(--table-body-text-color);
}
.body-data {
  overflow-y: auto;
}

/* ------------------------------------------- Resize-bar ------------------------------------------- */
.resize-bar {
  height: 2px;
  border-color: var(--resize-bar-color);
  background-color: var(--background-color-default);
  cursor: ns-resize;
}

.resizable-table {
  flex: 1;
  display: flex;
  flex-direction: column;
}

.table-bot {
  flex: 1;
}
</style>
