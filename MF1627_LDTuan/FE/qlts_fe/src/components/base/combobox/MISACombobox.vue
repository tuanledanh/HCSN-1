<template>
  <MISALabel
    v-if="label"
    :label="label"
    :required="required"
    :medium="medium"
    @click.stop="focusInput"
  ></MISALabel>
  <div v-click-outside="() => (isShowData = false)">
    <div
      id="selectOption"
      :class="['selectOption', { 'width-100per': self_adjust_size }]"
    >
      <div class="width-100per" @click.stop="onShowData">
        <MISAInput
          :input_35="input_35"
          combobox
          :normalGrid="self_adjust_size"
          :padding_2="self_adjust_size"
          :placeholder="placeholder"
          v-model="inputText"
          @indexHover="onIndexHover"
          @focus="$emit('focus')"
          ref="input"
          maxlength="100"
          :tabindex="tabindex"
          :error="error"
          :disabled="disabled"
        ></MISAInput>
      </div>
      <teleport to="body">
        <div
          v-show="isShowData"
          class="selectOption__data"
          ref="list"
          :style="{ top: top, left: left, width: rect?.width + 'px' }"
        >
          <div
            class="selectOption-item"
            :class="[
              { 'selectOption-item--selected': item == itemSelected },
              {
                'selectOption-item--hover':
                  itemFilter.indexOf(item) == indexHover,
              },
            ]"
            v-for="item in itemFilter"
            :key="item[propValue]"
            @click.stop="onSelectItem(item)"
          >
            <MISAIcon tick_true></MISAIcon>
            <span>{{ item[propText] }}</span>
          </div>
        </div>
      </teleport>
      <MISATooltip bottom content="Lọc">
        <MISAIcon filter :disabled="disabled"></MISAIcon>
      </MISATooltip>
      <MISAIcon
        v-if="canDelete"
        exit_small
        combobox_delete
        @click.stop="onDeleteText"
        :disabled="disabled"
      ></MISAIcon>
      <MISAIcon v-else drop_down combobox @click.stop="onShowData"></MISAIcon>
    </div>
  </div>
</template>
<script>
import axios from "axios";

