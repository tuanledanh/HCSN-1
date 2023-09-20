<template>
  <div
    :class="[
      { 'button-combo': combo },
      { large: large },
      { 'large-6': large_6 },
      { basic: basic },
      { disabled: disabled },
    ]"
  >
    <button
      :disabled="disabled"
      :class="[
        'button',
        'relative',
        { 'button--add-box': add_box_white },
        { 'button--add-box': add_box },
        { loading: loading },
        { border: basic },
        { disabled: disabled },
        { 'button--main': buttonMain },
        { 'button--short': short },
        { 'button--sub': buttonSub },
        { 'button--error': error },
        { 'button--outline': buttonOutline },
        { 'button--icon': buttonIcon },
        { 'button--icon-normal': button_icon_normal },
      ]"
      :tabindex="tabindex"
      ref="button"
    >
      <MISAIcon
        v-if="combo"
        button
        :add="add"
        :loading="loading"
        :add_box="add_box"
        :add_box_white="add_box_white"
        :disabled="disabled"
      ></MISAIcon>
      <MISATooltip :bottom="bottom" :bottom_end="bottom_end" :content="content">
        <MISAIcon
          v-if="buttonIcon || button_icon_normal"
          :exportIcon="exportIcon"
          :deleteIcon="deleteIcon"
          :exit="exit"
          :disabled="disabled"
        ></MISAIcon>
      </MISATooltip>

      {{ textButton }}
    </button>
  </div>
</template>
<script>
export default {
  name: "MISAButton",
  props: {
    // Ngăn click vào button
    disabled: {
      type: Boolean,
      default: false,
    },

    // Button gồm có cả icon và input
    combo: {
      type: Boolean,
      default: false,
    },
    // Button màu mè đơn giản
    basic: {
      type: Boolean,
      default: false,
    },

    // Button chính
    buttonMain: {
      type: Boolean,
      default: false,
    },
    // Button không set chiều cao
    short: {
      type: Boolean,
      default: false,
    },

    // Button phụ
    buttonSub: {
      type: Boolean,
      default: false,
    },
    // Button đỏ, dùng để cảnh báo
    error: {
      type: Boolean,
      default: false,
    },

    // Button phác thảo
    buttonOutline: {
      type: Boolean,
      default: false,
    },
    // Button load
    loading: {
      type: Boolean,
      default: false,
    },

    // Button chỉ có icon
    buttonIcon: {
      type: Boolean,
      default: false,
    },

    // Button chỉ có icon, nhưng background khác
    button_icon_normal: {
      type: Boolean,
      default: false,
    },

    // Text hiển thị ở phần input
    textButton: {
      type: String,
      default: "",
    },

    // Icon của button
    icon: {
      type: String,
      default: "",
    },

    // Icon xuất excel
    exportIcon: {
      type: Boolean,
      default: false,
    },

    // Icon xóa
    deleteIcon: {
      type: Boolean,
      default: false,
    },

    // Icon thoát
    exit: {
      type: Boolean,
      default: false,
    },
    // Nội dung tooltip
    content: {
      type: String,
      default: "",
    },
    bottom: {
      type: Boolean,
      default: false,
    },
    bottom_end: {
      type: Boolean,
      default: false,
    },
    tabindex: {
      type: Number,
      default: 0,
    },
    focus: {
      type: Boolean,
      default: false,
    },
    // Những button có width rộng hơn min width là 110px thì sẽ thêm padding
    large: {
      type: Boolean,
      default: false,
    },
    // Những button có width rộng hơn min width là 110px thì sẽ thêm padding
    large_6: {
      type: Boolean,
      default: false,
    },
    // Icon thêm mới
    add: {
      type: Boolean,
      default: false,
    },
    // Icon thêm mới có border xung quanh
    add_box: {
      type: Boolean,
      default: false,
    },
    // Icon thêm mới có border xung quanh nhưng màu trắng
    add_box_white: {
      type: Boolean,
      default: false,
    },
  },
  methods: {
    /**
     * Focus vào button
     * Author: LDTUAN (09/08/2023)
     */
    focusButton() {
      this.$refs.button.focus();
    },
  },
  mounted() {
    if (this.focus) {
      this.focusButton();
    }
  },
  watch: {
    focus(value) {
      console.log(value);
      if (value) {
        this.focusButton();
      }
    },
  },
};
</script>
<style scoped>
.button {
  width: 100px;
  height: 36px;
  font-family: Roboto, sans-serif;
  font-weight: 500;
  cursor: pointer;
  border-radius: 4px;
  padding: unset;
  transition: all ease-in-out 0.3s;
}

