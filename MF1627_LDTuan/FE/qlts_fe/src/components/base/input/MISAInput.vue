<template>
  <MISALabel
    v-if="label"
    :label="label"
    :required="required"
    :medium="medium"
    @click="focusInput"
  ></MISALabel>
  <div
    :class="[
      'input',
      { 'padding-2': padding_2 },
      { 'input--error': error },
      { 'input--search': search },
      { 'input--normal': normal },
      { 'input--normal-grid': normalGrid },
      { 'input--large': large },
      { 'input--large-grid': largeGrid },
      { 'input--disabled': disabled },
      { 'input--combobox': combobox },
      { 'input--dropdown-list': dropdown_list },
      { 'input--small': small },
      { 'input--incre-decre': inputInDe },
    ]"
  >
    <input
      :type="type"
      ref="input"
      :disabled="disabled"
      :readonly="readonly"
      :placeholder="placeholder"
      :required="required"
      v-model="inputText"
      :tabindex="tabindex"
      @input="onSearchItem"
      @keydown="inputOnKeyDown"
      :class="[
        { 'text-align--right': right },
        { 'text-align--center': center },
        { 'height-35': input_35 },
      ]"
      @focus.stop="$emit('focus')"
      :maxlength="maxlength"
    />
    <MISATooltip bottom content="Tìm kiếm">
      <MISAIcon v-if="search" input iconSearch></MISAIcon>
    </MISATooltip>

    <div
      v-if="inputInDe"
      class="icon__wrapper absolute"
      :class="[{ disabled: disabled }]"
    >
      <MISAIcon pull_up @click="btnIncrease" :disabled="disabled"></MISAIcon>
      <MISAIcon drop_down @click="btnDecrease" :disabled="disabled"></MISAIcon>
    </div>
  </div>
