<template>
  <MISALabel :label="label" :required="required"></MISALabel>
  <div :class="['input', 'input--popup', 'input--popup-width', 'tooltip']">
    <input class="" :type="type" v-model="dataValue" />
    <div class="icon--input icon--input-right">
      <div class="icon--date-picker"></div>
    </div>
    <span v-if="required" class="tooltipText">{{ label }} được yêu cầu</span>
  </div>
</template>
<script>
export default {
  name: "MISAInputDate",
  props: {
    label: {
      type: String,
      default: "",
    },
    type: {
      type: String,
      default: "text",
    },
    required: {
      type: Boolean,
      default: false,
    },
    modelValue: {
      type: String,
      default: "",
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
  },
};
</script>
