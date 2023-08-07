<template>
  <MISALabel v-if="label" :label="label" :required="required"></MISALabel>
  <VueDatePicker
    v-model="date"
    :format="format"
    ref="input"
    @focus.passive="$emit('focus')"
  />
</template>
<script>
export default {
  name: "MISAInputDatePicker",
  props: {
    // Nhãn dán
    label: {
      type: Boolean,
      default: false,
    },

    // Ô nhập liệu cần phải nhập
    required: {
      type: Boolean,
      default: false,
    },

    // Giá trị binding 2 chiều nhận được từ component cha
    modelValue: {
      type: String,
      default: "",
    },
  },
  data() {
    return {
      // Giá trị ngày tháng
      date: null,

      // Định dạng ngày tháng
      format: null,
    };
  },
  mounted() {
    // Nếu giá trị trả về từ component cha có giá trị thì gán cho date
    if (this.modelValue) {
      this.date = this.modelValue;
    }
  },
  created() {
    // Định dạng hiển thị của date
    this.format = (date) => {
      const day = date.getDate();
      const month = date.getMonth() + 1;
      const year = date.getFullYear();
      return `${day}/${month}/${year}`;
    };
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
     * Focus vào ô nhập liệu
     */
    focusInput() {
      this.$refs.input.focus();
    },
  },
};
</script>

<style>
.dp__input {
  border: 1px solid #afafaf;
  font-family: Roboto, sans-serif;
  font-weight: 400;
  font-size: 13px;
  width: 265px;
  height: 35px;
}

.dp__input:hover {
  border-color: #1aa4c8;
}

.dp__input_focus {
  border-color: #1aa4c8;
}

.dp__input_icon_pad {
  padding-left: 12px;
}

.dp__input_icon {
  right: 0;
  left: auto;
}

.dp__clear_icon {
  display: none;
}
</style>
