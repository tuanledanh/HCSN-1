<template>
  <div class="misa-dialog">
    <div class="misa-dialog-body">
      <div class="alert-icon-container">
        <div class="alert-icon"></div>
      </div>
      <div class="alert-text">{{ content }}</div>
    </div>
    <div class="dialog-footer">
      <button class="btn">Không</button>
      <button class="btn btn-primary">Hủy bỏ</button>
    </div>
  </div>
</template>

<script>
export default {
  name: "MisaDialog",
  props: ["content"]
}
</script>

<style scoped>
.misa-dialog {
  position: fixed;
  width: 400px;
  height: 173px;
  background-color: white;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
  border-radius: 4px;
  box-shadow: rgba(0, 0, 0, 0.35) 0px 5px 15px;
  z-index: 99999;
}

.misa-dialog-body {
  display: flex;
  align-items: center;
  justify-content: center;
  width: 100%;
  height: 70%;
  column-gap: 30px;
  padding-right: 40px;
}

.alert-icon-container {
  transform: scale(2);
}

.dialog-footer {
  display: flex;
  justify-content: flex-end;
  column-gap: 10px;
  padding: 0 20px 20px 0;
}
</style>