export default {
  name: "MISACombobox",
  props: {
    // Height của input
    input_35: {
      type: Boolean,
      default: false,
    },
    // disabled
    disabled: {
      type: Boolean,
      default: false,
    },
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
    // Id, mã code của bản ghi khi thực hiện cập nhật
    existCode: {
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
    // Tên nhãn dán
    label: {
      type: String,
      default: "",
    },
    // Kiểm tra coi có cần xài icon required không
    required: {
      type: Boolean,
      default: false,
    },
    // Giá trị nhận từ component cha, truyền về để search
    inputChange: {
      type: String,
      default: "",
    },
    // Giá trị nhận từ component cha, truyền về để search
    newDepartment: {
      type: String,
      default: null,
    },
    tabindex: {
      type: Number,
      default: 0,
    },
    // Border đỏ khi lỗi nhập liệu
    error: {
      type: Boolean,
      default: false,
    },
    // Width = 100% component cha chứa nó
    self_adjust_size: {
      type: Boolean,
      default: false,
    },
    // Padding cho combobox
    padding: {
      type: Boolean,
      default: false,
    },
    // In đậm label
    medium: {
      type: Boolean,
      default: false,
    },
  },
  data() {
    return {
      // Data đầy đủ nhận được từ api
      ComboboxItems: [],
      // Data dùng để search
      itemFilter: [],
      // Hiển thị danh sách dữ liệu hay không
      isShowData: false,
      // Item được chọn
      itemSelected: {},
      // Item được chọn bằng click chuột
      itemSelectedByClick: {},
      // Giá trị hiển thị trên ô input
      inputText: "",
      // Giá trị hover khi sử dụng bàn phím
      indexHover: -1,
      // Xem người dùng nhấn phím gì trên bàn phím
      keyCode: null,
      // Hiển thị icon xóa text
      canDelete: false,
      newInput: null,
      rect: null,
      top: null,
      left: null,
    };
  },
  updated() {},
  watch: {
    /**
     * Theo dõi sự thay đổi của input text và gán lại giá trị
     * @param {string} value giá trị mới của input
     * Author: LDTUAN (02/08/2023)
     */
    inputChange(value) {
      this.inputText = value;
    },

    /**
     * Kiểm tra nếu item trong combobox có chứa inputText thì gán các giá trị đó vào itemFilter để hiển thị
     * @param {string} value giá trị mới của input text
     * Author: LDTUAN (02/08/2023)
     */
    inputText(value) {
      if (value == null || value == "" || !value) {
        this.indexHover = -1;
        this.isShowData = true;
        this.canDelete = false;
      }
      this.itemFilter = [];
      let itemFilter = this.ComboboxItems.filter((i) => {
        if (i[this.propText] != null) {
          return i[this.propText].toLowerCase().includes(value.toLowerCase());
        }
      });
      if (itemFilter != null && itemFilter.length > 0) {
        this.itemFilter = itemFilter;
      } else {
        this.$emit("filter", "");
      }
      if (
        this.keyCode != "Enter" &&
        this.inputChange == null &&
        this.newInput == null
      ) {
        this.isShowData = true;
      } else {
        this.newInput = null;
        this.keyCode = null;
        this.indexHover = -1;
        this.itemFilter = this.ComboboxItems;
        if (this.inputChange != null) {
          const matchedItem = this.itemFilter.find((item) =>
            item[this.propText].toLowerCase().includes(value.toLowerCase())
          );
          this.itemSelected = matchedItem;
          this.itemSelectedByClick = matchedItem;
        }
      }
    },

    /**
     * Khi item được chọn thì đóng drop down lại, do khi chọn item thì inputText vẫn thay đổi
     * và nó sẽ cập nhật isShowData bằng true, nên cần chuyển thành false để đóng lại
     * Author: LDTUAN (02/08/2023)
     */
    itemSelected() {
      this.isShowData = false;
      this.itemFilter = this.ComboboxItems;
      if (this.itemSelectedByClick) {
        this.itemSelected = this.itemSelectedByClick;
      }
      if (this.itemSelected && this.inputText.length > 0) {
        this.canDelete = true;
      }
    },

    /**
     * Khi update thì hiển thị giá trị code đã tồn tại của bản ghi đó lên input
     * @param {string} value giá trị của code đã tồn tài khi update
     * Author: LDTUAN (02/08/2023)
     */
    existCode(value) {
      const matchedItem = this.itemFilter.find((item) =>
        item[this.propText].toLowerCase().includes(value.toLowerCase())
      );
      this.itemSelected = matchedItem;
      this.itemSelectedByClick = matchedItem;
      this.$emit("filter", matchedItem);
      this.inputText = value;
    },
  },
  mounted() {},
  created() {
    this.loadData();
    if (this.newDepartment !== null) {
      this.inputText = this.newDepartment;
      this.newInput = this.newDepartment;
      this.itemSelected = this.newDepartment;
      this.canDelete = true;
    }
  },
  methods: {
    /**
     * Xóa input text
     * Author: LDTUAN (09/08/2023)
     */
    onDeleteText() {
      this.inputText = "";
      this.$emit("deleteDepartment", "");
      this.indexHover = -1;
      this.itemSelected = null;
      this.itemSelectedByClick = null;
    },
    /**
     * Focus vào ô nhập liệu
     * Author: LDTUAN (02/08/2023)
     */
    focusInput() {
      this.$refs.input.focusInput();
    },

    /***
     * Tải dữ liệu từ api
     * Author: LDTUAN (02/08/2023)
     */
    loadData() {
      axios.get(this.api).then((res) => {
        // Lưu vào data gốc
        this.ComboboxItems = res.data;
        // Lưu vào data sẽ hiển thị lên combobox, ở đây nghiệp vụ đơn giản nên thấy trùng với data gốc
        // Nhưng nếu sau này data nhiều, kiểu muốn mỗi lần hiển thị 10 bản ghi thì dùng filter để giới hạn ngay từ đầu sẽ dễ hơn
        this.itemFilter = res.data;
      });
    },

    /**
     * Click để ẩn hiện combobox và focus vào dòng đầu tiên
     * Author: LDTUAN (02/08/2023)
     */
    onShowData() {
      this.rect = this.$refs.input.getBoundingClientRect();
      const check = window.innerHeight - this.rect.bottom - 252 > 0;
      const rect = this.rect;
      this.left = rect.left + "px";
      if (check) {
        this.top = rect.bottom + 2 + "px";
      } else {
        this.top = rect.top - 252 - 2 + "px";
      }

      this.isShowData = !this.isShowData;
      this.$refs.input.focusInput();
    },

    /**
     * Gửi đối tượng được chọn về cho component cha để load dữ liệu tương ứng
     * @param {object} item 1 đối tượng được chọn
     * Author: LDTUAN (02/08/2023)
     */
    onSelectItem(item) {
      this.inputText = item[this.propText];
      this.itemSelected = {};
      this.itemSelectedByClick = item;
      this.indexHover = this.itemFilter.indexOf(this.itemSelectedByClick);
      this.$emit("filter", item);
      this.isShowData = false;
    },

    /**
     * Sử dụng bàn phím để thực hiện thao tác chọn item
     * @param {int} keyCode giá trị tương ứng với các phím trên bàn phím
     * Author: LDTUAN (02/08/2023)
     */
    onIndexHover(keyCode) {
      //if (this.isShowData) {
      if (
        this.indexHover == -1 &&
        this.itemSelected != null &&
        this.inputText != ""
      ) {
        this.indexHover = this.itemFilter.indexOf(this.itemSelected);
      }
      switch (keyCode) {
        case this.$_MISAEnum.KEYCODE.DOWN:
          this.isShowData = true;
          if (this.indexHover < this.itemFilter.length - 1) {
            this.indexHover++;
          }
          this.$refs.list.scroll({
            top: 36 * this.indexHover,
            behavior: "smooth",
          });
          break;
        case this.$_MISAEnum.KEYCODE.UP:
          this.isShowData = true;
          if (this.indexHover > -1) {
            this.indexHover--;
          }
          this.$refs.list.scroll({
            top: 36 * this.indexHover,
            behavior: "smooth",
          });
          break;
        case this.$_MISAEnum.KEYCODE.ENTER:
          if (this.inputText) {
            const matchedItem = this.itemFilter.find((item) =>
              item[this.propText]
                .toLowerCase()
                .includes(this.inputText.toLowerCase())
            );
            if (!matchedItem) {
              return;
            }
          }
          this.itemSelected = this.itemFilter[this.indexHover];
          this.itemSelectedByClick = this.itemFilter[this.indexHover];
          if (this.indexHover > -1) {
            this.inputText = this.itemFilter[this.indexHover][this.propText];
            this.$emit("filter", this.itemFilter[this.indexHover]);
          } else {
            this.$emit("filter", "");
          }
          this.keyCode = "Enter";
          this.isShowData = false;
          this.itemFilter = this.ComboboxItems;
          break;
      }
    },
  },
};
</script>
<style>
@import url(../../../css/base/selectOption.css);
</style>
