<template>
  <div
    class="selectOption dropdownList"
    @dblclick="isShowData = true"
    v-click-outside="() => (isShowData = false)"
  >
    <MISAInput
      readonly
      dropdown_list
      ref="pagingLimit"
      :placeholder="placeholder"
      :modelValue="inputText"
      @indexHover="onIndexHover"
    ></MISAInput>
    <div v-if="isShowData" class="selectOption__data data-dropdownList">
      <div
        class="selectOption-item data-item"
        :class="[
          { 'selectOption-item--selected': item == itemSelected },
          {
            'selectOption-item--hover':
              ComboboxItems.indexOf(item) == indexHover,
          },
        ]"
        v-for="item in ComboboxItems"
        :key="item"
        @click="onSelectItem(item)"
      >
        <span>{{ item }}</span>
      </div>
    </div>
    <MISAIcon drop_down combobox @click="onShowData"></MISAIcon>
  </div>
</template>
<script>
export default {
  name: "MISACombobox",
  props: {
    // Nội dung của placeholder, truyền về cho input
    placeholder: {
      type: String,
      default: "",
    },

    // Api nhận từ component cha, để thực hiện lấy dữ liệu
    api: {
      type: String,
      default: null,
    },

    // Value của object hiển thị trên combobox
    propValue: {
      type: String,
      default: "",
    },

    // Giá trị hiển thị trên combobox
    propText: {
      type: String,
      default: "",
    },

    // Danh sách option số lượng bản ghi mỗi trang
    pageLimitList: {
      type: Array,
      default: null,
    },
  },
  data() {
    return {
      // Data đầy đủ nhận được từ api
      ComboboxItems: [],
      
      // Hiển thị danh sách dữ liệu hay không
      isShowData: false,
      
      // Item được chọn
      itemSelected: {},
      
      // Giá trị hiển thị trên ô input
      inputText: "",
      
      // Giá trị hover khi sử dụng bàn phím
      indexHover: 0,
      
      // Xem người dùng nhấn phím gì trên bàn phím
      keyCode: null,
    };
  },
  watch: {
    /**
     * Khi click vào ô input thì sẽ hiển thị dropdown
     * Author: LDTUAN (02/08/2023)
     */
    inputText(value) {
      this.inputText = value;
      if (this.keyCode != "Enter") {
        this.isShowData = true;
      } else {
        this.keyCode = null;
      }
      this.indexHover = this.ComboboxItems.indexOf(value);
    },

    /**
     * Khi item được chọn thì đóng drop down lại, do khi chọn item thì inputText vẫn thay đổi
     * và nó sẽ cập nhật isShowData bằng true, nên cần chuyển thành false để đóng lại
     * Author: LDTUAN (02/08/2023)
     */
    itemSelected() {
      this.isShowData = false;
    },
  },
  created() {
    this.onLoadData();
  },
  methods: {

    /**
     * Tải dữ liệu lên cho combobox
     * Author: LDTUAN (02/08/2023)
     */
    onLoadData() {
      this.ComboboxItems = this.pageLimitList;
      this.itemSelected = this.ComboboxItems[0];
      this.inputText = this.ComboboxItems[0];
    },

    /**
     * Click button để ẩn hiện data
     * Author: LDTUAN (02/08/2023)
     */
    onShowData() {
      this.isShowData = !this.isShowData;
      this.$refs.pagingLimit.focusInput();
    },

    /**
     * Chọn item và thực hiện load lại dữ liệu theo item đã chọn
     * @param {object} item đối tượng hiển thị lên màn hình
     * Author: LDTUAN (02/08/2023)
     */
    onSelectItem(item) {
      this.inputText = item;
      this.itemSelected = item;
      this.$emit("filter", item);
    },

    /**
     * Thực hiện di chuyển, chọn bằng phím
     * @param {int} keyCode mã code của phím
     * Author: LDTUAN (02/08/2023)
     */
    onIndexHover(keyCode) {
      if (this.isShowData) {
        switch (keyCode) {
          case this.$_MISAEnum.KEYCODE.DOWN:
            if (this.indexHover < this.ComboboxItems.length - 1) {
              this.indexHover++;
            }
            break;
          case this.$_MISAEnum.KEYCODE.UP:
            if (this.indexHover > 0) {
              this.indexHover--;
            }
            break;
          case this.$_MISAEnum.KEYCODE.ENTER:
            if (this.inputText) {
              const matchedItem = this.ComboboxItems.find((item) =>
                `${item}`.includes(`${this.inputText}`)
              );
              if (!matchedItem) {
                return;
              }
              this.itemSelected = this.ComboboxItems[this.indexHover];
              this.inputText = this.ComboboxItems[this.indexHover];
              this.keyCode = "Enter";
              this.$emit("filter", this.ComboboxItems[this.indexHover]);
            } else {
              this.itemSelected = this.ComboboxItems[this.indexHover];
              this.inputText = this.ComboboxItems[this.indexHover];
              this.keyCode = "Enter";
              this.$emit("filter", this.ComboboxItems[this.indexHover]);
            }
            this.isShowData = false;
            break;
        }
      }
    },
  },
};
</script>
<style>
@import url(../../../css/base/selectOption.css);
</style>