.button--short {
  height: auto;
  width: auto;
  white-space: nowrap;
  padding: 4px 6px;
}

.button--error {
  height: 36px;
  width: auto;
  padding: 0px 18px;
}

.button--main {
  background-color: #1aa4c8;
  color: var(--background-color-default);
  border: unset;
}

.button--main:hover {
  background-color: #0582a2 !important;
}

.button--main:focus {
  outline: 1px solid #28b7dc;
  transition: all 0.2s linear;
}

.button--main:active {
  background-color: #28b7dc !important;
}

.button--sub {
  border: 1px solid #1aa4c8;
  background-color: var(--background-color-default);
  color: #1aa4c8;
}

.button--sub:hover {
  background-color: #d1edf4;
}

.button--sub:focus {
  outline: 1px solid #23cbf5;
  transition: all 0.2s linear;
}

.button--sub:active {
  background-color: var(--background-color-default);
}

.button--error {
  border: 1px solid #c81a1a;
  background-color: var(--background-color-default);
  color: #c81a1a;
}

.button--error:hover {
  background-color: #f4d1d1;
}

.button--error:focus {
  outline: 1px solid var(--background-color-default);
  transition: all 0.2s linear;
}

.button--error:active {
  background-color: var(--background-color-default);
}

.button--outline {
  border: 1px solid #cdcdcd;
  background-color: var(--background-color-default);
  color: #000000;
}

.button--outline:hover {
  background-color: #1aa4c8;
  color: var(--background-color-default);
}

.button--outline:focus {
  border: none;
  outline: 1px solid #23cbf5;
  transition: all 0.2s linear;
  background-color: #1aa4c8;
}

.button--outline:active {
  background-color: #23cbf5;
  color: var(--background-color-default);
}

.button-combo {
  min-width: 110px;
  height: 36px;
  display: flex;
  flex-direction: row;
  align-items: center;
  position: relative;
  cursor: pointer;
}

.button-combo .button {
  width: 100%;
  height: 100%;
  padding: unset;
  padding-left: 24px;
  font-family: Roboto, sans-serif;
  font-weight: 500;
  font-size: 13px;
  text-align: left;
  background-color: #1aa4c8;
  border: unset;
  border-radius: 4px;
  box-shadow: 0 2px 6px rgba(0, 0, 0, 0.16);
  overflow: hidden;
  color: var(--background-color-default);
  cursor: pointer;
  box-sizing: border-box;
}

.basic .button {
  background-color: var(--background-color-default);
  color: #000000;
  border-color: #1aa4c8;
}

.basic .button:hover {
  background-color: #d1edf4 !important;
}

.button-combo .button--add-box {
  padding-left: 42px;
}

.button--icon {
  width: 36px;
  height: 36px;
  background-color: var(--background-color-default);
  box-shadow: 0 2px 6px rgba(0, 0, 0, 0.16);
  border: unset;
}

.button--icon:hover {
  background-color: #1aa4c8;
}

.button--icon:active {
  background-color: #28b7dc;
}

.button--icon-normal {
  width: 24px;
  height: 24px !important;
  background-color: var(--background-color-default);
  border: unset;
  box-shadow: unset;
}

.disabled {
  background-color: #f5f5f5 !important;
  border-color: #d4d4d4 !important;
  color: #000000;
  cursor: not-allowed !important;
}

.disabled:hover {
  background-color: #f5f5f5 !important;
  border-color: #d4d4d4 !important;
  color: #000000;
  cursor: not-allowed !important;
}

.disabled .button--main {
  background-color: #1aa4c8 !important;
  color: white !important;
  opacity: 0.5;
}
.disabled .button--main :hover {
  background-color: #1aa4c8 !important;
  color: white !important;
  opacity: 0.5;
}

.large .button {
  padding-right: 13px;
}

.large-6 .button {
  padding-right: 6px;
}

.loading {
  padding-left: 38px !important;
  background-color: var(--background-color-default) !important;
  color: #000000 !important;
}

.loading:hover {
  background-color: #d1edf4 !important;
}

.loading:focus {
  outline: 1px solid #23cbf5 !important;
  transition: all 0.2s linear !important;
}

.loading:active {
  background-color: var(--background-color-default) !important;
}
</style>