</template>
<script>
import { formatMoney } from "../../../helpers/common/format/format";
import { formatMoneyToInt } from "../../../helpers/common/format/format";
export default {
  name: "MISAInput",
  props: {
    // Input có height 35px
    input_35: {
      type: Boolean,
      default: false,
    },
    // Khóa input
    disabled: {
      type: Boolean,
      default: false,
    },

    // Chỉ để đọc
    readonly: {
      type: Boolean,
      default: false,
    },
    // Font weight của label
    medium: {
      type: Boolean,
      dedfault: false,
    },

    // Input dạng tìm kiếm
    search: {
      type: Boolean,
      default: false,
    },

    // Input dạng danh sách
    dropdown_list: {
      type: Boolean,
      default: false,
    },

    // Input có icon tăng giảm
    inputInDe: {
      type: Boolean,
      default: false,
    },

    // Độ rộng nhỏ
    small: {
      type: Boolean,
      default: false,
    },

    // Độ rộng bình thường
    normal: {
      type: Boolean,
      default: false,
    },

    // Độ rộng lớn
    large: {
      type: Boolean,
      default: false,
    },

    // Input dạng combobox
    combobox: {
      type: Boolean,
      default: false,
    },

    // Giá trị của placeHolder
    placeholder: {
      type: String,
      default: "",
    },

    // Input bắt buộc phải nhập
    required: {
      type: Boolean,
      default: false,
    },

    // Tên nhãn dán
    label: {
      type: String,
      default: "",
    },

    // Giá trị binding 2 chiều nhận từ component cha
    modelValue: {
      type: String,
      default: "",
    },

    // Focus vào ô nhập liệu
    focus: {
      type: Boolean,
      default: false,
    },

    // Input bên phải
    right: {
      type: Boolean,
      default: false,
    },
    // Input trung tâm
    center: {
      type: Boolean,
      default: false,
    },

    // Loại nhập liệu
    type: {
      type: String,
      default: "text",
    },

    // Loại giá trị của ô nhập liệu
    typeOfValue: {
      type: String,
      default: "text",
    },

    // Độ dài tối thiểu của ô nhập liệu
    maxlength: {
      type: String,
      default: "255",
    },
    // Số thứ tự khi xài tab
    tabindex: {
      type: Number,
      default: 0,
    },
    // Border đỏ khi lỗi nhập liệu
    error: {
      type: Boolean,
      default: false,
    },
    // Width 100%
    normalGrid: {
      type: Boolean,
      default: false,
    },
    // Padding cho input
    padding_2: {
      type: Boolean,
      default: false,
    },
  },
  data() {
    return {
      // Giá trị của ô nhập liệu
      inputText: "",

      // Vị trí đang hover
      indexHover: 0,
    };
  },
  mounted() {
    this.inputText = this.modelValue;
  },
  watch: {
    // Gán lại giá trị binding 2 chiều nhận được từ component cha cho inputText
    modelValue(value) {
      this.inputText = value;
    },
  },
  methods: {
    getBoundingClientRect() {
      return this.$refs.input.getBoundingClientRect();
    },
    /**
     * Hàm focus vào ô nhập liệu
     * Author: LDTUAN (02/08/2023)
     */
    focusInput() {
      this.$refs.input.focus();
    },

    /**
     * Hover bằng phím, nếu nhấn enter thì gửi dữ liệu về cho component cha để thực hiện load dữ liệu
     * Author: LDTUAN (02/08/2023)
     */
    inputOnKeyDown() {
      const keyCode = event.keyCode;
      switch (keyCode) {
        case this.$_MISAEnum.KEYCODE.DOWN:
          this.$emit("indexHover", this.$_MISAEnum.KEYCODE.DOWN);
          break;
        case this.$_MISAEnum.KEYCODE.UP:
          this.$emit("indexHover", this.$_MISAEnum.KEYCODE.UP);
          break;
        case this.$_MISAEnum.KEYCODE.ENTER:
          this.$emit("indexHover", this.$_MISAEnum.KEYCODE.ENTER);
          this.$emit("searchByCode", this.$_MISAEnum.KEYCODE.ENTER);
          break;
      }
    },

    /**
     * Tạo các rằng buộc cho input, ví dụ như chỉ được nhập số, không nhập chữ
     * @param {string} event input nhập vào
     * Author: LDTUAN (02/08/2023)
     */
    onSearchItem(event) {
      var value = event.target.value;
      switch (this.typeOfValue) {
        case "number":
          value = value
            .replace(/^0/g, "")
            .replace(/([^0-9])/g, "")
            .replace(/\./g, "")
            .replace(/\B(?=(\d{3})+(?!\d))/g, ".");

          if (value === "") value = "0";
          this.inputText = value;
          break;
        case "percent":
          value = value.replace(/^0/g, "").replace(/([^0-9,])/g, "");
          if (value.includes(",") && value.indexOf(",") !== value.length - 1) {
            value = value.replace(/,$/g, "");
          }
          if (Number(value) > 100) value = "100";
          if (value === "") value = "0";
          this.inputValue = value;
          break;
        case "code":
          value = value.replace(/([^0-9,a-z,A-Z])/g, "");
          this.inputText = value;
          break;
        default:
          this.inputText = value;
          break;
      }
      this.$emit("update:modelValue", this.inputText);
    },

    /**
     * Tăng số lượng
     * Author: LDTUAN (07/08/2023)
     */
    btnIncrease() {
      this.focusInput();
      switch (this.typeOfValue) {
        case "number":
          this.inputText = formatMoney(formatMoneyToInt(this.inputText) + 1);
          break;
      }
      this.$emit("update:modelValue", this.inputText);
    },

    /**
     * Giảm số lượng
     * Author: LDTUAN (07/08/2023)
     */
    btnDecrease() {
      if (formatMoneyToInt(this.inputText) > 0) {
        this.focusInput();
        switch (this.typeOfValue) {
          case "number":
            this.inputText = formatMoney(formatMoneyToInt(this.inputText) - 1);
            break;
        }
        if (!this.inputText) {
          this.inputText = "0";
        }
        this.$emit("update:modelValue", this.inputText);
      }
    },
  },
};
</script>
<style scoped>
.input {
  display: flex;
  flex-direction: row;
  align-items: center;
  height: 36px;
  position: relative;
  font-family: Roboto, sans-serif;
  font-weight: 400;
  font-size: 13px;
  box-sizing: border-box;
}

.input input {
  width: 100%;
  height: 100%;
  background-color: #ffffff;
  border-radius: 4px;
  border: 1px solid #afafaf;
  overflow: hidden;
  font-family: Roboto, sans-serif;
  font-weight: 400;
  font-size: 13px;
  padding: unset;
  padding: 0 14px;
  box-sizing: border-box;
  outline: unset;
}

.input input:hover {
  border-color: #1aa4c8;
}

.input input:focus {
  border-color: #1aa4c8;
}

.input input::placeholder {
  font-style: italic;
}

.input--error input {
  border: 1px solid red;
}

/*--------------------------------------------*/

.input--search {
  width: 180px;
}

.input--search input {
  padding-left: 34px;
}

.input--dropdown-list {
  width: 56px;
  height: 26px;
}

.input--normal {
  width: 265px;
}

.input--normal-grid {
  min-width: unset !important;
  width: 100%;
}

.input--large {
  width: 546px;
}

.input--large-grid {
  width: 100%;
}

.input--combobox {
  min-width: 265px;
  position: relative;
}

.input--combobox input {
  padding-left: 40px;
  padding-right: 24px;
}

.input--combobox input::placeholder {
  font-style: unset;
  color: #000000;
}

.input--disabled input {
  background-color: #f5f5f5 !important;
  cursor: not-allowed;
}

.input--disabled input:hover {
  border: 1px solid #afafaf !important;
}

.disabled {
  cursor: not-allowed;
}

.text--right {
  text-align: right;
}

.icon__wrapper {
  right: 0;
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  row-gap: 4px;
  width: 36px;
}

.input--incre-decre input {
  padding-right: 32px;
}
</style>
