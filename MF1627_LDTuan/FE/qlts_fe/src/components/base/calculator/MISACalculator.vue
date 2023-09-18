<template>
  <div class="table__calculator">
    <div
      class="calculator border--top"
      :class="[
        { 'transfer-list-expand': isChangeWidth && form === 'transfer-list' },
        { 'transfer-list': form === 'transfer-list' },
        { 'transfer-form': form === 'transfer-form' },
        { 'transfer-choose-form': form === 'transfer-choose-form' },
      ]"
    >
      <div
        v-for="n in numberColumnLeft"
        :key="`left-column-${n}`"
        class="column border--right"
      ></div>
      <div
        class="column display--center-right padding--right-10 border--right font-weight--500"
      >
        {{ totalPrice }}
      </div>
      <div
        class="column display--center-right padding--right-10 border--right font-weight--500"
      >
        {{ totalResidualValue }}
      </div>
      <div
        v-for="n in numberColumnRight"
        :key="`right-column-${n}`"
        class="column border--right"
      ></div>
    </div>
  </div>
</template>
<script>
export default {
  name: "MISACalculator",
  props: {
    isChangeWidth: {
      type: Boolean,
      default: false,
    },
    totalPrice: {
      type: String,
      default: "0",
    },
    totalResidualValue: {
      type: String,
      default: "0",
    },
    numberColumnLeft: {
      type: Int32Array,
      default: 0,
    },
    numberColumnRight: {
      type: Int32Array,
      default: 0,
    },
    form: {
      type: String,
      default: null,
      validator: function (value) {
        return (
          ["transfer-list", "transfer-form", "transfer-choose-form"].indexOf(
            value
          ) !== -1
        );
      },
    },
  },
  data() {
    return {};
  },

  methods: {},
};
</script>

<style scoped>
.calculator {
  height: 39px;
  width: 100%;
  box-sizing: border-box;
  background-color: var(--background-color-table-head);
  border-color: var(--table-border-color);
}

.transfer-list {
  display: grid;
  grid-template-columns: 44px 50px 120px 150px 150px 140px 200px calc(
      100% - 854px
    );
  height: 35px;
  cursor: pointer;
}

.transfer-list-expand {
  display: grid;
  grid-template-columns: 44px 50px 120px 150px 150px 140px 200px calc(
      100% - 854px
    );
  height: 35px;
  cursor: pointer;
}

.transfer-form {
  display: grid;
  grid-template-columns:
    44px 50px 160px 180px 200px 200px 200px 200px 295px
    110px;
  height: 35px;
  cursor: pointer;
}

.transfer-choose-form {
  display: grid;
  grid-template-columns: 44px 50px 160px 172px 176px 156px 200px calc(
      100% - 958px
    );
  height: 35px;
  cursor: pointer;
}

.column {
  border-color: var(--table-border-color);
}
</style>
