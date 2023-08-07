<template>
  <div class="loading">
    <div class="loading-wrapper">
      <div class="loading__icon"></div>
    </div>
  </div>
</template>
<script>
export default {
  name: "MISALoading",
};
</script>
<style scoped>
.loading {
  top: 0;
  right: 0;
  bottom: 0;
  left: 0;
  position: fixed;
  z-index: 10;
  display: grid;
  place-items: center;
  background-color: rgba(0, 0, 0, 0.188);
}
.loading-wrapper {
  width: 40px;
  height: 40px;
  border-radius: 50%;
  border: 4px solid #2ca01c;
  border-top: 4px solid #ffffff;
  animation: spin 2s linear infinite;
}
@keyframes spin {
  0% {
    transform: rotate(0deg);
  }
  20% {
    transform: rotate(180deg);
  }
  100% {
    transform: rotate(360deg);
  }
}
</style>
