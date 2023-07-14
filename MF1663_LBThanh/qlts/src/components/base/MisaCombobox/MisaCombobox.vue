<template>
  <div class="combobox">
    <input
      type="text"
      :placeholder="placeholder"
      :value="itemSelected[propText]"
    />
    <button class="combobox-button" @click="onShowData">
      <div class="chevd-icon"></div>
    </button>
    <div v-show="showData" class="combobox-data">
      <a
        class="combobox-item"
        v-for="(item, index) in data"
        :key="index"
        @click="onselectItem(item)"
        >{{ item[propText] }}
      </a>
    </div>
  </div>
</template>

<script>
import axios from "axios";
import { getAllAssets } from "@/assets/js/api";
export default {
  name: "MisaCombobox",
  props: ["api", "placeholder", "propText"],
  data() {
    return {
      showData: false,
      data: [],
      itemSelected: {},
    };
  },
  created() {
    /**
     * Gọi hàm loadData sau khi các thuộc tính data đã được khởi tạo và các events listeners đã được thiết lập
     * Author: L.B.Thành (10/7/2023)
     */
    this.loadData();
  },
  methods: {
    /**
     * Hàm được sử dụng để thực hiện tính năng ẩn hiện combobox data mỗi lần được gọi
     * Author: L.B.Thành (10/7/2023)
     */
    onShowData() {
      this.showData = !this.showData;
    },
    /**
     * Hàm được sử dụng để nhận dữ liệu từ api sau đó nạp vào data[]
     * Author: L.B.Thành (10/7/2023)
     */
    loadData() {
      axios.get(getAllAssets).then((res) => {
        this.data = res.data;
      });
    },
    /**
     * Hàm được sử dụng để gán dữ liệu được lấy từ data được click trong combobox
     * Author: L.B.Thành (10/7/2023)
     */
    onselectItem(item) {
      this.itemSelected = item;
      this.showData = false;
    },
  },
};
</script>

<style scoped>
@import url("../../../assets/css/base/combobox.css");
</style>
