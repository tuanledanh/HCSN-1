<template>
  <div class="icon absolute"><div class="icon-date-picker"></div></div>
</template>
<script>
export default { name: "MISAIconDatePicker" };
</script>
<style scoped>
.icon {
  display: flex;
  height: 100%;
  align-items: center;
  justify-content: center;
  cursor: pointer;
}
.icon-date-picker {
  background: url("../../../assets/icon/qlts-icon.svg") no-repeat -287px -67px;
  width: 18px;
  height: 18px;
}
</style>
