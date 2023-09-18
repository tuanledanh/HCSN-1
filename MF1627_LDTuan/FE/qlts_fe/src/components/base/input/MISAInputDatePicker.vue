<template>
  <MISALabel
    v-if="label"
    :label="label"
    :required="required"
    :medium="medium"
    @click="focusInput"
  ></MISALabel>
  <div :class="[{ 'date-picker': !medium }, {'date-picker-grid':medium}, {'disabled':disabled}]">
    <el-date-picker
      v-model="date"
      type="date"
      placeholder="Pick a day"
      format="DD/MM/YYYY"
      value-format="YYYY/MM/DD"
      :defaultValue="new Date()"
      :clear-icon="false"
      :prefix-icon="customIconClass"
      :tabindex="tabindex"
      :disabled="disabled"
      @focus.stop="$emit('focus')"
      ref="input"
      :class="[{ 'input--error': error }, { 'font-size-16': medium }]"
    >
      <template #default="cell">
        <div class="cell" :class="{ current: cell.isCurrent }">
          <span class="text">{{ cell.text }}</span>
        </div>
      </template>
    </el-date-picker>
  </div>
</template>
<script>
import misaIcon from "../icon/MISAIconDatepicker.vue";
export default {
  name: "MISAInputDatePicker",
  // components:{
  //   MISAIcon,
  // },
  props: {
    // Nhãn dán
    label: {
      type: Boolean,
      default: false,
    },
    disabled: {
      type: Boolean,
      default: false,
    },
    // Ô nhập liệu cần phải nhập
    required: {
      type: Boolean,
      default: false,
    },
    // Font weight của label
    medium: {
      type: Boolean,
      dedfault: false,
    },
    // Giá trị binding 2 chiều nhận được từ component cha
    modelValue: {
      type: String,
      default: "",
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
  },
  data() {
    return {
      date: null,
      customIconClass: misaIcon,
    };
  },
  mounted() {
    // Nếu giá trị trả về từ component cha có giá trị thì gán cho date
    if (this.modelValue) {
      this.date = this.modelValue;
    }
  },
  watch: {
    // Gán giá trị từ component cha cho date mỗi khi giá trị đó thay đổi
    modelValue(value) {
      this.date = value;
    },
    date(value) {
      this.$emit("update:modelValue", value);
    },
  },
  methods: {
    /**
     * Hàm focus vào ô nhập liệu
     * Author: LDTUAN (02/08/2023)
     */
    focusInput() {
      this.$refs.input.focus();
    },
  },
};
</script>
<style>
.cell {
  height: 30px;
  padding: 3px 0;
  box-sizing: border-box;
  position: relative; /* Thêm thuộc tính position */
}

.cell .text {
  width: 24px;
  height: 24px;
  display: block;
  margin: 0 auto;
  line-height: 24px;
  position: absolute;
  left: 50%;
  transform: translateX(-50%);
  border-radius: 50%;
}

.cell.current .text {
  background: #626aef;
  color: #fff;
}

.date-picker {
  width: 265px !important;
  height: 35px;
}

.date-picker-grid {
  width: 100% !important;
  height: 35px;
}
.el-input {
  width: 100% !important;
  height: 100% !important;
}

.el-input__wrapper {
  padding: unset !important;
  padding-left: 14px !important;
  width: 100% !important;
  height: 100%;
  font-family: Roboto, sans-serif;
  font-weight: 400;
  font-size: 13px;
  background-color: #ffffff;
  border-radius: 4px;
  border: 1px solid #afafaf;
  outline: none;
  box-shadow: unset;
}
.el-input__inner {
  font-family: Roboto, sans-serif;
  font-weight: 400;
  font-size: 13px;
  color: black;
}

.el-input__wrapper:hover {
  outline: none;
  box-shadow: none;
  border-color: #1aa4c8;
}

.el-input__wrapper.is-focus {
  box-shadow: unset !important;
  border-color: #1aa4c8;
}

.el-input__prefix {
  position: absolute !important;
  right: 2px !important;
}

.input--error .el-input__wrapper {
  border: 1px solid red !important;
}

.disabled .el-input__inner{
  -webkit-text-fill-color: #565555  !important;
}
.disabled .el-input__wrapper{
  background-color: #f5f5f5 !important;
  cursor: not-allowed;
}

.disabled .el-input__wrapper:hover {
  border: 1px solid #afafaf !important;
}

.disabled .icon-date-picker {
  cursor: not-allowed !important;
}
</style>
