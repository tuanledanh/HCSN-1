<template>
  <div :class="[{ 'button-combo': combo }]">
    <MISAIcon v-if="combo" button add></MISAIcon>
    <button
      :disabled="disabled"
      :class="[
        'button',
        { disabled: disabled },
        { 'button--main': buttonMain },
        { 'button--sub': buttonSub },
        { 'button--outline': buttonOutline },
        { 'button--icon': buttonIcon },
        { 'button--icon-normal': button_icon_normal },
      ]"
      :tabindex="tabindex"
      ref="button"
    >
      <MISATooltip :bottom="bottom" :bottom_end="bottom_end" :content="content">
        <MISAIcon
          v-if="buttonIcon || button_icon_normal"
          :exportIcon="exportIcon"
          :deleteIcon="deleteIcon"
          :exit="exit"
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

    // Button chính
    buttonMain: {
      type: Boolean,
      default: false,
    },

    // Button phụ
    buttonSub: {
      type: Boolean,
      default: false,
    },

    // Button phác thảo
    buttonOutline: {
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

.button .icon {
  font-weight: 300;
  width: 100%;
  height: 100%;
}

.button--main {
  background-color: #1aa4c8;
  color: #ffffff;
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
  background-color: #ffffff;
  color: #1aa4c8;
}

.button--sub:hover {
  background-color: #d1edf4;
}

.button--sub:focus {
  outline: 1px solid #ffffff;
  transition: all 0.2s linear;
}

.button--sub:active {
  background-color: #ffffff;
}

.button--outline {
  border: 1px solid #cdcdcd;
  background-color: #ffffff;
  color: #000000;
}

.button--outline:hover {
  background-color: #1aa4c8;
  color: #ffffff;
}

.button--outline:focus {
  border: none;
  outline: 1px solid #23cbf5;
  transition: all 0.2s linear;
  background-color: #1aa4c8;
}

.button--outline:active {
  background-color: #23cbf5;
  color: #ffffff;
}

.button-combo {
  width: 110px;
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
  font-weight: 400;
  font-size: 13px;
  text-align: left;
  background-color: #1aa4c8;
  border: unset;
  border-radius: 4px;
  box-shadow: 0 2px 6px rgba(0, 0, 0, 0.16);
  overflow: hidden;
  color: #ffffff;
  cursor: pointer;
  box-sizing: border-box;
}

.button--icon {
  width: 36px;
  height: 36px;
  background-color: #ffffff;
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
  background-color: #ffffff;
  border: unset;
  box-shadow: unset;
}

.disabled {
  pointer-events: none; /* Prevents the button from being clicked */
  opacity: 0.6;
  cursor: not-allowed;
}
</style>
