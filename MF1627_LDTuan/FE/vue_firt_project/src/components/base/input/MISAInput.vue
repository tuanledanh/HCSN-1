<template>
  <MISALabel :label="label" :required="required"></MISALabel>
  <div
    :class="[
      'input',
      'input--popup',
      'tooltip',
      inputClass,
      { 'input--disabled': disabled },
      {'input--error': required && !dataValue && isSubmittedForm},
    ]"
  >
    <input
      class="input__font--italic"
      :type="type"
      :placeholder="placeholder"
      v-model="dataValue"
      @input="onInput()"
      :disabled="disabled"
      ref="txtProperty"
    />
    <span v-if="required" class="tooltipText">{{ label }} được yêu cầu</span>
  </div>
</template>
<script>
export default {
  name: "MISAInput",
  props: {
    // Label cho thẻ input
    label: {
      type: String,
      default: "",
    },
    // Kiểu dữ liệu của input
    type: {
      type: String,
      default: "text",
    },
    // Model value thực hiện biding 2 chiều
    modelValue: {
      type: String,
      default: "",
    },
    placeholder: {
      type: String,
      default: "",
    },
    // Chuyển trạng thái của input sang disabled
    disabled: {
      type: Boolean,
      default: false,
    },
    // Điều kiện để hiển thị icon required
    required: {
      type: Boolean,
      default: false,
    },
    // Các class css cho input
    inputClass: {
      type: String,
      default: "",
    },
    // Form đã submit hay chưa
    isSubmittedForm:{
      type: Boolean,
      default: false,
    },
  },
  data() {
    return {
      dataValue: "",
    };
  },
  created() {
    this.dataValue = this.modelValue;
  },
  watch: {
    /**
     * Theo dõi và gán lại giá trị cho dataValue khi nhận qua v-model từ lớp cha
     * @param {*} newvalue giá trị mới, nhận đươc từ lớp cha
     * Author: LDTUAN (03/07/2023)
     */
    modelValue: function (newvalue) {
      console.log(newvalue);
      this.dataValue = this.modelValue;
    },
  },
  methods: {
    /**
     * Thực hiện update lại giá trị của modelValue khi dữ liệu thay đổi
     * Author: LDTUAN (03/07/2023)
     */
    onInput() {
      this.$emit("update:modelValue", this.dataValue);
    },
    /**
     * Thực hiện focus vào ô nhập liệu có refs là txtProperty khi mở popup
     * Author: LDTUAN (03/07/2023)
     */
    focus() {
      this.$refs.txtProperty.focus();
    },
  },
};
</script>
